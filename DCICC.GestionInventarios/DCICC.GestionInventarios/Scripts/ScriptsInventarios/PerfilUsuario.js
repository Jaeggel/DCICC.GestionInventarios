var datosUsuario;
var idUsuario;
var nickUsuario;
var passwdUser;

//Método ajax para obtener los datos de categorias
function obtenerUsuario(url) {
    url_metodo = url;
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            console.log("Datos Exitosos");
            datosUsuario = data;
            cargarDatosUsuario();

        }
    });
}

function cargarDatosUsuario(){
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

function guardarContraseña(urlModificar, urlSalir) {
    var comprobarIgual = modificarPassword();
    var comprobarNuevo = contrasenaIgual();
    var comprobarMayor = contrasenaMayor();
    console.log(comprobarMayor);
    if (comprobarIgual && comprobarNuevo && comprobarMayor) {
        var confirmarNuevoPasswd = document.getElementById("PasswordUsuarioConfirmar").value;
        console.log(urlModificar + "-" + urlSalir)
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
                            //showNotify("Error en la Actualización", 'No se ha podido modificar el Password: ' + data.MensajeError, "error");
                        }
                        
                        
                    }
                });
            } else {

            }
        });

    }   

}


function modificarDatosUsuario(urlModificar, urlSalir,urlHome) {
    var nombreUsuarioMod = document.getElementById("NombresUsuario").value;
    var correoUsuarioMod = document.getElementById("CorreoUsuario").value;
    var nickUsuarioMod = document.getElementById("NickUsuario").value;
    var telefonoUsuarioMod = document.getElementById("TelefonoUsuario").value;
    var celularUsuarioMod = document.getElementById("TelefonoCelUsuario").value;
    var direccionUsuarioMod = document.getElementById("DireccionUsuario").value;

    if (nickUsuarioMod != nickUsuario) {
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
                    data: {
                        "IdUsuario": idUsuario, "NombresUsuario": nombreUsuarioMod, "CorreoUsuario": correoUsuarioMod,
                        "NickUsuario": nickUsuarioMod, "TelefonoUsuario": telefonoUsuarioMod, "TelefonoCelUsuario": celularUsuarioMod,
                        "DireccionUsuario": direccionUsuarioMod, "PasswordUsuario": passwdUser
                    },
                    url: urlModificar,
                    type: 'post',
                    success: function (data) {
                        console.log(data.OperacionExitosa);
                        if (data.OperacionExitosa) {
                            window.location.href = urlSalir;
                            //showNotify("Actualización exitosa", 'Se ha modificado el Perfil de Usuario', "success");
                        } else {
                            
                            showNotify("Error en la Actualización", 'Ocurrió un error al modificar el Tipo de Activo: ' + data.MensajeError, "error");
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
                        console.log(data.OperacionExitosa);
                        if (data.OperacionExitosa) {
                            obtenerUsuario(url_metodo);
                            showNotify("Actualización exitosa", 'El Perfil de Usuario "' + nickUsuario + '" se ha modificado exitosamente 1', "success");
                        } else {
                            showNotify("Error en la Actualización", 'Ocurrió un error al modificar el Tipo de Activo: ' + data.MensajeError, "error");
                        }
                        
                    }
                });

            } else {
            }
        });
    }

}

//Mensajes para los tooltips
function mensajesTooltips() {
    document.getElementById("PasswordUsuario").title = "Ingreso de la contraseña Anterior.";
    document.getElementById("PasswordUsuarioNuevo").title = "La contraseña nueva debe ser mayor a 6 caracteres";
    document.getElementById("PasswordUsuarioConfirmar").title = "La confirmación de nueva contraseña debe ser igual a la nueva contraseña";
}