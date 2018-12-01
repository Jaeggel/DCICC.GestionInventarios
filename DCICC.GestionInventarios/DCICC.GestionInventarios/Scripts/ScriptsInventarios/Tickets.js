var url_idioma = obtenerIdioma();
var estados = listaEstadosTicket();
var url_metodo;
//var url_abierto;
//var url_en_curso;
//var url_resuelto;
var ticketsReportados;
//var ticketsAbiertos;
//var ticketsEnCurso;
//var ticketsResueltos;
var idTicketAbierto;

//Método para obtener los tickets
function obtenerTickets(url) {
    url_metodo = url;
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'get',
        success: function (data) {
            console.log("Datos Exitosos");
            ticketsReportados = data;
            contarTickets();            
            cargarEstadosAbiertoCmb();
            cargarEstadosEnCursoCmb();
            cargarTablaAbiertos();
            $('#dataTableAbiertos').DataTable({
                "language": {
                    "url": url_idioma
                }
            });

            cargarTablaEnCurso();
            $('#dataTableEnCurso').DataTable({
                "language": {
                    "url": url_idioma
                }
            });

            cargarTablaResueltos();
            $('#dataTableResueltos').DataTable({
                "language": {
                    "url": url_idioma
                }
            });

        }
    });
}

//Método para obtener el número de tickets
function contarTickets() {
    var contAbiertos = 0;
    var contEnCurso = 0;
    var contResueltos = 0;
    for (var i = 0; i < ticketsReportados.length; i++) {
        if (ticketsReportados[i].EstadoTicket == 'ABIERTO') {
            contAbiertos += 1;
        }
        if (ticketsReportados[i].EstadoTicket == 'EN CURSO') {
            contEnCurso += 1;
        }
        if (ticketsReportados[i].EstadoTicket == 'RESUELTO') {
            contResueltos += 1;
        }

    }
    $('#numeroAbiertos').html(contAbiertos).show();
    $('#numeroEnCurso').html(contEnCurso).show();
    $('#numeroResueltos').html(contResueltos).show();
}

///////////////Funciones para cargar los combobox
function cargarEstadosAbiertoCmb() {
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

//Función para el combobox de estados
function cargarEstadosEnCursoCmb() {
    var str = '<select id="EstadosEC" class="form-control" name="EstadosEC" required>';
    str += '<option value="">Escoga una opción...</option>';
    for (var i = 0; i < estados.length; i++) {
        if (estados[i] == 'RESUELTO') {
            str += '<option value="' + estados[i] + '">' + estados[i] + '</option>';
        }
    };
    str += '</select>';
    $("#cargarEnCursoCmb").html(str);
}

//Función para cargar la tabla de tickets Abiertos
function cargarTablaAbiertos() {
    var str = '<table id="dataTableAbiertos" class="table jambo_table bulk_action  table-bordered" style="width:100%">';
    str += '<thead> <tr> <th>Descripción del Incidente</th> <th>Laboratorio o Activo</th> <th>Prioridad</th> <th>Fecha de Apertura</th> <th>Reportado por:</th> <th>Modificar Estado</th> </tr> </thead>';
    str += '<tbody>';

    for (var i = 0; i < ticketsReportados.length; i++) {
        if (ticketsReportados[i].EstadoTicket == 'ABIERTO') {
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
            str += '</td><td><div class="text-center"><div class="col-md-12 col-sm-12 col-xs-12">' +
                '<button type="button" class="btn btn-info text-center" data-toggle="modal" data-target="#ModificarTickets" onclick = "formUpdateAbiertos(' + ticketsReportados[i].IdTicket + ');"> <strong><i class="fa fa-pencil-square-o"></i></strong></button>' +
                '</div></div>' +
                '</td></tr>';
        }       
    };
    str += '</tbody></table>';
    $("#ticketsAbiertos").html(str);
}

//Función para cargar la tabla de tickets en cruso
function cargarTablaEnCurso() {
    var str = '<table id="dataTableEnCurso" class="table jambo_table bulk_action  table-bordered" style="width:100%">';
    str += '<thead> <tr> <th>Descripción del Incidente</th> <th>Laboratorio o Activo</th> <th>Prioridad</th> <th>Fecha de Apertura</th> <th>Reportado por:</th> <th>Comentario</th> <th>Modificar Estado</th> </tr> </thead>';
    str += '<tbody>';

    for (var i = 0; i < ticketsReportados.length; i++) {
        if (ticketsReportados[i].EstadoTicket == 'EN CURSO') {
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
                '</td><td>' + ticketsReportados[i].NombreUsuario +
                '</td><td>' + ticketsReportados[i].ComentarioTicket;
            str += '</td><td><div class="text-center"><div class="col-md-12 col-sm-12 col-xs-12">' +
                '<button type="button" class="btn btn-info text-center" data-toggle="modal" data-target="#ModificarTicketsEnCurso" onclick = "formUpdateEnCurso(' + ticketsReportados[i].IdTicket + ');"> <strong><i class="fa fa-pencil-square-o"></i></strong></button>' +
                '</div></div>' +
                '</td></tr>';
        }   
    };
    str += '</tbody></table>';
    $("#ticketsEnCurso").html(str);
}

//Función para cargar la tabla de Usuarios
function cargarTablaResueltos() {
    var str = '<table id="dataTableResueltos" class="table jambo_table bulk_action table-bordered" style="width:100%">';
    str += '<thead> <tr> <th>Descripción del Incidente</th> <th>Laboratorio o Activo</th> <th>Prioridad</th> <th>Fecha de Apertura</th> <th>Reportado por:</th> <th>Fecha de Resolución</th> <th>Resuelto por:</th> <th>Comentario</th> </tr> </thead>';
    str += '<tbody>';

    for (var i = 0; i < ticketsReportados.length; i++) {
        if (ticketsReportados[i].EstadoTicket == 'RESUELTO') {
            //Función para dar formato a la fecha
            var fechaAper = new Date(parseInt((ticketsReportados[i].FechaAperturaTicket).substr(6)));
            var fechaApertura = (fechaAper.toLocaleDateString("es-ES") + " " + fechaAper.getHours() + ":" + fechaAper.getMinutes() + ":" + fechaAper.getSeconds());

            var fechaSol = new Date(parseInt((ticketsReportados[i].FechaSolucionTicket).substr(6)));
            var fechaSolucion = (fechaSol.toLocaleDateString("es-ES") + " " + fechaSol.getHours() + ":" + fechaSol.getMinutes() + ":" + fechaSol.getSeconds());

            str += '<tr><td>' + ticketsReportados[i].DescripcionTicket;
            if (ticketsReportados[i].IdLaboratorio != 0) {
                str += '</td><td> <strong>Laboratorio:</strong> ' + ticketsReportados[i].NombreLaboratorio;
            } else {
                str += '</td><td><strong> Activo:</strong> ' + ticketsReportados[i].NombreDetalleActivo;
            }
            str += '</td><td>' + ticketsReportados[i].PrioridadTicket +
                '</td><td>' + fechaApertura +
                '</td><td>' + ticketsReportados[i].NombreUsuario +
                '</td><td>' + fechaSolucion +
                '</td><td>' + ticketsReportados[i].NombreUsuarioResponsable +
                '</td><td>' + ticketsReportados[i].ComentarioTicket +
                '</td></tr>';
        }  
    };
    str += '</tbody></table>';
    $("#ticketsResueltos").html(str);
}

//Función para setear los valores en los inputs de tickets abiertos
function formUpdateAbiertos(idTicket) {
    console.log(idTicket);
    idTicketAbierto = idTicket;
    for (var i = 0; i < ticketsReportados.length; i++) {
        if (ticketsReportados[i].IdTicket == idTicket) {
            //Función para dar formato a la fecha
            var fechaAper = new Date(parseInt((ticketsReportados[i].FechaAperturaTicket).substr(6)));
            var fechaApertura = (fechaAper.toLocaleDateString("es-ES") + " " + fechaAper.getHours() + ":" + fechaAper.getMinutes() + ":" + fechaAper.getSeconds());
            document.getElementById("DescripcionTicket").value = ticketsReportados[i].DescripcionTicket;
            document.getElementById("PrioridadTicket").value = ticketsReportados[i].PrioridadTicket;
            document.getElementById("FechaAperturaTicket").value = fechaApertura;
            document.getElementById("NombreUsuario").value = ticketsReportados[i].NombreUsuario;
        }
    };
}

//Función para modificar el estado de un ticket activo
function modificarEstadoTicket(url_modificar) {
    var nick = document.getElementById("usuarioActual").innerHTML;
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
            $.ajax({
                data: { "IdTicket": idTicketAbierto, "EstadoTicket": Estado, "ComentarioTicket": comentario, "NombreUsuarioResponsable": nick },
                url: url_modificar,
                type: 'post',
                success: function (data) {
                    console.log(data.OperacionExitosa);
                    if (data.OperacionExitosa) {
                        $('#ModificarTickets').modal('hide');
                        showNotify("Actualización exitosa", 'El Ticket cambió de estado', "success");
                        obtenerTickets(url_metodo);
                    } else {
                        $('#ModificarTickets').modal('hide');
                        showNotify("Error en la Actualización", 'No se ha podido actualizar el Ticket: ' + data.MensajeError, "error");
                    }
                    
                }
            });

        } else {
            $('#ModificarTickets').modal('hide');
        }
    });
}

//Función para setear los valores en los inputs de tickets en curso
function formUpdateEnCurso(idTicket) {
    console.log(idTicket);
    idTicketAbierto = idTicket;
    for (var i = 0; i < ticketsReportados.length; i++) {
        if (ticketsReportados[i].IdTicket == idTicket) {
            //Función para dar formato a la fecha
            var fechaAper = new Date(parseInt((ticketsReportados[i].FechaAperturaTicket).substr(6)));
            var fechaApertura = (fechaAper.toLocaleDateString("es-ES") + " " + fechaAper.getHours() + ":" + fechaAper.getMinutes() + ":" + fechaAper.getSeconds());
            document.getElementById("DescripcionTicketEC").value = ticketsReportados[i].DescripcionTicket;
            document.getElementById("PrioridadTicketEC").value = ticketsReportados[i].PrioridadTicket;
            document.getElementById("FechaAperturaTicketEC").value = fechaApertura;
            document.getElementById("NombreUsuarioEC").value = ticketsReportados[i].NombreUsuario;
            document.getElementById("ComentarioTicketEC").value = ticketsReportados[i].ComentarioTicket;
        }
    };
}

//Función para modificar tickets en curso
function modificarEstadoTicketEnCurso(url_modificar) {
    var nick = document.getElementById("usuarioActual").innerHTML;
    var cmbEstado = document.getElementById("EstadosEC");
    var Estado = cmbEstado.options[cmbEstado.selectedIndex].value;
    var comentario = document.getElementById("ComentarioTicketEC").value;

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
            $.ajax({
                data: { "IdTicket": idTicketAbierto, "EstadoTicket": Estado, "ComentarioTicket": comentario, "NombreUsuarioResponsable": nick },
                url: url_modificar,
                type: 'post',
                success: function (data) {
                    if (data.OperacionExitosa) {
                        $('#ModificarTicketsEnCurso').modal('hide');
                        showNotify("Actualización exitosa", 'El Ticket cambió a estado Resuelto', "success");
                        obtenerTickets(url_metodo);            
                    } else {
                        $('#ModificarTicketsEnCurso').modal('hide');
                        showNotify("Error en la Actualización", 'No se ha podido actualizar el Ticket: ' + data.MensajeError, "error");
                    }
                }
            });

        } else {
            $('#ModificarTicketsEnCurso').modal('hide');
        }
    });
}


