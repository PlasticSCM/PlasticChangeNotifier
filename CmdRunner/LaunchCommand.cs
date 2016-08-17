using System;
using System.IO;
using System.Xml.Serialization;

namespace Codice.CmdRunner
{
    [Serializable]
    public class LaunchCommandConfig
    {
        public string FullServerCommand;
        public string CmShellComand;
        public string ClientPath;
    }

    public class LaunchCommand
    {
        public static LaunchCommand Get()
        {
            if (mInstance == null)
                mInstance = new LaunchCommand();

            return mInstance;
        }

        public string GetCmShellCommand()
        {
            return mConfig.CmShellComand;
        }

        public string GetClientPath()
        {
            return mConfig.ClientPath;
        }

        LaunchCommandConfig mConfig;

        LaunchCommand()
        {
            mConfig = new LaunchCommandConfig();

            if (String.IsNullOrEmpty(mConfig.CmShellComand))
                mConfig.CmShellComand = string.Format("{0} shell --logo", mExecutablePath);

            if (mConfig.ClientPath == null)
                mConfig.ClientPath = string.Empty;

            if (mConfig.CmShellComand == null)
                mConfig.CmShellComand = string.Empty;
        }

        static LaunchCommand mInstance = null;
        static string mExecutablePath = "bcm";
    }
}
