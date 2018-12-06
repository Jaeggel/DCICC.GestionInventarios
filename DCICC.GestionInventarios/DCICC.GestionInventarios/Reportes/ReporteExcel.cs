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
        string path_Plantilla = System.Web.Hosting.HostingEnvironment.MapPath("~/Reportes/PlantillaExcel.xlsx");
        /// <summary>
        /// Método para generar el reporte en excel.
        /// </summary>
        /// <param name="infoTable">Datatable con la información que se desplegará en el Excel</param> 
        /// <param name="tituloReporte">Título que se mostrará en el Excel</param>
        /// <returns></returns>
        public MemoryStream GenerarReporteExcel(DataTable infoTable, string tituloReporte)
        {
            MemoryStream memStream;
            //Cargar la plantilla para reportes en Excel
            var fileinfo = new FileInfo(path_Plantilla);
            using (ExcelPackage pck = new ExcelPackage(fileinfo))
            {
                //Carga de la primera hoja de trabajo del Excel
                var ws = pck.Workbook.Worksheets[1];
                //Carga datos del html
                ws.Cells["A9"].LoadFromDataTable(infoTable, true);
                //Título del la lista del reporte
                using (ExcelRange Rng = ws.Cells[8, 1, 8, infoTable.Columns.Count])
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
                using (ExcelRange Rng = ws.Cells[9, 1, 9, infoTable.Columns.Count])
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
                using (ExcelRange Rng = ws.Cells[10, 1, 9 + infoTable.Rows.Count, infoTable.Columns.Count])
                {
                    Rng.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    Rng.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    Rng.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    Rng.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                }
                //Autoajustar las celdas para los datos
                ws.Cells[8, 1, 8 + infoTable.Rows.Count, infoTable.Columns.Count].AutoFitColumns();
                //Transformar el archivo a Bytes
                memStream = new MemoryStream(pck.GetAsByteArray());
            }
            return memStream;
        }
    }
}