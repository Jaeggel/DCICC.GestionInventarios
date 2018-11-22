var url_idioma = obtenerIdioma();
var estados = listaEstadosTicket();
var url_abierto;
var url_en_curso;
var url_resuelto;
var ticketsAbiertos;
var ticketsEnCurso;
var ticketsResueltos;
var idTicketAbierto;

//Método ajax para obtener los tickets con estado abierto
function obtenerTicketsAbiertos(url) {
    url_abierto = url;
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'get',
        success: function (data) {
            console.log("Datos Exitosos");
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

//Método ajax para obtener los tickets con estado en curso
function obtenerTicketsEnCurso(url) {
    url_en_curso = url;
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'get',
        success: function (data) {
            console.log("Datos correctos");
            ticketsEnCurso = data;
            contarEnCurso();
            cargarTablaEnCurso();
            cargarEstadosEnCursoCmb();
            $('#dataTableEnCurso').DataTable({
                "language": {
                    "url": url_idioma
                }
            });
        }
    });
}

//Método ajax para obtener los tickets con estado Resuelto
function obtenerTicketsResueltos(url) {
    url_resuelto = url;
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'get',
        success: function (data) {
            console.log("Datos Correctos");
            ticketsResueltos = data;
            contarResueltos();
            cargarTablaResueltos();
            $('#dataTableResueltos').DataTable({
                "language": {
                    "url": url_idioma
                }
            });
        }
    });
}

///////////////////////////////////////////////SECCIÓN PARA TICKETS ABIERTOS////////////////////////////////

//función para conocer el número de tickets abiertos
function contarAbiertos() {
    var contAbiertos = 0;
    for (var i = 0; i < ticketsAbiertos.length; i++) {
        if (ticketsAbiertos[i].EstadoTicket == 'Abierto') {
            contAbiertos += 1;
        }
    }
    $('#numeroAbiertos').html(contAbiertos).show();
}

//Función para cargar el combobox de la sección de tickets Abiertos
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

//Función para cargar la tabla de tickets Abiertos
function cargarTablaAbiertos() {
    var str = '<table id="dataTableAbiertos" class="table jambo_table bulk_action  table-bordered" style="width:100%">';
    str += '<thead> <tr> <th>Descripción del Incidente</th> <th>Laboratorio o Activo</th> <th>Prioridad</th> <th>Fecha de Apertura</th> <th>Reportado por:</th> <th>Modificar Estado</th> </tr> </thead>';
    str += '<tbody>';

    for (var i = 0; i < ticketsAbiertos.length; i++) {
        //Función para dar formato a la fecha
        var fechaAper = new Date(parseInt((ticketsAbiertos[i].FechaAperturaTicket).substr(6)));
        var fechaApertura = (fechaAper.toLocaleDateString("es-ES") + " " + fechaAper.getHours() + ":" + fechaAper.getMinutes() + ":" + fechaAper.getSeconds());
        
        str += '<tr><td>' + ticketsAbiertos[i].DescripcionTicket;
        if (ticketsAbiertos[i].IdLaboratorio != 0) {
            str += '</td><td> <strong>Laboratorio:</strong> ' + ticketsAbiertos[i].NombreLaboratorio;
        } else {
            str += '</td><td><strong> Activo:</strong> ' + ticketsAbiertos[i].NombreDetalleActivo;
        }

        str += '</td><td>' + ticketsAbiertos[i].PrioridadTicket +
            '</td><td>' + fechaApertura +
            '</td><td>' + ticketsAbiertos[i].NombreUsuario;
        str += '</td><td><div class="text-center"><div class="col-md-12 col-sm-12 col-xs-12">' +
            '<button type="button" class="btn btn-info text-center" data-toggle="modal" data-target="#ModificarTickets" onclick = "formUpdateAbiertos(' + ticketsAbiertos[i].IdTicket + ');"> <strong><i class="fa fa-pencil-square-o"></i></strong></button>' +
            '</div></div>' +
            '</td></tr>';
    };
    str += '</tbody></table>';
    $("#ticketsAbiertos").html(str);
}

//Función para setear los valores en los inputs
function formUpdateAbiertos(idTicket) {
    console.log(idTicket);
    idTicketAbierto = idTicket;
    for (var i = 0; i < ticketsAbiertos.length; i++) {
        if (ticketsAbiertos[i].IdTicket == idTicket) {
            //Función para dar formato a la fecha
            var fechaAper = new Date(parseInt((ticketsAbiertos[i].FechaAperturaTicket).substr(6)));
            var fechaApertura = (fechaAper.toLocaleDateString("es-ES") + " " + fechaAper.getHours() + ":" + fechaAper.getMinutes() + ":" + fechaAper.getSeconds());
            document.getElementById("DescripcionTicket").value = ticketsAbiertos[i].DescripcionTicket ;
            document.getElementById("PrioridadTicket").value = ticketsAbiertos[i].PrioridadTicket;
            document.getElementById("FechaAperturaTicket").value = fechaApertura;
            document.getElementById("NombreUsuario").value = ticketsAbiertos[i].NombreUsuario;
        }
    };
}

//Función para modificar el estado de un ticket
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
                success: function () {
                    $('#ModificarTickets').modal('hide');
                    showNotify("Actualización exitosa", 'El Ticket se actualizó correctamente', "success");
                    obtenerTicketsAbiertos(url_abierto);
                    obtenerTicketsEnCurso(url_en_curso);
                    obtenerTicketsResueltos(url_resuelto);
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


//////////////////////////////////////////////SECCIÓN DE TICKET EN CURSO//////////////////////////////////////////////

//Función para conocer el número de tickets en curso
function contarEnCurso() {
    var contEnCurso = 0;
    for (var i = 0; i < ticketsEnCurso.length; i++) {
        if (ticketsEnCurso[i].EstadoTicket == 'En Curso') {
            contEnCurso += 1;
        }
    }
    $('#numeroEnCurso').html(contEnCurso).show();
}

//Función para el combobox de estados
function cargarEstadosEnCursoCmb() {
    var str = '<select id="EstadosEC" class="form-control" name="EstadosEC" required>';
    str += '<option value="">Escoga una opción...</option>';
    for (var i = 0; i < estados.length; i++) {
        if ( estados[i] == 'Resuelto') {
            str += '<option value="' + estados[i] + '">' + estados[i] + '</option>';
        }

    };
    str += '</select>';
    $("#cargarEnCursoCmb").html(str);
}

//Función para cargar la tabla de tickets en cruso
function cargarTablaEnCurso() {
    var str = '<table id="dataTableEnCurso" class="table jambo_table bulk_action  table-bordered" style="width:100%">';
    str += '<thead> <tr> <th>Descripción del Incidente</th> <th>Laboratorio o Activo</th> <th>Prioridad</th> <th>Fecha de Apertura</th> <th>Reportado por:</th> <th>Comentario</th> <th>Modificar Estado</th> </tr> </thead>';
    str += '<tbody>';

    for (var i = 0; i < ticketsEnCurso.length; i++) {
        //Función para dar formato a la fecha
        var fechaAper = new Date(parseInt((ticketsEnCurso[i].FechaAperturaTicket).substr(6)));
        var fechaApertura = (fechaAper.toLocaleDateString("es-ES") + " " + fechaAper.getHours() + ":" + fechaAper.getMinutes() + ":" + fechaAper.getSeconds());

        str += '<tr><td>' + ticketsEnCurso[i].DescripcionTicket;
        if (ticketsEnCurso[i].IdLaboratorio != 0) {
            str += '</td><td> <strong>Laboratorio:</strong> ' + ticketsEnCurso[i].NombreLaboratorio;
        } else {
            str += '</td><td><strong> Activo:</strong> ' + ticketsEnCurso[i].NombreDetalleActivo;
        }
        str += '</td><td>' + ticketsEnCurso[i].PrioridadTicket +
            '</td><td>' + fechaApertura +
            '</td><td>' + ticketsEnCurso[i].NombreUsuario +
            '</td><td>' + ticketsEnCurso[i].ComentarioTicket;
        str += '</td><td><div class="text-center"><div class="col-md-12 col-sm-12 col-xs-12">' +
            '<button type="button" class="btn btn-info text-center" data-toggle="modal" data-target="#ModificarTicketsEnCurso" onclick = "formUpdateEnCurso(' + ticketsEnCurso[i].IdTicket + ');"> <strong><i class="fa fa-pencil-square-o"></i></strong></button>' +
            '</div></div>' +
            '</td></tr>';
    };
    str += '</tbody></table>';
    $("#ticketsEnCurso").html(str);
}

//Función para setear los valores en los inputs
function formUpdateEnCurso(idTicket) {
    console.log(idTicket);
    idTicketAbierto = idTicket;
    for (var i = 0; i < ticketsEnCurso.length; i++) {
        if (ticketsEnCurso[i].IdTicket == idTicket) {
            //Función para dar formato a la fecha
            var fechaAper = new Date(parseInt((ticketsEnCurso[i].FechaAperturaTicket).substr(6)));
            var fechaApertura = (fechaAper.toLocaleDateString("es-ES") + " " + fechaAper.getHours() + ":" + fechaAper.getMinutes() + ":" + fechaAper.getSeconds());
            document.getElementById("DescripcionTicketEC").value = ticketsEnCurso[i].DescripcionTicket;
            document.getElementById("PrioridadTicketEC").value = ticketsEnCurso[i].PrioridadTicket;
            document.getElementById("FechaAperturaTicketEC").value = fechaApertura;
            document.getElementById("NombreUsuarioEC").value = ticketsEnCurso[i].NombreUsuario;
            document.getElementById("ComentarioTicketEC").value = ticketsEnCurso[i].ComentarioTicket;
        }
    };
}

//Función para modificar tickets 
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
                success: function () {
                    $('#ModificarTicketsEnCurso').modal('hide');
                    showNotify("Actualización exitosa", 'El Ticket se actualizó correctamente', "success");
                    obtenerTicketsAbiertos(url_abierto);
                    obtenerTicketsEnCurso(url_en_curso);
                    obtenerTicketsResueltos(url_resuelto);
                }, error: function () {
                    $('#ModificarTicketsEnCurso').modal('hide');
                    showNotify("Error en la Actualización", 'No se ha podido actualizar el Ticket', "error");
                }
            });

        } else {
            $('#ModificarTicketsEnCurso').modal('hide');
        }
    });
}


///////////////////////////////////////SECCIÓN TICKETS RESUELTOS/////////////////////////////////////

//Función para conocer el número de tickets resueltos
function contarResueltos() {
    var contResueltos = 0;
    for (var i = 0; i < ticketsResueltos.length; i++) {
        if (ticketsResueltos[i].EstadoTicket == 'Resuelto') {
            contResueltos += 1;
        }
    }
    $('#numeroResueltos').html(contResueltos).show();
}

//Función para cargar la tabla de Usuarios
function cargarTablaResueltos() {
    var str = '<table id="dataTableResueltos" class="table jambo_table bulk_action  table-bordered" style="width:100%">';
    str += '<thead> <tr> <th>Descripción del Incidente</th> <th>Laboratorio o Activo</th> <th>Prioridad</th> <th>Fecha de Apertura</th> <th>Reportado por:</th> <th>Fecha de Resolución</th> <th>Resuelto por:</th> <th>Comentario</th> </tr> </thead>';
    str += '<tbody>';

    for (var i = 0; i < ticketsResueltos.length; i++) {
        //Función para dar formato a la fecha
        var fechaAper = new Date(parseInt((ticketsResueltos[i].FechaAperturaTicket).substr(6)));
        var fechaApertura = (fechaAper.toLocaleDateString("es-ES") + " " + fechaAper.getHours() + ":" + fechaAper.getMinutes() + ":" + fechaAper.getSeconds());

        var fechaSol = new Date(parseInt((ticketsResueltos[i].FechaSolucionTicket).substr(6)));
        var fechaSolucion = (fechaSol.toLocaleDateString("es-ES") + " " + fechaSol.getHours() + ":" + fechaSol.getMinutes() + ":" + fechaSol.getSeconds());

        str += '<tr><td>' + ticketsResueltos[i].DescripcionTicket;
        if (ticketsResueltos[i].IdLaboratorio != 0) {
            str += '</td><td> <strong>Laboratorio:</strong> ' + ticketsResueltos[i].NombreLaboratorio;
        } else {
            str += '</td><td><strong> Activo:</strong> ' + ticketsResueltos[i].NombreDetalleActivo;
        }
           str+= '</td><td>' + ticketsResueltos[i].PrioridadTicket +
               '</td><td>' + fechaApertura +
            '</td><td>' + ticketsResueltos[i].NombreUsuario +
               '</td><td>' + fechaSolucion +
               '</td><td>' + ticketsResueltos[i].NombreUsuarioResponsable +
               '</td><td>' + ticketsResueltos[i].ComentarioTicket +
            '</td></tr>';
    };
    str += '</tbody></table>';
    $("#ticketsResueltos").html(str);
}
