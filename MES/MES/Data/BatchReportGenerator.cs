﻿using MES.Acquintance;
using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;
using System.Collections.Generic;
using System.IO;

namespace MES
{
    class BatchReportGenerator
    {
        private ExcelPackage ep;

        /// <summary>
        /// Generates and fills an excel file with given data.
        /// </summary>
        /// <param name="batchID"></param> ID of a batch,
        /// <param name="productType"></param> Type of product.
        /// <param name="aProduct"></param> Amount of acceptable products produced
        /// <param name="dProduct"></param> Amount of defect products produced.
        /// <param name="timeUsed"></param> Time spent in the different machine states.
        /// <param name="tData"></param> Temperature over production time.
        /// <param name="hData"></param> Humidity over production time.
        public void GenerateFile(float batchID, float productType, int aProduct,
            int dProduct, int[] timeUsed, ISet<IList<IBatchValue>> batchValues)
        {
            ep = new ExcelPackage();
            IList<IBatchValue> tData = new List<IBatchValue>();
            IList<IBatchValue> hData = new List<IBatchValue>();

            foreach (IList<IBatchValue> list in batchValues)
            {
                if (list[0].Type < 0)
                {
                    tData = list;
                }
                else if (list[0].Type == 0)
                {
                    hData = list;
                }
            }

            // A workbook must have at least on cell
            var ws = ep.Workbook.Worksheets.Add("Batch Report");
            var temp = ep.Workbook.Worksheets.Add("Temperature");
            var humid = ep.Workbook.Worksheets.Add("Humidity");

            // To set values in the spreadsheet use the Cells indexer
            #region Cell values

            ws.Cells["A1"].Value = "Batch_ID:";
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
            ws.Cells["B6"].Value = 1; // this, along with other hardcoded int values, has not been implemented yet.
            ws.Cells["A7"].Value = "Stopped:";
            ws.Cells["B7"].Value = 2;
            ws.Cells["A8"].Value = "Idle:";
            ws.Cells["B8"].Value = 3;
            ws.Cells["A9"].Value = "Suspended:";
            ws.Cells["B9"].Value = 4;
            ws.Cells["A10"].Value = "Execute:";
            ws.Cells["B10"].Value = 5;
            ws.Cells["A11"].Value = "Aborted:";
            ws.Cells["B11"].Value = 6;
            ws.Cells["A12"].Value = "Held:";
            ws.Cells["B12"].Value = 7;
            ws.Cells["A13"].Value = "Complete:";
            ws.Cells["B13"].Value = 8;
            ws.Cells["A15"].Value = "TempatureProductionTime:";
            ws.Cells["A15"].Style.Font.Bold = true;
            ws.Cells["A16"].Value = "HumidityProductionTime:";
            ws.Cells["A16"].Style.Font.Bold = true;
            ws.Cells[ws.Dimension.Address].AutoFitColumns();

            #endregion

            CreateGraph(ws, 6, 7, 300, 500, "B6:B14", "A6:A14", "Time spent Diagram",
                eChartType.ColumnStacked);

            // Insert temperature and humidity data
            WriteData(tData, temp, "Temperature over prod time");
            WriteData(hData, humid, "Humidity over prod time");

            string path = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            path = Directory.GetParent(path).FullName;
            path = Directory.GetParent(Directory.GetParent(path).FullName).FullName;
            path += @"\MES\Data\BatchReports\";

            // Save the new workbook
            ep.SaveAs(new FileInfo(path + "BatchReport" + batchID + ".xlsx"));
        }

        /// <summary>
        /// Inserts array of data into two columns
        /// </summary>
        /// <param name="data"></param> Array filled with either temperature or humidity data.
        /// <param name="ew"></param> Worksheet to write in.
        /// <param name="title"></param> Title of graph.
        private void WriteData(IList<IBatchValue> data, ExcelWorksheet ew, string title)
        {
            ew.Cells["A1"].Value = "Temp:";
            ew.Cells["A1"].Style.Font.Bold = true;

            ew.Cells["B1"].Value = "Time:";
            ew.Cells["B1"].Style.Font.Bold = true;

            for (int i = 0; i < data.Count; i++)
            {
                ew.Cells["A" + (i + 2)].Value = data[i].Timestamp;
                ew.Cells["B" + (i + 2)].Value = data[i].Value;
            }

            //Add the XY graph
            CreateGraph(ew, 2, 3, 300, 1000, "B2:B101", "A2:A101", title, eChartType.XYScatterLines);
        }

        /// <summary>
        /// Creates a specificed graph type in a given worksheet
        /// </summary>
        /// <param name="ew"></param> Worksheet to create graph in.
        /// <param name="row"></param> Position of graph (row).
        /// <param name="col"></param> Position of graph (column).
        /// <param name="height"></param> Height of graph.
        /// <param name="width"></param> Width of graph.
        /// <param name="valueSeries"></param> Series of cells containing the values used for the graph.
        /// <param name="nameSeries"></param> Series of cells containing the "names" of the values used for the graph.
        /// <param name="title"></param> Title of the graph.
        /// <param name="chartType"></param> Graph type.
        private void CreateGraph(ExcelWorksheet ew, int row, int col, int height, int width,
            string valueSeries, string nameSeries, string title, eChartType chartType)
        {
            //Add the XY graph
            var xyGraph = ew.Drawings.AddChart("chart", chartType);
            xyGraph.ShowHiddenData = true;
            //Set position, size & add series
            xyGraph.SetPosition(row, 0, col, 0);
            xyGraph.SetSize(width, height);
            xyGraph.Series.Add((valueSeries), (nameSeries));
            // set title
            xyGraph.Title.Text = title;
            // remove the legend
            xyGraph.Legend.Remove();
        }
    }
}
