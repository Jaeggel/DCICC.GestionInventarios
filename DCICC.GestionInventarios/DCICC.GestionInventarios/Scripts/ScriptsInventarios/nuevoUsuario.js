var datosRoles;
var datosUsuarios;

function obtenerMetodoRol(url_Rol,url_Usu) {
    
    //Método ajax para obtener los roles de la base de datos
    $.ajax({
        dataType: 'json',
        url: url_Rol,
        type: 'post',
        success: function (data) {
            console.log(data);
            datosRoles = data;
            console.log("siiiiii: ");
            cargarRolesCmb();
        }
    });

    //Método ajax para obtener usuarios de la base de datos
    $.ajax({
        dataType: 'json',
        url: url_Usu,
        type: 'post',
        success: function (data) {
            console.log(data);
            datosUsuarios = data;
            console.log("siiiiii: ");
        }
    });



}

//Función para cargar los elementos en el combobox de Roles de Usuario
function cargarRolesCmb() {
    var str = '<select class="form-control" name="IdRol" required>';
    str += '<option value="">Escoga una opción...</option>';
    for (var i = 0; i < datosRoles.length; i++) {
        str += '<option value="' + datosRoles[i].IdRol + '">' + datosRoles[i].NombreRol + '</option>';
    };
    str += '</select>';
    $("#cargarRoles").html(str);
}

//Función para evitar correos electronicos repertidos
function comprobarCorreo(correo) {
    correo = correo.toLowerCase();
    var comprobar = false;
    for (var i = 0; i < datosUsuarios.length; i++) {
        if (datosUsuarios[i].CorreoUsuario == correo) {
            comprobar = true;
        }
    }

    console.log(comprobar);

    if (comprobar == true) {
        document.getElementById("CorreoUsuario").setCustomValidity("El correo " + correo + " ya existe");
    } else {
        document.getElementById("CorreoUsuario").setCustomValidity("");
    }
}  


//Función para evitar nombres de nick repetidos
function comprobarNick(nick) {

    nick = nick.toLowerCase();
    var comprobar = false;
    for (var i = 0; i < datosUsuarios.length; i++) {
        if (datosUsuarios[i].NickUsuario == nick) {
            comprobar = true;
        }
    }

    console.log(comprobar);
    if (comprobar == true) {
        document.getElementById("NickUsuario").setCustomValidity("El nick de usuario " + nick + " ya existe");
    } else {
        document.getElementById("NickUsuario").setCustomValidity("");
    }

}
