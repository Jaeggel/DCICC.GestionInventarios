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

    if (nuevoPassword != confirmarNuevoPasswd) {
        document.getElementById("PasswordUsuarioConfirmar").setCustomValidity("Las contraseñas no coinciden");
    }

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