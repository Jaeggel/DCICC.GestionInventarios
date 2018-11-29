var datosUsuario;
var idUsuario;
var nickUsuario;
var passwdUser;

//Método ajax para obtener los datos de categorias
function obtenerUsuario(url) {
    
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
    console.log(datosUsuario.length);
   
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

function modificarPassword(urlModificar,urlSalir) {
    var antiguoPassword = document.getElementById("PasswordUsuario").value;
    var nuevoPassword = document.getElementById("PasswordUsuarioNuevo").value;
    var confirmarNuevoPasswd = document.getElementById("PasswordUsuarioConfirmar").value;

    if (antiguoPassword != passwdUser) {
        document.getElementById("PasswordUsuario").setCustomValidity("La contraseña no coincide con la anterior");
    }
    else if (nuevoPassword != confirmarNuevoPasswd) {
        document.getElementById("PasswordUsuario").setCustomValidity("");
        document.getElementById("PasswordUsuarioConfirmar").setCustomValidity("Las contraseñas no coinciden");
    } else {
        
        guardarContraseña(urlModificar, urlSalir, confirmarNuevoPasswd);
    }

}

function guardarContraseña(urlModificar, urlSalir, confirmarNuevoPasswd) {
    console.log(urlModificar + "-" + urlSalir)
    swal({
        title: 'Confirmación de Cambio de Estado',
        text: "¿Está seguro de Cambiar su contraseña?",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#26B99A',
        cancelButtonColor: '#337ab7',
        confirmButtonText: 'Confirmar',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                data: { "IdUsuario": passwdUser, "NickUsuario": nickUsuario, "PasswordUsuario": confirmarNuevoPasswd },
                url: urlModificar,
                type: 'post',
                success: function () {
                    console.log("actualizacion exitosa");
                    window.location.href = urlSalir;
                }, error: function (e) {
                    console.log(e);
                }
            });
        } else {

        }
    });

}


function modificarDatosUsuario(urlModificar, urlSalir) {
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
                        showNotify("Actualización exitosa", 'El usuario se ha modificado correctamente', "success");
                        obtenerUsuarios(url_metodo);
                    }, error: function () {                       
                        showNotify("Error en la Actualización", 'No se ha podido modificar el usuario', "error");
                    }
                });

            } else {
            }
        });
    }

}