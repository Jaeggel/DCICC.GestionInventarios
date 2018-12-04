using HtmlAgilityPack;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace DCICC.GestionInventarios.Reportes
{
    public class ReporteExcel
    {
        /// <summary>
        /// Método para generar el reporte en excel.
        /// </summary>
        /// <param name="infoTable">Datatable con la información que se desplegará en el Excel</param> 
        /// <param name="tituloReporte">Título que se mostrará en el Excel</param>
        /// <returns></returns>
        public MemoryStream GenerarReporteExcel(DataTable infoTable, string tituloReporte)
        {
            MemoryStream memStream;
            string path = System.Web.Hosting.HostingEnvironment.MapPath("~/Reportes/PlantillaExcel.xlsx");
            //Cargar la plantilla para reportes en Excel
            var fileinfo = new FileInfo(path);
            using (ExcelPackage pck = new ExcelPackage(fileinfo))
            {
                //Carga de la primera hoja de trabajo del Excel
                var ws = pck.Workbook.Worksheets[1];
                //Carga datos del html
                ws.Cells["A6"].LoadFromDataTable(infoTable, true);
                //Título del la lista del reporte
                using (ExcelRange Rng = ws.Cells[5, 1, 5, infoTable.Columns.Count])
                {
                    Rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    Rng.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#3c5a77"));//73879C,2a3f54,
                    Rng.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#E7E7E7"));
                    Rng.Style.Font.Bold = true;
                    Rng.Value = "LISTADO DE " + tituloReporte.ToUpper();
                    Rng.Merge = true;
                    Rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    Rng.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    Rng.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    Rng.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    Rng.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                }
                //Encabezado de la tabla
                using (ExcelRange Rng = ws.Cells[6, 1, 6, infoTable.Columns.Count])
                {
                    Rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    Rng.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#3c5a77"));
                    Rng.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#E7E7E7"));
                    Rng.Style.Font.Bold = true;
                    Rng.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    Rng.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    Rng.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    Rng.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                }
                //Datos
                using (ExcelRange Rng = ws.Cells[7, 1, 6 + infoTable.Rows.Count, infoTable.Columns.Count])
                {
                    Rng.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    Rng.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    Rng.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    Rng.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                }
                //Autoajustar las celdas para los datos
                ws.Cells[ws.Dimension.Address].AutoFitColumns();
                //Transformar el archivo a Bytes
                memStream = new MemoryStream(pck.GetAsByteArray());
            }
            return memStream;
        }
        /// <summary>
        /// Método para generar  el DataTable a partir de una tabla HTML.
        /// </summary>
        /// <param name="infoHTML"></param>
        /// <returns></returns>
        public DataTable ObtenerDatosTablaHTML(string infoHTML)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(infoHTML);
            var nodes = doc.DocumentNode.SelectNodes("//table//tr");
            var table = new DataTable("ReporteTablaHtml");
            var headers = nodes[0]
                .Elements("th")
                .Select(th => th.InnerText.Trim());
            foreach (var header in headers)
            {
                table.Columns.Add(header.ToUpper());
            }
            var rows = nodes.Skip(1).Select(tr => tr
                .Elements("td")
                .Select(td => td.InnerText.Trim())
                .ToArray());
            foreach (var row in rows)
            {
                table.Rows.Add(row);
            }
            return table;
        }
    }
}