var url_idioma = obtenerIdioma();
var estados = listaEstadosTicket();
var url_metodo;
var ticketsReportados;
var responsables=[];
var fechas = [];

//Método ajax para obtener datos de los tickets
function obtenerTickets(url) {
    url_metodo = url;
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'get',
        success: function (data) {
            console.log("Datos Exitosos");
            ticketsReportados = data;
            cargarEstadosCmb();
            cargarTablaTickets();
            $('#dataTableTickets').DataTable({
                "language": {
                    "url": url_idioma
                }
            });

        }
    });
}


///////////////Funciones para cargar el combobox de estados
function cargarEstadosCmb() {
    var str = '<select id="Estados" class="form-control" name="Estados" required>';
    str += '<option value="">Mostrar Todos</option>';
    for (var i = 0; i < estados.length; i++) {
        str += '<option value="' + estados[i] + '">' + estados[i] + '</option>';      
    }
    str += '</select>';
    $("#cargarEstados").html(str);
    ///////CAMBIO DEL COMBOBOX
    $('#Estados').change(function () {
        var opcion = document.getElementById("Estados");
        var nick = opcion.options[opcion.selectedIndex];
        if (nick.value == "") {
            $('#dataTableTickets').DataTable().column(0).search(
                ""
            ).draw();
        } else {
            $('#dataTableTickets').DataTable().column(0).search(
                nick.text
            ).draw();
        }
    });
}

//Función para cargar el combobox de responsables
function cargarResponsablesCmb(unicos) {
    var str = '<select id="Responsables" class="form-control" name="Responsables">';
    str += '<option value="">Mostrar Todos</option>';
    for (var i = 0; i < unicos.length; i++) {
        str += '<option value="' + unicos[i] + '">' + unicos[i] + '</option>';
    }
    str += '</select>';
    $("#cargarResponsables").html(str);
    ///////CAMBIO DEL COMBOBOX
    $('#Responsables').change(function () {
        var opcion = document.getElementById("Responsables");
        var respon = opcion.options[opcion.selectedIndex];
        if (respon.value == "") {
            $('#dataTableTickets').DataTable().column(7).search(
                ""
            ).draw();
        } else {
            $('#dataTableTickets').DataTable().column(7).search(
                respon.text
            ).draw();
        }
    });
}

//Función para cargar la tabla de tickets Abiertos
function cargarTablaTickets() {
    var str = '<table id="dataTableTickets" class="table jambo_table bulk_action  table-bordered" style="width:100%">';
    str += '<thead> <tr> <th>Estado del Ticket</th> <th>Laboratorio o Activo</th> <th>Descripción del Incidente</th> <th>Prioridad</th> <th>Fecha de Apertura</th> <th>Reportado Por</th><th>Fecha de Atención del Ticket</th> <th>Atendido Por</th></tr> </thead>';
    str += '<tbody>';

    for (var i = 0; i < ticketsReportados.length; i++) {      
            //Función para dar formato a la fecha
        var fechaAper = new Date(parseInt((ticketsReportados[i].FechaAperturaTicket).substr(6)));
        var fechaordenar = (fechaAper.toLocaleDateString("en-US"));
        var fechaSol = new Date(parseInt((ticketsReportados[i].FechaSolucionTicket).substr(6)));
        var fechaSolucion = (fechaSol.toLocaleDateString("en-US"));

        function pad(n) { return n < 10 ? "0" + n : n; }
        var fechaApertura = pad(fechaAper.getMonth() + 1) + "/" + pad(fechaAper.getDate()) + "/" + fechaAper.getFullYear();
        fechas[i] = fechaordenar;

        str += '<tr><td>' + ticketsReportados[i].EstadoTicket;
        if (ticketsReportados[i].IdLaboratorio != 0) {
            str += '</td><td> <strong>Laboratorio:</strong> ' + ticketsReportados[i].NombreLaboratorio;
        } else {
            str += '</td><td><strong> Activo:</strong> ' + ticketsReportados[i].NombreDetalleActivo;
        }
        str +='</td><td>' + ticketsReportados[i].DescripcionTicket+
              '</td><td>' + ticketsReportados[i].PrioridadTicket +
              '</td><td>' + fechaApertura +
              '</td><td>' + ticketsReportados[i].NombreUsuario +
              '</td><td>' + fechaSolucion +
            '</td><td>' + ticketsReportados[i].NombreUsuarioResponsable;
        str += '</td></tr>';
        responsables[i] = ticketsReportados[i].NombreUsuarioResponsable;
    }
    str += '</tbody></table>';
    $("#tablaReportesTickets").html(str);

    var unicos = Array.from(new Set(responsables));
    cargarResponsablesCmb(unicos);
    fechas = fechas.sort();
    var minDate = fechas[0];
    var maxDate = fechas[fechas.length - 1];
    inicioFecha(minDate, maxDate);
    finFecha(minDate, maxDate);
}


//Funciones para obtener la fecha máxima y mínima de búsqueda
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
    var table = $('#dataTableTickets').DataTable();
    $.fn.dataTable.ext.search.push(
        function (settings, data, dataIndex) {
            var min = new Date($("#FechaInicio").val()).getTime();
            var max = new Date($("#FechaFin").val()).getTime();
            var startDate = new Date(data[4]).getTime();
            if (min == null && max == null) { return true; }
            if (min == null && startDate <= max) { return true; }
            if (max == null && startDate >= min) { return true; }
            if (startDate <= max && startDate >= min) { return true; }
            return false;
        }
    );
    table.draw();
}

