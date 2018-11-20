var url_idioma = obtenerIdioma();
var datosLogs;
var idCategoriaModificar;

function obtenerLogs(url) {
    console.log(url);
    //Método ajax para traer los roles de la base de datos
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            console.log(data);
            datosLogs = data;
            console.log("siiiiii: ");
            cargarLogsTabla();
            $('#dataTableLogs').DataTable({
                "language": {
                    "url": url_idioma
                }
            } );
        }
    });
}

//Función para cargar la tabla de Categorias
function cargarLogsTabla() {
    var str = '<table id="dataTableLogs" class="table jambo_table bulk_action  table-bordered dt-responsive nowrap" style="width:100%">';
    str += '<thead> <tr> <th>Usuario</th> <th>IP</th> <th>Fecha</th> <th>Operación</th> <th>Tabla Afectada</th> <th>Valores Anteriores</th> <th>Valores Modificados</th> </tr> </thead>';
    str += '<tbody>';
    for (var i = 0; i < datosLogs.length; i++) {
        str += '<tr><td>' + datosLogs[i].IdUsuario +
            '</td><td>' + datosLogs[i].IpLogs +
            '</td><td>' + new Date(parseInt((datosLogs[i].FechaLogs).substr(6))) +
            '</td><td>' + datosLogs[i].OperacionLogs +
            '</td><td>' + datosLogs[i].TablaLogs +
            '</td><td>' + datosLogs[i].ValorAnteriorLogs +
            '</td><td>' + datosLogs[i].ValorActualLogs;
    };
    str += '</tbody>' +
        '<tfoot><tr> <th>Usuario</th> <th>IP</th> <th>Fecha</th> <th>Operación</th> <th>Tabla Afectada</th> <th>Valores Anteriores</th> <th>Valores Modificads</th> </tr> </thead></tfoot>' +
        '</table > ';
    $("#tablaLogs").html(str);
}