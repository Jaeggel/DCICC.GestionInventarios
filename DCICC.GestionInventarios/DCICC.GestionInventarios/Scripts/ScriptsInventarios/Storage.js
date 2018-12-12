var url_idioma = obtenerIdioma();
var url_metodo;
var datosStorage;
var idStorageModificar;
var urlEstado;
var nombresStorage = [];
var nicksStorage = [];

//Método ajax para obtener los datos de categorias
function obtenerStorage(url) {
    url_metodo = url;
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            console.log("Datos Exitosos");
            datosStorage = data;
            cargarStorageTabla();
            $('#dataTableStorage').DataTable({
                "language": {
                    "url": url_idioma
                }
            });
            cargarNombresStorage();
        }
    });
}

//Función para obtener la url de modificación
function urlEstados(url) {
    urlEstado = url;
}

//Función para cargar la tabla de Categorias
function cargarStorageTabla() {
    var str = '<table id="dataTableStorage" class="table jambo_table bulk_action  table-bordered " style="width:100%">';
    str += '<thead> <tr> <th>Nombre Storage</th> <th>Nick Storage</th> <th>Capacidad (GB/TB)</th> <th>Descripcion</th> <th>Estado</th> <th>Modificar</th> <th>Habilitar/<br>Deshabilitar</th> </tr> </thead>';
    str += '<tbody>';
    for (var i = 0; i < datosStorage.length; i++) {
        str += '<tr><td>' + datosStorage[i].NombreStorage +
            '</td><td>' + datosStorage[i].NickStorage+
            '<tr><td>' + datosStorage[i].CapacidadStorage +
            '<tr><td>' + datosStorage[i].DescripcionStorage ;

        if (datosStorage[i].HabilitadoStorage) {
            str += '</td><td> Habilitado';
        } else {
            str += '</td><td> Deshabilitado';
        }
        str += '</td><td><div class="text-center"><div class="col-md-12 col-sm-12 col-xs-12">' +
            '<button type="button" class="btn btn-info text-center" data-toggle="modal" data-target="#ModificarStorage" onclick = "formUpdateStorage(' + datosStorage[i].IdStorage + ');"> <strong><i class="fa fa-pencil-square-o"></i></strong></button> ' +
            '</div></div>' +
            '</td><td><div class="text-center"><div class="col-md-12 col-sm-12 col-xs-12">';
        if (datosStorage[i].HabilitadoStorage) {
            str += '<button type = "button" class="btn btn-success text-center" onclick = "habilitarOdeshabilitar(' + datosStorage[i].IdStorage + ',' + datosStorage[i].HabilitadoStorage + ');" > <strong><span class="fa fa-toggle-on"></span></strong></button> ';
        } else {
            str += '<button type = "button" class="btn btn-danger text-center" onclick = "habilitarOdeshabilitar(' + datosStorage[i].IdStorage + ',' + datosStorage[i].HabilitadoStorage + ');"> <strong><i class="fa fa-toggle-off"></i></strong></button> ';
        }
        str += '</div></div></td></tr>';
    };
    str += '</tbody></table>';
    $("#tablaModificarStorage").html(str);
}

//Función para setear los valores en los inputs en modificaciones
function formUpdateCategoria(idCategoria) {
    idCategoriaModificar = idCategoria;
    for (var i = 0; i < datosStorage.length; i++) {
        if (datosStorage[i].IdCategoriaActivo == idCategoria) {
            //Métodos para setear los valores a modificar
            document.getElementById("NombreCategoriaActivo").value = datosStorage[i].NombreCategoriaActivo;
            document.getElementById("DescripcionCategoriaActivo").value = datosStorage[i].DescripcionCategoriaActivo;

            //Método para el check del update de Categorias
            var valor = datosStorage[i].HabilitadoCategoriaActivo;
            var estado = $('#HabilitadoCategoriaActivo').prop('checked');
            if (estado && valor == false) {
                document.getElementById("HabilitadoCategoriaActivo").click();
            }
            if (estado == false && valor == true) {
                document.getElementById("HabilitadoCategoriaActivo").click();
            }
        };
    };
}

//Función para modificar la categoria especificada
function modificarCategoria(url_modificar) {
    var nombreCategoria = document.getElementById("NombreCategoriaActivo").value;
    var descripcionCategoria = document.getElementById("DescripcionCategoriaActivo").value;
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
                        console.log(data.OperacionExitosa);
                        if (data.OperacionExitosa) {
                            console.log("actualizacion exitosa");
                            $('#ModificarCategoria').modal('hide');
                            showNotify("Actualización exitosa", 'La Categoria de Activo se ha modificado correctamente', "success");
                            obtenerCategorias(url_metodo);
                        } else {
                            $('#ModificarCategoria').modal('hide');
                            showNotify("Error en la Actualización", 'No se ha podido modificar la Categoría del Activo: ' + data.MensajeError, "error");
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
        text: "¿Está seguro de Cambiar de Estado la Categoria?",
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
                    console.log(data.OperacionExitosa);
                    if (data.OperacionExitosa) {
                        showNotify("Actualización exitosa", 'El Estado de la Categoria de Activo se ha modificado correctamente', "success");
                        obtenerCategorias(url_metodo);
                    } else {
                        showNotify("Error en la Actualización", 'No se ha podido modificar el Estado de la Categoría del Activo: ' + data.MensajeError, "error");
                    }
                }
            });
        } else {

        }
    });
}

////////////Función para evitar nombres de categorias repetidos
function comprobarNombre() {
    var nomStr = document.getElementById("NombreStorage");
    nomStr.value = nomStr.value.toUpperCase();
    if (nomStr.value.length <= 0) {
        nomStr.style.borderColor = "#900C3F";
        $('#errorNombreStorage').html('El campo nombre no debe estar vacio').show();
        setTimeout("$('#errorNombreStorage').html('').hide('slow')", 6000);
    } else {
        for (var i = 0; i < datosStorage.length; i++) {
            if ((datosStorage[i].NombreStorage).toUpperCase() == nomStr.value) {
                nomStr.style.borderColor = "#900C3F";
                $('#errorNombreStorage').html("El nombre del Storage: " + nomStr.value + " ya existe").show();
                setTimeout("$('#errorNombreStorage').html('').hide('slow')", 6000);
                nomStr.value = "";
                break;
            } else {
                nomStr.style.borderColor = "#ccc";
                $('#errorNombreStorage').html('').hide();
            }
        }
    }


}

/////////////////////////Funciones para cargar el campo de autocompletado
function cargarNombresStorage() {
    for (var i = 0; i < datosStorage.length; i++) {
        nombresStorage[i] = datosStorage[i].NombreStorage;
        nicksStorage[i] = datosStorage[i].NombreStorage;
    }
}
//Función para cargar los nombres en el campo de nombre de ingreso  de storage
$(function () {
    $("#NombreStorage").autocomplete({
        source: nombresStorage
    });
});

$(function () {
    $("#NickStorage").autocomplete({
        source: nicksStorage
    });
});

/////////////Funciones para validaciones de campos de texto
function validarInputsVaciosModificacion() {
    var esValido = true;
    var boton = document.getElementById("confirmarCategoria");
    var nomStr = document.getElementById("NombreCategoriaActivo");
    //Valicación para el campo de texto nombre de categoria
    if (nomStr.value.length <= 0) {
        esValido = false;
        nomStr.style.borderColor = "#900C3F";
        $('#errorNombreCategoria').html('El campo nombre no debe estar vacio').show();
        setTimeout("$('#errorNombreCategoria').html('').hide('slow')", 6000);
        boton.disabled = true;
    } else {
        nomStr.style.borderColor = "#ccc";
        $('#errorNombreCategoria').html('').hide();
        boton.disabled = false;
    }
    return esValido;
}
//Mensajes para los tooltips
function mensajesTooltips() {
    document.getElementById("NombreStorage").title = "Máximo 80 caracteres en Mayúscula.\n Caracteres especiales permitidos - / _ .";
    document.getElementById("NickStorage").title = "Máximo 20 caracteres en Mayúscula y sin espacios.\n Caracteres especiales permitidos - / _ .";
    document.getElementById("CapacidadStorage").title = "Solo números. De 1 a 100 GB o TB";
    document.getElementById("DescripcionStorage").title = "Máximo 150 caracteres.\n Caracteres especiales permitidos - / _ .";
}
