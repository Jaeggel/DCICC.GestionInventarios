var url_idioma = obtenerIdioma();
var datosLogs;
var idCategoriaModificar;
var datosUsuarios;
var fechas = [];

//Método ajax para obtener los datos de Logs
function obtenerLogs(url) {
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            console.log("Datos Exitosos");
            datosLogs = data;
            cargarLogsTabla();
            $('#dataTableLogs').DataTable({
                "language": {
                    "url": url_idioma
                },
                //"bPaginate": false
            });
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
    };
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
    str += '<thead> <tr> <th>Responsable de la Operación</th> <th>IP</th> <th>Fecha</th> <th>Operación</th> <th>Tabla Afectada</th></tr> </thead>';
    str += '<tbody>';
    for (var i = 0; i < datosLogs.length; i++) {
        //Método para dar formato a la fecha y hora
        var fechaLog = new Date(parseInt((datosLogs[i].FechaLogs).substr(6)));
        var fechaordenar = (fechaLog.toLocaleDateString("en-US"));

        function pad(n) { return n < 10 ? "0" + n : n; }
        var fechaApertura = pad(fechaLog.getMonth() + 1) + "/" + pad(fechaLog.getDate()) + "/" + fechaLog.getFullYear();

        fechas[i] = fechaordenar;

        str += '<tr><td>' + datosLogs[i].IdUsuario +
            '</td><td>' + datosLogs[i].IpLogs +
            '</td><td>' + fechaApertura +
            '</td><td>' + datosLogs[i].OperacionLogs +
            '</td><td>' + datosLogs[i].TablaLogs +
            '</td></tr>';
    }
    str += '</tbody>' +
        '</table>';
    $("#tablaReportesLogs").html(str);
    fechas = fechas.sort();
    console.log(fechas);
    var minDate = fechas[0];
    var maxDate = fechas[fechas.length - 1];
    inicioFecha(minDate, maxDate);
    finFecha(minDate, maxDate);
}

function inicioFecha(minDate, maxDate) {
    $(function () {
        $('input[name="FechaInicio"]').daterangepicker({
            startDate: minDate,
            format: 'mm-dd-yyyy',
            singleDatePicker: true,
            showDropdowns: true,
            minDate: minDate,
            maxDate: maxDate
        });
    });
}

function finFecha(minDate, maxDate) {
    $(function () {
        $('input[name="FechaFin"]').daterangepicker({
            startDate: 0,
            dateFormat: 'mm-dd-yyyyy',
            singleDatePicker: true,
            showDropdowns: true,
            minDate: minDate,
            maxDate: 0
        });
    });
}

//Funcion para consultar x Fechas
function consultarFechas() {
    var table = $('#dataTableLogs').DataTable();
    $.fn.dataTable.ext.search.push(
        function (settings, data, dataIndex) {
            var min = new Date($("#FechaInicio").val()).getTime();
            var max = new Date($("#FechaFin").val()).getTime();
            var startDate = new Date(data[2]).getTime();
            if (min == null && max == null) { return true; }
            if (min == null && startDate <= max) { return true; }
            if (max == null && startDate >= min) { return true; }
            if (startDate <= max && startDate >= min) { return true; }
            return false;
        }
    );
    table.draw();
}