using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using System.IO;
using OfficeOpenXml.Drawing.Chart;

namespace MES
{
    class BatchReportGenerator
    {
        private ExcelPackage ep = new ExcelPackage();
        private Random rand = new Random();
        public void GenerateFile(string batchID, string productType, string aProduct, string dProduct, int[] timeUsed, string tempatureProductionTime, string HumidityProductionTime)
        {

            using (ep)
            {
                //A workbook must have at least on cell, so lets add one... 
                var ws = ep.Workbook.Worksheets.Add("Batch Report");
                var temp = ep.Workbook.Worksheets.Add("Temperature");
                var humid = ep.Workbook.Worksheets.Add("Humidity");
                //To set values in the spreadsheet use the Cells indexer.
                ws.Cells["A1"].Value = "BatchID:";
                ws.Cells["A1"].Style.Font.Bold = true;
                ws.Cells["B1"].Value = batchID;
                ws.Cells["A2"].Value = "ProductType:";
                ws.Cells["A2"].Style.Font.Bold = true;
                ws.Cells["B2"].Value = productType;
                ws.Cells["A3"].Value = "DefectProducts:";
                ws.Cells["A3"].Style.Font.Bold = true;
                ws.Cells["C3"].Value = "Acceptable:";
                ws.Cells["E3"].Value = "Total:";
                ws.Cells["B3"].Value = aProduct;
                ws.Cells["D3"].Value = dProduct;
                ws.Cells["F3"].Formula = "B3 + D3";
                ws.Cells["A5"].Value = "Time spent in different states:";
                ws.Cells["A5"].Style.Font.Bold = true;
                ws.Cells["A6"].Value = "Deactivated:";
                ws.Cells["B6"].Value = timeUsed[0];
                ws.Cells["A7"].Value = "Stopped:";
                ws.Cells["B7"].Value = timeUsed[1];
                ws.Cells["A8"].Value = "Idle:";
                ws.Cells["B8"].Value = timeUsed[2];
                ws.Cells["A9"].Value = "Suspended:";
                ws.Cells["B9"].Value = timeUsed[3];
                ws.Cells["A10"].Value = "Execute:";
                ws.Cells["B10"].Value = timeUsed[4];
                ws.Cells["A11"].Value = "Aborted:";
                ws.Cells["B11"].Value = timeUsed[5];
                ws.Cells["A12"].Value = "Held:";
                ws.Cells["B12"].Value = timeUsed[6];
                ws.Cells["A13"].Value = "Complete:";
                ws.Cells["B13"].Value = timeUsed[7];

                ws.Cells["A15"].Value = "TempatureProductionTime:";
                ws.Cells["A15"].Style.Font.Bold = true;
                ws.Cells["A16"].Value = "HumidityProductionTime:";
                ws.Cells["A16"].Style.Font.Bold = true;



                //Add the piechart
                var pieChart = ws.Drawings.AddChart("chart", eChartType.ColumnStacked);
                pieChart.ShowHiddenData = true;
                //Set top left corner to row 1 column 2
                pieChart.SetPosition(6, 0, 7, 0);
                pieChart.SetSize(500, 300);
                pieChart.Series.Add(("B6:B14"), ("A6:A14"));

                pieChart.Title.Text = "Time spent diagram";
                //Set datalabels and remove the legend

                pieChart.Legend.Remove();


                ws.Cells[ws.Dimension.Address].AutoFitColumns();
                WriteData(Test1(), temp);
                WriteData(Test1(), humid);


                //Save the new workbook. We haven't specified the filename so use the Save as method.
                ep.SaveAs(new FileInfo(@"C:\Users\Bruger\Documents\test\BatchReport.xlsx"));


            }

        } private void WriteData(ValueOverProdTime[] data, ExcelWorksheet ew)
        {
            ew.Cells["A1"].Value = "Temp:";
            ew.Cells["A1"].Style.Font.Bold = true;

            ew.Cells["B1"].Value = "Time:";
            ew.Cells["B1"].Style.Font.Bold = true;


            for (int i = 0; i < data.Length; i++)
            {
                
                ew.Cells["A" + (i + 2)].Value = i;
                ew.Cells["B" + (i + 2)].Value = data[i].Value;
            }
            //Add the piechart
            var pieChart = ew.Drawings.AddChart("chart", eChartType.XYScatterLines);
            pieChart.ShowHiddenData = true;
            //Set top left corner to row 1 column 2
            pieChart.SetPosition(6, 0, 7, 0);
            pieChart.SetSize(1000, 300);
            pieChart.Series.Add(("B1:B100"), ("A1:A100"));

            pieChart.Title.Text = "Time spent diagram";
            //Set datalabels and remove the legend

            pieChart.Legend.Remove();




        } private ValueOverProdTime[] Test1() {
            ValueOverProdTime[] brrt = new ValueOverProdTime[100];
            for (int i = 0; i < 100; i++)
            {
                ValueOverProdTime temp = new ValueOverProdTime();
                temp.Value = rand.Next(100);
                temp.Time = DateTime.Today.AddDays(rand.Next(1000));
                brrt[i] = temp;
            }
            return brrt;
        }
            


        }
    }
