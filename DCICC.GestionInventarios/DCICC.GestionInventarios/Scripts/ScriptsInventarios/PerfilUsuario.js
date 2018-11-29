﻿var datosUsuario;
var idUsuario;
var nickUsuario;
var passwdUser;

//Método ajax para obtener los datos de categorias
function obtenerUsuario(url) {
    console.log(url);
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'get',
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
    var nuevoPassword = document.getElementById("PasswordUsuarioNuevo").value;
    var confirmarNuevoPasswd = document.getElementById("PasswordUsuarioConfirmar").value;
    

    if (antiguoPassword != passwdUser) {
        esValido = false;
        $("#PasswordUsuario").focus();
        $('#errorPassword').html('La contraseña no coincide con la anterior').show();
        setTimeout("$('#errorNombre').fadeOut('slow');", 5000);
    } else {
        $('#errorPassword').html('').hide();
    }

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

function guardarContraseña(urlModificar, urlSalir) {
    var comprobar = modificarPassword();
    console.log(comprobar);
    if (comprobar == true) {
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
                    success: function () {
                        console.log("actualizacion exitosa");
                        window.location.href = urlSalir;
                        showNotify("Actualización exitosa", 'Se ha modificado el password de Usuario', "success");
                    }, error: function (e) {
                        console.log(e);

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
                    data: { "IdUsuario": idUsuario, "NombresUsuario": nombreUsuarioMod, "CorreoUsuario": correoUsuarioMod, "NickUsuario": nickUsuarioMod, "TelefonoUsuario": telefonoUsuarioMod, "TelefonoCelUsuario": celularUsuarioMod, "DireccionUsuario": direccionUsuarioMod },
                    url: urlModificar,
                    type: 'post',
                    success: function () {
                        console.log("actualizacion exitosa");
                        window.location.href = urlSalir;
                    }, error: function () {
                       
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
                    success: function () {                       
                        window.location.href = urlHome;
                        showNotify("Actualización exitosa", 'Se ha modificado el perfil de Usuario', "success");
                    }, error: function () {                       
                        showNotify("Error en la Actualización", 'No se ha podido modificar el usuario', "error");
                    }
                });

            } else {
            }
        });
    }

}