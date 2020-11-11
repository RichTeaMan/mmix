using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace mmixal.test.integration
{
    class ExternalProgram
    {
        public Process Mmixal()
        {
            Process externalProcess = new Process();
            externalProcess.StartInfo.FileName = "mmixal";
            externalProcess.StartInfo.RedirectStandardOutput = true;
            externalProcess.StartInfo.UseShellExecute = false;
            return externalProcess;
        }

        public Process Mmix()
        {
            Process externalProcess = new Process();
            externalProcess.StartInfo.FileName = "mmix";
            externalProcess.StartInfo.RedirectStandardOutput = true;
            externalProcess.StartInfo.UseShellExecute = false;
            return externalProcess;
        }
    }
}
