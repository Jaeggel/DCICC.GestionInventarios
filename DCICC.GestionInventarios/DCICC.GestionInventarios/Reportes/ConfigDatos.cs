using DCICC.GestionInventarios.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DCICC.GestionInventarios.Reportes
{
    public class ConfigDatos
    {
        /// <summary>
        /// Método para generar  el DataTable a partir de una tabla HTML.
        /// </summary>
        /// <param name="infoHTML"></param>
        /// <returns></returns>
        public DataTable ObtenerDatosTablaHTML(string infoHTML)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(infoHTML);
            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//table//tr");
            DataTable table = new DataTable("ReporteTablaHtml");
            IEnumerable<string> headers = nodes[0]
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
        /// <summary>
        /// Método para generar una lista tipo Activos a partir de un DataTable.
        /// </summary>
        /// <param name="dtInfoActivosQR"></param>
        /// <returns></returns>
        public List<Activos> ObtenerListaActivosQR(DataTable dtInfoActivosQR)
        {
            List<Activos> lstActivos = new List<Activos>();
            foreach (DataRow r in dtInfoActivosQR.Rows)
            {
                Activos objActivo = new Activos();
                objActivo.NombreActivo= r[3].ToString();
                objActivo.IdCQR= r[1].ToString();
                lstActivos.Add(objActivo);
            }
            return lstActivos;
        }
        /// <summary>
        /// Método para generar una lista tipo Accesorios a partir de un DataTable.
        /// </summary>
        /// <param name="dtInfoAccesoriosQR"></param>
        /// <returns></returns>
        public List<Accesorios> ObtenerListaAccesoriosQR(DataTable dtInfoAccesoriosQR)
        {
            List<Accesorios> lstAccesorios = new List<Accesorios>();
            foreach (DataRow r in dtInfoAccesoriosQR.Rows)
            {
                Accesorios objAccesorio = new Accesorios();
                objAccesorio.NombreDetalleActivo = r[1].ToString();
                objAccesorio.NombreAccesorio = r[2].ToString();
                objAccesorio.IdCQR = r[6].ToString();
                lstAccesorios.Add(objAccesorio);
            }
            return lstAccesorios;
        }
    }
}