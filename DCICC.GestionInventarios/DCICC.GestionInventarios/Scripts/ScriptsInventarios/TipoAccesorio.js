var url_idioma = obtenerIdioma();
var url_bloquear;
var url_metodo;
var datosTipoAccesorio;
var idTipoAccesorio;
var nombreTipoAccModificar;
var urlEstado;
var nombresTipo = [];

/* --------------------------------------SECCIÓN PARA OBTENER DATOS DEL SERVIDOR---------------------------------*/
//Método ajax para obtener los datos de Tipo Accesorio
function obtenerTipoAccesorio(url) {
    url_metodo = url;
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            if (data.OperacionExitosa) {
                datosTipoAccesorio = data.ListaObjetoInventarios;
                cargarTipoAccTabla();
                $('#dataTableTipoAcc').DataTable({
                    "language": {
                        "url": url_idioma
                    }
                });
                cargarNombresTipoAcc();
            } else {
                showNotify("Error en la Consulta", 'No se ha podido mostrar los datos: ' + data.MensajeError, "error");
            }          
        }
    });
}

//Función para obtener la url de modificación
function urlEstados(url) {
    urlEstado = url;
}

//Función para obtener la url de modificación
function botones(url) {
    url_bloquear = url;
}

/* --------------------------------------SECCIÓN PARA CARGAR TABLAS Y COMBOBOX---------------------------------*/

//Función para cargar la tabla de Tipos de Accesorio
function cargarTipoAccTabla() {
    var str = '<table id="dataTableTipoAcc" class="table jambo_table bulk_action table-bordered" style="width:100%">';
    str += '<thead> <tr> <th>Nombre del Tipo de Accesorio</th> <th>Descripción</th> <th>Estado</th> <th>Modificar</th> <th>Habilitar/<br>Deshabilitar</th> </tr> </thead>';
    str += '<tbody>';
    for (var i = 0; i < datosTipoAccesorio.length; i++) {
        str += '<tr><td>' + datosTipoAccesorio[i].NombreTipoAccesorio +
            '</td><td class="text-justify">' + datosTipoAccesorio[i].DescripcionTipoAccesorio;

        if (datosTipoAccesorio[i].HabilitadoTipoAccesorio) {
            str += '</td><td> Habilitado';
        } else {
            str += '</td><td> Deshabilitado';
        }
        str += '</td><td><div class="text-center"><div class="col-md-12 col-sm-12 col-xs-12">' +
            '<button id="modificar" type="button" class="btn btn-info text-center" data-toggle="modal" data-target="#ModificarTipoAcc" onclick = "formUpdateTipoAcc(' + datosTipoAccesorio[i].IdTipoAccesorio + ');"> <strong><i class="fa fa-pencil-square-o"></i></strong></button> ' +
            '</div></div>' +
            '</td><td><div class=" text-center"><div class="col-md-12 col-sm-12 col-xs-12">';
        if (datosTipoAccesorio[i].HabilitadoTipoAccesorio) {
            str += '<button type="button" class="btn btn-success text-center" onclick = "habilitarOdeshabilitar(' + datosTipoAccesorio[i].IdTipoAccesorio + ',' + datosTipoAccesorio[i].HabilitadoTipoAccesorio +');"> <strong><span class="fa fa-toggle-on"></span></strong></button>';
        } else {
            str += '<button type="button" class="btn btn-danger text-center" onclick = "habilitarOdeshabilitar(' + datosTipoAccesorio[i].IdTipoAccesorio + ',' + datosTipoAccesorio[i].HabilitadoTipoAccesorio +');"> <strong><i class="fa fa-toggle-off"></i></strong></button>';
        }    
        str +='</div></div></td></tr>';

    }
    str += '</tbody></table>';
    $("#tablaModificarTipoAccesorio").html(str);
    bloquearBotones();
}

/* --------------------------------------SECCIÓN PARA MODIFICACION DE DATOS---------------------------------*/
//Función para setear los valores en los inputs
function formUpdateTipoAcc(idTipo) {
    idTipoAccesorio = idTipo;
    for (var i = 0; i < datosTipoAccesorio.length; i++) {
        if (datosTipoAccesorio[i].IdTipoAccesorio == idTipo) {
            nombreTipoAccModificar = datosTipoAccesorio[i].NombreTipoAccesorio;
            //Métodos para setear los valores a modificar
            document.getElementById("NombreTipoAccesorio").value = datosTipoAccesorio[i].NombreTipoAccesorio;
            document.getElementById("DescripcionTipoAccesorio").value = datosTipoAccesorio[i].DescripcionTipoAccesorio;

            //Método para el check del update de Tipo Accesorio
            var valor = datosTipoAccesorio[i].HabilitadoTipoAccesorio;
            var estado = $('#HabilitadoTipoAccesorio').prop('checked');
            if (estado && valor == false) {
                document.getElementById("HabilitadoTipoAccesorio").click();
            }
            if (estado == false && valor == true) {
                document.getElementById("HabilitadoTipoAccesorio").click();
            }
        }
    }
}

//Función para modificar el Tipo Accesorio especificado
function modificarTipoAcc(url_modificar) {
    var nombreTipo=document.getElementById("NombreTipoAccesorio").value;
    var descripcionTipo=document.getElementById("DescripcionTipoAccesorio").value;
    var habilitadoTipo = $('#HabilitadoTipoAccesorio').prop('checked');

    if (validarInputNombre() ) {
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
                    data: { "IdTipoAccesorio": idTipoAccesorio, "NombreTipoAccesorio": nombreTipo, "DescripcionTipoAccesorio": descripcionTipo, "HabilitadoTipoAccesorio": habilitadoTipo },
                    url: url_modificar,
                    type: 'post',
                    success: function (data) {
                        if (data.OperacionExitosa) {
                            $('#ModificarTipoAcc').modal('hide');
                            showNotify("Actualización exitosa", 'El Tipo " ' + nombreTipo.toUpperCase() + ' " se ha modificado exitosamente', "success");
                            obtenerTipoAccesorio(url_metodo);
                        } else {
                            $('#ModificarTipoAcc').modal('hide');
                            showNotify("Error en la Actualización", 'Ocurrió un error al modificar el Tipo de Accesorio: ' + data.MensajeError, "error");
                        }

                    }
                });
            } else {
                $('#ModificarTipoAcc').modal('hide');
            }
        });
    }  
}

//Función para habilitar o deshabilitar la categoria
function habilitarOdeshabilitar(idTipoAcc, estadoTipoAcc) {
    var nuevoEstado = true;
    if (estadoTipoAcc) {
        nuevoEstado = false;
    } else {
        nuevoEstado = true;
    }
    swal({
        title: 'Confirmación de Cambio de Estado',
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
                data: { "IdTipoAccesorio": idTipoAcc, "HabilitadoTipoAccesorio": nuevoEstado },
                url: urlEstado,
                type: 'post',
                success: function (data) {  
                    if (data.OperacionExitosa) {
                        showNotify("Actualización exitosa", 'El Estado del Tipo de Accesorio se ha modificado exitosamente', "success");
                        obtenerTipoAccesorio(url_metodo);
                    } else {
                        showNotify("Error en la Actualización", 'Ocurrió un error al modificar el estado del Tipo de Accesorio: ' + data.MensajeError, "error");
                    }                  
                }
            });
        } else {

        }
    });
}

/* --------------------------------------SECCIÓN PARA CAMPOS DE AUTOCOMPLETE---------------------------------*/
//Funciones para cargar el campo de autocompletado
function cargarNombresTipoAcc() {
    for (var i = 0; i < datosTipoAccesorio.length; i++) {
        nombresTipo[i] = datosTipoAccesorio[i].NombreTipoAccesorio;
    }
}
//Función para cargar los nombres en el campo de nombre de Tipo Accesorios
$(function () {
    $("#NombreTipoAccesorio").autocomplete({
        source: nombresTipo
    });
});

/* --------------------------------------SECCIÓN PARA COMPROBACIONES Y VALIDACIONES---------------------------------*/

//Función para evitar nombres de Tipo Accesorio repetidos
function comprobarNombre() {    
    var nomTipo = document.getElementById("NombreTipoAccesorio");
    nomTipo.value = nomTipo.value.toUpperCase();
    //Validación para el campo de texto nombre de Tipo Accesorio
    if (nomTipo.value.length <= 0) {
        nomTipo.style.borderColor = "#900C3F";
        $('#errorNombreTipo').html('El campo nombre no debe estar vacio').show();
        setTimeout("$('#errorNombreTipo').html('').hide('slow')", 6000);
    } else {
        for (var i = 0; i < datosTipoAccesorio.length; i++) {
            if ((datosTipoAccesorio[i].NombreTipoAccesorio).toUpperCase() == nomTipo.value) {
                nomTipo.style.borderColor = "#900C3F";
                $('#errorNombreTipo').html("El nombre del tipo accesorio: " + nomTipo.value + " ya existe").show();
                setTimeout("$('#errorNombreTipo').html('').hide('slow')", 6000);
                nomTipo.value = "";
                break;
            } else {
                nomTipo.style.borderColor = "#ccc";
                $('#errorNombreTipo').html('').hide();
            }
        }
    }
}

//Función para valida nombres repetidos de Tipo Accesorio en modificación
function validarNombreModificacion() {
    var nomTipo = document.getElementById("NombreTipoAccesorio");
    nomTipo.value = nomTipo.value.toUpperCase();
    //Validación para el campo de texto nombre de Tipo Accesorio
    if (nomTipo.value != nombreTipoAccModificar.toUpperCase()) {
        for (var i = 0; i < datosTipoAccesorio.length; i++) {
            if ((datosTipoAccesorio[i].NombreTipoAccesorio).toUpperCase() == nomTipo.value) {
                nomTipo.style.borderColor = "#900C3F";
                $('#errorNombreTipo').html("El nombre del tipo accesorio: " + nomTipo.value + " ya existe").show();
                setTimeout("$('#errorNombreTipo').html('').hide('slow')", 6000);
                nomTipo.value = "";
                break;
            } else {
                nomTipo.style.borderColor = "#ccc";
                $('#errorNombreTipo').html('').hide();
            }
        }
    } else {
        nomTipo.style.borderColor = "#ccc";
        $('#errorNombreTipo').html('').hide();
    }
}

//Funciones para validaciones de campos de texto
function validarInputNombre() {
    var esValido = true;
    var nomTipo = document.getElementById("NombreTipoAccesorio");
    //Validación para el campo de texto nombre de Tipo Accesorio
    if (nomTipo.value.length <= 0) {
        esValido = false;
        nomTipo.style.borderColor = "#900C3F";
        $('#errorNombreTipo').html('El campo nombre no debe estar vacio').show();
        setTimeout("$('#errorNombreTipo').html('').hide('slow')", 6000);
    } else {
        nomTipo.style.borderColor = "#ccc";
        $('#errorNombreTipo').html('').hide();
    }
    return esValido;
}

/* --------------------------------------SECCIÓN PARA MENSAJES DE TOOLTIPS---------------------------------*/
//Mensajes para los tooltips
function mensajesTooltips() {
    document.getElementById("NombreTipoAccesorio").title = "Máximo 50 caracteres en Mayúscula, sin Espacios.\n Caracteres especiales permitidos - / _ .";
    document.getElementById("DescripcionTipoAccesorio").title = "Máximo 150 caracteres.\n Caracteres especiales permitidos - / _ .";
}

/* --------------------------------------SECCIÓN PARA OPERACIONES CON USUARIO INVITADO---------------------------------*/
//Función para bloquear botones cuando el usuario es invitado
function bloquearBotones() {
    $.ajax({
        dataType: 'json',
        url: url_bloquear,
        type: 'post',
        success: function (data) {
            if (data == "Invitado") {
                $(':button').prop('disabled', true);
                var table = $('#dataTableCategorias').DataTable();
                var rows = table.rows({ 'search': 'applied' }).nodes();
                $('button', rows).attr("disabled", "disabled");
            }
        }
    });
}
