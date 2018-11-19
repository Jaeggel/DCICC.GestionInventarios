var datosRoles;
var datosUsuarios;
var usuarioActual;
var idUsuarioModificar;

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
            $('#dataTableUsuario').DataTable();
            //$('#dataTableUsuario').DataTable({
            //    "language": {
            //        "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Spanish.json"
            //    }
            //});
            console.log("siiiiii: ");
        }
    });

}

function obtenerMetodoUsuarios(url_rol,url_Usu,usuario) {
    //Método ajax para obtener usuarios de la base de datos
    $.ajax({
        dataType: 'json',
        url: url_Usu,
        type: 'post',
        success: function (data) {
            datosUsuarios = data;   
            console.log("siiiiii: ");
        }
    });
    //Método ajax para obtener el usuario de la sesión
    $.ajax({
        dataType: 'json',
        url: usuario,
        type: 'post',
        success: function (data) {
            usuarioActual = data;
            cargarUsuarioTabla(data);
            $('#dataTableUsuario').DataTable();
            //$('#dataTableUsuario').DataTable({
            //    "language": {
            //        "url": "http://localhost/Content/Spanish.json"
            //    }
            //});
            console.log(data);
        }
    });

    //Método ajax para obtener los roles de la base de datos
    $.ajax({
        dataType: 'json',
        url: url_rol,
        type: 'post',
        success: function (data) {
            console.log(data);
            datosRoles = data;
            console.log("siiiiii: ");
            cargarRolesUpdateCmb();
        }
    });

}

function obtenerUsuariosUpdate(url_Usu) {
    //Método ajax para obtener usuarios de la base de datos
    $.ajax({
        dataType: 'json',
        url: url_Usu,
        type: 'post',
        success: function (data) {
            console.log(data);
            datosUsuarios = data;
            cargarUsuarioTabla(usuarioActual);
            $('#dataTableUsuario').DataTable();
            //$('#dataTableUsuario').DataTable({
            //    "language": {
            //        "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/Spanish.json"
            //    }
            //});
            console.log("siiiiii: ");
        }
    });

}

//Función para cargar la tabla de Usuarios
function cargarUsuarioTabla(nick) {
    var str = '<table id="dataTableUsuario"class="table table-striped jambo_table bulk_action table-responsive table-bordered">';
    str += '<thead> <tr> <th>Nombre Usuario</th> <th>Nick</th> <th>Rol</th> <th>Correo</th> <th>Celular</th> <th>Estado</th> <th>Modificar</th> <th>Habilitar/<br>Deshabilitar</th> </tr> </thead>';
    str += '<tbody>';
    for (var i = 0; i < datosUsuarios.length; i++) {
        if (datosUsuarios[i].NickUsuario != nick) {
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
            str += '</td><td><div class="text-center"><div class="col-md-12 col-sm-12 col-xs-12">'+
                '<button type="button" class="btn btn-info text-center" data-toggle="modal" data-target="#ModificarUsuario" onclick = "formUpdateUsuario('+ datosUsuarios[i].IdUsuario+');"> <strong><i class="fa fa-pencil-square-o"></i></strong></button> ' +
                '</div></div>'+
                '</td><td><div class=" text-center"><div class="col-md-12 col-sm-12 col-xs-12">' +
                '<button type = "button" class="btn btn-danger text-center" > <strong><i class="fa fa-times-circle"></i></strong></button> ' +
                '</div></div></td></tr>';
        }
    };
    str += '</tbody><tfoot <tr> <th>Nombre Usuario</th> <th>Nick</th> <th>Rol</th> <th>Correo</th> <th>Celular</th> <th>Estado</th> <th>Modificar</th> <th>Habilitar/<br>Deshabilitar</th> </tr> </tfoot></table>';
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

//Función para cargar los elementos en el combobox de s de Usuario
function cargarRolesUpdateCmb() {
    var str = '<select id="rolCmb" class="form-control" name="IdRol" required>';
    str += '<option value="">Escoga una opción...</option>';
    for (var i = 0; i < datosRoles.length; i++) {
        str += '<option value="' + datosRoles[i].IdRol + '">' + datosRoles[i].NombreRol + '</option>';
    };
    str += '</select>';
    $("#cargarRolesUpdate").html(str);
}

//Función para setear los valores en los inputs
function formUpdateUsuario(idUsuario) {
    console.log(idUsuario);
    idUsuarioModificar = idUsuario;
    for (var i = 0; i < datosUsuarios.length; i++) {
       
        if (datosUsuarios[i].IdUsuario == idUsuario) {
            //Métodos para setear los valores a modificar
            var element = document.getElementById("rolCmb");
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
            if (estado==false && valor == true) {
                document.getElementById("HabilitadoUsuario").click();
            }
        };       
    };
}

//Función para modificar el usuario especificado
function modificarUsuario(url_modificar,url_Usu) {
    console.log(url_modificar);
    var cmbRol = document.getElementById("rolCmb");
    var idRol = cmbRol.options[cmbRol.selectedIndex].value;
    var nombreUsuario =document.getElementById("NombresUsuario").value;
    var correoUsuario =document.getElementById("CorreoUsuario").value;
    var nickUsuario=document.getElementById("NickUsuario").value;
    var passwordUsuario =document.getElementById("PasswordUsuario").value;
    var telefonoUsuario=document.getElementById("TelefonoUsuario").value;
    var celularUsuario=document.getElementById("TelefonoCelUsuario").value;
    var direccionUsuario=document.getElementById("DireccionUsuario").value;
    var habilitadoUsuario = $('#HabilitadoUsuario').prop('checked');

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
            //Método ajax para modificar el usuario de la base de datos
            $.ajax({
                data: { "IdUsuario": idUsuarioModificar, "IdRol": idRol, "NombresUsuario": nombreUsuario, "CorreoUsuario": correoUsuario, "NickUsuario": nickUsuario, "PasswordUsuario": passwordUsuario, "TelefonoUsuario": telefonoUsuario, "TelefonoCelUsuario": celularUsuario, "DireccionUsuario": direccionUsuario, "HabilitadoUsuario": habilitadoUsuario },
                url: url_modificar,
                type: 'post',
                success: function () {
                    $('#ModificarUsuario').modal('hide');                    
                    showNotify("Actualización exitosa", 'El usuario se ha modificado correctamente', "success");
                    obtenerUsuariosUpdate(url_Usu);
                }, error: function () {
                    $('#ModificarUsuario').modal('hide');
                    showNotify("Error en la Actualización", 'No se ha podido modificar el usuario', "error");
                }
            });

        } else {
            $('#ModificarUsuario').modal('hide');
        }
    });


}

//Función para evitar correos electronicos repertidos
function comprobarCorreo(correo) {
    console.log(correo)
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
        document.getElementById("NickUsuario").setCustomValidity("El nick de usuario: <strong> " + nick + " </strong> ya existe");
    } else {
        document.getElementById("NickUsuario").setCustomValidity("");
    }
}




