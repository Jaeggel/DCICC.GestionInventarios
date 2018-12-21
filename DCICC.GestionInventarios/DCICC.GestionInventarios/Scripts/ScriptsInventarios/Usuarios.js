var url_idioma = obtenerIdioma();
var url_metodo;
var datosUsuarios;
var cmbRoles;
var idUsuarioModificar;
var nick;
var urlEstado;
var nombresUsuarios = [];
var correosUsuarios = [];
var rolesEstablecidos = [];
var nickActual;
var correoActual;
var rolActual;

/* --------------------------------------SECCIÓN PARA OBTENER DATOS DEL SERVIDOR---------------------------------*/
//Método ajax para obtener los datos de los usuarios
function obtenerUsuarios(url) {
    url_metodo = url;
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            if (data.OperacionExitosa) {
                datosUsuarios = data.ListaObjetoInventarios;
                cargarUsuariosTabla();
                $('#dataTableUsuarios').DataTable({
                    "language": {
                        "url": url_idioma
                    }
                });
                cargarNombresUsuarios();
            } else {
                showNotify("Error en la Consulta", 'No se ha podido mostrar los datos: ' + data.MensajeError, "error");
            }            
        }
    });
}

//Método ajax para obtener los datos de los Roles de usuario
function obtenerRoles(url) {
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            console.log(data.OperacionExitosa);
            if (data.OperacionExitosa) {              
                cmbRoles = data.ListaObjetoInventarios;
                cargarRolesCmb();
                cargarRolesCmbModificar();
            } else {
                showNotify("Error en la Consulta", 'No se ha podido mostrar los datos: ' + data.MensajeError, "error");
            }       
        }
    });
}

/* --------------------------------------SECCIÓN PARA CARGAR TABLAS Y COMBOBOX---------------------------------*/
//Función para cargar la tabla de Usuarios
function cargarUsuariosTabla() {
    var nick = document.getElementById("usuarioActual").innerHTML;
    console.log(nick);
    var str = '<table id="dataTableUsuarios" class="table jambo_table bulk_action  table-bordered" style="width:100%">';
    str += '<thead> <tr> <th>Nombre Usuario</th> <th>Nick</th> <th>Rol</th> <th>Correo</th> <th>Teléfono/Celular</th> <th>Dirección</th> <th>Estado</th> <th>Modificar</th> <th>Habilitar/<br>Deshabilitar</th> </tr> </thead>';
    str += '<tbody>';

    for (var i = 0; i < datosUsuarios.length; i++) {
        //if (datosUsuarios[i].NickUsuario != nick || datosUsuarios[i].NombresUsuario != nick) {
        if ((datosUsuarios[i].NickUsuario).toLowerCase() != nick.toLowerCase() || (datosUsuarios[i].NickUsuario).toLowerCase() != "administracion") {
            str += '<tr><td>' + datosUsuarios[i].NombresUsuario +
                '</td><td>' + datosUsuarios[i].NickUsuario +
                '</td><td>' + datosUsuarios[i].NombreRol +
                '</td><td>' + datosUsuarios[i].CorreoUsuario +
                '</td><td>' + datosUsuarios[i].TelefonoUsuario + '<br/>' + datosUsuarios[i].TelefonoCelUsuario+
                '</td><td class="text-justify">' + datosUsuarios[i].DireccionUsuario;

        if (datosUsuarios[i].HabilitadoUsuario) {
            str += '</td><td> Habilitado';
        } else {
            str += '</td><td> Deshabilitado';
        }
            str += '</td><td><div class="text-center"><div class="col-md-12 col-sm-12 col-xs-12">' +
                '<button type="button" class="btn btn-info text-center" data-toggle="modal" data-target="#ModificarUsuario" onclick = "formUpdateUsuario(' + datosUsuarios[i].IdUsuario + ');"> <strong><i class="fa fa-pencil-square-o"></i></strong></button> ' +
                '</div></div>' +
                '</td><td><div class=" text-center"><div class="col-md-12 col-sm-12 col-xs-12">';
            if (datosUsuarios[i].HabilitadoUsuario) {
                str += '<button type = "button" class="btn btn-success text-center" onclick = "habilitarOdeshabilitar(' + datosUsuarios[i].IdUsuario + ',' + datosUsuarios[i].HabilitadoUsuario + ');"> <strong><span class="fa fa-toggle-on"></span></strong></button> ';
            } else {
                str += '<button type = "button" class="btn btn-danger text-center" onclick = "habilitarOdeshabilitar(' + datosUsuarios[i].IdUsuario + ',' + datosUsuarios[i].HabilitadoUsuario + ');"> <strong><i class="fa fa-toggle-off"></i></strong></button> ';
            }
            str += '</div></div></td></tr>';
        }
    }
    str += '</tbody></table>';
    $("#tablaModificarUsuarios").html(str);
}

//Función para cargar el combobox de roles
function cargarRolesCmb() {
    var str = '<select id="IdRol" class="form-control" name="IdRol" required>';
    str += '<option value="">Escoga una opción...</option>';
    for (var i = 0; i < cmbRoles.length; i++) {
        rolesEstablecidos[i] = cmbRoles[i].NombreRol;
        if (cmbRoles[i].HabilitadoRol) {
            str += '<option value="' + cmbRoles[i].IdRol + '">' + cmbRoles[i].NombreRol + '</option>';
        }              
    }
    str += '</select>';
    $("#cargarRoles").html(str);
}

//Función para cargar el combobox de roles
function cargarRolesCmbModificar() {
    var str = '<select id="IdRol" class="form-control" name="IdRol" required>';
    for (var i = 0; i < cmbRoles.length; i++) {
        rolesEstablecidos[i] = cmbRoles[i].NombreRol;
            str += '<option value="' + cmbRoles[i].IdRol + '">' + cmbRoles[i].NombreRol + '</option>';
    }
    str += '</select>';
    $("#cargarRolesModificar").html(str);
}

/* --------------------------------------SECCIÓN PARA MODIFICACION DE DATOS---------------------------------*/
//Función para setear los valores en los inputs
function formUpdateUsuario(idUsuario) {
    idUsuarioModificar = idUsuario;
    for (var i = 0; i < datosUsuarios.length; i++) {
        if (datosUsuarios[i].IdUsuario == idUsuario) {
            nickActual = datosUsuarios[i].NickUsuario;
            correoActual = datosUsuarios[i].CorreoUsuario;
            rolActual = datosUsuarios[i].NombreRol;
            //Métodos para setear los valores a modificar
            var element = document.getElementById("IdRol");
            element.value = datosUsuarios[i].IdRol;
            document.getElementById("NombresUsuario").value = datosUsuarios[i].NombresUsuario;
            document.getElementById("CorreoUsuario").value = datosUsuarios[i].CorreoUsuario;
            document.getElementById("NickUsuario").value = datosUsuarios[i].NickUsuario;
            document.getElementById("PasswordUsuario").value = datosUsuarios[i].PasswordUsuario;
            document.getElementById("TelefonoUsuario").value = datosUsuarios[i].TelefonoUsuario;
            document.getElementById("TelefonoCelUsuario").value = datosUsuarios[i].TelefonoCelUsuario;
            document.getElementById("DireccionUsuario").value = datosUsuarios[i].DireccionUsuario;

            //Método para el check del update de Usuarios
            var valor = datosUsuarios[i].HabilitadoUsuario;
            var estado = $('#HabilitadoUsuario').prop('checked');
            if (estado && valor == false) {
                document.getElementById("HabilitadoUsuario").click();
            }
            if (estado == false && valor == true) {
                document.getElementById("HabilitadoUsuario").click();
            }
        }
    }
}

//Función para modificar el usuario especificado
function modificarUsuario(url_modificar) {
    var cmbRol = document.getElementById("IdRol");
    var idRol = cmbRol.options[cmbRol.selectedIndex].value;
    var nomRol = cmbRol.options[cmbRol.selectedIndex].text;
    var nombreUsuario = document.getElementById("NombresUsuario").value;
    var correoUsuario = document.getElementById("CorreoUsuario").value;
    var nickUsuario = document.getElementById("NickUsuario").value;
    var passwordUsuario = document.getElementById("PasswordUsuario").value;
    var telefonoUsuario = document.getElementById("TelefonoUsuario").value;
    var celularUsuario = document.getElementById("TelefonoCelUsuario").value;
    var direccionUsuario = document.getElementById("DireccionUsuario").value;
    var habilitadoUsuario = $('#HabilitadoUsuario').prop('checked');

    if (validarInputNombre() && validarInputCorreo() && validarInputNick() && validarInputPass() && validarCmbRol() && validarCorreoCorrecto()) {
        if (nomRol != rolActual) {
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
                            "IdUsuario": idUsuarioModificar, "IdRol": idRol, "NombresUsuario": nombreUsuario, "CorreoUsuario": correoUsuario,
                            "NickUsuario": nickUsuario, "PasswordUsuario": passwordUsuario, "TelefonoUsuario": telefonoUsuario,
                            "TelefonoCelUsuario": celularUsuario, "DireccionUsuario": direccionUsuario, "NombreRolAntiguo": rolActual, "NombreRol": nomRol,
                            "HabilitadoUsuario": habilitadoUsuario
                        },
                        url: url_modificar,
                        type: 'post',
                        success: function (data) {
                            console.log(data.OperacionExitosa);
                            if (data.OperacionExitosa) {
                                $('#ModificarUsuario').modal('hide');
                                showNotify("Actualización exitosa", 'El Usuario "' + nickUsuario.toLowerCase() + '" se ha modificado exitosamente', "success");
                                obtenerUsuarios(url_metodo);
                            } else {
                                $('#ModificarUsuario').modal('hide');
                                showNotify("Error en la Actualización", 'Ocurrió un error al modificar el Usuario: ' + data.MensajeError, "error");
                            }
                        }
                    });

                } else {
                    $('#ModificarUsuario').modal('hide');
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
                            "IdUsuario": idUsuarioModificar, "IdRol": idRol, "NombresUsuario": nombreUsuario, "CorreoUsuario": correoUsuario,
                            "NickUsuario": nickUsuario, "PasswordUsuario": passwordUsuario, "TelefonoUsuario": telefonoUsuario,
                            "TelefonoCelUsuario": celularUsuario, "DireccionUsuario": direccionUsuario, "HabilitadoUsuario": habilitadoUsuario
                        },
                        url: url_modificar,
                        type: 'post',
                        success: function (data) {
                            console.log(data.OperacionExitosa);
                            if (data.OperacionExitosa) {
                                $('#ModificarUsuario').modal('hide');
                                showNotify("Actualización exitosa", 'El Usuario "' + nickUsuario.toLowerCase() + '" se ha modificado exitosamente', "success");
                                obtenerUsuarios(url_metodo);
                            } else {
                                $('#ModificarUsuario').modal('hide');
                                showNotify("Error en la Actualización", 'Ocurrió un error al modificar el Usuario: ' + data.MensajeError, "error");
                            }

                        }
                    });

                } else {
                    $('#ModificarUsuario').modal('hide');
                }
            });
        }
    }
}


//Función para obtener la url de modificación
function urlEstados(url) {
    urlEstado = url;
}

//Función para habilitar o deshabilitar la categoria
function habilitarOdeshabilitar(idUsuario, estadoUsuario) {
    var nuevoEstado = true;
    if (estadoUsuario) {
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
                data: { "IdUsuario": idUsuario, "HabilitadoUsuario": nuevoEstado },
                url: urlEstado,
                type: 'post',
                success: function (data) {
                    console.log(data.OperacionExitosa);
                    if (data.OperacionExitosa) {
                        showNotify("Actualización exitosa", 'El Estado del Usuario se ha modificado exitosamente', "success");
                        obtenerUsuarios(url_metodo);
                    } else {
                        showNotify("Error en la Actualización", 'Ocurrió un error al modificar el estado del Usuario: ' + data.MensajeError, "error");
                    }
                }
            });
        } else {

        }
    });
}

/* --------------------------------------SECCIÓN PARA CAMPOS DE AUTOCOMPLETE---------------------------------*/
//Funciones para cargar el campo de autocompletado
function cargarNombresUsuarios() {
    for (var i = 0; i < datosUsuarios.length; i++) {
        nombresUsuarios[i] = datosUsuarios[i].NickUsuario;
        correosUsuarios[i] = datosUsuarios[i].CorreoUsuario;
    }
}
//Función para cargar los nombres en el campo de nombre de laboratorios
$(function () {
    $("#NickUsuario").autocomplete({
        source: nombresUsuarios
    });
});

//Función para cargar los nombres en el campo de nombre de laboratorios
$(function () {
    $("#CorreoUsuario").autocomplete({
        source: correosUsuarios
    });
});

/* --------------------------------------SECCIÓN PARA COMPROBACIONES Y VALIDACIONES---------------------------------*/
//Función para evitar correos electronicos repertidos
function comprobarCorreo(correo) {
    correo = correo.toLowerCase();
    var comprobar = false;
    for (var i = 0; i < datosUsuarios.length; i++) {
        if (datosUsuarios[i].CorreoUsuario == correo) {
            comprobar = true;
        }
    }
    if (comprobar == true) {
        document.getElementById("CorreoUsuario").setCustomValidity("El correo de Usuario: '" + correo + "' ya existe");
    } else {
        document.getElementById("CorreoUsuario").setCustomValidity("");
    }
}

//Función para evitar correos electronicos repertidos
function comprobarCorreoModificar() {
    var correo = document.getElementById("CorreoUsuario");
    correo.value = correo.value.toLowerCase();
    var comprobar = false;
    if (correo.value != correoActual.toLowerCase()) {
        for (var i = 0; i < datosUsuarios.length; i++) {
            if (datosUsuarios[i].CorreoUsuario == correo.value) {
                comprobar = true;
            }
        }
    }
    console.log(comprobar);
    if (comprobar == true) {
        correo.style.borderColor = "#900C3F";
        $('#errorCorreo').html('El correo de Usuario: ' + correo.value + ' ya existe.').show();
        setTimeout("$('#errorCorreo').html('').hide('slow')", 6000);
    } else {
        correo.style.borderColor = "#ccc";
        $('#errorCorreo').html('').hide();
    }
}

//Función para evitar nombres de nick repetidos
function comprobarNick() {
    var nick = document.getElementById("NickUsuario");
    
    if (nick.value.length <= 0) {
        nick.style.borderColor = "#900C3F";
        $('#errorNick').html('El campo nick de usuario no debe estar vacio').show();
        setTimeout("$('#errorNick').html('').hide('slow')", 7000);
    } else if (comprobarRol_Nick()) {
        nick.style.borderColor = "#900C3F";
        $('#errorNick').html("El nick de Usuario: " + nick.value + " no puede ser igual a un nombre de rol").show();
        setTimeout("$('#errorNick').html('').hide('slow')", 7000);
        nick.value = "";
    } else {
        for (var i = 0; i < datosUsuarios.length; i++) {
            if ((datosUsuarios[i].NickUsuario).toLowerCase() == nick.value.toLowerCase()) {
                nick.style.borderColor = "#900C3F";
                $('#errorNick').html("El nick de Usuario: " + nick.value + " ya existe").show();
                setTimeout("$('#errorNick').html('').hide('slow')", 7000);
                nick.value = "";
                break;
            } else {
                nick.style.borderColor = "#ccc";
                $('#errorNick').html('').hide();
            }
        }
    }
}

//Funcion para evitar nombre de usuario como rol
function comprobarRol_Nick() {
    var comprobar = false;
    var nick = document.getElementById("NickUsuario");
    for (var i = 0; i < rolesEstablecidos.length; i++) {
        if ((rolesEstablecidos[i]).toLowerCase() == nick.value.toLowerCase()) {
            comprobar = true;          
            break;
        } 
    }
    ///.log(comprobar);
    return comprobar;
}

function validarCorreoCorrecto() { 
    var esValido = true;
    var correo = document.getElementById("CorreoUsuario");
    correo.value = correo.value.toLowerCase();
    var pattern = /^([a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+(\.[a-z\d!#$%&'*+\-\/=?^_`{|}~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]+)*|"((([ \t]*\r\n)?[ \t]+)?([\x01-\x08\x0b\x0c\x0e-\x1f\x7f\x21\x23-\x5b\x5d-\x7e\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|\\[\x01-\x09\x0b\x0c\x0d-\x7f\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))*(([ \t]*\r\n)?[ \t]+)?")@(([a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\d\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.)+([a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]|[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF][a-z\d\-._~\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]*[a-z\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])\.?$/i;
    var aux = pattern.test(correo.value);
    if (!aux) {
        esValido = false;
        correo.value = "";
        correo.style.borderColor = "#900C3F";
        $('#errorCorreo').html('El correo ingresado no es válido.').show();
        setTimeout("$('#errorCorreo').html('').hide('slow')", 6000);
    } else {
        correo.style.borderColor = "#ccc";
        $('#errorCorreo').html('').hide();
    }
    return esValido;
    
}

//función para comprobar el nick en modificación
function comprobarNickModificacion() {
    var nick = document.getElementById("NickUsuario");

    console.log(nick.value +"-"+ nickActual);

    if (comprobarRol_Nick()) {
        nick.style.borderColor = "#900C3F";
        $('#errorNick').html("El nick de Usuario: " + nick.value + " no puede ser igual a un nombre de rol").show();
        setTimeout("$('#errorNick').html('').hide('slow')", 7000);
        nick.value = "";
    } else if (nick.value != nickActual) {
        for (var i = 0; i < datosUsuarios.length; i++) {
            if ((datosUsuarios[i].NickUsuario).toLowerCase() == nick.value.toLowerCase()) {
                nick.style.borderColor = "#900C3F";
                $('#errorNick').html("El nick de Usuario: " + nick.value + " ya existe").show();
                setTimeout("$('#errorNick').html('').hide('slow')", 6000);
                nick.value = "";
                break;
            } else {
                nick.style.borderColor = "#ccc";
                $('#errorNick').html('').hide();
            }
        }
    } else {
        nick.style.borderColor = "#ccc";
        $('#errorNick').html('').hide();
    }
}



//Funciones para validaciones de campos de texto
function validarInputNombre() {
    var esValido = true;
    var nomUsu = document.getElementById("NombresUsuario");
    //Validación para el campo de texto nombre de laboratorio
    if (nomUsu.value.length <= 0) {
        nomUsu.value = "";
        nomUsu.style.borderColor = "#900C3F";
        $('#errorNombreCompleto').html('El campo nombre no debe estar vacio').show();
        setTimeout("$('#errorNombreCompleto').html('').hide('slow')", 6000);
    } else {
        nomUsu.style.borderColor = "#ccc";
        $('#errorNombreCompleto').html('').hide();
    }
    return esValido;
}

///Funciones para validaciones de campos de texto
function validarInputCorreo() {
    var esValido = true;
    var correo = document.getElementById("CorreoUsuario");

    //Validación para el campo de texto nombre de Máquina virtual
    if (correo.value.length <= 0) {
        esValido = false;
        correo.style.borderColor = "#900C3F";
        $('#errorCorreo').html('El campo correo no debe estar vacio').show();
        setTimeout("$('#errorCorreo').html('').hide('slow')", 6000);
    } else {
        correo.style.borderColor = "#ccc";
        $('#errorCorreo').html('').hide();
    }
    return esValido;

}

//Función para validar el nombre de Usuario
function validarInputNick() {
    var esValido = true;
    var nick = document.getElementById("NickUsuario");

    if (nick.value.length <= 0) {
        esValido = false;
        nick.style.borderColor = "#900C3F";
        $('#errorNick').html('El campo nick de Usuario no debe estar vacio').show();
        setTimeout("$('#errorNick').html('').hide('slow')", 6000);
    } else {
        nick.style.borderColor = "#ccc";
        $('#errorNick').html('').hide();
    }
    return esValido;
}

//Función para validar dirección IP
function validarInputPass() {
    var esValido = true;
    var passw = document.getElementById("PasswordUsuario");
    //Validación para el campo de texto nombre de Máquina virtual
    if (passw.value.length <= 0) {
        esValido = false;
        passw.style.borderColor = "#900C3F";
        $('#errorPassword').html('El campo password no debe estar vacio').show();
        setTimeout("$('#errorPassword').html('').hide('slow')", 6000);
    } else if (passw.value.length > 0 && passw.value.length <= 6) {
        esValido = false;
        passw.style.borderColor = "#900C3F";
        $('#errorPassword').html('El password debe ser mayor a 6 caracteres').show();
        setTimeout("$('#errorPassword').html('').hide('slow')", 6000);
    }else {
        passw.style.borderColor = "#ccc";
        $('#errorPassword').html('').hide();
    }
    return esValido;
}


///Función para validar los combobox de roles
function validarCmbRol() { 
    var esValido = true;
    var cmbRol = document.getElementById("IdRol");
    
    //Validación para el combobox de SO
    if (cmbRol.value == "") {
        esValido = false;
        cmbRol.style.borderColor = "#900C3F";
        $('#errorRol').html('Debe seleccionar una opción').show();
        setTimeout("$('#errorRol').html('').hide('slow')", 6000);
    } else {
        cmbRol.style.borderColor = "#ccc";
        $('#errorRol').html('').hide();
    }
    return esValido;
}

//Mensajes para los tooltips
function mensajesTooltips() {
    document.getElementById("NombresUsuario").title = "Máximo 80 caracteres.\n Caracteres especiales permitidos - / _ .";
    document.getElementById("CorreoUsuario").title = "Máximo 50 caracteres.\n Caracteres especiales permitidos _ - @ .";
    document.getElementById("NickUsuario").title = "Máximo 15 caracteres, sin espacios.\n Caracteres especiales permitidos _";
    document.getElementById("PasswordUsuario").title = "Máximo 20 caracteres.\n Caracteres especiales permitidos ! @ # $ % ^ & * ? _ -";
    document.getElementById("TelefonoUsuario").title = "10 números, incluyendo el código de la provincia (02).";
    document.getElementById("TelefonoCelUsuario").title = "10 números, empezando con el código (09)";
    document.getElementById("DireccionUsuario").title = "Máximo 80 caracteres.\n Caracteres especiales permitidos - / _ .";
   
}
