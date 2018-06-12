using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


namespace Shell
{
  class Program
  {
    static void Main(string[] args)
    {
      string strInput = "";

      while (strInput != "exit")
      {
        strInput = Console.ReadLine();
        //Empty input
        if (string.IsNullOrWhiteSpace(strInput))
          continue;
        //Commands splited
        string[] strCommand = strInput.Split(' ');
        switch (strCommand[0].ToLower())
        {
          //Add a command
          case "add":
            //If the array must have at less 3 items (add->Command->Path)
            if (strCommand.Length < 3)
            {
              Console.WriteLine("Error en los datos introducidos");
              continue;
            }
            //Add Command joining the end of the array
            CommandMgr.AddCommand(strCommand[1], string.Join(" ", strCommand.ToList().Skip(2)));
            break;
          //Execute
          default:
            string strPath = CommandMgr.GetCommandPath(strCommand[0]);
            if (string.IsNullOrWhiteSpace(strPath))
            {
              Console.WriteLine("Comando no registrado");
              continue;
            }
            Process commandProcess = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = strPath;
            startInfo.Arguments = /*"/c " + */string.Join(" ", strCommand.ToList().Skip(1));
            startInfo.RedirectStandardOutput = true;            
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardError = true;
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            commandProcess.StartInfo = startInfo;
            commandProcess.OutputDataReceived += (s, e) => Console.WriteLine(e.Data);
            commandProcess.ErrorDataReceived += (s, e) => Console.WriteLine(e.Data);
            commandProcess.Start();
            commandProcess.BeginOutputReadLine();
            commandProcess.BeginErrorReadLine();
            //commandProcess.WaitForExit();
            break;
        }
      }
    }
  }
}
