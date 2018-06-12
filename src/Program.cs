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
              //This will byPass ConsoleLog check
              string strMesageOut = "Error en los datos introducidos";
              Console.WriteLine(strMesageOut);
              continue;
            }
            //Add Command joining the end of the array
            CommandMgr.AddCommand(strCommand[1], string.Join(" ", strCommand.ToList().Skip(2)));
            break;
          //Remove a command
          case "remove":
            //Todo
            break;
          //Execute
          default:
            //Get the path of the command
            string strPath = CommandMgr.GetCommandPath(strCommand[0]);
            //If the path is empty, continue
            if (string.IsNullOrWhiteSpace(strPath))
            {
              //This will byPass ConsoleLog check
              string strMesageOut = "Comando no registrado";
              Console.WriteLine(strMesageOut);
              continue;
            }
            //Create the process
            Process commandProcess = new Process();
            //Create the start info
            ProcessStartInfo startInfo = new ProcessStartInfo();
            //Set the start path
            startInfo.FileName = strPath;
            //Set the arguments
            startInfo.Arguments = /*"/c " + */string.Join(" ", strCommand.ToList().Skip(1));
            //Redirecto de info streams
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
            //Start the process
            commandProcess.Start();
            //Start the events firer
            commandProcess.BeginOutputReadLine();
            commandProcess.BeginErrorReadLine();
            //Wait for exit
            commandProcess.WaitForExit();
            break;
        }
      }
    }
  }
}
