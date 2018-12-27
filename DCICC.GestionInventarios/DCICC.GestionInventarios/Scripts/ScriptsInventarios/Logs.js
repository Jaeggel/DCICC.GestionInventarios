var url_idioma = obtenerIdioma();
var datosLogs;
var idCategoriaModificar;
var datosUsuarios;

//Método ajax para obtener los datos de Logs
function obtenerLogs(url) {
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            if (data.OperacionExitosa) {
                datosLogs = data.ListaObjetoInventarios;
                cargarLogsTabla();
                $('#dataTableLogs').DataTable({
                    "language": {
                        "url": url_idioma
                    }
                });
            } else {
                showNotify("Error en la Consulta", 'No se ha podido mostrar los datos: ' + data.MensajeError, "error");
            }          
        }
    });
}

//Método ajax para obtener los datos de Usuarios
function obtenerNicksUsuarios(url) {
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            console.log("ya esta");
            datosUsuarios = data;
            cargarNicksCmb()
        }
    });
}

//Función para cargar el combobox de tipo de activo
function cargarNicksCmb() {
    var str = '<select id="NicksUsuario" class="form-control" name="NicksUsuario">';
    str += '<option value="">Mostrar Todos</option>';
    for (var i = 0; i < datosUsuarios.length; i++) {
        str += '<option value="' + datosUsuarios[i] + '">' + datosUsuarios[i] + '</option>';
    }
    str += '</select>';
    $("#cargarNicks").html(str);
    ///////CAMBIO DEL COMBOBOX
    $('#NicksUsuario').change(function () {
        var opcion = document.getElementById("NicksUsuario");
        var nick = opcion.options[opcion.selectedIndex];
        if (nick.value == "") {
            $('#dataTableLogs').DataTable().column(0).search(
                ""
            ).draw();
        } else {
            $('#dataTableLogs').DataTable().column(0).search(
                nick.text
            ).draw();
        }
    });
}

function consultaOperacion(tipoOpe) {
    if (tipoOpe.value == "") {
        $('#dataTableLogs').DataTable().column(3).search(
            ""
        ).draw();
    } else {
        $('#dataTableLogs').DataTable().column(3).search(
            tipoOpe.value
        ).draw();
    }
}

//Función para cargar la tabla de Logs
function cargarLogsTabla() {
    var str = '<table id="dataTableLogs" class="table jambo_table bulk_action table-bordered" style="width:100%">';
    str += '<thead><tr><th>Usuario</th> <th>IP</th> <th>Fecha<br/>(dd/mm/yyyy)</th> <th>Operación</th> <th>Tabla Afectada</th> <th>Valores Anteriores</th> <th>Valores Modificados</th></tr></thead>';
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
    }
    str += '</tbody>' +
        '<tfoot><tr><th>Usuario</th> <th>IP</th> <th>Fecha</th> <th>Operación</th> <th>Tabla Afectada</th> <th>Valores Anteriores</th> <th>Valores Modificads</th></tr></tfoot>' +
        '</table>';
    $("#tablaLogs").html(str);
}