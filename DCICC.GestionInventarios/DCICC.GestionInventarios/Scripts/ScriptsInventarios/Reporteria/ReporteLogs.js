var url_idioma = obtenerIdioma();
var datosLogs;
var idCategoriaModificar;

//Método ajax para obtener los datos de Logs
function obtenerLogs(url) {
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'get',
        success: function (data) {
            console.log("Datos Exitosos");
            datosLogs = data;
            cargarLogsTabla();
            $('#dataTableLogs').DataTable({
                "language": {
                    "url": url_idioma
                }
            });
        }
    });
}

//Función para cargar la tabla de Logs
function cargarLogsTabla() {
    var str = '<table id="dataTableLogs" class="table jambo_table bulk_action table-bordered" style="width:100%">';
    str += '<thead> <tr> <th>Usuario</th> <th>IP</th> <th>Fecha</th> <th>Operación</th> <th>Tabla Afectada</th> <th>Valores Anteriores</th> <th>Valores Modificados</th> </tr> </thead>';
    str += '<tbody>';
    for (var i = 0; i < datosLogs.length; i++) {
        //Método para dar formato a la fecha y hora
        var fechaLog = new Date(parseInt((datosLogs[i].FechaLogs).substr(6)));
        var fechaApertura = (fechaLog.toLocaleDateString("es-ES") + " " + fechaLog.getHours() + ":" + fechaLog.getMinutes() + ":" + fechaLog.getSeconds());

        str += '<tr><td>' + datosLogs[i].IdUsuario +
            '</td><td>' + datosLogs[i].IpLogs +
            '</td><td>' + fechaApertura +
            '</td><td>' + datosLogs[i].OperacionLogs +
            '</td><td>' + datosLogs[i].TablaLogs +
            '</td><td>' + datosLogs[i].ValorAnteriorLogs +
            '</td><td>' + datosLogs[i].ValorActualLogs +
            '</td></tr>';
    };
    str += '</tbody>' +
        '<tfoot><tr> <th>Usuario</th> <th>IP</th> <th>Fecha</th> <th>Operación</th> <th>Tabla Afectada</th> <th>Valores Anteriores</th> <th>Valores Modificads</th> </tr> </tfoot>' +
        '</table>';
    $("#tablaReportesLogs").html(str);
}