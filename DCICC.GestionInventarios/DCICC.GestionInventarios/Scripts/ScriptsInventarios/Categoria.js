var url_idioma = obtenerIdioma();
var url_bloquear;
var url_metodo;
var datosCategorias;
var idCategoriaModificar;
var nombreCategoriaModificar;
var urlEstado;
var nombresCat = [];

/* --------------------------------------SECCIÓN PARA OBTENER DATOS DEL SERVIDOR---------------------------------*/
//Método ajax para obtener los datos de categorias
function obtenerCategorias(url) {
    url_metodo = url;
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            if (data.OperacionExitosa) {
                datosCategorias = data.ListaObjetoInventarios;
                cargarCategoriaTabla();
                cargarNombresCategoria();
                $('#dataTableCategorias').DataTable({
                    "language": {
                        "url": url_idioma
                    }
                });
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

//Función para cargar la tabla de Categorias
function cargarCategoriaTabla() {
    var str = '<table id="dataTableCategorias" class="table jambo_table bulk_action  table-bordered " style="width:100%">';
    str += '<thead> <tr> <th>Nombre Categoría</th> <th>Descripción</th> <th>Estado</th> <th>Modificar</th> <th>Habilitar/<br>Deshabilitar</th> </tr> </thead>';
    str += '<tbody>';
    for (var i = 0; i < datosCategorias.length; i++) {
        str += '<tr><td>' + datosCategorias[i].NombreCategoriaActivo +
            '</td><td class="text-justify">' + datosCategorias[i].DescripcionCategoriaActivo;
        if (datosCategorias[i].HabilitadoCategoriaActivo) {
            str += '</td><td> Habilitado';
        } else {
            str += '</td><td> Deshabilitado';
        }
        str += '</td><td><div class="text-center"><div class="col-md-12 col-sm-12 col-xs-12">' +
            '<button id="modificar" type="button" class="btn btn-info text-center" data-toggle="modal" data-target="#ModificarCategoria" onclick = "formUpdateCategoria(' + datosCategorias[i].IdCategoriaActivo + ');"> <strong><i class="fa fa-pencil-square-o"></i></strong></button> ' +
            '</div></div>' +
            '</td><td><div class="text-center"><div class="col-md-12 col-sm-12 col-xs-12">';
        if (datosCategorias[i].HabilitadoCategoriaActivo) {
            str += '<button type = "button" class="btn btn-success text-center" onclick = "habilitarOdeshabilitar(' + datosCategorias[i].IdCategoriaActivo + ',' + datosCategorias[i].HabilitadoCategoriaActivo+');" > <strong><span class="fa fa-toggle-on"></span></strong></button> ';
        } else {
            str += '<button type = "button" class="btn btn-danger text-center" onclick = "habilitarOdeshabilitar(' + datosCategorias[i].IdCategoriaActivo + ',' + datosCategorias[i].HabilitadoCategoriaActivo +');"> <strong><i class="fa fa-toggle-off"></i></strong></button> ';
        }  
        str += '</div></div></td></tr>';
    }
    str += '</tbody></table>';
    $("#tablaModificarCategorias").html(str);
    bloquearBotones();
}

/* --------------------------------------SECCIÓN PARA MODIFICACION DE DATOS---------------------------------*/

//Función para setear los valores en los inputs en modificaciones
function formUpdateCategoria(idCategoria) {
    idCategoriaModificar = idCategoria;
    for (var i = 0; i < datosCategorias.length; i++) {
        if (datosCategorias[i].IdCategoriaActivo == idCategoria) {
            nombreCategoriaModificar = datosCategorias[i].NombreCategoriaActivo;
            //Métodos para setear los valores a modificar
            document.getElementById("NombreCategoriaActivo").value = datosCategorias[i].NombreCategoriaActivo;
            document.getElementById("DescripcionCategoriaActivo").value = datosCategorias[i].DescripcionCategoriaActivo;

            //Método para el check del update de Categorias
            var valor = datosCategorias[i].HabilitadoCategoriaActivo;
            var estado = $('#HabilitadoCategoriaActivo').prop('checked');
            if (estado && valor == false) {
                document.getElementById("HabilitadoCategoriaActivo").click();
            }
            if (estado == false && valor == true) {
                document.getElementById("HabilitadoCategoriaActivo").click();
            }
        }
    }
}

//Función para modificar la categoria especificada
function modificarCategoria(url_modificar) {
    var nombreCategoria=document.getElementById("NombreCategoriaActivo").value;
    var descripcionCategoria=document.getElementById("DescripcionCategoriaActivo").value;
    var habilitadoCategoria = $('#HabilitadoCategoriaActivo').prop('checked');

    if (validarInputsVaciosModificacion()) {
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
                //Método ajax para modificar la categoria de la base de datos
                $.ajax({
                    data: { "IdCategoriaActivo": idCategoriaModificar, "NombreCategoriaActivo": nombreCategoria, "DescripcionCategoriaActivo": descripcionCategoria, "HabilitadoCategoriaActivo": habilitadoCategoria },
                    url: url_modificar,
                    type: 'post',
                    success: function (data) {
                        if (data.OperacionExitosa) {
                            console.log("actualizacion exitosa");
                            $('#ModificarCategoria').modal('hide');
                            showNotify("Actualización exitosa", 'La Categoría "' + nombreCategoria.toUpperCase() + '" se ha modificado exitosamente', "success");
                            obtenerCategorias(url_metodo);
                        } else {
                            $('#ModificarCategoria').modal('hide');
                            showNotify("Error en la Actualización", 'Ocurrió un error al modificar la Categoría: ' + data.MensajeError, "error");
                        }

                    }
                });
            } else {
                $('#ModificarCategoria').modal('hide');
            }
        });
    }
   
}

//Función para habilitar o deshabilitar la categoria
function habilitarOdeshabilitar(idCat, estadoCat) {
    var nuevoEstado = true;
    if (estadoCat) {
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
                data: { "IdCategoriaActivo": idCat, "HabilitadoCategoriaActivo": nuevoEstado },
                url: urlEstado,
                type: 'post',
                success: function (data) {
                    if (data.OperacionExitosa) {
                        showNotify("Actualización exitosa", 'El Estado de la Categoría se ha modificado exitosamente', "success");
                        obtenerCategorias(url_metodo);
                    } else {                     
                        showNotify("Error en la Actualización", 'Ocurrió un error al modificar el estado de la Categoría: ' + data.MensajeError, "error");
                    }                              
                }
            });
        } else {
            
        }
    });
}

/* --------------------------------------SECCIÓN PARA CAMPOS DE AUTOCOMPLETE---------------------------------*/

//Funciones para cargar el campo de autocompletado
function cargarNombresCategoria() {
    for (var i = 0; i < datosCategorias.length; i++) {
        nombresCat[i] = datosCategorias[i].NombreCategoriaActivo;
    }
}
//Función para cargar los nombres en el campo de nombre de ingreso  de categoria
$(function () {
    $("#NombreCategoriaActivo").autocomplete({
        source: nombresCat
    });
});

/* --------------------------------------SECCIÓN PARA COMPROBACIONES Y VALIDACIONES---------------------------------*/

//Función para evitar nombres de categorias repetidos
function comprobarNombre() {
    var nomCat = document.getElementById("NombreCategoriaActivo");
    nomCat.value = nomCat.value.toUpperCase();
    if (nomCat.value.length <= 0) {
        nomCat.style.borderColor = "#900C3F";
        $('#errorNombreCategoria').html('El campo nombre no debe estar vacio').show();
        setTimeout("$('#errorNombreCategoria').html('').hide('slow')", 6000);
    } else {
        for (var i = 0; i < datosCategorias.length; i++) {
            if ((datosCategorias[i].NombreCategoriaActivo).toUpperCase() == nomCat.value) {
                nomCat.style.borderColor = "#900C3F";
                $('#errorNombreCategoria').html("El nombre de la Categoría: " + nomCat.value + " ya existe").show();
                setTimeout("$('#errorNombreCategoria').html('').hide('slow')", 6000);
                nomCat.value = "";
                break;
            } else {
                nomCat.style.borderColor = "#ccc";
                $('#errorNombreCategoria').html('').hide();
            }
        }
    }
}

//funcion para validar nombre repetido en la modificación
function validarNombreModificación() {
    var nomCat = document.getElementById("NombreCategoriaActivo");
    nomCat.value = nomCat.value.toUpperCase();
    if (nomCat.value != nombreCategoriaModificar.toUpperCase()) {
        for (var i = 0; i < datosCategorias.length; i++) {
            if ((datosCategorias[i].NombreCategoriaActivo).toUpperCase() == nomCat.value) {
                nomCat.style.borderColor = "#900C3F";
                $('#errorNombreCategoria').html("El nombre de la Categoría: " + nomCat.value + " ya existe").show();
                setTimeout("$('#errorNombreCategoria').html('').hide('slow')", 6000);
                nomCat.value = "";
                break;
            } else {
                nomCat.style.borderColor = "#ccc";
                $('#errorNombreCategoria').html('').hide();
            }
        }
    } else {
        nomCat.style.borderColor = "#ccc";
        $('#errorNombreCategoria').html('').hide();
    }  
}


//Funciones para validaciones de campos de texto
function validarInputsVaciosModificacion() {
    var esValido = true;
    var nomCat = document.getElementById("NombreCategoriaActivo");
    //Valicación para el campo de texto nombre de categoria
    if (nomCat.value.length <= 0) {
        esValido = false;
        nomCat.style.borderColor = "#900C3F";
        $('#errorNombreCategoria').html('El campo nombre no debe estar vacio').show();
        setTimeout("$('#errorNombreCategoria').html('').hide('slow')", 6000);
    } else {
        nomCat.style.borderColor = "#ccc";
        $('#errorNombreCategoria').html('').hide();
    }
    return esValido;
}

/* --------------------------------------SECCIÓN PARA MENSAJES DE TOOLTIPS---------------------------------*/

//Mensajes para los tooltips
function mensajesTooltips() {
    document.getElementById("NombreCategoriaActivo").title = "Máximo 50 caracteres en Mayúscula, sin Espacios ni Números.\n Caracteres especiales permitidos - / _ .";
    document.getElementById("DescripcionCategoriaActivo").title = "Máximo 150 caracteres.\n Caracteres especiales permitidos - / _ . , : ;";
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

