using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace mmixal.test.integration
{
    /// <summary>
    /// Tests that assemble mmixal and run the output against the mmix simulator.
    /// </summary>
    [TestClass]
    public class IntegrationTests
    {
        [TestMethod]
        public void MmixalNoArgs()
        {
            using var mmixalProcess = new ExternalProgram().Mmixal();
            mmixalProcess.Start();
            string output = mmixalProcess.StandardOutput.ReadToEnd();
            mmixalProcess.WaitForExit();

            Assert.AreEqual(-1, mmixalProcess.ExitCode);

            Console.WriteLine("MMIXAL output:");
            Console.WriteLine(output);
        }

        [TestMethod]
        public void MmixNoArgs()
        {
            using var mmixProcess = new ExternalProgram().Mmix();
            mmixProcess.Start();
            string output = mmixProcess.StandardOutput.ReadToEnd();
            mmixProcess.WaitForExit();

            Assert.AreEqual(-1, mmixProcess.ExitCode);

            Console.WriteLine("MMIX output:");
            Console.WriteLine(output);
        }

        [TestMethod]
        public void HelloWorld()
        {
            string mmixalOutput;
            using (var mmixalProcess = new ExternalProgram().Mmixal())
            {
                mmixalProcess.StartInfo.Arguments = "programs/hello.mms";
                mmixalProcess.Start();
                mmixalOutput = mmixalProcess.StandardOutput.ReadToEnd();
                mmixalProcess.WaitForExit();

                Assert.AreEqual(0, mmixalProcess.ExitCode);
            }

            Console.WriteLine("MMIXAL output:");
            Console.WriteLine(mmixalOutput);
            Console.WriteLine();
            Console.WriteLine("-----------");
            Console.WriteLine();

            string mmixOutput;
            using (var mmixProcess = new ExternalProgram().Mmix())
            {
                mmixProcess.StartInfo.Arguments = "programs/hello.mmo";
                mmixProcess.Start();
                mmixOutput = mmixProcess.StandardOutput.ReadToEnd();
                mmixProcess.WaitForExit();

                Assert.AreEqual(0, mmixProcess.ExitCode);
            }

            Console.WriteLine("MMIX output:");
            Console.WriteLine(mmixOutput);
            Console.WriteLine();
            Console.WriteLine("-----------");
            Console.WriteLine();
        }
    }
}
