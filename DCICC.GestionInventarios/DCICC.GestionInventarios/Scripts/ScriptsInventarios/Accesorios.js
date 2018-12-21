var url_idioma = obtenerIdioma();
var cmbEstados = listaEstadosActivos();
var url_metodo_accesorio;
var cmbTipoAccesorio;
var datosAccesorios;
var idAccesorioMod;
var NombreAccesorioMod;
var nombresAccesorio = [];
var rol;
var url_bloquear_acc;

/* --------------------------------------SECCIÓN PARA OBTENER DATOS DEL SERVIDOR---------------------------------*/
//Método ajax para obtener los datos de categorias
function obtenerAccesorios(url) {
    url_metodo_accesorio = url;
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            if (data.OperacionExitosa) {
                datosAccesorios = data.ListaObjetoInventarios;
                cargarAccesoriosTabla();
                $('#dataTableAccesorios').DataTable({
                    "language": {
                        "url": url_idioma
                    },
                    "order": [[1, "asc"]]
                });
                cargarNombresAccesorios();
            } else {
                showNotify("Error en la Consulta", 'No se ha podido mostrar los datos: ' + data.MensajeError, "error");
            }

        }
    });
}

function datosTipoAccesorio(url) {
    //url_metodo = url;
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            if (data.OperacionExitosa) {
                cmbTipoAccesorio = data.ListaObjetoInventarios;
                cargarAccesoriosCmb();
                cargarAccesoriosIngresoCmb();
                TipoAccesoriosFiltro();
            } else {
                showNotify("Error en la Consulta", 'No se ha podido mostrar los datos: ' + data.MensajeError, "error");
            }
        }
    });
}

/* --------------------------------------SECCIÓN PARA CARGAR TABLAS Y COMBOBOX---------------------------------*/
//Función para cargar el combobox de estados de accesorios
function cargarEstadosAccesoriosCmb() {
    var str = '<select id="EstadoAccesorio" class="form-control" name="EstadoAccesorio" required>';
    for (var i = 0; i < cmbEstados.length; i++) {
        str += '<option value="' + cmbEstados[i] + '">' + cmbEstados[i] + '</option>';
    }
    str += '</select>';
    $("#cargarEstadosAccesorio").html(str);
}

//Función para cargar el combobox de cambio de estado de un accesorio
function cargarEstadosAccesoriosMod() {
    var str = '<select id="EstadoAccesorioMod" class="form-control" name="EstadoAccesorioMod" required>';
    str += '<option value="">Escoga una opción...</option>';
    for (var i = 0; i < cmbEstados.length; i++) {
        str += '<option value="' + cmbEstados[i] + '">' + cmbEstados[i] + '</option>';
    }
    str += '</select>';
    $("#cargarEstadosAccesorioMod").html(str);
}

//Función para cargar el combobox de tipos de accesorios
function cargarAccesoriosCmb() {
    var str = '<select id="AccesorioActivo" class="form-control" name="AccesorioActivo" required>';
    for (var i = 0; i < cmbTipoAccesorio.length; i++) {
        str += '<option value="' + cmbTipoAccesorio[i].IdTipoAccesorio + '">' + cmbTipoAccesorio[i].NombreTipoAccesorio + '</option>';
    }
    str += '</select>';
    $("#cargarTipoAccesorio").html(str);
}

//Función para cargar el combobox de estados para ingreso de accesorios
function cargarEstadosAccesoriosIng() {
    var str = '<select id="EstadoAccesorioIng" class="form-control" name="EstadoAccesorioIng" required>';
    str += '<option value="">Escoga una opción...</option>';
    for (var i = 0; i < cmbEstados.length; i++) {
        if (cmbEstados[i] != "DE BAJA")
        str += '<option value="' + cmbEstados[i] + '">' + cmbEstados[i] + '</option>';
    }
    str += '</select>';
    $("#cargarEstadosAccesorioIngreso").html(str);
}

//Función para cargar el combobox de tipos de accesorios para ingreso
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

/* --------------------------------------SECCIÓN PARA CARGAR COMBOBOX DE FILTROS---------------------------------*/
//Función para cargar el combobox de tipos de accesorios
function TipoAccesoriosFiltro() {
    var str = '<select id="TipoAccesoriosFiltro" class="form-control" name="TipoAccesoriosFiltro" required>';
    str += '<option value="">Mostrar Todos</option>';
    for (var i = 0; i < cmbTipoAccesorio.length; i++) {
        str += '<option value="' + cmbTipoAccesorio[i].IdTipoAccesorio + '">' + cmbTipoAccesorio[i].NombreTipoAccesorio + '</option>';
    }
    str += '</select>';
    $("#cargarTipoAccesorioFiltro").html(str);
    //Método para búsqueda con filtros
    $('#TipoAccesoriosFiltro').change(function () {
        var opcion = document.getElementById("TipoAccesoriosFiltro");
        var tipoLab = opcion.options[opcion.selectedIndex];
        if (tipoLab.value == "") {
            $('#dataTableAccesorios').DataTable().column(0).search(
                ""
            ).draw();
        } else {
            $('#dataTableAccesorios').DataTable().column(0).search('^' + tipoLab.text + '$', true, false
            ).draw();
        }
    });
}

//Función para cargar el combobox de estados para ingreso de accesorios
function EstadosAccesoriosFiltro() {
    var str = '<select id="EstadosAccesoriosFiltro" class="form-control" name="EstadosAccesoriosFiltro" required>';
    str += '<option value="">Mostrar Todos</option>';
    for (var i = 0; i < cmbEstados.length; i++) {
        if (cmbEstados[i] !="DE BAJA") {
            str += '<option value="' + cmbEstados[i] + '">' + cmbEstados[i] + '</option>';
        }      
    }
    str += '</select>';
    $("#cargarEstadosAccesorioFiltro").html(str);
    //Método para búsqueda con filtros
    $('#EstadosAccesoriosFiltro').change(function () {
        var opcion = document.getElementById("EstadosAccesoriosFiltro");
        var tipoLab = opcion.options[opcion.selectedIndex];
        if (tipoLab.value == "") {
            $('#dataTableAccesorios').DataTable().column(5).search(
                ""
            ).draw();
        } else {
            $('#dataTableAccesorios').DataTable().column(5).search('^' + tipoLab.text + '$', true, false
            ).draw();
        }
    });
}


//Función para cargar la tabla de Activos
function cargarAccesoriosTabla() {
    var str = '<table id="dataTableAccesorios" class="table jambo_table bulk_action  table-bordered " style="width:100%">';
    str += '<thead> <tr> <th>Tipo de Accesorio</th> <th>Nombre de Accesorio</th> <th>Activo al que pertenece</th> <th>Serial</th> <th>Modelo</th> <th>Estado de Accesorio</th> <th>Modificar</th> <th>Cambiar Estado</th> </tr> </thead>';
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
                '<button id="modificar" type="button" class="btn btn-info text-center" data-toggle="modal" data-target="#ModificarAccesorio" onclick = "formUpdateAccesorio(' + datosAccesorios[i].IdAccesorio + ');"> <strong><i class="fa fa-pencil-square-o"></i></strong></button> ' +
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
    BloquearbotonesAccesorios();
}

/* --------------------------------------SECCIÓN PARA MODIFICACION DE DATOS---------------------------------*/
//Función para setear los inputs con el accesorio seleccionado
function formUpdateAccesorio(idAccesorio) {
    idAccesorioMod = idAccesorio;
    for (var i = 0; i < datosAccesorios.length; i++) {
        if (datosAccesorios[i].IdAccesorio == idAccesorio) {
            NombreAccesorioMod=datosAccesorios[i].NombreAccesorio;
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
    }

}

//Función para actualizar un accesorio
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
                            showNotify("Actualización exitosa", 'El Accesorio " ' + nombreAccesorio.toUpperCase() + ' " se ha modificado exitosamente', "success");
                            obtenerAccesorios(url_metodo_accesorio);
                        } else {
                            $('#ModificarAccesorio').modal('hide');
                            showNotify("Error en la Actualización", 'Ocurrió un error al modificar el Accesorio: ' + data.MensajeError, "error");
                        }
                    }
                });

            } else {
                $('#ModificarAccesorio').modal('hide');
            }
        });
    }   

}

//Función para cambiar el estado de un accesorio
function habilitarOdeshabilitarAcc(idActivo) {
    idAccesorioMod = idActivo;
}
//Función para cambiar el estado de un accesorio
function actualizarEstadoAccesorios(urlAccesorio) {

    var cmbEstado = document.getElementById("EstadoAccesorioMod");
    var idEstado = cmbEstado.options[cmbEstado.selectedIndex].value;

    if (validacionesEstadoAccesorio()) {
        swal({
            title: 'Confirmación de Actualización',
            text: "¿Está seguro de cambiar el estado del registro?",
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
                            showNotify("Actualización exitosa", 'Estado del Accesorio se ha modificado exitosamente', "success");
                            obtenerAccesorios(url_metodo_accesorio);
                        } else {
                            $('#ModificarEstadoAccesorio').modal('hide');
                            showNotify("Error en la Actualización", 'Ocurrió un error al modificar el estado del Accesorio: ' + data.MensajeError, "error");
                        }

                    }
                });
            } else {
                $('#ModificarEstadoAccesorio').modal('hide');

            }
        });
    }

}

/* --------------------------------------SECCIÓN PARA CAMPOS DE AUTOCOMPLETE---------------------------------*/
//Funciones para cargar el campo de autocompletado
function cargarNombresAccesorios() {
    for (var i = 0; i < datosAccesorios.length; i++) {
        nombresAccesorio[i] = datosAccesorios[i].NombreAccesorio;
    }
}
//Función para cargar los nombres en el campo de nombre de accesorios para ingresos
$(function () {
    $("#NombreAccesorioIngreso").autocomplete({
        source: nombresAccesorio
    });
});

//Función para cargar los nombres en el campo de nombre de accesorios para modificaciones
$(function () {
    $("#NombreAccesorio").autocomplete({
        source: nombresAccesorio
    });
});

/* --------------------------------------SECCIÓN PARA COMPROBACIONES Y VALIDACIONES---------------------------------*/
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
        $('#errorTipoAccesorio').html('Debe seleccionar una Opción de Tipo de Activo').show();
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
        $('#errorNombreAccesorio').html('El campo Nombre de Accesorio es obligatorio').show();
        setTimeout("$('#errorNombreAccesorio').html('').hide('slow')", 6000);
    } else {
        document.getElementById("NombreAccesorio").style.borderColor = "#ccc";
        $('#errorNombreAccesorio').html('').hide();
    }
    //Validación para el modelo del activo
    if (!modeloActivo && modeloActivo.length <= 0) {
        isValid = false;
        document.getElementById("ModeloAccesorio").style.borderColor = "#900C3F";
        $('#errorModeloAccesorio').html('El campo Modelo de Accesorio es obligatorio').show();
        setTimeout("$('#errorModeloAccesorio').html('').hide('slow')", 6000);
    } else {
        document.getElementById("ModeloAccesorio").style.borderColor = "#ccc";
        $('#errorModeloAccesorio').html('').hide();
    }
    //Validación para el serial del activo
    if (!serialActivo && serialActivo.length <= 0) {
        isValid = false;
        document.getElementById("SerialAccesorio").style.borderColor = "#900C3F";
        $('#errorSerialAccesorio').html('El campo Serial de Accesorio es obligatorio').show();
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
        $('#errorEstadoAcc').html('Debe seleccionar una Opción de Estado de Accesorio').show();
        console.log("ss");
        setTimeout("$('#errorEstadoAcc').html('').hide('slow')", 6000);
    } else {
        document.getElementById("EstadoAccesorioMod").style.borderColor = "#ccc";
        $('#errorEstadoAcc').html('').hide();
    }
    return isValid;
}

//Función para evitar nombres de accesorios repetidos
function comprobarNombreAccesorio() {
    var nombreAcc = document.getElementById("NombreAccesorioIngreso").value;
    var comprobar = false;
    for (var i = 0; i < nombresAccesorio.length; i++) {
        if ((nombresAccesorio[i]).toUpperCase() == nombreAcc.toUpperCase()) {
            comprobar = true;
        }
    }
    if (comprobar) {
        document.getElementById("NombreAccesorioIngreso").value = "";
        document.getElementById("NombreAccesorioIngreso").style.borderColor = "#900C3F";
        $('#errorNombreAccesorioIng').html('El nombre de Accesorio: '+nombreAcc.toUpperCase() + ' ya existe').show();
        setTimeout("$('#errorNombreAccesorioIng').html('').hide('slow')", 6000);
    } else {
        document.getElementById("NombreAccesorioIngreso").style.borderColor = "#ccc";
        $('#errorNombreAccesorioIng').html('').hide();
    }
}

//Función para evitar nombres de laboratorios repetidos
function validarNombreAccesorioModificar() {
    var nombreAcc = document.getElementById("NombreAccesorio");
    nombreAcc.value = nombreAcc.value.toUpperCase();
    console.log(nombreAcc.value + " - " + NombreAccesorioMod.toUpperCase());
    if (nombreAcc.value != NombreAccesorioMod.toUpperCase()) {
        for (var i = 0; i < nombresAccesorio.length; i++) {
            if ((nombresAccesorio[i]).toUpperCase() == nombreAcc.value) {
                nombreAcc.style.borderColor = "#900C3F";
                $('#errorNombreAccesorio').html('El nombre de Accesorio: ' + nombreAcc.value + ' ya existe').show();
                setTimeout("$('#errorNombreAccesorio').html('').hide('slow')", 6000);
                nombreAcc.value = "";
                break;
            } else {
                nombreAcc.style.borderColor = "#ccc";
                $('#errorNombreAccesorio').html('').hide();
            }
        }
    } else {
        nombreAcc.style.borderColor = "#ccc";
        $('#errorNombreAccesorio').html('').hide();
    }
}

/* --------------------------------------SECCIÓN PARA OPERACIONES CON USUARIO INVITADO---------------------------------*/
//Función para bloquear botones cuando el usuario es invitado
function botonesAccesorios(url) {
    url_bloquear_acc = url;
}

function BloquearbotonesAccesorios() {
    $.ajax({
        dataType: 'json',
        url: url_bloquear_acc,
        type: 'post',
        success: function (data) {
            rol = data;
            if (data == "Invitado") {
                $(':button').prop('disabled', true);
                var table = $('#dataTableAccesorios').DataTable();
                var rows = table.rows({ 'search': 'applied' }).nodes();
                $('button', rows).attr("disabled", "disabled");
            }
        }
    });
}
