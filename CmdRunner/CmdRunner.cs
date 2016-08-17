using System;
using System.Collections;
using System.Diagnostics;

namespace Codice.CmdRunner
{
    public static class CmdRunner
    {
        public static string ExecuteCommandWithStringResult(
            string command, string path,
            bool bUseShell)
        {
            string output, error;
            mRunner.InternalExecuteCommand(command, path, null, out output, out error, bUseShell);
            return output;
        }

        static CodiceCmdRunner mRunner = new CodiceCmdRunner();
    }
}