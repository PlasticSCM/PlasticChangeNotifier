using System;
using System.IO;
using System.Threading;
using Codice.CmdRunner;

namespace PlasticNotifier
{
    class Program
    {
        static void Main(string[] args)
        {
            Toaster.ShortCutCreator.TryCreateShortcut(APP_ID, "PlasticNotifier");

            CommandLineArguments cla = CommandLineArguments.Parse(args);

            if (cla == null)
            {
                CommandLineArguments.ShowUsage();
                return;
            }

            Console.WriteLine("Monitoring repo {0}", cla.RepoToMonitor);

            Console.WriteLine("Type 'exit' to quit");

            Thread t = new Thread(MonitorChangesets);
            t.Start(cla);

            while (Console.ReadLine().ToLower() != "exit");
        }

        static void MonitorChangesets(object o)
        {
            string logoFile = CreateTemporaryImageForToast();

            CommandLineArguments cla = o as CommandLineArguments;

            LaunchCommand.Get().SetCm(cla.CmCommand);

            DateTime last = cla.Since;

            string query = System.IO.Path.GetFileName(cla.CmCommand) +
                " find changesets where date >= '{0}' on repository '{1}' --nototal";

            string formatStr =
                " --format=\"[LINE]{changesetid}|||{owner}|||{branch}|||{comment}\"";

            while (true)
            {
                string command = string.Format(query, last.ToString(), cla.RepoToMonitor)
                    + formatStr;

                Console.WriteLine(command);

                string output = CmdRunner.ExecuteCommandWithStringResult(
                    command, Environment.CurrentDirectory, true);

                if (string.IsNullOrEmpty(output))
                {
                    last = DateTime.Now;
                    System.Threading.Thread.Sleep(cla.PollIntervalSeconds * 1000);
                    continue;
                }

                Console.WriteLine(output);

                string[] lines = output.Split(new string[] { "[LINE]" },
                    StringSplitOptions.RemoveEmptyEntries);

                foreach (string line in lines)
                {
                    string[] parts = line.Split(new string[] { "|||" },
                        StringSplitOptions.None);

                    if (parts.Length < 2)
                        break;

                    Toaster.ShowImageToast(
                        APP_ID,
                        string.Format("{0} - {1}", parts[1], parts[2]),
                        parts[3],
                        logoFile);
                }

                last = last = DateTime.Now;
                System.Threading.Thread.Sleep(cla.PollIntervalSeconds * 1000);
            }
        }

        static string CreateTemporaryImageForToast()
        {
            string fileName = Path.Combine(Path.GetTempPath(), "plasticlogo.png");

            if (File.Exists(fileName))
                return fileName;

            Resources.plasticlogo.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);

            return fileName;
        }

        class CommandLineArguments
        {
            internal string CmCommand = "cm";

            internal int PollIntervalSeconds = 30;

            internal string RepoToMonitor;
            internal DateTime Since = DateTime.Now;

            static internal CommandLineArguments Parse(string[] args)
            {
                CommandLineArguments result = new CommandLineArguments();

                int i = 0;

                while (i < args.Length)
                {
                    string arg = args[i++];

                    switch (arg)
                    {
                        case "--cm":
                            if (args.Length == i) return null;
                            result.CmCommand = args[i++];
                            break;
                        case "--poll":
                            if (args.Length == i) return null;
                            result.PollIntervalSeconds = int.Parse(args[i++]);
                            break;
                        case "--since":
                            if (args.Length == i) return null;
                            result.Since = DateTime.Parse(args[i++]);
                            break;
                        default:
                            result.RepoToMonitor = arg;
                            break;
                    }
                }

                if (i == 0)
                    return null;

                return result;
            }

            static internal void ShowUsage()
            {
                Console.WriteLine("plasticnotifier repository [--cm command] [--poll seconds] [--since date]");
                Console.WriteLine("\trepository:     repo to monitor. Sample myrepo@myserver:7074");
                Console.WriteLine("\t--cm command:   to specify an alternative cm. We use it internally");
                Console.WriteLine("\t--poll seconds: seconds between each query to the repo. Default 30");
                Console.WriteLine("\t--since date:   date to use for the first query. Useful to test it");
            }
        }

        const string APP_ID = "PlasticSCM.Notifier";
    }
}
