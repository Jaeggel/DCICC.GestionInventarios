var url_idioma = obtenerIdioma();
var estados = listaEstadosTicket();
var url_metodo;
var ticketsReportados;
var idTicketAbierto;
var responsables;
var idResponsable;

/**
 * *********************************************************************************
 *                SECCIÓN PARA OPERACIONES DE TICKETS ABIERTOS
 * *********************************************************************************
 */
/* --------------------------------------SECCIÓN PARA OBTENER DATOS DEL SERVIDOR---------------------------------*/
//Método para obtener los tickets
function obtenerTickets(url) {
    url_metodo = url;
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            if (data.OperacionExitosa) {
                console.log("Datos Exitosos");
                ticketsReportados = data.ListaObjetoInventarios;
                contarTickets();
                cargarEstadosAbiertoCmb();
                cargarEstadosEnCursoCmb();
                cargarEstadosEnEsperaCmb();
                cargarTablaAbiertos();
                $('#dataTableAbiertos').DataTable({
                    "language": {
                        "url": url_idioma
                    },
                    "aaSorting": []
                });

                cargarTablaEnCurso();
                $('#dataTableEnCurso').DataTable({
                    "language": {
                        "url": url_idioma
                    }
                });

                cargarTablaEnEspera();
                $('#dataTableEnEspera').DataTable({
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
            } else {
                showNotify("Error en la Consulta", 'No se ha podido mostrar los datos: ' + data.MensajeError, "error");
            }
        }
    });
}

//Método para obtener el número de tickets
function contarTickets() {
    var contAbiertos = 0;
    var contEnCurso = 0;
    var contEnEspera = 0;
    var contResueltos = 0;
    for (var i = 0; i < ticketsReportados.length; i++) {
        if (ticketsReportados[i].EstadoTicket == 'ABIERTO') {
            contAbiertos += 1;
        }
        if (ticketsReportados[i].EstadoTicket == 'EN PROCESO') {
            contEnCurso += 1;
        }
        if (ticketsReportados[i].EstadoTicket == 'EN ESPERA') {
            contEnEspera += 1;
        }
        if (ticketsReportados[i].EstadoTicket == 'RESUELTO') {
            contResueltos += 1;
        }
    }
    $('#numeroAbiertos').html(contAbiertos).show();
    $('#numeroEnCurso').html(contEnCurso).show();
    $('#numeroEnEspera').html(contEnEspera).show();
    $('#numeroResueltos').html(contResueltos).show();
}

//Método ajax para obtener los datos de los usuarios administradores y pasantes
function datosResponsables(url) {
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            responsables = data.ListaObjetoInventarios;
            cargarResponsablesCmb();
        }
    });
}

/* --------------------------------------SECCIÓN PARA CARGAR TABLAS Y COMBOBOX---------------------------------*/
//Funciones para cargar los combobox de estados abierto
function cargarEstadosAbiertoCmb() {
    var str = '<select id="Estados" class="form-control" name="Estados" required>';
    str += '<option value="">Escoga una opción...</option>';
    for (var i = 0; i < estados.length; i++) {
        if (estados[i] != 'ABIERTO') {
            str += '<option value="' + estados[i] + '">' + estados[i] + '</option>';
        }
    }
    str += '</select>';
    $("#cargarAbiertosCmb").html(str);
}

//Funciones para cargar los combobox de estados abierto
function cargarResponsablesCmb() {
    var str = '<select id="Responsables" class="form-control" name="Responsables" data-toggle="tooltip" data-placement="rigth" title="Seleccione el responsable que se hará cargo del Ticket" required>';
    str += '<option value="">Escoga una opción...</option>';
    for (var i = 0; i < responsables.length; i++) {
        str += '<option value="' + responsables[i].IdUsuario + '">' + responsables[i].NombresUsuario + '</option>';
    }
    str += '</select>';
    $("#cargarResponsablesCmb").html(str);
}

//Función para cargar la tabla de tickets Abiertos
function cargarTablaAbiertos() {
    var str = '<table id="dataTableAbiertos" class="table jambo_table bulk_action  table-bordered" style="width:100%">';
    str += '<thead> <tr> <th>Descripción del Incidente</th> <th>Laboratorio o Activo</th> <th>Prioridad</th> <th>Fecha de Apertura</th> <th>Usuario Solicitante</th> <th>Gestionar Ticket</th> </tr> </thead>';
    str += '<tbody>';

    for (var i = 0; i < ticketsReportados.length; i++) {
        if (ticketsReportados[i].EstadoTicket == 'ABIERTO') {
            //Función para dar formato a la fecha
            var fechaAper = new Date(parseInt((ticketsReportados[i].FechaAperturaTicket).substr(6)));
            var fechaApertura = (fechaAper.toLocaleDateString("es-ES") + " " + fechaAper.getHours() + ":" + fechaAper.getMinutes() );

            str += '<tr><td class="text-justify">' + ticketsReportados[i].DescripcionTicket;
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
    }
    str += '</tbody></table>';
    $("#ticketsAbiertos").html(str);
}

//Función para setear los valores en los inputs de tickets abiertos
function formUpdateAbiertos(idTicket) {
    idTicketAbierto = idTicket;
    for (var i = 0; i < ticketsReportados.length; i++) {
        if (ticketsReportados[i].IdTicket == idTicket) {
            //Función para dar formato a la fecha
            var fechaAper = new Date(parseInt((ticketsReportados[i].FechaAperturaTicket).substr(6)));
            var fechaApertura = (fechaAper.toLocaleDateString("es-ES") + " " + fechaAper.getHours() + ":" + fechaAper.getMinutes());
            document.getElementById("DescripcionTicket").value = ticketsReportados[i].DescripcionTicket;
            document.getElementById("PrioridadTicket").value = ticketsReportados[i].PrioridadTicket;
            document.getElementById("FechaAperturaTicket").value = fechaApertura;
            document.getElementById("NombreUsuario").value = ticketsReportados[i].NombreUsuario;
        }
    }
}

//Función para modificar el estado de un ticket activo
function modificarEstadoTicket(url_modificar) {
    var cmbResponsable = document.getElementById("Responsables");
    var responsable = cmbResponsable.options[cmbResponsable.selectedIndex].value;
    var asignado = cmbResponsable.options[cmbResponsable.selectedIndex].text;
    var cmbEstado = document.getElementById("Estados");
    var Estado = cmbEstado.options[cmbEstado.selectedIndex].value;
    var comentario = document.getElementById("ComentarioTicket").value;

    if (validarCmbAbierto()) {
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
                    data: { "IdTicket": idTicketAbierto, "IdResponsableUsuario": responsable, "EstadoTicket": Estado, "ComentarioTicket": comentario },
                    url: url_modificar,
                    type: 'post',
                    success: function (data) {
                        console.log(data.OperacionExitosa);
                        if (data.OperacionExitosa) {
                            $('#ModificarTickets').modal('hide');
                            showNotify("Actualización exitosa", 'El Ticket se asignó a: "' + asignado.toUpperCase() + '" y cambió de Estado a: ' + Estado, "success");
                            obtenerTickets(url_metodo);
                        } else {
                            $('#ModificarTickets').modal('hide');
                            showNotify("Error en la Actualización", 'Ocurrió un error al modificar el estado del Ticket: ' + data.MensajeError, "error");
                        }

                    }
                });

            } else {
                $('#ModificarTickets').modal('hide');
            }
        });
    }
}

///Función para validar el combobox de Estado Abierto
function validarCmbAbierto() {
    var esValido = true;
    var cmbCat = document.getElementById("Estados");
    var cmbRes = document.getElementById("Responsables");
    //Validación para el combobox de categorias
    if (cmbCat.value == "") {
        esValido = false;
        cmbCat.style.borderColor = "#900C3F";
        $('#errorAbiertos').html('Debe seleccionar una opción').show();
        setTimeout("$('#errorAbiertos').html('').hide('slow')", 6000);
    } else {
        cmbCat.style.borderColor = "#ccc";
        $('#errorAbiertos').html('').hide();
    }

    if (cmbRes.value == "") {
        esValido = false;
        cmbRes.style.borderColor = "#900C3F";
        $('#errorResponsableAbi').html('Debe seleccionar una opción').show();
        setTimeout("$('#errorResponsableAbi').html('').hide('slow')", 6000);
    } else {
        cmbRes.style.borderColor = "#ccc";
        $('#errorResponsableAbi').html('').hide();
    }
    return esValido;
}

/* --------------------------------------SECCIÓN PARA MENSAJES DE TOOLTIPS---------------------------------*/

//Mensajes para los tooltips
function mensajesTooltipsAbiertos() {
    //document.getElementById("Responsables").title = "Seleccione el responsable que se hará cargo del Ticket";
    document.getElementById("ComentarioTicket").title = "Máximo 300 caracteres.\n Caracteres especiales permitidos - / _ .";

}

/**
 * *********************************************************************************
 *                SECCIÓN PARA OPERACIONES DE TICKETS EN PROCESO
 * *********************************************************************************
 */

//Función para cargar la tabla de tickets en curso
function cargarTablaEnCurso() {
    var str = '<table id="dataTableEnCurso" class="table jambo_table bulk_action  table-bordered" style="width:100%">';
    str += '<thead> <tr> <th>Fecha de Apertura</th> <th>Usuario Solicitante</th> <th>Descripción del Incidente</th> <th>Laboratorio o Activo</th> <th>Prioridad</th> <th>Responsable Asignado</th> <th>Fecha de Asignación</th> <th>Comentario En Proceso</th> <th>Gestionar Ticket</th> </tr> </thead>';
    str += '<tbody>';

    for (var i = 0; i < ticketsReportados.length; i++) {
        if (ticketsReportados[i].EstadoTicket == 'EN PROCESO') {
            //Función para dar formato a la fecha
            var fechaAper = new Date(parseInt((ticketsReportados[i].FechaAperturaTicket).substr(6)));
            var fechaApertura = (fechaAper.toLocaleDateString("es-ES") + " " + fechaAper.getHours() + ":" + fechaAper.getMinutes() );

            var fechaEC = new Date(parseInt((ticketsReportados[i].FechaEnProcesoTicket).substr(6)));
            var fechaEnC = (fechaEC.toLocaleDateString("es-ES") + " " + fechaEC.getHours() + ":" + fechaEC.getMinutes() );

            str += '<tr><td>' + fechaApertura +
                '</td><td>' + ticketsReportados[i].NombreUsuario +
                '</td><td class="text-justify">' + ticketsReportados[i].DescripcionTicket;
            if (ticketsReportados[i].IdLaboratorio != 0) {
                str += '</td><td> <strong>Laboratorio:</strong> ' + ticketsReportados[i].NombreLaboratorio;
            } else {
                str += '</td><td><strong> Activo:</strong> ' + ticketsReportados[i].NombreDetalleActivo;
            }
            str += '</td><td>' + ticketsReportados[i].PrioridadTicket +
                '</td><td>' + ticketsReportados[i].NombreUsuarioResponsable +
                '</td><td>' + fechaEnC +
                '</td><td class="text-justify">' + ticketsReportados[i].ComentarioEnProcesoTicket;
            str += '</td><td><div class="text-center"><div class="col-md-12 col-sm-12 col-xs-12">' +
                '<button type="button" class="btn btn-info text-center" data-toggle="modal" data-target="#ModificarTicketsEnCurso" onclick = "formUpdateEnCurso(' + ticketsReportados[i].IdTicket + ');"> <strong><i class="fa fa-pencil-square-o"></i></strong></button>' +
                '</div></div>' +
                '</td></tr>';
        }   
    }
    str += '</tbody></table>';
    $("#ticketsEnCurso").html(str);
}

//Función para el combobox de estados
function cargarEstadosEnCursoCmb() {
    var str = '<select id="EstadosEC" class="form-control" name="EstadosEC" required>';
    str += '<option value="">Escoga una opción...</option>';
    for (var i = 0; i < estados.length; i++) {
        if (estados[i] != 'ABIERTO' && estados[i] != 'EN PROCESO') {
            str += '<option value="' + estados[i] + '">' + estados[i] + '</option>';
        }
    }
    str += '</select>';
    $("#cargarEnCursoCmb").html(str);
}

//Función para setear los valores en los inputs de tickets en curso
function formUpdateEnCurso(idTicket) {
    idTicketAbierto = idTicket;
    for (var i = 0; i < ticketsReportados.length; i++) {
        if (ticketsReportados[i].IdTicket == idTicket) {
            //Función para dar formato a la fecha
            var fechaAper = new Date(parseInt((ticketsReportados[i].FechaAperturaTicket).substr(6)));
            var fechaApertura = (fechaAper.toLocaleDateString("es-ES") + " " + fechaAper.getHours() + ":" + fechaAper.getMinutes());
            var fechaEC = new Date(parseInt((ticketsReportados[i].FechaEnProcesoTicket).substr(6)));
            var fechaEnC = (fechaEC.toLocaleDateString("es-ES") + " " + fechaEC.getHours() + ":" + fechaEC.getMinutes());
            document.getElementById("DescripcionTicketEC").value = ticketsReportados[i].DescripcionTicket;
            document.getElementById("PrioridadTicketEC").value = ticketsReportados[i].PrioridadTicket;
            document.getElementById("FechaAperturaTicketEC").value = fechaApertura;
            document.getElementById("NombreUsuarioEC").value = ticketsReportados[i].NombreUsuario;
            document.getElementById("ResponsableAsignadoEC").value = ticketsReportados[i].NombreUsuarioResponsable;
            document.getElementById("FechaAsignadoEC").value = fechaEnC;
            idResponsable = ticketsReportados[i].IdResponsableUsuario;
        }
    }
}

//Función para modificar tickets en curso
function modificarEstadoTicketEnCurso(url_modificar) {
    var cmbEstado = document.getElementById("EstadosEC");
    var Estado = cmbEstado.options[cmbEstado.selectedIndex].value;
    var comentario = document.getElementById("ComentarioTicketEC").value;

    if (validarCmbCurso()) {
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
                    data: { "IdTicket": idTicketAbierto, "EstadoTicket": Estado, "ComentarioTicket": comentario, "IdResponsableUsuario": idResponsable },
                    url: url_modificar,
                    type: 'post',
                    success: function (data) {
                        if (data.OperacionExitosa) {
                            $('#ModificarTicketsEnCurso').modal('hide');
                            showNotify("Actualización exitosa", 'El Ticket cambió de Estado a: ' + Estado, "success");
                            obtenerTickets(url_metodo);
                        } else {
                            $('#ModificarTicketsEnCurso').modal('hide');
                            showNotify("Error en la Actualización", 'Ocurrió un error al modificar el estado del Ticket: ' + data.MensajeError, "error");
                        }
                    }
                });

            } else {
                $('#ModificarTicketsEnCurso').modal('hide');
            }
        });
    }

}

///Función para validar el combobox de Estado En curso
function validarCmbCurso() {
    var esValido = true;
    var cmbCat = document.getElementById("EstadosEC");
    //Validación para el combobox de categorias
    if (cmbCat.value == "") {
        esValido = false;
        cmbCat.style.borderColor = "#900C3F";
        $('#errorEnCurso').html('Debe seleccionar una opción').show();
        setTimeout("$('#errorEnCurso').html('').hide('slow')", 6000);
    } else {
        cmbCat.style.borderColor = "#ccc";
        $('#errorEnCurso').html('').hide();
    }

    return esValido;
}

/* --------------------------------------SECCIÓN PARA MENSAJES DE TOOLTIPS---------------------------------*/

//Mensajes para los tooltips
function mensajesTooltipsEnCurso() {
    //document.getElementById("Responsables").title = "Seleccione el responsable que se hará cargo del Ticket";
    document.getElementById("ComentarioTicketEC").title = "Máximo 300 caracteres.\n Caracteres especiales permitidos - / _ .";

}


/**
 * *********************************************************************************
 *                SECCIÓN PARA OPERACIONES DE TICKETS EN ESPERA
 * *********************************************************************************
 */

//Función para cargar la tabla de tickets en espera
function cargarTablaEnEspera() {
    var str = '<table id="dataTableEnEspera" class="table jambo_table bulk_action  table-bordered" style="width:100%">';
    str += '<thead> <tr>  <th>Fecha de Apertura</th> <th>Usuario Solicitante</th> <th>Descripción del Incidente</th> <th>Laboratorio o Activo</th> <th>Prioridad</th> <th>Responsable Asignado</th> <th>Fecha de Asignación</th> <th>Comentario En Espera</th> <th>Gestionar Ticket</th> </tr> </thead>';
    str += '<tbody>';

    for (var i = 0; i < ticketsReportados.length; i++) {
        if (ticketsReportados[i].EstadoTicket == 'EN ESPERA') {
            //Función para dar formato a la fecha
            var fechaAper = new Date(parseInt((ticketsReportados[i].FechaAperturaTicket).substr(6)));
            var fechaApertura = (fechaAper.toLocaleDateString("es-ES") + " " + fechaAper.getHours() + ":" + fechaAper.getMinutes());

            var fechaEE = new Date(parseInt((ticketsReportados[i].FechaEnEsperaTicket).substr(6)));
            var fechaEnE = (fechaEE.toLocaleDateString("es-ES") + " " + fechaEE.getHours() + ":" + fechaEE.getMinutes());

            str += '<tr><td>' + fechaApertura +
                '</td><td>' + ticketsReportados[i].NombreUsuario +
                '</td><td class="text-justify">' + ticketsReportados[i].DescripcionTicket;
            if (ticketsReportados[i].IdLaboratorio != 0) {
                str += '</td><td> <strong>Laboratorio:</strong> ' + ticketsReportados[i].NombreLaboratorio;
            } else {
                str += '</td><td><strong> Activo:</strong> ' + ticketsReportados[i].NombreDetalleActivo;
            }
            str += '</td><td>' + ticketsReportados[i].PrioridadTicket +
                '</td><td>' + ticketsReportados[i].NombreUsuarioResponsable +
                '</td><td>' + fechaEnE +
                '</td><td class="text-justify">' + ticketsReportados[i].ComentarioEnEsperaTicket;
            str += '</td><td><div class="text-center"><div class="col-md-12 col-sm-12 col-xs-12">' +
                '<button type="button" class="btn btn-info text-center" data-toggle="modal" data-target="#ModificarTicketsEnEspera" onclick = "formUpdateEnEspera(' + ticketsReportados[i].IdTicket + ');"> <strong><i class="fa fa-pencil-square-o"></i></strong></button>' +
                '</div></div>' +
                '</td></tr>';
        }
    }
    str += '</tbody></table>';
    $("#ticketsEnEspera").html(str);
}

//Función para el combobox de estados
function cargarEstadosEnEsperaCmb() {
    var str = '<select id="EstadosEnEs" class="form-control" name="EstadosEnEs" required>';
    str += '<option value="">Escoga una opción...</option>';
    for (var i = 0; i < estados.length; i++) {
        if (estados[i] == 'RESUELTO') {
            str += '<option value="' + estados[i] + '">' + estados[i] + '</option>';
        }
    }
    str += '</select>';
    $("#cargarEnEsperaCmb").html(str);
}

//Función para setear los valores en los inputs de tickets en espera
function formUpdateEnEspera(idTicket) {
    idTicketAbierto = idTicket;
    for (var i = 0; i < ticketsReportados.length; i++) {
        if (ticketsReportados[i].IdTicket == idTicket) {
            //Función para dar formato a la fecha
            var fechaAper = new Date(parseInt((ticketsReportados[i].FechaAperturaTicket).substr(6)));
            var fechaApertura = (fechaAper.toLocaleDateString("es-ES") + " " + fechaAper.getHours() + ":" + fechaAper.getMinutes());
            var fechaEE = new Date(parseInt((ticketsReportados[i].FechaEnEsperaTicket).substr(6)));
            var fechaEnE = (fechaEE.toLocaleDateString("es-ES") + " " + fechaEE.getHours() + ":" + fechaEE.getMinutes());
            document.getElementById("DescripcionTicketEnEs").value = ticketsReportados[i].DescripcionTicket;
            document.getElementById("PrioridadTicketEnEs").value = ticketsReportados[i].PrioridadTicket;
            document.getElementById("FechaAperturaTicketEnEs").value = fechaApertura;
            document.getElementById("NombreUsuarioEnEs").value = ticketsReportados[i].NombreUsuario;
            document.getElementById("ResponsableAsignadoEnEs").value = ticketsReportados[i].NombreUsuarioResponsable;
            document.getElementById("FechaAsignadoEnEs").value = fechaEnE;
            idResponsable = ticketsReportados[i].IdResponsableUsuario;
        }
    }
}

//Función para modificar tickets en curso
function modificarEstadoTicketEnEspera(url_modificar) {
    var cmbEstado = document.getElementById("EstadosEnEs");
    var Estado = cmbEstado.options[cmbEstado.selectedIndex].value;
    var comentario = document.getElementById("ComentarioTicketEnEs").value;

    if (validarCmbEspera()) {
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
                    data: { "IdTicket": idTicketAbierto, "EstadoTicket": Estado, "ComentarioTicket": comentario, "IdResponsableUsuario": idResponsable },
                    url: url_modificar,
                    type: 'post',
                    success: function (data) {
                        if (data.OperacionExitosa) {
                            $('#ModificarTicketsEnEspera').modal('hide');
                            showNotify("Actualización exitosa", 'El Ticket cambió de Estado a: ' + Estado, "success");
                            obtenerTickets(url_metodo);
                        } else {
                            $('#ModificarTicketsEnEspera').modal('hide');
                            showNotify("Error en la Actualización", 'Ocurrió un error al modificar el estado del Ticket: ' + data.MensajeError, "error");
                        }
                    }
                });

            } else {
                $('#ModificarTicketsEnEspera').modal('hide');
            }
        });
    }

}

///Función para validar el combobox de Estado En curso
function validarCmbEspera() {
    var esValido = true;
    var cmbCat = document.getElementById("EstadosEnEs");
    //Validación para el combobox de categorias
    if (cmbCat.value == "") {
        esValido = false;
        cmbCat.style.borderColor = "#900C3F";
        $('#errorEnEspera').html('Debe seleccionar una opción').show();
        setTimeout("$('#errorEnEspera').html('').hide('slow')", 6000);
    } else {
        cmbCat.style.borderColor = "#ccc";
        $('#errorEnEspera').html('').hide();
    }

    return esValido;
}

/* --------------------------------------SECCIÓN PARA MENSAJES DE TOOLTIPS---------------------------------*/

//Mensajes para los tooltips
function mensajesTooltipsEnEspera() {
    //document.getElementById("Responsables").title = "Seleccione el responsable que se hará cargo del Ticket";
    document.getElementById("ComentarioTicketEnEs").title = "Máximo 300 caracteres.\n Caracteres especiales permitidos - / _ .";
}

/**
 * *********************************************************************************
 *                SECCIÓN PARA OPERACIONES DE TICKETS RESUELTOS
 * *********************************************************************************
 */
//Función para cargar la tabla de Usuarios
function cargarTablaResueltos() {
    var str = '<table id="dataTableResueltos" class="table jambo_table bulk_action table-bordered" style="width:100%">';
    str += '<thead> <tr> <th>Fecha de Apertura</th> <th>Usuario Solicitante</th> <th>Descripción del Incidente</th> <th>Laboratorio o Activo</th> <th>Prioridad</th> <th>Responsable Asignado</th> <th>Fecha de Solución</th> <th>Comentario de Solución</th> </tr> </thead>';
    str += '<tbody>';

    for (var i = 0; i < ticketsReportados.length; i++) {
        if (ticketsReportados[i].EstadoTicket == 'RESUELTO') {
            //Función para dar formato a la fecha
            var fechaAper = new Date(parseInt((ticketsReportados[i].FechaAperturaTicket).substr(6)));
            var fechaApertura = (fechaAper.toLocaleDateString("es-ES") + " " + fechaAper.getHours() + ":" + fechaAper.getMinutes());

            var fechaSol = new Date(parseInt((ticketsReportados[i].FechaResueltoTicket).substr(6)));
            var fechaSolucion = (fechaSol.toLocaleDateString("es-ES") + " " + fechaSol.getHours() + ":" + fechaSol.getMinutes());

            str += '<tr><td>' + fechaApertura +
                '</td><td>' + ticketsReportados[i].NombreUsuario +
                '</td><td class="text-justify">' + ticketsReportados[i].DescripcionTicket;
            if (ticketsReportados[i].IdLaboratorio != 0) {
                str += '</td><td> <strong>Laboratorio:</strong> ' + ticketsReportados[i].NombreLaboratorio;
            } else {
                str += '</td><td><strong> Activo:</strong> ' + ticketsReportados[i].NombreDetalleActivo;
            }
            str += '</td><td>' + ticketsReportados[i].PrioridadTicket +
                '</td><td>' + ticketsReportados[i].NombreUsuarioResponsable +
                '</td><td>' + fechaSolucion +
                '</td><td class="text-justify">' + ticketsReportados[i].ComentarioResueltoTicket;
                '</td></tr>';
        }  
    }
    str += '</tbody></table>';
    $("#ticketsResueltos").html(str);
}



