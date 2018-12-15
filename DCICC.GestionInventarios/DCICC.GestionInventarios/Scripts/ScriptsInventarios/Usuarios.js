var url_idioma = obtenerIdioma();
var url_metodo;
var datosUsuarios;
var cmbRoles;
var idUsuarioModificar;
var nick;
var urlEstado;
var nombresUsuarios = [];

//Método ajax para obtener los datos de los usuarios
function obtenerUsuarios(url) {
    url_metodo = url;
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            if (data.OperacionExitosa) {
                datosUsuarios = data.ListaObjetoInventarios;
                cargarUsuariosTabla();
                $('#dataTableUsuarios').DataTable({
                    "language": {
                        "url": url_idioma
                    }
                });
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
            if (data.OperacionExitosa) {
                cmbRoles = data.ListaObjetoInventarios;
                cargarRolesCmb();
            } else {
                showNotify("Error en la Consulta", 'No se ha podido mostrar los datos: ' + data.MensajeError, "error");
            }       
        }
    });
}


//Función para cargar la tabla de Usuarios
function cargarUsuariosTabla() {
    var nick = document.getElementById("usuarioActual").innerHTML;
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
                '</td><td><div class=" text-center"><div class="col-md-12 col-sm-12 col-xs-12">';
            if (datosUsuarios[i].HabilitadoUsuario) {
                str += '<button type = "button" class="btn btn-success text-center" onclick = "habilitarOdeshabilitar(' + datosUsuarios[i].IdUsuario + ',' + datosUsuarios[i].HabilitadoUsuario + ');"> <strong><span class="fa fa-toggle-on"></span></strong></button> ';
            } else {
                str += '<button type = "button" class="btn btn-danger text-center" onclick = "habilitarOdeshabilitar(' + datosUsuarios[i].IdUsuario + ',' + datosUsuarios[i].HabilitadoUsuario + ');"> <strong><i class="fa fa-toggle-off"></i></strong></button> ';
            }
            str += '</div></div></td></tr>';
        }
    }
    str += '</tbody></table>';
    $("#tablaModificarUsuarios").html(str);
}

//Función para cargar el combobox de roles
function cargarRolesCmb() {
    var str = '<select id="IdRol" class="form-control" name="IdRol" onBlur = "validarCmbRol();" required>';
    str += '<option value="">Escoga una opción...</option>';
    for (var i = 0; i < cmbRoles.length; i++) {
        str += '<option value="' + cmbRoles[i].IdRol + '">' + cmbRoles[i].NombreRol + '</option>';
    }
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
        }
    }
}

//Función para modificar el usuario especificado
function modificarUsuario(url_modificar) {
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

    if (validarInputNombre() && validarInputCorreo() && validarInputNick() && validarInputPass() && validarCmbRol()) {
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
                $.ajax({
                    data: { "IdUsuario": idUsuarioModificar, "IdRol": idRol, "NombresUsuario": nombreUsuario, "CorreoUsuario": correoUsuario, "NickUsuario": nickUsuario, "PasswordUsuario": passwordUsuario, "TelefonoUsuario": telefonoUsuario, "TelefonoCelUsuario": celularUsuario, "DireccionUsuario": direccionUsuario, "HabilitadoUsuario": habilitadoUsuario },
                    url: url_modificar,
                    type: 'post',
                    success: function (data) {
                        console.log(data.OperacionExitosa);
                        if (data.OperacionExitosa) {
                            $('#ModificarUsuario').modal('hide');
                            showNotify("Actualización exitosa", 'El usuario se ha modificado correctamente', "success");
                            obtenerUsuarios(url_metodo);
                        } else {
                            $('#ModificarUsuario').modal('hide');
                            showNotify("Error en la Actualización", 'No se ha podido modificar el usuario: ' + data.MensajeError, "error");
                        }

                    }
                });

            } else {
                $('#ModificarUsuario').modal('hide');
            }
        });
    }

    
}



//Función para obtener la url de modificación
function urlEstados(url) {
    urlEstado = url;
}

//Función para habilitar o deshabilitar la categoria
function habilitarOdeshabilitar(idUsuario, estadoUsuario) {
    var nuevoEstado = true;
    if (estadoUsuario) {
        nuevoEstado = false;
    } else {
        nuevoEstado = true;
    }
    console.log(nuevoEstado);
    swal({
        title: 'Confirmación de Cambio de Estado',
        text: "¿Está seguro de Cambiar de Estado del Usuario?",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#26B99A',
        cancelButtonColor: '#337ab7',
        confirmButtonText: 'Confirmar',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                data: { "IdUsuario": idUsuario, "HabilitadoUsuario": nuevoEstado },
                url: urlEstado,
                type: 'post',
                success: function (data) {
                    console.log(data.OperacionExitosa);
                    if (data.OperacionExitosa) {
                        showNotify("Actualización exitosa", 'El estado del Usuario se ha modificado correctamente', "success");
                        obtenerUsuarios(url_metodo);
                    } else {
                        showNotify("Error en la Actualización", 'No se ha podido modificar el estado del Usuario: ' + data.MensajeError, "error");
                    }
                }
            });
        } else {

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
function comprobarNick() {
    var nick = document.getElementById("NickUsuario");
    nick.value = nick.value.toLowerCase();
    if (nick.value.length <= 0) {
        nick.style.borderColor = "#900C3F";
        $('#errorNick').html('El campo nick de usuario no debe estar vacio').show();
        setTimeout("$('#errorNick').html('').hide('slow')", 6000);
    } else {
        for (var i = 0; i < datosUsuarios.length; i++) {
            if ((datosUsuarios[i].NickUsuario).toLowerCase() == nick.value) {
                nick.style.borderColor = "#900C3F";
                $('#errorNick').html("El nick de usuario: " + nick.value + " ya existe").show();
                setTimeout("$('#errorNick').html('').hide('slow')", 6000);
                nick.value = "";
                break;
            } else {
                nick.style.borderColor = "#ccc";
                $('#errorNick').html('').hide();
            }
        }
    }
}

/////////////////////////Funciones para cargar el campo de autocompletado
function cargarNombresUsuarios() {
    for (var i = 0; i < datosUsuarios.length; i++) {
        nombresUsuarios[i] = datosUsuarios[i].NickUsuario;
    }
}
//Función para cargar los nombres en el campo de nombre de laboratorios
$(function () {
    $("#NickUsuario").autocomplete({
        source: nombresUsuarios
    });
});

/////////////Funciones para validaciones de campos de texto
function validarInputNombre() {
    var esValido = true;
    var boton = document.getElementById("confirmarUsuario");
    var nomUsu = document.getElementById("NombresUsuario");
    //Validación para el campo de texto nombre de laboratorio
    if (nomUsu.value.length <= 0) {
        nomUsu.value = "";
        nomUsu.style.borderColor = "#900C3F";
        $('#errorNombreCompleto').html('El campo nombre no debe estar vacio').show();
        setTimeout("$('#errorNombreCompleto').html('').hide('slow')", 6000);
        boton.disabled = true;
    } else {
        nomUsu.style.borderColor = "#ccc";
        $('#errorNombreCompleto').html('').hide();
        boton.disabled = false;
    }
    return esValido;
}

/////////////Funciones para validaciones de campos de texto
function validarInputCorreo() {
    var esValido = true;
    var boton = document.getElementById("confirmarUsuario");
    var correo = document.getElementById("CorreoUsuario");

    //Validación para el campo de texto nombre de Máquina virtual
    if (correo.value.length <= 0) {
        esValido = false;
        correo.style.borderColor = "#900C3F";
        $('#errorCorreo').html('El campo correo no debe estar vacio').show();
        setTimeout("$('#errorCorreo').html('').hide('slow')", 6000);
        boton.disabled = true;
    } else {
        correo.style.borderColor = "#ccc";
        $('#errorCorreo').html('').hide();
        boton.disabled = false;
    }
    return esValido;

}

//Función para validar el nombre de Usuario
function validarInputNick() {
    var esValido = true;
    var boton = document.getElementById("confirmarUsuario");
    var nick = document.getElementById("NickUsuario");

    if (nick.value.length <= 0) {
        esValido = false;
        nick.style.borderColor = "#900C3F";
        $('#errorNick').html('El campo nick de usuario no debe estar vacio').show();
        setTimeout("$('#errorNick').html('').hide('slow')", 6000);
        boton.disabled = true;
    } else {
        nick.style.borderColor = "#ccc";
        $('#errorNick').html('').hide();
        boton.disabled = false;
    }
    return esValido;
}

//Función para validar dirección IP
function validarInputPass() {
    var esValido = true;
    var boton = document.getElementById("confirmarUsuario");
    var passw = document.getElementById("PasswordUsuario");
    //Validación para el campo de texto nombre de Máquina virtual
    if (passw.value.length <= 0) {
        esValido = false;
        passw.style.borderColor = "#900C3F";
        $('#errorPassword').html('El campo password no debe estar vacio').show();
        setTimeout("$('#errorPassword').html('').hide('slow')", 6000);
        boton.disabled = true;
    } else if (passw.value.length > 0 && passw.value.length <= 6) {
        esValido = false;
        passw.style.borderColor = "#900C3F";
        $('#errorPassword').html('El password debe ser mayor a 6 caracteres').show();
        setTimeout("$('#errorPassword').html('').hide('slow')", 6000);
        boton.disabled = true;
    }else {
        passw.style.borderColor = "#ccc";
        $('#errorPassword').html('').hide();
        boton.disabled = false;
    }
    return esValido;
}


///Función para validar los combobox de maquinas virtuales
function validarCmbRol() { 
    var esValido = true;
    var boton = document.getElementById("confirmarUsuario");
    var cmbRol = document.getElementById("IdRol");
    
    //Validación para el combobox de SO
    if (cmbRol.value == "") {
        esValido = false;
        cmbRol.style.borderColor = "#900C3F";
        $('#errorRol').html('Debe seleccionar una opción').show();
        setTimeout("$('#errorRol').html('').hide('slow')", 6000);
        boton.disabled = true;
    } else {
        cmbRol.style.borderColor = "#ccc";
        $('#errorRol').html('').hide();
        boton.disabled = false;
    }
    return esValido;
}

//Mensajes para los tooltips
function mensajesTooltips() {
    document.getElementById("NombresUsuario").title = "Máximo 80 caracteres.\n No se puede ingresar caracteres especiales.";
    document.getElementById("CorreoUsuario").title = "Máximo 50 caracteres.\n No se puede ingresar caracteres especiales ni espacios.";
    document.getElementById("NickUsuario").title = "Máximo 15 caracteres en Mayúscula.\n No se puede ingresar caracteres especiales ni espacios.";
    document.getElementById("PasswordUsuario").title = "Máximo 20 caracteres en Mayúscula.\n No se puede ingresar caracteres especiales ni espacios.";
    document.getElementById("TelefonoUsuario").title = "10 números, incluyendo el código de la provincia (02).";
    document.getElementById("TelefonoCelUsuario").title = "10 números, empezando con el código (09)";
    document.getElementById("DireccionUsuario").title = "Máximo 80 caracteres.\n No se puede ingresar caracteres especiales.";
}
