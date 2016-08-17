using System;

namespace Codice.CmdRunner
{
    public class PlatformIdentifier
    {
        public static bool IsWindows()
        {
            if (!bIsWindowsInitialized)
            {
                switch (Environment.OSVersion.Platform)
                {
                    case PlatformID.Win32Windows:
                    case PlatformID.Win32NT:
                        bIsWindows = true;
                        break;
                }
                bIsWindowsInitialized = true;
            }
            return bIsWindows;
        }

        private static bool bIsWindowsInitialized = false;
        private static bool bIsWindows = false;
    }
}
