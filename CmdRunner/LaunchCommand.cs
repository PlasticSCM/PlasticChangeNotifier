using System;

namespace Codice.CmdRunner
{
    public class LaunchCommand
    {
        public static LaunchCommand Get()
        {
            if (mInstance == null)
                mInstance = new LaunchCommand();

            return mInstance;
        }

        public void SetCm(string cm)
        {
            mExecutablePath = cm;

            Console.WriteLine("cm set to {0}", mExecutablePath);
        }

        public string GetCmShellCommand()
        {
            string cmShellCommand = string.Format("{0} shell --logo", mExecutablePath);
            Console.WriteLine("CmShell is {0}", cmShellCommand);
            return cmShellCommand;
        }

        public string GetClientPath()
        {
            return string.Empty;
        }

        static LaunchCommand mInstance = null;
        string mExecutablePath = "cm";
    }
}
