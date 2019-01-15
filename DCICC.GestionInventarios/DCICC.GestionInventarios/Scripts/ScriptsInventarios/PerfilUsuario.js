var datosUsuario;
var idUsuario;
var nickUsuario;
var passwdUser;
var datosUsuarios;
var datosRoles;
var nombresUsuarios =[];
var correosUsuarios = [];
var rolesEstablecidos = [];
var correoActual;
var nickActual;

/* --------------------------------------SECCIÓN PARA OBTENER DATOS DEL SERVIDOR---------------------------------*/
//Método ajax para obtener los datos del Usuario de Sesión
function obtenerUsuario(url) {
    url_metodo = url;
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            datosUsuario = data;
            cargarDatosUsuario();
        }
    });
}

//Método ajax para obtener los datos de los usuarios
function obtenerUsuariosComp(url) {
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            if (data.OperacionExitosa) {
                datosUsuarios = data.ListaObjetoInventarios;
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
                datosRoles = data.ListaObjetoInventarios;
                cargarRoles();
            } else {
                showNotify("Error en la Consulta", 'No se ha podido mostrar los datos: ' + data.MensajeError, "error");
            }
        }
    });
}

/* --------------------------------------SECCIÓN PARA MODIFICACION DE DATOS---------------------------------*/
//Función para cargar el combobox de roles
function cargarRoles() {  
    for (var i = 0; i < datosRoles.length; i++) {
        rolesEstablecidos[i] = datosRoles[i].NombreRol;
    }
}


//Función para cargar los datos en el panel de modificación
function cargarDatosUsuario() {
    correoActual = datosUsuario.CorreoUsuario;
    nickActual = datosUsuario.NickUsuario;
    idUsuario = datosUsuario.IdUsuario;
    console.log(idUsuario);
    nickUsuario = datosUsuario.NickUsuario;
    passwdUser = datosUsuario.PasswordUsuario;
    document.getElementById("NombresUsuario").value = datosUsuario.NombresUsuario;
    document.getElementById("NickUsuario").value = datosUsuario.NickUsuario;
    document.getElementById("CorreoUsuario").value = datosUsuario.CorreoUsuario;
    document.getElementById("TelefonoUsuario").value = datosUsuario.TelefonoUsuario;
    document.getElementById("TelefonoCelUsuario").value = datosUsuario.TelefonoCelUsuario;
    document.getElementById("DireccionUsuario").value = datosUsuario.DireccionUsuario;         
}

//Función para modificación de contraseña de usuario
function guardarContraseña(urlModificar, urlSalir) {
    var comprobarIgual = modificarPassword();
    var comprobarNuevo = contrasenaIgual();
    var comprobarMayor = contrasenaMayor();
    if (comprobarIgual && comprobarNuevo && comprobarMayor) {
        var confirmarNuevoPasswd = document.getElementById("PasswordUsuarioConfirmar").value;
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
                //Método ajax para modificar la categoria de la base de datos
                $.ajax({
                    data: { "IdUsuario": idUsuario, "NickUsuario": nickUsuario, "PasswordUsuario": confirmarNuevoPasswd },
                    url: urlModificar,
                    type: 'post',
                    success: function (data) {
                        console.log(data.OperacionExitosa);
                        if (data.OperacionExitosa) {
                            window.location.href = urlSalir;
                            //showNotify("Actualización exitosa", 'Se ha modificado el Password de Usuario', "success");
                        } else {    
                            $('#ModificarContraseña').modal('hide');
                            showNotify("Error en la Actualización", 'Ocurrió un error al modificar el Password: ' + data.MensajeError, "error");
                        }                       
                    }
                });
            } else {
                $('#ModificarContraseña').modal('hide');
            }
        });

    }   
}

//Función para modificar Datos de Usuario
function modificarDatosUsuario(urlModificar, urlSalir,urlHome) {
    var nombreUsuarioMod = document.getElementById("NombresUsuario").value;
    var correoUsuarioMod = document.getElementById("CorreoUsuario").value;
    var nickUsuarioMod = document.getElementById("NickUsuario").value;
    var telefonoUsuarioMod = document.getElementById("TelefonoUsuario").value;
    var celularUsuarioMod = document.getElementById("TelefonoCelUsuario").value;
    var direccionUsuarioMod = document.getElementById("DireccionUsuario").value;
    //Condición para determinar cambios en el nick de usuario
    if (validarInputNombre() && validarInputCorreo() && validarInputNick() && validarCorreoCorrecto() ) {
        if (nickUsuarioMod != nickUsuario) {
            swal({
                title: 'Confirmación de Actualización',
                text: "¿Está seguro de modificar sus Datos?",
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
                            "IdUsuario": idUsuario, "NombresUsuario": nombreUsuarioMod, "CorreoUsuario": correoUsuarioMod,
                            "NickUsuario": nickUsuarioMod, "TelefonoUsuario": telefonoUsuarioMod, "TelefonoCelUsuario": celularUsuarioMod,
                            "DireccionUsuario": direccionUsuarioMod, "PasswordUsuario": passwdUser
                        },
                        url: urlModificar,
                        type: 'post',
                        success: function (data) {
                            if (data.OperacionExitosa) {
                                window.location.href = urlSalir;
                                //showNotify("Actualización exitosa", 'Se ha modificado el Perfil de Usuario', "success");
                            } else {

                                showNotify("Error en la Actualización", 'Ocurrió un error al modificar el Usuario: ' + data.MensajeError, "error");
                            }
                        }
                    });

                } else {
                }
            });
        } else {
            swal({
                title: 'Confirmación de Actualización',
                text: "¿Está seguro de modificar datos del usuario?",
                type: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#26B99A',
                cancelButtonColor: '#337ab7',
                confirmButtonText: 'Confirmar',
                cancelButtonText: 'Cancelar'
            }).then((result) => {
                if (result.value) {
                    $.ajax({
                        data: { "IdUsuario": idUsuario, "NombresUsuario": nombreUsuarioMod, "CorreoUsuario": correoUsuarioMod, "NickUsuario": nickUsuarioMod, "TelefonoUsuario": telefonoUsuarioMod, "TelefonoCelUsuario": celularUsuarioMod, "DireccionUsuario": direccionUsuarioMod },
                        url: urlModificar,
                        type: 'post',
                        success: function (data) {
                            if (data.OperacionExitosa) {
                                window.location.href = urlHome;
                                //showNotify("Actualización exitosa", 'El Perfil de Usuario "' + nickUsuario + '" se ha modificado exitosamente 1', "success");
                            } else {
                                showNotify("Error en la Actualización", 'Ocurrió un error al modificar el Usuario: ' + data.MensajeError, "error");
                                window.location.href = urlHome;
                            }

                        }
                    });

                } else {
                }
            });
        }
    }

    

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

//Funcion para evitar nombre de usuario como rol
function comprobarRol_Nick() {
    var comprobar = false;
    var nick = document.getElementById("NickUsuario");
    nick.value = nick.value.toLowerCase();
    for (var i = 0; i < rolesEstablecidos.length; i++) {
        if ((rolesEstablecidos[i]).toLowerCase() == nick.value) {
            comprobar = true;
            break;
        }
    }
    ///.log(comprobar);
    return comprobar;
}

//función para comprobar el nick en modificación
function comprobarNickModificacion() {
    var nick = document.getElementById("NickUsuario");
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
        esValido = false;
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


//Función para validar correo correcto
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

//Función para verificación de contraseña anterior
function modificarPassword() {
    var esValido = true;
    var antiguoPassword = document.getElementById("PasswordUsuario").value;
    if (antiguoPassword != passwdUser) {
        esValido = false;
        $("#PasswordUsuario").focus();
        $('#errorPassword').html('La contraseña no coincide con la anterior').show();
        setTimeout("$('#errorNombre').fadeOut('slow');", 5000);
    } else {
        $('#errorPassword').html('').hide();
    }
    return esValido;
}

//Función para la verificación de contraseñas nuevas iguales
function contrasenaIgual() {
    var esValido = true;
    var nuevoPassword = document.getElementById("PasswordUsuarioNuevo").value;
    var confirmarNuevoPasswd = document.getElementById("PasswordUsuarioConfirmar").value;
    if (nuevoPassword != confirmarNuevoPasswd) {
        esValido = false;
        $("#PasswordUsuarioNuevo").focus();
        $('#errorNuevoPassword').html('La contraseña no coincide con la anterior').show();
        setTimeout("$('#errorNuevoPassword').fadeOut('slow');", 5000);
    } else {
        $('#errorNuevoPassword').html('').hide();
    }
    return esValido;
}

//Función para validar que el campo contraseña sea mayor a 6 caracteres
function contrasenaMayor() {
    var esValido = true;
    var nuevoPassword = document.getElementById("PasswordUsuarioNuevo").value;
    var confirmarNuevoPasswd = document.getElementById("PasswordUsuarioConfirmar").value;
    if (nuevoPassword.length < 6 || confirmarNuevoPasswd.length < 6) {
        esValido = false;
        $('#errorVacios').html('La nueva contraseña debe ser mayor a 6 caracteres').show();
        setTimeout("$('#errorVacios').html('').hide('slow')", 5000);
    } else {
        $('#errorVacios').html('').hide();
    }
    return esValido;
}


/* --------------------------------------SECCIÓN PARA OPERACIONES CON USUARIO INVITADO---------------------------------*/
//Mensajes para los tooltips
function mensajesTooltips() {
    document.getElementById("NombresUsuario").title = "Máximo 80 caracteres.\n Caracteres especiales permitidos - / _ .";
    document.getElementById("CorreoUsuario").title = "Máximo 50 caracteres.\n Caracteres especiales permitidos _ - @ .";
    document.getElementById("NickUsuario").title = "Máximo 15 caracteres, sin espacios.\n Caracteres especiales permitidos - / _ .";
    document.getElementById("TelefonoUsuario").title = "10 números, incluyendo el código de la provincia (02).";
    document.getElementById("TelefonoCelUsuario").title = "10 números, empezando con el código (09)";
    document.getElementById("DireccionUsuario").title = "Máximo 80 caracteres.\n Caracteres especiales permitidos - / _ . , : ;";

    document.getElementById("PasswordUsuario").title = "Ingreso de la contraseña Anterior.";
    document.getElementById("PasswordUsuarioNuevo").title = "La contraseña nueva debe ser mayor a 6 caracteres";
    document.getElementById("PasswordUsuarioConfirmar").title = "La confirmación de nueva contraseña debe ser igual a la nueva contraseña";
}