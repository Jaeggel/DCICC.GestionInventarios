var url_idioma = obtenerIdioma();
var estados = listaEstadosTicket();
var url_metodo;
var ticketsReportados;

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
            $('#ataTableTickets').DataTable({
                "language": {
                    "url": url_idioma
                }
            });

        }
    });
}

///////////////Funciones para cargar los combobox
function cargarEstadosCmb() {
    var str = '<select id="Estados" class="form-control" name="Estados" required>';
    str += '<option value="">Escoga una opción...</option>';
    for (var i = 0; i < estados.length; i++) {
        if (estados[i] != 'ABIERTO') {
            str += '<option value="' + estados[i] + '">' + estados[i] + '</option>';
        }
    };
    str += '</select>';
    $("#cargarAbiertosCmb").html(str);
}

//Función para cargar la tabla de tickets Abiertos
function cargarTablaTickets() {
    var str = '<table id="dataTableTickets" class="table jambo_table bulk_action  table-bordered" style="width:100%">';
    str += '<thead> <tr> <th>Descripción del Incidente</th> <th>Laboratorio o Activo</th> <th>Prioridad</th> <th>Fecha de Apertura</th> <th>Reportado por:</th> <th>Modificar Estado</th> </tr> </thead>';
    str += '<tbody>';

    for (var i = 0; i < ticketsReportados.length; i++) {
        
            //Función para dar formato a la fecha
            var fechaAper = new Date(parseInt((ticketsReportados[i].FechaAperturaTicket).substr(6)));
            var fechaApertura = (fechaAper.toLocaleDateString("es-ES") + " " + fechaAper.getHours() + ":" + fechaAper.getMinutes() + ":" + fechaAper.getSeconds());

            str += '<tr><td>' + ticketsReportados[i].DescripcionTicket;
            if (ticketsReportados[i].IdLaboratorio != 0) {
                str += '</td><td> <strong>Laboratorio:</strong> ' + ticketsReportados[i].NombreLaboratorio;
            } else {
                str += '</td><td><strong> Activo:</strong> ' + ticketsReportados[i].NombreDetalleActivo;
            }

            str += '</td><td>' + ticketsReportados[i].PrioridadTicket +
                '</td><td>' + fechaApertura +
                '</td><td>' + ticketsReportados[i].NombreUsuario;
            str +='</td></tr>';
        
    };
    str += '</tbody></table>';
    $("#tablaReportesTickets").html(str);
}