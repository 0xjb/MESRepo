using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OfficeOpenXml;


namespace MES {
    [TestFixture]
    class BatchReportTest {
        private readonly BatchReportGenerator brg = new BatchReportGenerator();
        private readonly Random rand = new Random();
        [Test]
        public void CheckFileCreation() {
            int[] stringArray = { 2, 5, 7, 9, 3, 5, 4, 12 };
            ValueOverProdTime[] temperatureData = GenerateTestData();
            ValueOverProdTime[] humidityData = GenerateTestData();
            brg.GenerateFile("10", "10", "10", "10", stringArray, temperatureData, humidityData);
            // booleans for verification
            bool fileExists = File.Exists(@"C:\Users\J\Documents\BatchReport.xlsx");

            ExcelPackage ep = new ExcelPackage(new FileInfo(@"C:\Users\J\Documents\a\BatchReport.xlsx"));
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
        private ValueOverProdTime[] GenerateTestData() {
            ValueOverProdTime[] brrt = new ValueOverProdTime[100];
            for (int i = 0; i < 100; i++) {
                ValueOverProdTime temp = new ValueOverProdTime();
                temp.Value = rand.Next(100);
                temp.Time = DateTime.Today.AddDays(rand.Next(1000));
                brrt[i] = temp;
            }
            return brrt;
        }

    }
}
