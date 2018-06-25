using System;
using System.Diagnostics;
using System.Linq;
using CommandLine;
using System.Reflection;
using System.IO;
namespace Shell
{
  static class Program
  {
    static Process commandProcess;
    static void Main(string[] args)
    {
      //Read XML
      var strBinaryFullPath = Assembly.GetEntryAssembly().Location;
      var strBinaryPath = Path.GetDirectoryName(strBinaryFullPath);
      var strXmlFullPath = Path.Combine(strBinaryPath,"Commands.xml");
      CommandMgr.Load(strXmlFullPath);

      bool bContinue = true;
      bool bExternal = false;
      string[] Argumments = args;
      //Support for external executions
      if (Argumments.Length > 0)
      {
        bContinue = false;
        bExternal = true;
      }
      do
      {
        if (!bExternal)
        {
          string strInput = Console.ReadLine();
          //Empty input
          if (string.IsNullOrWhiteSpace(strInput))
          {
            continue;
          }
          Argumments = strInput.Split(' ');
        }
        //Commands splited
        switch (Argumments[0].ToLowerInvariant())
        {
          //Add a command
          case "add":
            var resAdd = Parser.Default.ParseArguments<Command>(Argumments);
            if (resAdd.Tag == ParserResultType.NotParsed)
            {
              continue;
            }
            AddCommand(MakeResult(resAdd));
            break;
          //Remove a command
          case "remove":
            RemoveCommand(new Command { Name = Argumments[1] });
            break;
          //list Commands
          case "list":
            ListCommand();
            break;
          //Clear the window
          case "clear":
            Console.Clear();
            break;
          //End the current process
          case "close":
            commandProcess?.Kill();
            break;
          //Eit the shell
          case "exit":
            commandProcess?.Kill();
            bContinue = false;
            continue;
          //Execute
          default:
            var command = CommandMgr.GetCommandByName(Argumments[0]);
            if (command != null)
            {
              Execute(command, string.Join(" ", Argumments.ToList().Skip(1)), bExternal);
            }
            break;
        }
      } while (bContinue);
    }

    static void Execute(Command CurrentCommand, string strArgumments, bool bSync)
    {
      //Check if a process is in execution
      if (commandProcess != null)
      {
        Console.WriteLine($"The command {CommandMgr.GetCommandNameByPath(commandProcess.StartInfo.FileName)} is in execution");
        Console.WriteLine("Write \"close\" for end it or wait");
        return;
      }
      //Create the process
      commandProcess = new Process();
      //Create the start info
      ProcessStartInfo startInfo = new ProcessStartInfo();
      //Set the start path
      startInfo.FileName = CurrentCommand.Path;
      //Set the arguments
      startInfo.Arguments = strArgumments;
      if (!CurrentCommand.Dettached)
      {//Redirec to the info streams
        startInfo.RedirectStandardOutput = true;
        startInfo.RedirectStandardInput = true;
        startInfo.RedirectStandardError = true;
        startInfo.CreateNoWindow = true;
        startInfo.UseShellExecute = false;
        //Set the start info
        commandProcess.StartInfo = startInfo;
        //Suscribe the stream events
        commandProcess.OutputDataReceived += (s, e) => Console.WriteLine(e.Data);
        commandProcess.ErrorDataReceived += (s, e) => Console.WriteLine(e.Data);
        //Activate and suscribe the exit event
        commandProcess.EnableRaisingEvents = true;
        commandProcess.Exited += (s, e) =>
        {
          Console.WriteLine($"->{CommandMgr.GetCommandNameByPath(commandProcess.StartInfo.FileName)} ended!!");
          commandProcess = null;
        };
      }
      else
      {
        //Set the start info
        commandProcess.StartInfo = startInfo;
      }
      //Start the process
      commandProcess.Start();
      if (!CurrentCommand.Dettached)
      {
        //Start the events firer
        commandProcess.BeginOutputReadLine();
        commandProcess.BeginErrorReadLine();
      }
      else
      {
        commandProcess = null;
      }
      if (bSync)
      {
        commandProcess?.WaitForExit();
      }
    }
    static void AddCommand(Command command)
    {
      if (!CommandMgr.Exists(command))
      {
        //Add Command joining the end of the array
        CommandMgr.AddCommand(command);
        Console.WriteLine("Added command");
      }
      else
      {
        Console.WriteLine("The command already exists");
      }
    }
    private static void RemoveCommand(Command command)
    {
      if (CommandMgr.Exists(command))
      {
        //remove Command
        CommandMgr.RemoveCommand(command);
        Console.WriteLine("Removed command");
      }
      else
      {
        Console.WriteLine("The command doesn't exist");
      }
    }
    static void ListCommand()
    {
      Console.WriteLine("Listing commands...");
      Console.WriteLine($"Command\t->\tDettached\t->\tExecution path");
      foreach (var command in CommandMgr.GetCommands())
      {
        Console.WriteLine($"{command.Name}\t->\t{command.Dettached}\t->\t{command.Path}");
      }
    }

    static Command MakeResult(ParserResult<Command> result)
    {
      Command ret = new Command();
      result.WithParsed(x => ret = x);
      return ret;
    }
  }
}
