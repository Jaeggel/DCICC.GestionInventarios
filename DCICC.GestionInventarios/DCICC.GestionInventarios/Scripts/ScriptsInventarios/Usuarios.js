var url_idioma = obtenerIdioma();
var url_metodo;
var datosUsuarios;
var cmbRoles;
var idUsuarioModificar;
var nick;

function obtenerUsuarios(url) {
    url_metodo = url;
    console.log(url);
    //Método ajax para traer las marcas de la base de datos
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            console.log(data);
            datosUsuarios = data;
            cargarUsuariosTabla();
            $('#dataTableUsuarios').DataTable({
                "language": {
                    "url": url_idioma
                }
            });
        }
    });
}

function obtenerRoles(url) {
    console.log(url);
    //Método ajax para traer las marcas de la base de datos
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            console.log(data);
            cmbRoles = data;
            cargarRolesCmb();
        }
    });
}


//Función para cargar la tabla de Usuarios
function cargarUsuariosTabla() {
    var nick = document.getElementById("usuarioActual").innerHTML;
    console.log(nick);
    var str = '<table id="dataTableUsuarios" class="table jambo_table bulk_action  table-bordered" style="width:100%">';
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
        str += '</td><td><div class="text-center"><div class="col-md-12 col-sm-12 col-xs-12">' +
            '<button type="button" class="btn btn-info text-center" data-toggle="modal" data-target="#ModificarUsuario" onclick = "formUpdateUsuario(' + datosUsuarios[i].IdUsuario + ');"> <strong><i class="fa fa-pencil-square-o"></i></strong></button> ' +
            '</div></div>' +
            '</td><td><div class=" text-center"><div class="col-md-12 col-sm-12 col-xs-12">' +
            '<button type = "button" class="btn btn-danger text-center" > <strong><i class="fa fa-times-circle"></i></strong></button> ' +
            '</div></div></td></tr>';
        }
    };
    str += '</tbody></table>';
    $("#tablaModificarUsuarios").html(str);
}

function cargarRolesCmb() {
    var str = '<select id="IdRol" class="form-control" name="IdRol" required>';
    str += '<option value="">Escoga una opción...</option>';
    for (var i = 0; i < cmbRoles.length; i++) {
        str += '<option value="' + cmbRoles[i].IdRol + '">' + cmbRoles[i].NombreRol + '</option>';
    };
    str += '</select>';
    $("#cargarRoles").html(str);
}

//Función para setear los valores en los inputs
function formUpdateUsuario(idUsuario) {
    console.log(idUsuario);
    idUsuarioModificar = idUsuario;
    for (var i = 0; i < datosUsuarios.length; i++) {

        if (datosUsuarios[i].IdUsuario == idUsuario) {
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
        };
    };
}

//Función para modificar el usuario especificado
function modificarUsuario(url_modificar) {
    console.log(url_modificar);
    var cmbRol = document.getElementById("IdRol");
    var idRol = cmbRol.options[cmbRol.selectedIndex].value;
    var nombreUsuario = document.getElementById("NombresUsuario").value;
    var correoUsuario = document.getElementById("CorreoUsuario").value;
    var nickUsuario = document.getElementById("NickUsuario").value;
    var passwordUsuario = document.getElementById("PasswordUsuario").value;
    var telefonoUsuario = document.getElementById("TelefonoUsuario").value;
    var celularUsuario = document.getElementById("TelefonoCelUsuario").value;
    var direccionUsuario = document.getElementById("DireccionUsuario").value;
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
                    obtenerUsuarios(url_metodo);
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
        if ((datosUsuarios[i].NickUsuario).toLowerCase() == nick) {
            comprobar = true;
        }
    }

    console.log(comprobar);
    if (comprobar == true) {
        document.getElementById("NickUsuario").setCustomValidity("El nick de usuario: " + nick + " ya existe");
    } else {
        document.getElementById("NickUsuario").setCustomValidity("");
    }
}