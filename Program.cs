using System;
using System.Threading;
using Codice.CmdRunner;

namespace PlasticNotifier
{
    class Program
    {
        static void Main(string[] args)
        {
            Toaster.ShortCutCreator.TryCreateShortcut(APP_ID, "PlasticNotifier");

            mRepoName = args[0];

            Console.WriteLine("Monitoring repo {0}", mRepoName);

            Console.WriteLine("Type 'exit' to quit");

            Thread t = new Thread(MonitorChangesets);
            t.Start();

            while (Console.ReadLine().ToLower() != "exit");
        }

        static void MonitorChangesets()
        {
            DateTime last = DateTime.Parse("2016/08/05 11:00");

            string query = "bcm find changesets where date >= '{0}' on repository '{1}' --nototal";

            string formatStr =
                " --format=\"[LINE]{changesetid}|||{owner}|||{branch}|||{comment}\"";

            while (true)
            {
                string command = string.Format(query, last.ToString(), mRepoName)
                    + formatStr;

                Console.WriteLine(command);

                string output = CmdRunner.ExecuteCommandWithStringResult(
                    command, Environment.CurrentDirectory, true);

                if (string.IsNullOrEmpty(output))
                {
                    last = DateTime.Now;
                    System.Threading.Thread.Sleep(30 * 1000);
                    continue;
                }

                Console.WriteLine(output);

                string[] lines = output.Split(new string[] { "[LINE]" },
                    StringSplitOptions.RemoveEmptyEntries);

                foreach (string line in lines)
                {
                    string[] parts = line.Split(new string[] { "|||" },
                        StringSplitOptions.None);

                    Toaster.ShowImageToast(
                        APP_ID,
                        string.Format("{0} - {1}", parts[1], parts[2]),
                        parts[3]);
                }

                last = last = DateTime.Now;
                System.Threading.Thread.Sleep(30 * 1000);
            }
        }

        static string mRepoName;

        const string APP_ID = "PlasticSCM.Notifier";
    }
}
