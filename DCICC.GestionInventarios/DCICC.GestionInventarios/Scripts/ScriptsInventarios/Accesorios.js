var url_idioma = obtenerIdioma();
var cmbEstados = listaEstadosActivos();
var url_metodo_accesorio;
var cmbTipoAccesorio;
var datosAccesorios;
var idAccesorioMod;
var nombresAccesorio = [];

//Método ajax para obtener los datos de categorias
function obtenerAccesorios(url) {
    url_metodo_accesorio = url;
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'get',
        success: function (data) {
            console.log("Datos Exitosos");
            datosAccesorios = data;
            cargarAccesoriosTabla();
            $('#dataTableAccesorios').DataTable({
                "language": {
                    "url": url_idioma
                },
                "order": [[1, "asc"]]
            });
            cargarNombresAccesorios();
        }
    });
}

function datosTipoAccesorio(url) {
    //url_metodo = url;
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'get',
        success: function (data) {
            cmbTipoAccesorio = data;
            cargarAccesoriosCmb();
            cargarAccesoriosIngresoCmb();

        }
    });
}

function cargarEstadosAccesoriosCmb() {
    var str = '<select id="EstadoAccesorio" class="form-control" name="EstadoAccesorio" required>';
    str += '<option value="">Escoga una opción...</option>';
    for (var i = 0; i < cmbEstados.length; i++) {
        str += '<option value="' + cmbEstados[i] + '">' + cmbEstados[i] + '</option>';
    }
    str += '</select>';
    $("#cargarEstadosAccesorio").html(str);
}


function cargarEstadosAccesoriosMod() {
    var str = '<select id="EstadoAccesorioMod" class="form-control" name="EstadoAccesorioMod" required>';
    str += '<option value="">Escoga una opción...</option>';
    for (var i = 0; i < cmbEstados.length; i++) {
        str += '<option value="' + cmbEstados[i] + '">' + cmbEstados[i] + '</option>';
    }
    str += '</select>';
    $("#cargarEstadosAccesorioMod").html(str);
}

function cargarAccesoriosCmb() {
    var str = '<select id="AccesorioActivo" class="form-control" name="AccesorioActivo" required>';
    str += '<option value="">Escoga una opción...</option>';
    for (var i = 0; i < cmbTipoAccesorio.length; i++) {
        str += '<option value="' + cmbTipoAccesorio[i].IdTipoAccesorio + '">' + cmbTipoAccesorio[i].NombreTipoAccesorio + '</option>';
    }
    str += '</select>';
    $("#cargarTipoAccesorio").html(str);
}

////////////FUNCIONES PARA INGRESO DE ACCESORIO////////////
function cargarEstadosAccesoriosIng() {
    var str = '<select id="EstadoAccesorioIng" class="form-control" name="EstadoAccesorioIng" required>';
    str += '<option value="">Escoga una opción...</option>';
    for (var i = 0; i < cmbEstados.length; i++) {
        str += '<option value="' + cmbEstados[i] + '">' + cmbEstados[i] + '</option>';
    }
    str += '</select>';
    $("#cargarEstadosAccesorioIngreso").html(str);
}

function cargarAccesoriosIngresoCmb() {
    var str = '<select id="AccesorioIngreso" class="form-control" name="AccesorioIngreso" required>';
    str += '<option value="">Escoga una opción...</option>';
    for (var i = 0; i < cmbTipoAccesorio.length; i++) {
        if (cmbTipoAccesorio[i].HabilitadoTipoAccesorio) {
            str += '<option value="' + cmbTipoAccesorio[i].IdTipoAccesorio + '">' + cmbTipoAccesorio[i].NombreTipoAccesorio + '</option>';
        }      
    }
    str += '</select>';
    $("#cargarAccesoriosIngreso").html(str);
}


//Función para cargar la tabla de Activos
function cargarAccesoriosTabla() {
    var str = '<table id="dataTableAccesorios" class="table jambo_table bulk_action  table-bordered " style="width:100%">';
    str += '<thead> <tr> <th>Tipo de Accesorio</th> <th>Nombre de Accesorio</th> <th>Activo al que pertenece:</th> <th>Serial de Accesorio</th> <th>Modelo de Accesorio</th> <th>Estado de Accesorio</th> <th>Modificar</th> <th>Cambiar Estado</th> </tr> </thead>';
    str += '<tbody>';
    for (var i = 0; i < datosAccesorios.length; i++) {
        if (datosAccesorios[i].EstadoAccesorio != "DE BAJA") {
            str += '<tr><td>' + datosAccesorios[i].NombreTipoAccesorio +
                '</td><td>' + datosAccesorios[i].NombreAccesorio +
                '</td><td>' + datosAccesorios[i].NombreDetalleActivo +
                '</td><td>' + datosAccesorios[i].SerialAccesorio +
                '</td><td>' + datosAccesorios[i].ModeloAccesorio +
                '</td><td>' + datosAccesorios[i].EstadoAccesorio;
            str += '</td><td><div class="text-center"><div class="col-md-12 col-sm-12 col-xs-12">' +
                '<button type="button" class="btn btn-info text-center" data-toggle="modal" data-target="#ModificarAccesorio" onclick = "formUpdateAccesorio(' + datosAccesorios[i].IdAccesorio + ');"> <strong><i class="fa fa-pencil-square-o"></i></strong></button> ' +
                '</div></div>' +
                '</td><td><div class="text-center"><div class="col-md-12 col-sm-12 col-xs-12">';
            if (datosAccesorios[i].EstadoAccesorio == "OPERATIVO") {
                str += '<button type = "button" class="btn btn-success text-center" data-toggle="modal" data-target="#ModificarEstadoAccesorio" onclick = "habilitarOdeshabilitarAcc(' + datosAccesorios[i].IdAccesorio + ');" > <strong><span class="fa fa-check"></span></strong></button> ';
            } else if (datosAccesorios[i].EstadoAccesorio == "NO OPERATIVO") {
                str += '<button type = "button" class="btn btn-warning text-center" data-toggle="modal" data-target="#ModificarEstadoAccesorio" onclick = "habilitarOdeshabilitarAcc(' + datosAccesorios[i].IdAccesorio + ');" > <strong><span class="fa fa-warning"></span></strong></button> ';
            } else {
                str += '<button type = "button" class="btn btn-danger text-center" data-toggle="modal" data-target="#ModificarEstadoAccesorio" onclick = "habilitarOdeshabilitarAcc(' + datosAccesorios[i].IdAccesorio + ');" > <strong><span class="fa fa-close"></span></strong></button> ';
            }

            str += '</div></div></td></tr>';
        }     
    }
    str += '</tbody>' +
        '</table > ';
    $("#tablaAccesorios").html(str);
}

function formUpdateAccesorio(idAccesorio) {
    idAccesorioMod = idAccesorio;
    for (var i = 0; i < datosAccesorios.length; i++) {
        if (datosAccesorios[i].IdAccesorio == idAccesorio) {
            
            var element = document.getElementById("AccesorioActivo");
            element.value = datosAccesorios[i].IdTipoAccesorio;
            
            
            document.getElementById("NombreActivoAccesorio").value = datosAccesorios[i].NombreDetalleActivo;
            
            document.getElementById("NombreAccesorio").value = datosAccesorios[i].NombreAccesorio;
            
            document.getElementById("SerialAccesorio").value = datosAccesorios[i].SerialAccesorio;

            document.getElementById("ModeloAccesorio").value = datosAccesorios[i].ModeloAccesorio;

            document.getElementById("DescripcionAccesorio").value = datosAccesorios[i].DescripcionAccesorio;

            var element3 = document.getElementById("EstadoAccesorio");
            element3.value = datosAccesorios[i].EstadoAccesorio;

            
            
        }
    };

}

function actualizarAccesorio(url) {

    //Obtener Valor del tipo de activo
    var cmbTipoAccesorio = document.getElementById("AccesorioActivo");
    var idTipoAccesorio = cmbTipoAccesorio.options[cmbTipoAccesorio.selectedIndex].value;
    
    var nombreAccesorio=document.getElementById("NombreAccesorio").value;

    var serialAccesorio=document.getElementById("SerialAccesorio").value;

    var modeloAccesorio=document.getElementById("ModeloAccesorio").value;

    var descripcionAccesorio = document.getElementById("DescripcionAccesorio").value;

    //Obtener valor del Estado
    var cmbEstadoAccesorio = document.getElementById("EstadoAccesorio");
    var idEstadoAccesorio = cmbEstadoAccesorio.options[cmbEstadoAccesorio.selectedIndex].value;

    if (validacionesModificacionAccesorio()) {
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
                    data: {
                        "IdAccesorio": idAccesorioMod, "IdTipoAccesorio": idTipoAccesorio, "EstadoAccesorio": idEstadoAccesorio, "NombreAccesorio": nombreAccesorio,
                        "SerialAccesorio": serialAccesorio, "ModeloAccesorio": modeloAccesorio, "DescripcionAccesorio": descripcionAccesorio
                    },
                    url: url,
                    dataType: 'json',
                    type: 'post',
                    success: function (data) {
                        console.log(data.OperacionExitosa);
                        if (data.OperacionExitosa) {
                            $('#ModificarAccesorio').modal('hide');
                            showNotify("Actualización exitosa", 'Se ha modificado el Accesorio', "success");
                            obtenerAccesorios(url_metodo_accesorio);
                        } else {
                            $('#ModificarAccesorio').modal('hide');
                            showNotify("Error en la Actualización", 'No se ha podido modificar el Accesorio: ' + data.MensajeError, "error");
                        }
                    }
                });

            } else {
                $('#ModificarAccesorio').modal('hide');
            }
        });
    }   

}

function habilitarOdeshabilitarAcc(idActivo) {
    idAccesorioMod = idActivo;
}

function actualizarEstadoAccesorios(urlAccesorio) {

    var cmbEstado = document.getElementById("EstadoAccesorioMod");
    var idEstado = cmbEstado.options[cmbEstado.selectedIndex].value;

    if (validacionesEstadoAccesorio()) {
        swal({
            title: 'Confirmación de Actualización',
            text: "¿Está seguro de modificar el estado del Activo?",
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
                        "IdAccesorio": idAccesorioMod, "EstadoAccesorio": idEstado
                    },
                    url: urlAccesorio,
                    type: 'post',
                    success: function (data) {
                        console.log(data.OperacionExitosa);
                        if (data.OperacionExitosa) {
                            $('#ModificarEstadoAccesorio').modal('hide');
                            showNotify("Actualización exitosa", 'Se ha modificado el Estado del Accesorio', "success");
                            obtenerAccesorios(url_metodo_accesorio);
                        } else {
                            $('#ModificarEstadoAccesorio').modal('hide');
                            showNotify("Error en la Actualización", 'No se ha podido modificar el Estado del Accesorio: ' + data.MensajeError, "error");
                        }

                    }
                });
            } else {
                $('#ModificarEstadoAccesorio').modal('hide');

            }
        });
    }

}

//función para validar campos de modificacion de accesorios
function validacionesModificacionAccesorio() {
    var isValid = true;
    var nombreActivo = document.getElementById("NombreAccesorio").value;
    var modeloActivo = document.getElementById("ModeloAccesorio").value;
    var serialActivo = document.getElementById("SerialAccesorio").value;

    //Validación para combobox tipo de Activo
    if (document.getElementById("AccesorioActivo").value == "") {
        isValid = false;
        document.getElementById("AccesorioActivo").style.borderColor = "#900C3F";
        $('#errorTipoAccesorio').html('Debe seleccionar una Opción del Tipo de Activo').show();
        setTimeout("$('#errorTipoAccesorio').html('').hide('slow')", 6000);
    } else {
        document.getElementById("AccesorioActivo").style.borderColor = "#ccc";
        $('#errorTipoAccesorio').html('').hide();
    }

    //Validación para combobox estado de Activo
    if (document.getElementById("EstadoAccesorio").value == "") {
        isValid = false;
        document.getElementById("EstadoAccesorio").style.borderColor = "#900C3F";
        $('#errorEstadoAccesorio').html('Debe seleccionar una Opción de Estado').show();
        setTimeout("$('#errorEstadoAccesorio').html('').hide('slow')", 6000);
    } else {
        document.getElementById("EstadoAccesorio").style.borderColor = "#ccc";
        $('#errorEstadoAccesorio').html('').hide();
    }

    //Validación para el nombre de Activo
    if (!nombreActivo && nombreActivo.length <= 0) {
        isValid = false;
        document.getElementById("NombreAccesorio").style.borderColor = "#900C3F";
        $('#errorNombreAccesorio').html('El campo Nombre del Activo es obligatorio').show();
        setTimeout("$('#errorNombreAccesorio').html('').hide('slow')", 6000);
    } else {
        document.getElementById("NombreAccesorio").style.borderColor = "#ccc";
        $('#errorNombreAccesorio').html('').hide();
    }
    //Validación para el modelo del activo
    if (!modeloActivo && modeloActivo.length <= 0) {
        isValid = false;
        document.getElementById("ModeloAccesorio").style.borderColor = "#900C3F";
        $('#errorModeloAccesorio').html('El campo Modelo de Activo es obligatorio').show();
        setTimeout("$('#errorModeloAccesorio').html('').hide('slow')", 6000);
    } else {
        document.getElementById("ModeloAccesorio").style.borderColor = "#ccc";
        $('#errorModeloAccesorio').html('').hide();
    }
    //Validación para el serial del activo
    if (!serialActivo && serialActivo.length <= 0) {
        isValid = false;
        document.getElementById("SerialAccesorio").style.borderColor = "#900C3F";
        $('#errorSerialAccesorio').html('El campo Serial de Activo es obligatorio').show();
        setTimeout("$('#errorSerialAccesorio').html('').hide('slow')", 6000);
    } else {
        document.getElementById("SerialAccesorio").style.borderColor = "#ccc";
        $('#errorSerialAccesorio').html('').hide();
    }

    return isValid;
}

//Función para validar el estado de accesorio
function validacionesEstadoAccesorio() {
    var isValid = true;

    //Validación para combobox tipo de Activo
    if (document.getElementById("EstadoAccesorioMod").value == "") {
        isValid = false;
        document.getElementById("EstadoAccesorioMod").style.borderColor = "#900C3F";
        $('#errorEstadoAcc').html('Debe seleccionar una Opción del Estado de Accesorio').show();
        console.log("ss");
        setTimeout("$('#errorEstadoAcc').html('').hide('slow')", 6000);
    } else {
        document.getElementById("EstadoAccesorioMod").style.borderColor = "#ccc";
        $('#errorEstadoAcc').html('').hide();
    }
    return isValid;
}

/////////////////////////Funciones para cargar el campo de autocompletado
function cargarNombresAccesorios() {
    for (var i = 0; i < datosAccesorios.length; i++) {
        nombresAccesorio[i] = datosAccesorios[i].NombreAccesorio;
    }
}
//Función para cargar los nombres en el campo de nombre de laboratorios
$(function () {
    $("#NombreAccesorioIngreso").autocomplete({
        source: nombresAccesorio
    });
});

//Función para cargar los nombres en el campo de nombre de laboratorios
$(function () {
    $("#NombreAccesorio").autocomplete({
        source: nombresAccesorio
    });
});

//Función para evitar nombres de laboratorios repetidos
function comprobarNombreAccesorio() {
    var nombreAcc = document.getElementById("NombreAccesorioIngreso").value;
    var comprobar = false;
    for (var i = 0; i < nombresAccesorio.length; i++) {
        if ((nombresAccesorio[i]).toUpperCase() == nombreAcc) {
            comprobar = true;
        }
    }
    if (comprobar) {
        document.getElementById("NombreAccesorioIngreso").value = "";
        document.getElementById("NombreAccesorioIngreso").style.borderColor = "#900C3F";
        $('#errorNombreAccesorioIng').html('El Nombre del Accesorio ya existe').show();
        setTimeout("$('#errorNombreAccesorioIng').html('').hide('slow')", 6000);
    } else {
        document.getElementById("NombreAccesorioIngreso").style.borderColor = "#ccc";
        $('#errorNombreAccesorioIng').html('').hide();
    }
}