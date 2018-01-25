// *****************************************************************
// CtxtRestart CtxtRestart
// (c) 2018 - 2018 Netzalist GmbH & Co.KG
// *****************************************************************

#region Using

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

#endregion

namespace CtxtRestart
{
    /// <summary>
    /// This program kills all running CtxtRun.exe instances and if provided with start parameters then restarts
    /// Contextor with these parameters.
    /// 
    /// Usage: CtxtRestart [params]
    /// Without parametrers it just kills the running program. Else it restarts Contextor from the default installation path
    /// (Programs x86)\Contextor\Interactive\CtxtRun.exe
    /// </summary>
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                KillRunningContextorInstances();
                if(args.Length > 0)
                    RestartContextor(args);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error stopping / restarting Contextor: {ex.Message}");
            }
        }

        /// <summary>
        /// Kill all running Contextor Runtime instances in parallel.
        /// </summary>
        private static void KillRunningContextorInstances()
        {
            var tasksToKill = Process.GetProcessesByName("CtxtRun");
            Console.WriteLine($"Found {tasksToKill.Length} tasks to kill.");

            Task.WaitAll(
                tasksToKill.Select(p => Task.Factory.StartNew(p.Kill)).ToArray(), TimeSpan.FromSeconds(10));
            Console.WriteLine($"Killed {tasksToKill.Length} tasks");
        }

        /// <summary>
        /// Restart Contextor and pass it our own arguments
        /// </summary>
        private static void RestartContextor(string[] args)
        {
            var ctxtRunPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86),
                "Contextor", "Interactive", "CtxtRun.exe");

            if (!File.Exists(ctxtRunPath))
            {
                Console.WriteLine($"Contextor not found at {ctxtRunPath}");
                return;
            }
            var startInfo = new ProcessStartInfo()
            {
                FileName = ctxtRunPath,
                Arguments = args.Aggregate("", (acc, e) => $"{acc} \"{e}\"")
            };

            var newProcess = Process.Start(startInfo);
            Console.WriteLine(
                $"New process {newProcess?.Id} started: {ctxtRunPath} {args.Aggregate("", (acc, e) => $"{acc} \"{e}\"")}");
        }
    }
}