var ticketsAbiertos;
var tiketsEnCurso;
var ticketsResueltos;

function obtenerTicketsAbiertos(url) {
    url_metodo = url;
    console.log(url);
    //Método ajax para traer las marcas de la base de datos
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            console.log(data);
            ticketsAbiertos = data;
            contarAbiertos();
            cargarTablaAbiertos();
            $('#dataTableAbiertos').DataTable({
                "language": {
                    "url": url_idioma
                }
            });
        }
    });
}

function contarAbiertos() {
    var contAbiertos = 0;
    for (var i = 0; i < ticketsAbiertos.length; i++) {
        if (ticketsAbiertos[i].EstadoTicket == 'Abierto') {
            contAbiertos += 1;
        }
    }
    $('#numeroAbiertos').html(contAbiertos).show();
}

//Función para cargar la tabla de Usuarios
function cargarTablaAbiertos() {
    var str = '<table id="dataTableAbiertos" class="table jambo_table bulk_action  table-bordered" style="width:100%">';
    str += '<thead> <tr> <th>Descripción del Incidente</th> <th>Prioridad</th> <th>Fecha de Apertura</th> <th>Reportado por:</th> <th>Modificar Estado</th> </tr> </thead>';
    str += '<tbody>';

    for (var i = 0; i < ticketsAbiertos.length; i++) {

        str += '<tr><td>' + ticketsAbiertos[i].DescripcionTicket +
            '</td><td>' + ticketsAbiertos[i].PrioridadTicket +
            '</td><td>' + ticketsAbiertos[i].FechaAperturaTicket +
            '</td><td>' + ticketsAbiertos[i].NombreUsuario;
        str += '</td><td><div class="text-center"><div class="col-md-12 col-sm-12 col-xs-12">' +
            '<button type="button" class="btn btn-info text-center" data-toggle="modal" data-target="#ModificarTickets" onclick = "formUpdateTipoAct(' + ticketsAbiertos[i].IdTicket + ');"> <strong><i class="fa fa-pencil-square-o"></i></strong></button>' +
            '</div></div>' +
            '</td></tr>';
    };
    str += '</tbody></table>';
    $("#ticketsAbiertos").html(str);
}




