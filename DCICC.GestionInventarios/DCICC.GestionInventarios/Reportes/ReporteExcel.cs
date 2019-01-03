using DCICC.GestionInventarios.Controllers;
using HtmlAgilityPack;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
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
            ReportesController objReportesCont = new ReportesController();
            Models.Reportes infoParametros = objReportesCont.ObtenerParametrosReporteJSON();
            MemoryStream memStream=new MemoryStream();
            //Cargar la plantilla para reportes en Excel
            var fileinfo = new FileInfo(path_Plantilla);
            using (ExcelPackage pck = new ExcelPackage(fileinfo))
            {
                if (labFiltro == "Mostrar Todos")
                {
                    labFiltro = "Todos";
                }
                else if (labFiltro == "")
                {
                    labFiltro = "-";
                }
                string nombreImage = string.Empty;
                string pathAux = System.Web.Hosting.HostingEnvironment.MapPath("~/Content/Images/LogoReporte/LogoNuevo.jpg");
                string pathAux2 = System.Web.Hosting.HostingEnvironment.MapPath("~/Content/Images/LogoReporte/LogoNuevo.png");
                if (File.Exists(pathAux))
                {
                    nombreImage = "LogoNuevo.jpg";
                }
                else if (File.Exists(pathAux2))
                {
                    nombreImage = "LogoNuevo.png";
                }
                else
                {
                    nombreImage = "LogoUPS.png";
                }
                //Carga de la primera hoja de trabajo del Excel
                ExcelWorksheet ws = pck.Workbook.Worksheets[1];
                ws.Column(1).Width = 25;
                Image img = Image.FromFile(System.Web.Hosting.HostingEnvironment.MapPath(string.Format("~/Content/Images/LogoReporte/{0}", nombreImage)));
                ExcelPicture pic = ws.Drawings.AddPicture("Logo", img);
                pic.From.Column = 0;
                pic.From.Row = 0;
                pic.SetSize(170, 60);
                pic.From.ColumnOff = 2 * 9525;
                pic.From.RowOff = 2 * 9525;

                ws.Cells["A8"].LoadFromDataTable(infoTable, true);
                //Titulos Organización
                ConfigCell(1, 2, 1, infoTable.Columns.Count, ws, infoTable.Columns.Count, string.Format("{0} {1}", infoParametros.TituloCarrera, infoParametros.TituloSedeCampus), true, true, Color.White, Color.Black, ExcelHorizontalAlignment.Center);
                ConfigCell(2, 2, 2, infoTable.Columns.Count, ws, infoTable.Columns.Count, infoParametros.TituloSistema, true, true, Color.White, Color.Black, ExcelHorizontalAlignment.Center);
                ConfigCell(3, 2, 3, infoTable.Columns.Count, ws, infoTable.Columns.Count, infoParametros.TituloReporte, true, true, Color.White, Color.Black, ExcelHorizontalAlignment.Center);
                //ws.Cells[1, 1, 7, infoTable.Columns.Count].AutoFitColumns();

                //Carga de Parámetros
                //Campus
                //ConfigCell(4,2,4, infoTable.Columns.Count,ws, infoTable.Columns.Count, "Quito, campus sur.",true,false,Color.White, Color.Black, ExcelHorizontalAlignment.Left);
                //Fecha
                ConfigCell(4, 2, 4, infoTable.Columns.Count, ws, infoTable.Columns.Count, DateTime.Now.ToLongDateString(), true, false, Color.White, Color.Black, ExcelHorizontalAlignment.Left);
                //ws.Cells[4, 2, 4, infoTable.Columns.Count].AutoFitColumns();
                //Laboratorio
                ConfigCell(5, 2, 5, infoTable.Columns.Count, ws, infoTable.Columns.Count, labFiltro, true, false, Color.White, Color.Black, ExcelHorizontalAlignment.Left);
                //N° Equipos
                ConfigCell(6, 2, 6, infoTable.Columns.Count, ws, infoTable.Columns.Count, infoTable.Rows.Count + "", true, false, Color.White, Color.Black, ExcelHorizontalAlignment.Left);
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
                ws.Cells[7, 2, 7 + infoTable.Rows.Count, infoTable.Columns.Count].AutoFitColumns();
                ws.Cells["A8"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                //Transformar el archivo a Bytes
                memStream = new MemoryStream(pck.GetAsByteArray());
                pck.Dispose();
                ws.Dispose();
                img.Dispose();
                pic.Dispose();
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
                //Rng.AutoFitColumns();
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