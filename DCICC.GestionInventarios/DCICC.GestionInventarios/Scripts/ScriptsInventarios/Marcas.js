var url_idioma = obtenerIdioma();
var url_bloquear;
var url_metodo;
var datosMarcas;
var idMarcaModificar;
var nombreMarcaModificar;
var urlEstado;
var nombresMarcas = [];

/* --------------------------------------SECCIÓN PARA OBTENER DATOS DEL SERVIDOR---------------------------------*/
//Método ajax para obtener los datos de Marcas
function obtenerMarcas(url) {
    url_metodo = url;
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            if (data.OperacionExitosa) {
                datosMarcas = data.ListaObjetoInventarios;
                cargarMarcasTabla();
                $('#dataTableMarcas').DataTable({
                    "language": {
                        "url": url_idioma
                    }
                });
                cargarNombresMarcas();
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

//Función para cargar la tabla de las Marcas
function cargarMarcasTabla() {
    var str = '<table id="dataTableMarcas" class="table jambo_table bulk_action table-bordered" style="width:100%">';
    str += '<thead> <tr> <th>Nombre Marca</th> <th>Descripción</th> <th>Estado</th> <th>Modificar</th> <th>Habilitar/<br>Deshabilitar</th> </tr> </thead>';
    str += '<tbody>';
    for (var i = 0; i < datosMarcas.length; i++) {
        str += '<tr><td>' + datosMarcas[i].NombreMarca +
            '</td><td class="text-justify">' + datosMarcas[i].DescripcionMarca;

        if (datosMarcas[i].HabilitadoMarca) {
            str += '</td><td> Habilitado';
        } else {
            str += '</td><td> Deshabilitado';
        }
        str += '</td><td><div class="text-center"><div class="col-md-12 col-sm-12 col-xs-12">' +
            '<button id="modificar" type="button" class="btn btn-info text-center" data-toggle="modal" data-target="#ModificarMarca" onclick = "formUpdateMarca(' + datosMarcas[i].IdMarca + ');"> <strong><i class="fa fa-pencil-square-o"></i></strong></button> ' +
            '</div></div>' +
            '</td><td><div class=" text-center"><div class="col-md-12 col-sm-12 col-xs-12">';
        if (datosMarcas[i].HabilitadoMarca) {
            str += '<button type="button" class="btn btn-success text-center" onclick = "habilitarOdeshabilitar(' + datosMarcas[i].IdMarca + ',' + datosMarcas[i].HabilitadoMarca +');"> <strong><span class="fa fa-toggle-on"></span></strong></button>';
        } else {
            str += '<button type="button" class="btn btn-danger text-center" onclick = "habilitarOdeshabilitar(' + datosMarcas[i].IdMarca + ',' + datosMarcas[i].HabilitadoMarca +');"> <strong><i class="fa fa-toggle-off"></i></strong></button>';
        }   
        str +='</div></div></td></tr>';
    }
    str += '</tbody></table>';
    $("#tablaModificarMarca").html(str);
    bloquearBotones();
}

/* --------------------------------------SECCIÓN PARA MODIFICACION DE DATOS---------------------------------*/
//Función para setear los valores en los inputs
function formUpdateMarca(idMarca) {
    idMarcaModificar = idMarca;
    for (var i = 0; i < datosMarcas.length; i++) {
        if (datosMarcas[i].IdMarca == idMarca) {
            nombreMarcaModificar = datosMarcas[i].NombreMarca;
            //Métodos para setear los valores a modificar
            document.getElementById("NombreMarca").value = datosMarcas[i].NombreMarca;
            document.getElementById("DescripcionMarca").value = datosMarcas[i].DescripcionMarca;

            //Método para el check del update de Marcas
            var valor = datosMarcas[i].HabilitadoMarca;
            var estado = $('#HabilitadoMarca').prop('checked');
            if (estado && valor == false) {
                document.getElementById("HabilitadoMarca").click();
            }
            if (estado == false && valor == true) {
                document.getElementById("HabilitadoMarca").click();
            }
        }
    }
}

//Función para modificar la marca especificada
function modificarMarca(url_modificar) {
    var nombreMarca=document.getElementById("NombreMarca").value;
    var descripcionMarca=document.getElementById("DescripcionMarca").value;
    var habilitadoMarca = $('#HabilitadoMarca').prop('checked');
    if (validarInputNombre()) {
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
                    data: { "IdMarca": idMarcaModificar, "NombreMarca": nombreMarca, "DescripcionMarca": descripcionMarca, "HabilitadoMarca": habilitadoMarca },
                    url: url_modificar,
                    type: 'post',
                    success: function (data) {
                        if (data.OperacionExitosa) {
                            $('#ModificarMarca').modal('hide');
                            showNotify("Actualización exitosa", 'La Marca " ' + nombreMarca.toUpperCase() + ' " se ha modificado exitosamente', "success");
                            obtenerMarcas(url_metodo);
                        } else {
                            $('#ModificarMarca').modal('hide');
                            showNotify("Error en la Actualización", 'Ocurrió un error al modificar la Marca: ' + data.MensajeError, "error");
                        }
                    }
                });
            } else {
                $('#ModificarMarca').modal('hide');
            }
        });
    }  
}

//Función para habilitar o deshabilitar la marca
function habilitarOdeshabilitar(idMarc, estadoMarc) {
    var nuevoEstado = true;
    if (estadoMarc) {
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
            //Método ajax para modificar la categoria de la base de datos
            $.ajax({
                data: { "IdMarca": idMarc, "HabilitadoMarca": nuevoEstado },
                url: urlEstado,
                type: 'post',
                success: function (data) {
                    console.log(data.OperacionExitosa);
                    if (data.OperacionExitosa) {
                        showNotify("Actualización exitosa", 'El Estado de la Marca se ha modificado exitosamente', "success");
                        obtenerMarcas(url_metodo);
                    } else {
                        showNotify("Error en la Actualización", 'Ocurrió un error al modificar el estado de la Marca: ' + data.MensajeError, "error");
                    }                   
                }
            });
        } else {

        }
    });
}

/* --------------------------------------SECCIÓN PARA CAMPOS DE AUTOCOMPLETE---------------------------------*/

//Funciones para cargar el campo de autocompletado
function cargarNombresMarcas() {
    for (var i = 0; i < datosMarcas.length; i++) {
        nombresMarcas[i] = datosMarcas[i].NombreMarca;
    }
}
//Función para cargar los nombres en el campo de nombre de Marcas
$(function () {
    $("#NombreMarca").autocomplete({
        source: nombresMarcas
    });
});

/* --------------------------------------SECCIÓN PARA COMPROBACIONES Y VALIDACIONES---------------------------------*/
//Función para evitar nombres de marcas repetidas
function comprobarNombre() {
    var nomMarca = document.getElementById("NombreMarca");
    nomMarca.value = nomMarca.value.toUpperCase();
    //Validación para el campo de texto nombre de Marcas
    if (nomMarca.value.length <= 0) {
        nomMarca.style.borderColor = "#900C3F";
        $('#errorNombreMarca').html('El campo nombre no debe estar vacio').show();
        setTimeout("$('#errorNombreMarca').html('').hide('slow')", 6000);
    } else {
        for (var i = 0; i < datosMarcas.length; i++) {
            if ((datosMarcas[i].NombreMarca).toUpperCase() == nomMarca.value) {
                nomMarca.style.borderColor = "#900C3F";
                $('#errorNombreMarca').html("El nombre de la Marca: " + nomMarca.value + " ya existe").show();
                setTimeout("$('#errorNombreMarca').html('').hide('slow')", 6000);
                nomMarca.value = "";
                break;
            } else {
                nomMarca.style.borderColor = "#ccc";
                $('#errorNombreMarca').html('').hide();
            }
        }
    }
}

//Función para evitar nombres repetidos de marcas en la modificación
function validarNombreModificacion() {
    var nomMarca = document.getElementById("NombreMarca");
    nomMarca.value = nomMarca.value.toUpperCase();
    //Validación para el campo de texto nombre de Marcas
    if (nomMarca.value != nombreMarcaModificar.toUpperCase()) {
        for (var i = 0; i < datosMarcas.length; i++) {
            if ((datosMarcas[i].NombreMarca).toUpperCase() == nomMarca.value) {
                nomMarca.style.borderColor = "#900C3F";
                $('#errorNombreMarca').html("El nombre de la Marca: " + nomMarca.value + " ya existe").show();
                setTimeout("$('#errorNombreMarca').html('').hide('slow')", 6000);
                nomMarca.value = "";
                break;
            } else {
                nomMarca.style.borderColor = "#ccc";
                $('#errorNombreMarca').html('').hide();
            }
        }
    } else {
        nomMarca.style.borderColor = "#ccc";
        $('#errorNombreMarca').html('').hide();
    } 
}

//Funciones para validaciones de campos de texto
function validarInputNombre() {
    var esValido = true;
    var nomMarca = document.getElementById("NombreMarca");
    //Validación para el campo de texto nombre de Marcas
    if (nomMarca.value.length <= 0) {
        esValido = false;
        nomMarca.style.borderColor = "#900C3F";
        $('#errorNombreMarca').html('El campo nombre no debe estar vacio').show();
        setTimeout("$('#errorNombreMarca').html('').hide('slow')", 6000);
    } else {
        nomMarca.style.borderColor = "#ccc";
        $('#errorNombreMarca').html('').hide();
    }
    return esValido;
}

/* --------------------------------------SECCIÓN PARA MENSAJES DE TOOLTIPS---------------------------------*/

//Mensajes para los tooltips
function mensajesTooltips() {
    document.getElementById("NombreMarca").title = "Máximo 50 caracteres en Mayúscula, sin Espacios.\n Caracteres especiales permitidos - / _ .";
    document.getElementById("DescripcionMarca").title = "Máximo 150 caracteres.\n Caracteres especiales permitidos - / _ .";
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
                var table = $('#dataTableMarcas').DataTable();
                var rows = table.rows({ 'search': 'applied' }).nodes();
                $('button', rows).attr("disabled", "disabled");
            }
        }
    });
}
