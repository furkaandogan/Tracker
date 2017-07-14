using System;

namespace Exline.ProcessTracker.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            ITrackerProgram trackerProgram = new ProcessTrackerConsoleProgram();
            trackerProgram.ExecuteCommand(args);
            Console.WriteLine("cikis icin bir tusa basiniz.");
            Console.ReadKey();
        }
    }
}