using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MES.Acquintance;
using NUnit.Framework;
using OfficeOpenXml;


namespace MES.Tests {
    [TestFixture]
    class BatchReportTest {
        private readonly BatchReportGenerator brg = new BatchReportGenerator();
        private readonly Random rand = new Random();

        [Test]
        public void CheckFileCreation() {
            int[] stringArray = { 2, 5, 7, 9, 3, 5, 4, 12 };
            ValueOverProdTime[] temperatureData = GenerateTestData(-1);
            ValueOverProdTime[] humidityData = GenerateTestData(0);
            ValueOverProdTime[] vibrationData = GenerateTestData(1);
            ISet<IList<IBatchValue>> iBatchValueSet = new HashSet<IList<IBatchValue>>();
            iBatchValueSet.Add(temperatureData);
            iBatchValueSet.Add(humidityData);
            iBatchValueSet.Add(vibrationData);
            brg.GenerateFile(10, 10, 10, 10, stringArray, iBatchValueSet );
            // booleans for verification
            bool fileExists = File.Exists(AppDomain.CurrentDomain.BaseDirectory + "BatchReport.xlsx");

            ExcelPackage ep =
                new ExcelPackage(new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "BatchReport.xlsx"));
            ExcelWorksheet ws = ep.Workbook.Worksheets[1]; // worksheet containing batch report
            // Check if file has been created
            Assert.IsTrue(fileExists, "File exists.");
            // check if values were inserted correctly
            Assert.AreEqual(ws.Cells["B1"].Value, "10");
            Assert.AreEqual(ws.Cells["B2"].Value, "10");
            Assert.AreEqual(ws.Cells["B3"].Value, "10");
            Assert.AreEqual(ws.Cells["D3"].Value, "10");
            // check if correct sum of products has been calculated
            ws.Cells["F3"].Calculate();
            Assert.AreEqual(ws.Cells["F3"].Value, 20);
        }

        private ValueOverProdTime[] GenerateTestData(int type) {
            ValueOverProdTime[] brrt = new ValueOverProdTime[100];
            for (int i = 0; i < 100; i++) {
                ValueOverProdTime temp = new ValueOverProdTime(rand.Next(100), DateTime.Now.ToString(), type);
                brrt[i] = temp;
            }

            return brrt;
        }
    }
}