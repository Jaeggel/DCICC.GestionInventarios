var url_idioma = obtenerIdioma();
var estados = listaEstadosTicket();
var url_resuelto;
var ticketsAbiertos;
var tiketsEnCurso;
var ticketsResueltos;
var idTicketAbierto;


function obtenerTicketsAbiertos(url) {
    url_resuelto = url;
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
            cargarEstadosAbiertoCmb();
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

function cargarEstadosAbiertoCmb() {
    var str = '<select id="Estados" class="form-control" name="Estados" required>';
    str += '<option value="">Escoga una opción...</option>';
    for (var i = 0; i < estados.length; i++) {
        if (estados[i] != 'Abierto') {
            str += '<option value="' + estados[i] + '">' + estados[i] + '</option>';
        }
        
    };
    str += '</select>';
    $("#cargarAbiertosCmb").html(str);
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
            '<button type="button" class="btn btn-info text-center" data-toggle="modal" data-target="#ModificarTickets" onclick = "formUpdateActivos(' + ticketsAbiertos[i].IdTicket + ');"> <strong><i class="fa fa-pencil-square-o"></i></strong></button>' +
            '</div></div>' +
            '</td></tr>';
    };
    str += '</tbody></table>';
    $("#ticketsAbiertos").html(str);
}

//Función para setear los valores en los inputs
function formUpdateActivos(idTicket) {
    console.log(idTicket);
    idTicketAbierto = idTicket;
    for (var i = 0; i < ticketsAbiertos.length; i++) {
        
        if (ticketsAbiertos[i].IdTicket == idTicket) {
            document.getElementById("DescripcionTicket").value = ticketsAbiertos[i].DescripcionTicket ;
            document.getElementById("PrioridadTicket").value = ticketsAbiertos[i].PrioridadTicket;
            document.getElementById("FechaAperturaTicket").value = ticketsAbiertos[i].FechaAperturaTicket;
            document.getElementById("NombreUsuario").value = ticketsAbiertos[i].NombreUsuario;
        }
    };
}

//Función para modificar el Tipo de activo especificado
function modificarEstadoTicket(url_modificar) {
    console.log(url_modificar);
    var cmbEstado = document.getElementById("Estados");
    var Estado = cmbEstado.options[cmbEstado.selectedIndex].value;
    var comentario = document.getElementById("ComentarioTicket").value;

    swal({
        title: 'Confirmación de Actualización',
        text: "¿Está seguro de modificar el registro?",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#26B99A',
        cancelButtonColor: '#337ab7',
        confirmButtonText: 'Confirmar',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.value) {
            //Método ajax para modificar el usuario de la base de datos
            $.ajax({
                data: { "IdTicket": idTicketAbierto, "EstadoTicket": Estado, "ComentarioTicket": comentario },
                url: url_modificar,
                type: 'post',
                success: function () {
                    $('#ModificarTickets').modal('hide');
                    showNotify("Actualización exitosa", 'El Ticket se actualizó correctamente', "success");
                    obtenerTicketsAbiertos(url_resuelto);
                }, error: function () {
                    $('#ModificarTickets').modal('hide');
                    showNotify("Error en la Actualización", 'No se ha podido actualizar el Ticket', "error");
                }
            });

        } else {
            $('#ModificarTickets').modal('hide');
        }
    });
}
////////////////////////////////////////////TICKET EN CURSO




