var url_idioma = obtenerIdioma();
var estados = listaEstadosTicket();
var cmbPrioridades = listaPrioridadesTicket();
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
            cargarPrioridadesCmb();
            cargarTablaTickets();
            $('#dataTableTickets').DataTable({
                "language": {
                    "url": url_idioma
                },
                "aaSorting": []
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
            $('#dataTableTickets').DataTable().column(5).search(
                ""
            ).draw();
        } else {
            $('#dataTableTickets').DataTable().column(5).search(
                nick.text
            ).draw();
        }
    });
}

//Funciones para cargar el combobox de prioridades
function cargarPrioridadesCmb() {
    var str = '<select id="Prioridades" class="form-control" name="Prioridades" required>';
    str += '<option value="">Mostrar Todos</option>';
    for (var i = 0; i < cmbPrioridades.length; i++) {
        str += '<option value="' + cmbPrioridades[i] + '">' + cmbPrioridades[i] + '</option>';
    }
    str += '</select>';
    $("#cargarPrioridades").html(str);
    ///////CAMBIO DEL COMBOBOX
    $('#Prioridades').change(function () {
        var opcion = document.getElementById("Prioridades");
        var nick = opcion.options[opcion.selectedIndex];
        if (nick.value == "") {
            $('#dataTableTickets').DataTable().column(4).search(
                ""
            ).draw();
        } else {
            $('#dataTableTickets').DataTable().column(4).search(
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
            $('#dataTableTickets').DataTable().column(6).search(
                ""
            ).draw();
        } else {
            $('#dataTableTickets').DataTable().column(6).search(
                respon.text
            ).draw();
        }
    });
}

//Función para cargar la tabla de tickets Abiertos
function cargarTablaTickets() {
    var str = '<table id="dataTableTickets" class="table jambo_table bulk_action  table-bordered" style="width:100%">';
    str += '<thead> <tr> <th>Fecha de Apertura</th>  <th>Usuario Solicitante</th>  <th>Laboratorio o Activo</th> <th>Descripción del Incidente</th> <th>Prioridad</th> <th>Estado del Ticket</th>  <th>Responsable Asignado</th> <th>Comentario En Proceso</th> <th>Comentario En Espera</th> <th>Comentario Resuelto</th></tr> </thead>';
    str += '<tbody>';

    for (var i = 0; i < ticketsReportados.length; i++) {      
            //Función para dar formato a la fecha
        var fechaAper = new Date(parseInt((ticketsReportados[i].FechaAperturaTicket).substr(6)));
        var fechaordenar = (fechaAper.toLocaleDateString("en-US"));

        var fechaEnCurso = new Date(parseInt((ticketsReportados[i].FechaEnProcesoTicket).substr(6))).toLocaleDateString("en-US");
        var fechaEnEspera = new Date(parseInt((ticketsReportados[i].FechaEnEsperaTicket).substr(6))).toLocaleDateString("en-US");
        var fechaSolucion = new Date(parseInt((ticketsReportados[i].FechaResueltoTicket).substr(6))).toLocaleDateString("en-US");

        function pad(n) { return n < 10 ? "0" + n : n; }
        var fechaApertura = pad(fechaAper.getMonth() + 1) + "/" + pad(fechaAper.getDate()) + "/" + fechaAper.getFullYear();
        fechas[i] = fechaordenar;

        str += '<tr><td>' + fechaApertura +
            '</td><td>' + ticketsReportados[i].NombreUsuario;
        if (ticketsReportados[i].IdLaboratorio != 0) {
            str += '</td><td> <strong>Laboratorio:</strong> ' + ticketsReportados[i].NombreLaboratorio;
        } else {
            str += '</td><td><strong> Activo:</strong> ' + ticketsReportados[i].NombreDetalleActivo;
        }
        str += '</td><td>' + ticketsReportados[i].DescripcionTicket +
            '</td><td>' + ticketsReportados[i].PrioridadTicket +
            '</td><td>' + ticketsReportados[i].EstadoTicket +
            '</td><td>' + ticketsReportados[i].NombreUsuarioResponsable;
        if (ticketsReportados[i].ComentarioEnProcesoTicket != "") {
            str += '</td><td class="text-justify"> <strong>Comentario: </strong>' + ticketsReportados[i].ComentarioEnProcesoTicket + '<br/> <strong>Fecha: </strong>' + fechaEnCurso ;
        } else {
            str += '</td><td>';
        }
        if (ticketsReportados[i].ComentarioEnEsperaTicket != "") {
            str += '</td><td class="text-justify"> <strong>Comentario: </strong>' + ticketsReportados[i].ComentarioEnEsperaTicket + '<br/> <strong>Fecha: </strong>' + fechaEnEspera ;
        } else {
            str += '</td><td>';
        } 
        if (ticketsReportados[i].ComentarioResueltoTicket != "") {
            str += '</td><td class="text-justify"> <strong>Comentario: </strong>' + ticketsReportados[i].ComentarioResueltoTicket + '<br/> <strong>Fecha: </strong>' + fechaSolucion;
        } else {
            str += '</td><td>';
        } 
        str += '</td></tr>';
        responsables[i] = ticketsReportados[i].NombreUsuarioResponsable;
    }
    str += '</tbody></table>';
    $("#tablaReportesTickets").html(str);

    var unicos = Array.from(new Set(responsables));
    cargarResponsablesCmb(unicos);
    var minDate = fechas[0];
    inicioFecha(minDate);
    finFecha(minDate);
}

//Funciones para obtener la fecha máxima y mínima de búsqueda
function inicioFecha(minDate) {
    $(function () {
        $('input[name="FechaInicio"]').daterangepicker({
            startDate: minDate,
            format: 'mm-dd-yyyy',
            singleDatePicker: true,
            showDropdowns: true,
            minDate: minDate,
            maxDate: new Date()
        });
    });
}

function finFecha(minDate) {
    $(function () {
        $('input[name="FechaFin"]').daterangepicker({
            startDate: new Date(),
            dateFormat: 'mm-dd-yyyyy',
            singleDatePicker: true,
            showDropdowns: true,
            minDate: minDate,
            maxDate: new Date()
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
            var startDate = new Date(data[0]).getTime();
            if (min == null && max == null) { return true; }
            if (min == null && startDate <= max) { return true; }
            if (max == null && startDate >= min) { return true; }
            if (startDate <= max && startDate >= min) { return true; }
            return false;
        }
    );
    table.draw();
}

