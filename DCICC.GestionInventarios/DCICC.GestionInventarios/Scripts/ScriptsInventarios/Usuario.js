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
            cargarUsuarioTabla();
            console.log("siiiiii: ");
        }
    });

}

function obtenerMetodoUsuarios(url_Usu) {

    //Método ajax para obtener usuarios de la base de datos
    $.ajax({
        dataType: 'json',
        url: url_Usu,
        type: 'post',
        success: function (data) {
            console.log(data);
            datosUsuarios = data;
            cargarUsuarioTabla();
            console.log("siiiiii: ");
        }
    });

}

//Función para cargar la tabla de Usuarios
function cargarUsuarioTabla() {
    var str = '<table class="table table-striped jambo_table bulk_action table-responsive table-bordered">';
    str += '<thead> <tr> <th>Nombre Usuario</th> <th>Nick</th> <th>Rol</th> <th>Correo</th> <th>Celular</th> <th>Estado</th> <th>Modificar</th> <th>Habilitar/<br>Deshabilitar</th> </tr> </thead>';
    str += '<tbody>';
    for (var i = 0; i < datosUsuarios.length; i++) {
        var nom = "'" + datosUsuarios[i].NombreRol + "'";
        console.log(nom);

        

        str += '<tr><td>' + datosUsuarios[i].NombresUsuario +
            '</td><td>' + datosUsuarios[i].NickUsuario +
            '</td><td>' + datosUsuarios[i].NombreRol +
            '</td><td>' + datosUsuarios[i].CorreoUsuario +
            '</td><td>' + datosUsuarios[i].TelefonoCelUsuario;

        if (datosUsuarios[i].HabilitadoUsuario) {
            str += '</td><td> Habilitado';
        } else {
            str += '</td><td> Deshabilitado';
        }

        //console.log(nom);
        str += '</td><td><button type="button" class="btn btn-info " ><strong><i class="fa fa-pencil-square-o"></i></strong></button>' +
            '</td><td><button type="button" class="btn btn-danger " ><strong><i class="fa fa-times-circle"></i></strong></button>' +
            '</td></tr>';

    };
    str += '</tbody></table>';
    $("#tablaModificarUsuarios").html(str);
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




