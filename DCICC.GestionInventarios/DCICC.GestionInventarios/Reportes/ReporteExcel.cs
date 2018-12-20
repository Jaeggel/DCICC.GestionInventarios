using HtmlAgilityPack;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace DCICC.GestionInventarios.Reportes
{
    public class ReporteExcel
    {
        readonly string path_Plantilla = System.Web.Hosting.HostingEnvironment.MapPath("~/Reportes/PlantillaExcel.xlsx");
        /// <summary>
        /// Método para generar el reporte en excel.
        /// </summary>
        /// <param name="infoTable">Datatable con la información que se desplegará en el Excel</param> 
        /// <param name="tituloReporte">Título que se mostrará en el Excel</param>
        /// <returns></returns>
        public MemoryStream GenerarReporteExcel(DataTable infoTable, string tituloReporte,string labFiltro,string firmaUsuario)
        {
            MemoryStream memStream;
            //Cargar la plantilla para reportes en Excel
            var fileinfo = new FileInfo(path_Plantilla);
            using (ExcelPackage pck = new ExcelPackage(fileinfo))
            {
                if (labFiltro == "Mostrar Todos")
                {
                    labFiltro = "Todos";       
                }else if (labFiltro == "")
                {
                    labFiltro = "-";
                }
                //Carga de la primera hoja de trabajo del Excel
                ExcelWorksheet ws = pck.Workbook.Worksheets[1];
                //Carga datos del html
                ws.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                ws.Cells["A8"].LoadFromDataTable(infoTable, true);
                //Titulos Organización
                ConfigCell(1, 2, 1, infoTable.Columns.Count, ws, infoTable.Columns.Count, "Ingeniería en Ciencias de la Computación Sede Quito - Campus Sur", true, true, Color.White, Color.Black, ExcelHorizontalAlignment.Center);
                ConfigCell(2, 2, 2, infoTable.Columns.Count, ws, infoTable.Columns.Count, "Sistema de Gestión de Inventarios y Ticketing para Soporte Técnico", true, true, Color.White, Color.Black, ExcelHorizontalAlignment.Center);
                ConfigCell(3, 2, 3, infoTable.Columns.Count, ws, infoTable.Columns.Count, "Reporte de Activos de TI del Data Center y Laboratorios del ICC", true, true, Color.White, Color.Black, ExcelHorizontalAlignment.Center);
                ws.Cells[1, 1, 7, infoTable.Columns.Count].AutoFitColumns();
                
                //Carga de Parámetros
                //Campus
                //ConfigCell(4,2,4, infoTable.Columns.Count,ws, infoTable.Columns.Count, "Quito, campus sur.",true,false,Color.White, Color.Black, ExcelHorizontalAlignment.Left);
                //Fecha
                ConfigCell(4, 2, 4, infoTable.Columns.Count, ws, infoTable.Columns.Count, DateTime.Now.ToLongDateString(), true, false, Color.White, Color.Black, ExcelHorizontalAlignment.Left);
                ws.Cells[4, 2, 4, infoTable.Columns.Count].AutoFitColumns();
                //Laboratorio
                ConfigCell(5, 2, 5, infoTable.Columns.Count, ws, infoTable.Columns.Count, labFiltro, true, false, Color.White, Color.Black, ExcelHorizontalAlignment.Left);
                //N° Equipos
                ConfigCell(6, 2, 6, infoTable.Columns.Count, ws, infoTable.Columns.Count, infoTable.Rows.Count-1+"", true, false, Color.White, Color.Black, ExcelHorizontalAlignment.Left);
                //Título del la lista del reporte
                ConfigCell(7, 1, 7, infoTable.Columns.Count, ws, infoTable.Columns.Count, "LISTADO DE " + tituloReporte.ToUpper(), true, true, ColorTranslator.FromHtml("#3c5a77"), ColorTranslator.FromHtml("#E7E7E7"), ExcelHorizontalAlignment.Center);
                //Encabezado de la tabla
                ConfigCell(8, 1, 8, infoTable.Columns.Count, ws, infoTable.Columns.Count, null, false, true, ColorTranslator.FromHtml("#3c5a77"), ColorTranslator.FromHtml("#E7E7E7"), ExcelHorizontalAlignment.Center);
                //Datos
                ConfigCell(9, 1, 8 + infoTable.Rows.Count, infoTable.Columns.Count, ws, infoTable.Columns.Count, null, false, false, Color.White, Color.Black, ExcelHorizontalAlignment.Left);
                //Elaborado por
                ConfigCell(9 + infoTable.Rows.Count, 1, 9 + infoTable.Rows.Count, 1, ws, 1, "ELABORADO POR: ", false, true, ColorTranslator.FromHtml("#ededed"), ColorTranslator.FromHtml("#23527c"), ExcelHorizontalAlignment.Left);
                ConfigCell(9 + infoTable.Rows.Count, 2, 9 + infoTable.Rows.Count, infoTable.Columns.Count, ws, infoTable.Columns.Count, Regex.Replace(firmaUsuario, @"(^\w)|(\s\w)", m => m.Value.ToUpper()), true, false, ColorTranslator.FromHtml("#ededed"), ColorTranslator.FromHtml("#23527c"), ExcelHorizontalAlignment.Left);
                //Autoajustar las celdas para los datos
                ws.Cells[7, 1, 7 + infoTable.Rows.Count, infoTable.Columns.Count].AutoFitColumns();
                
                //Transformar el archivo a Bytes
                memStream = new MemoryStream(pck.GetAsByteArray());
            }
            return memStream;
        }
        /// <summary>
        /// Método para configurar una celda en el reporte de Excel.
        /// </summary>
        /// <param name="fi"></param>
        /// <param name="ci"></param>
        /// <param name="ff"></param>
        /// <param name="cf"></param>
        /// <param name="sheetReporte"></param>
        /// <param name="columnCountReporte"></param>
        /// <param name="valorCelda"></param>
        /// <param name="mergeCelda"></param>
        /// <param name="boldCelda"></param>
        /// <param name="colorFondo"></param>
        /// <param name="colorLetra"></param>
        /// <param name="aliCelda"></param>
        public void ConfigCell(int fi,int ci,int ff,int cf, ExcelWorksheet sheetReporte,int columnCountReporte,string valorCelda,bool mergeCelda,bool boldCelda,Color colorFondo, Color colorLetra,ExcelHorizontalAlignment aliCelda)
        {
            using (ExcelRange Rng = sheetReporte.Cells[fi, ci, ff, cf])
            {
                if (valorCelda != null)
                {
                    Rng.Value = valorCelda;
                }
                Rng.Merge = mergeCelda;
                Rng.AutoFitColumns();
                Rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                Rng.Style.Fill.BackgroundColor.SetColor(colorFondo);
                Rng.Style.Font.Color.SetColor(colorLetra);
                Rng.Style.Font.Bold = boldCelda;
                Rng.Style.HorizontalAlignment = aliCelda;
                Rng.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                Rng.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                Rng.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                Rng.Style.Border.Left.Style = ExcelBorderStyle.Thin;
            }
        }
    }
}