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