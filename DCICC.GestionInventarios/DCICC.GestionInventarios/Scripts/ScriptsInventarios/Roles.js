var url_idioma = obtenerIdioma();
var url_metodo;
var datosRoles;
var idRolMod;
var nombresRoles = [];
var nombreRolModificar;
var estadoRolActual;

/* --------------------------------------SECCIÓN PARA OBTENER DATOS DEL SERVIDOR---------------------------------*/
//Método ajax para obtener los datos de roles
function obtenerRoles(url) {
    url_metodo = url;
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            if (data.OperacionExitosa) {
                datosRoles = data.ListaObjetoInventarios;
                cargarRolesTabla();
                $('#dataTableRoles').DataTable({
                    "language": {
                        "url": url_idioma
                    }
                });
                cargarNombresTipo();
            } else {
                showNotify("Error en la Consulta", 'No se ha podido mostrar los datos: ' + data.MensajeError, "error");
            }

        }
    });
}

//Función para obtener la url de modificación
function urlEstados(url) {
    urlEstado = url;
}

/* --------------------------------------SECCIÓN PARA CARGAR TABLAS Y COMBOBOX---------------------------------*/

//Función para cargar la tabla de Tipo de Activo
function cargarRolesTabla() {
    var str = '<table id="dataTableRoles" class="table jambo_table bulk_action  table-bordered" style="width:100%">';
    str += '<thead> <tr> <th>Nombre de Rol</th> <th>Activos de TI</th> <th>Reportes</th> <th>Máquinas Virtuales</th> <th>Tickets</th> <th>Descripción</th> <th>Estado</th> <th>Modificar</th> <th>Habilitar/<br>Deshabilitar</th> </tr> </thead>';
    str += '<tbody>';
    for (var i = 0; i < datosRoles.length; i++) {
        str += '<tr><td>' + datosRoles[i].NombreRol;

        if (datosRoles[i].PermisoActivos) {
            str += '</td><td class="text-center"><span style="color: #169F85;" class="fa fa-check" aria-hidden="true"></span>';
        } else {
            str += '</td><td class="text-center"><span style="color:#c9302c;" class="fa fa-close" aria-hidden="true"></span>';
        }
        if (datosRoles[i].PermisoReportes) {
            str += '</td><td class="text-center"><span style="color: #169F85;" class="fa fa-check" aria-hidden="true"></span>';
        } else {
            str += '</td><td class="text-center"><span style="color:#c9302c;" class="fa fa-close" aria-hidden="true"></span>';
        }
        if (datosRoles[i].PermisoMaqVirtuales) {
            str += '</td><td class="text-center"><span style="color: #169F85;" class="fa fa-check" aria-hidden="true"></span>';
        } else {
            str += '</td><td class="text-center"><span style="color:#c9302c;" class="fa fa-close" aria-hidden="true"></span>';
        }
        if (datosRoles[i].PermisoTickets) {
            str += '</td><td class="text-center"><span style="color: #169F85;" class="fa fa-check" aria-hidden="true"></span>';
        } else {
            str += '</td><td class="text-center"><span style="color:#c9302c;" class="fa fa-close" aria-hidden="true"></span>';
        }

        if (datosRoles[i].DescripcionRol !="") {
            str += '</td><td class="text-justify">' + datosRoles[i].DescripcionRol;
        } else {
            str += '</td><td class="text-justify">';
        }

        if (datosRoles[i].HabilitadoRol) {
            str += '</td><td> Habilitado';
        } else {
            str += '</td><td> Deshabilitado';
        }

        if (datosRoles[i].NombreRol == "administrador" || datosRoles[i].NombreRol == "docente" || datosRoles[i].NombreRol == "invitado" || datosRoles[i].NombreRol == "pasante" || datosRoles[i].NombreRol == "reporteria" || datosRoles[i].NombreRol == "generador_tickets") {
            str += '</td><td><div class="text-center"><div class="col-md-12 col-sm-12 col-xs-12">' +
                '<button disabled="true" id="modificar" type="button" class="btn btn-info text-center" data-toggle="modal" data-target="#ModificarRoles" onclick = "formUpdateRol(' + datosRoles[i].IdRol + ');"> <strong><i class="fa fa-pencil-square-o"></i></strong></button> ' +
                '</div></div>' +
                '</td><td><div class=" text-center"><div class="col-md-12 col-sm-12 col-xs-12">';
            if (datosRoles[i].HabilitadoRol) {
                str += '<button disabled="true" type="button" class="btn btn-success text-center" onclick = "habilitarOdeshabilitar(' + datosRoles[i].IdRol + ',' + datosRoles[i].HabilitadoRol + ');"> <strong><span class="fa fa-toggle-on"></span></strong></button>';
            } else {
                str += '<button disabled="true" type="button" class="btn btn-danger text-center" onclick = "habilitarOdeshabilitar(' + datosRoles[i].IdRol + ',' + datosRoles[i].HabilitadoRol + ');"> <strong><i class="fa fa-toggle-off"></i></strong></button>';
            }
            str += '</div></div></td></tr>';
        } else {
            str += '</td><td><div class="text-center"><div class="col-md-12 col-sm-12 col-xs-12">' +
                '<button id="modificar" type="button" class="btn btn-info text-center" data-toggle="modal" data-target="#ModificarRoles" onclick = "formUpdateRol(' + datosRoles[i].IdRol + ');"> <strong><i class="fa fa-pencil-square-o"></i></strong></button> ' +
                '</div></div>' +
                '</td><td><div class=" text-center"><div class="col-md-12 col-sm-12 col-xs-12">';
            if (datosRoles[i].HabilitadoRol) {
                str += '<button type="button" class="btn btn-success text-center" onclick = "habilitarOdeshabilitar(' + datosRoles[i].IdRol + ',' + datosRoles[i].HabilitadoRol + ');"> <strong><span class="fa fa-toggle-on"></span></strong></button>';
            } else {
                str += '<button type="button" class="btn btn-danger text-center" onclick = "habilitarOdeshabilitar(' + datosRoles[i].IdRol + ',' + datosRoles[i].HabilitadoRol + ');"> <strong><i class="fa fa-toggle-off"></i></strong></button>';
            }
            str += '</div></div></td></tr>';
        }      

    }
    str += '</tbody></table>';
    $("#tablaModificarRoles").html(str);

}

/* --------------------------------------SECCIÓN PARA MODIFICACION DE DATOS---------------------------------*/
//Función para setear los valores en los inputs
function formUpdateRol(idRol) {
    idRolMod = idRol;
    for (var i = 0; i < datosRoles.length; i++) {
        if (datosRoles[i].IdRol == idRol) {
            nombreRolModificar = datosRoles[i].NombreRol;
            estadoRolActual = datosRoles[i].HabilitadoRol;
            //Métodos para setear los valores a modificar      

            document.getElementById("NombreRol").value = datosRoles[i].NombreRol;

            //Método para el check de activos de TI
            var act = datosRoles[i].PermisoActivos;
            var estado1 = $('#PermisoActivos').prop('checked');
            if (estado1 && act == false) {
                document.getElementById("PermisoActivos").click();
            }
            if (estado1 == false && act == true) {
                document.getElementById("PermisoActivos").click();
            }

            //Método para el check de reportes
            var rep = datosRoles[i].PermisoReportes;
            var estado2 = $('#PermisoReportes').prop('checked');
            if (estado2 && rep == false) {
                document.getElementById("PermisoReportes").click();
            }
            if (estado2 == false && rep == true) {
                document.getElementById("PermisoReportes").click();
            }

            //Método para el check de maquinas virtuales
            var mv = datosRoles[i].PermisoMaqVirtuales;
            var estado3 = $('#PermisoMaqVirtuales').prop('checked');
            if (estado3 && mv == false) {
                document.getElementById("PermisoMaqVirtuales").click();
            }
            if (estado3 == false && mv == true) {
                document.getElementById("PermisoMaqVirtuales").click();
            }

            //Método para el check de tickets
            var tick = datosRoles[i].PermisoTickets;
            var estado4 = $('#PermisoTickets').prop('checked');
            if (estado4 && tick == false) {
                document.getElementById("PermisoTickets").click();
            }
            if (estado4 == false && tick == true) {
                document.getElementById("PermisoTickets").click();
            }


            document.getElementById("DescripcionRol").value = datosRoles[i].DescripcionRol;


            break;
        }
    }
}

//Función para modificar el rol especificado
function modificarRol(url_modificar) {
    var nombre = document.getElementById("NombreRol").value;
    var activoTi = $('#PermisoActivos').prop('checked');
    var maqVir = $('#PermisoMaqVirtuales').prop('checked');
    var reporte = $('#PermisoReportes').prop('checked');
    var ticket = $('#PermisoTickets').prop('checked');
    var descripcion = document.getElementById("DescripcionRol").value;

    if (validarInputsVaciosIngreso()) {
        if (nombreRolModificar != nombre) {
            console.log(nombreRolModificar);
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
                        data: {
                            "IdRol": idRolMod, "NombreRol": nombre, "NombreRolAntiguo": nombreRolModificar, "DescripcionRol": descripcion, "HabilitadoRol": estadoRolActual,
                            "PermisoActivos": activoTi, "PermisoMaqVirtuales": maqVir, "PermisoTickets": ticket, "PermisoReportes": reporte
                        },
                        url: url_modificar,
                        type: 'post',
                        success: function (data) {
                            if (data.OperacionExitosa) {
                                $('#ModificarRoles').modal('hide');
                                showNotify("Actualización exitosa", 'El Rol "' + nombre.toUpperCase() + '" se ha modificado exitosamente', "success");
                                obtenerRoles(url_metodo);
                            } else {
                                $('#ModificarRoles').modal('hide');
                                showNotify("Error en la Actualización", 'Ocurrió un error al modificar el Rol: ' + data.MensajeError, "error");
                            }
                        }
                    });

                } else {
                    $('#ModificarRoles').modal('hide');
                }
            });
        } else {
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
                        data: {
                            "IdRol": idRolMod, "NombreRol": nombre, "DescripcionRol": descripcion, "HabilitadoRol": estadoRolActual,
                            "PermisoActivos": activoTi, "PermisoMaqVirtuales": maqVir, "PermisoTickets": ticket, "PermisoReportes": reporte
                        },
                        url: url_modificar,
                        type: 'post',
                        success: function (data) {
                            if (data.OperacionExitosa) {
                                $('#ModificarRoles').modal('hide');
                                showNotify("Actualización exitosa", 'El Rol "' + nombre.toUpperCase() + '" se ha modificado exitosamente', "success");
                                obtenerRoles(url_metodo);
                            } else {
                                $('#ModificarRoles').modal('hide');
                                showNotify("Error en la Actualización", 'Ocurrió un error al modificar el Rol: ' + data.MensajeError, "error");
                            }
                        }
                    });

                } else {
                    $('#ModificarRoles').modal('hide');
                }
            });
        }


        
    }
}

//Función para habilitar o deshabilitar la categoria
function habilitarOdeshabilitar(idRol, estadoRol) {
    var nuevoEstado = true;
    if (estadoRol) {
        nuevoEstado = false;
    } else {
        nuevoEstado = true;
    }
    swal({
        title: 'Confirmación de Cambio de Estado',
        text: "¿Está seguro de cambiar el estado del registro?",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#26B99A',
        cancelButtonColor: '#337ab7',
        confirmButtonText: 'Confirmar',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                data: { "IdRol": idRol, "HabilitadoRol": nuevoEstado },
                url: urlEstado,
                type: 'post',
                success: function (data) {
                    console.log(data.OperacionExitosa);
                    if (data.OperacionExitosa) {
                        showNotify("Actualización exitosa", 'El Estado del Rol se ha modificado exitosamente', "success");
                        obtenerRoles(url_metodo);
                    } else {
                        showNotify("Error en la Actualización", 'Ocurrió un error al modificar el estado del Rol: ' + data.MensajeError, "error");
                    }
                }
            });
        } 
    });
}


/* --------------------------------------SECCIÓN PARA CAMPOS DE AUTOCOMPLETE---------------------------------*/

//Funciones para cargar el campo de autocompletado
function cargarNombresTipo() {
    for (var i = 0; i < datosRoles.length; i++) {
        nombresRoles[i] = datosRoles[i].NombreRol;
    }
}
//Función para cargar los nombres en el campo de nombre de ingreso  de tipo
$(function () {
    $("#NombreRol").autocomplete({
        source: nombresRoles
    });
});

/* --------------------------------------SECCIÓN PARA COMPROBACIONES Y VALIDACIONES---------------------------------*/

//Función para evitar nombres de tipo activo repetidos
function comprobarNombre() {
    var nombre = document.getElementById("NombreRol");
    nombre.value = nombre.value.toLowerCase();
    if (nombre.value.length <= 0) {
        nombre.style.borderColor = "#900C3F";
        $('#errorNombreRol').html('El campo nombre no debe estar vacio').show();
        setTimeout("$('#errorNombreRol').html('').hide('slow')", 6000);
    } else {
        for (var i = 0; i < datosRoles.length; i++) {
            if ((datosRoles[i].NombreRol).toLowerCase() == nombre.value) {
                nombre.style.borderColor = "#900C3F";
                $('#errorNombreRol').html("El nombre de rol: " + nombre.value + " ya existe").show();
                setTimeout("$('#errorNombreRol').html('').hide('slow')", 8000);
                nombre.value = "";
                break;
            } else {
                nombre.style.borderColor = "#ccc";
                $('#errorNombreRol').html('').hide();
            }
        }
    }
}

//Función para evitar nombres repetidos en la modificación de tipo
function validarNombreModificacion() {
    var nombre = document.getElementById("NombreRol");
    nombre.value = nombre.value.toLowerCase();
    if (nombre.value != nombreRolModificar.toLowerCase()) {
        for (var i = 0; i < datosRoles.length; i++) {
            if ((datosRoles[i].NombreRol).toLowerCase() == nombre.value) {
                nombre.style.borderColor = "#900C3F";
                $('#errorNombreRol').html("El nombre de rol: " + nombre.value + " ya existe").show();
                setTimeout("$('#errorNombreRol').html('').hide('slow')", 8000);
                nombre.value = "";
                break;
            } else {
                nombre.style.borderColor = "#ccc";
                $('#errorNombreRol').html('').hide();
            }
        }
    } else {
        nombre.style.borderColor = "#ccc";
        $('#errorNombreRol').html('').hide();
    }
}

//Función para validación de input de ingreso
function validarInputsVaciosIngreso() {
    var esValido = true;
    var nomTipo = document.getElementById("NombreRol");
    //Validación para el campo de texto nombre de tipo
    if (nomTipo.value.length <= 0) {
        esValido = false;
        nomTipo.style.borderColor = "#900C3F";
        $('#errorNombreRol').html('El campo nombre no debe estar vacio').show();
        setTimeout("$('#errorNombreRol').html('').hide('slow')", 6000);
    } else {
        nomTipo.style.borderColor = "#ccc";
        $('#errorNombreRol').html('').hide();
    }
    return esValido;
}


/* --------------------------------------SECCIÓN PARA MENSAJES DE TOOLTIPS---------------------------------*/

//Mensajes para los tooltips
function mensajesTooltips() {
    document.getElementById("NombreRol").title = "Máximo 30 caracteres en Minúscula, sin Espacios ni Números.\n Caracteres especiales permitidos _ ";
    document.getElementById("DescripcionRol").title = "Máximo 150 caracteres.\n Caracteres especiales permitidos - / _ . , : ;";
}