using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Shell
{
  static class Program
  {
    static void Main(string[] args)
    {
      Process commandProcess = null;
      string strInput = "";

      while (strInput != "exit")
      {
        strInput = Console.ReadLine();
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
            //If the array must have at less 3 items (add->Command->Path)
            if (strCommand.Length < 3)
            {
              Console.WriteLine("Error adding the command, follow the template \"add CommandName CommandPath\"");
              continue;
            }
            //Add Command joining the end of the array
            CommandMgr.AddCommand(strCommand[1], string.Join(" ", strCommand.ToList().Skip(2)));
            Console.WriteLine("Added command");
            break;
          //Remove a command
          case "remove":
            //remove Command
            CommandMgr.RemoveCommand(strCommand[1]);
            Console.WriteLine("Removed command");
            break;
          //list Commands
          case "list":
            Console.WriteLine("Listing commands...");
            Console.WriteLine($"Command\t\t->\t\tExecution path");
            foreach (var command in CommandMgr.GetCommands())
            {
              Console.WriteLine($"{command.Name}\t\t->\t\t{command.Path}");
            }
            break;
            //Clear the window
          case "clear":
            Console.Clear();
            break;
            //End the current process
          case "close":
            commandProcess?.Kill();
            break;
          //Execute
          default:
            //Check if a process is in execution
            if(commandProcess != null)
            {
              Console.WriteLine($"The command {CommandMgr.GetCommandNameByPath(commandProcess.StartInfo.FileName)} is in execution");
              Console.WriteLine("Write \"close\" for end it or wait");
              continue;
            }
            //Get the path of the command
            string strPath = CommandMgr.GetCommandPathByName(strCommand[0]);
            //If the path is empty, continue
            if (string.IsNullOrWhiteSpace(strPath))
            {
              Console.WriteLine("Comando no registrado");
              continue;
            }
            //Create the process
            commandProcess = new Process();
            //Create the start info
            ProcessStartInfo startInfo = new ProcessStartInfo();
            //Set the start path
            startInfo.FileName = strPath;
            //Set the arguments
            startInfo.Arguments = /*"/c " + */string.Join(" ", strCommand.ToList().Skip(1));
            //Redirec to the info streams
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardError = true;
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            //Set the start info
            commandProcess.StartInfo = startInfo;
            //Suscribe the events
            commandProcess.OutputDataReceived += (s, e) => Console.WriteLine(e.Data);
            commandProcess.ErrorDataReceived += (s, e) => Console.WriteLine(e.Data);
            commandProcess.EnableRaisingEvents = true;
            commandProcess.Exited += (s, e) => commandProcess = null;
            //Start the process
            commandProcess.Start();
            //Start the events firer
            commandProcess.BeginOutputReadLine();
            commandProcess.BeginErrorReadLine();            
            break;
        }
      }
    }
  }
}
