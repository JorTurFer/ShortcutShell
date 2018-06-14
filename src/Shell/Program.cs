using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Shell
{
  static class Program
  {
    static Process commandProcess = null;
    static void Main(string[] args)
    {      
      bool bContinue = true;

      while (bContinue)
      {
        string strInput = Console.ReadLine();
        //Empty input
        if (string.IsNullOrWhiteSpace(strInput))
        {
          continue;
        }
        //Commands splited
        string[] strCommand = strInput.Split(' ');
        switch (strCommand[0].ToLowerInvariant())
        {
          //Add a command
          case "add":
            AddCommand(strCommand);
            break;
          //Remove a command
          case "remove":
            RemoveCommand(strCommand[1]);
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
            Execute(strCommand);
            break;
        }
      }
    }

    private static void Execute(string[] strCommand)
    {
      //Check if a process is in execution
      if (commandProcess != null)
      {
        Console.WriteLine($"The command {CommandMgr.GetCommandNameByPath(commandProcess.StartInfo.FileName)} is in execution");
        Console.WriteLine("Write \"close\" for end it or wait");
        return;
      }
      //Get the path of the command
      string strPath = CommandMgr.GetCommandPathByName(strCommand[0]);
      //If the path is empty, continue
      if (string.IsNullOrWhiteSpace(strPath))
      {
        Console.WriteLine("Comando no registrado");
        return;
      }
      //Create the process
      commandProcess = new Process();
      //Create the start info
      ProcessStartInfo startInfo = new ProcessStartInfo();
      //Set the start path
      startInfo.FileName = strPath;
      //Set the arguments
      startInfo.Arguments = string.Join(" ", strCommand.ToList().Skip(1));
      //Redirec to the info streams
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
      //Start the process
      commandProcess.Start();
      //Start the events firer
      commandProcess.BeginOutputReadLine();
      commandProcess.BeginErrorReadLine();
    }

    static void AddCommand(string[] strCommand)
    {
      //If the array must have at less 3 items (add->Command->Path)
      if (strCommand.Length < 3)
      {
        Console.WriteLine("Error adding the command, follow the template \"add CommandName CommandPath\"");
        return; ;
      }
      if (!CommandMgr.Exists(strCommand[1]))
      {
        //Add Command joining the end of the array
        CommandMgr.AddCommand(strCommand[1], string.Join(" ", strCommand.ToList().Skip(2)));
        Console.WriteLine("Added command");
      }
      else
      {
        Console.WriteLine("The command already existed");
      }
    }
    private static void RemoveCommand(string strName)
    {
      if (CommandMgr.Exists(strName))
      {
        //remove Command
        CommandMgr.RemoveCommand(strName);
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
      Console.WriteLine($"Command\t\t->\t\tExecution path");
      foreach (var command in CommandMgr.GetCommands())
      {
        Console.WriteLine($"{command.Name}\t->\t{command.Path}");
      }
    }
  }
}
