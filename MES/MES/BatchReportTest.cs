using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace MES
{
    [TestFixture]
    class BatchReportTest
    {
        private readonly BatchReportGenerator brg = new BatchReportGenerator();
        [Test]
        public void CheckFileCreation()
        {
            int[] stringArray = { 2, 5, 7, 9, 3, 5, 4, 12 };
            brg.GenerateFile("10", "10", " 10", "10", stringArray, "10", "10");
             
            bool b = File.Exists(@"C:\Users\Bruger\Documents\test\BatchReport.xlsx");

              Assert.IsTrue(b); 

        }

    }
}
