var datosUsuario;
var idUsuario;
var nickUsuario;
var passwdUser;

//Método ajax para obtener los datos de categorias
function obtenerCategorias(url) {
    url_metodo = url;
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

function cargarDatosUsuario() {

    for (var i = 0; i < datosUsuario.length; i++) {
        idUsuario = datosUsuario[i].IdUsuario;
        nickUsuario = datosUsuario[i].NickUsuario;
        passwdUser = datosUsuario[i].PasswordUsuario;
        document.getElementById("NombresUsuario").value = datosUsuario[i].NombresUsuario;
        document.getElementById("NickUsuario").value = datosUsuario[i].NickUsuario;
        document.getElementById("CorreoUsuario").value = datosUsuario[i].CorreoUsuario;
        document.getElementById("TelefonoUsuario").value = datosUsuario[i].TelefonoUsuario;
        document.getElementById("TelefonoCelUsuario").value = datosUsuario[i].TelefonoCelUsuario;
        document.getElementById("DireccionUsuario").value = datosUsuario[i].DireccionUsuario;       
    };
}

function modificarPassword() {
    var antiguoPassword = document.getElementById("PasswordUsuario").value;
    var nuevoPassword = document.getElementById("PasswordUsuarioNuevo").value;
    var confirmarNuevoPasswd = document.getElementById("PasswordUsuarioConfirmar").value;

    if (antiguoPassword != passwdUser) {

        antiguoPassword.addEventListener("keyup", function (event) {
            if (antiguoPassword.validity.typeMismatch) {
                antiguoPassword.setCustomValidity("!Su contraseña anterior no coincide");
            } else {
                antiguoPassword.setCustomValidity("");
            }
        });
    }

    if (nuevoPassword != confirmarNuevoPasswd) {
        confirmarNuevoPasswd.addEventListener("keyup", function (event) {
            if (confirmarNuevoPasswd.validity.typeMismatch) {
                confirmarNuevoPasswd.setCustomValidity("!Las contraseñas no coinciden!");
            } else {
                confirmarNuevoPasswd.setCustomValidity("");
            }
        });
    }

}