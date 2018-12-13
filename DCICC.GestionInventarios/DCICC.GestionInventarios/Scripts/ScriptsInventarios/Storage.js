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
    console.log(url_metodo);
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
function urlEstadosStorage(url) {
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
            '</td><td>' + datosStorage[i].CapacidadStorage +
            '</td><td>' + datosStorage[i].DescripcionStorage ;

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
function formUpdateStorage(idStorage) {
    idStorageModificar = idStorage;
    for (var i = 0; i < datosStorage.length; i++) {
        if (datosStorage[i].IdStorage == idStorage) {
            //Métodos para setear los valores a modificar
            document.getElementById("NombreStorage").value = datosStorage[i].NombreStorage;
            document.getElementById("NickStorage").value = datosStorage[i].NickStorage;
            document.getElementById("CapacidadStorage").value = datosStorage[i].SizeStorage;

            var element = document.getElementById("UnidadStorage");
            element.value = datosStorage[i].UnidadStorage;

            document.getElementById("DescripcionStorage").value = datosStorage[i].DescripcionStorage;

            //Método para el check del update de Categorias
            var valor = datosStorage[i].HabilitadoStorage;
            var estado = $('#HabilitadoStorage').prop('checked');
            if (estado && valor == false) {
                document.getElementById("HabilitadoStorage").click();
            }
            if (estado == false && valor == true) {
                document.getElementById("HabilitadoStorage").click();
            }
        };
    };
}

//Función para modificar la categoria especificada
function modificarStorage(url_modificar) {
    var nombre=document.getElementById("NombreStorage").value;
    var nick=document.getElementById("NickStorage").value;
    var capacidad = document.getElementById("CapacidadStorage").value;
    //Obtener valor del combobox
    var cmbUnidad = document.getElementById("UnidadStorage");
    var unidad = cmbUnidad.options[cmbUnidad.selectedIndex].value;
    //Obtener valores de inputs
    var descripcion=document.getElementById("DescripcionStorage").value;
    var habilitadoStorage = $('#HabilitadoStorage').prop('checked');

    if (validarInputsVacios() && validarNickVacios() && validarNumero()) {
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
                    data: {
                        "IdStorage": idStorageModificar, "NombreStorage": nombre, "NickStorage": nick,
                        "CapacidadStorage": capacidad, "UnidadStorage": unidad,
                        "DescripcionStorage": descripcion, "HabilitadoStorage": habilitadoStorage
                    },
                    url: url_modificar,
                    type: 'post',
                    success: function (data) {
                        console.log(data.OperacionExitosa);
                        if (data.OperacionExitosa) {
                            $('#ModificarStorage').modal('hide');
                            showNotify("Actualización exitosa", 'El Storage se ha modificado correctamente', "success");
                            obtenerStorage(url_metodo);
                        } else {
                            $('#ModificarStorage').modal('hide');
                            showNotify("Error en la Actualización", 'No se ha podido modificar el Storage: ' + data.MensajeError, "error");
                        }

                    }
                });
            } else {
                $('#ModificarStorage').modal('hide');
            }
        });
    }

}

//Función para habilitar o deshabilitar la categoria
function habilitarOdeshabilitar(idStr, estadoStr) {
    var nuevoEstado = true;
    if (estadoStr) {
        nuevoEstado = false;
    } else {
        nuevoEstado = true;
    }
    swal({
        title: 'Confirmación de Cambio de Estado',
        text: "¿Está seguro de Cambiar de Estado el Storage?",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#26B99A',
        cancelButtonColor: '#337ab7',
        confirmButtonText: 'Confirmar',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                data: { "IdStorage": idStr, "HabilitadoStorage": nuevoEstado },
                url: urlEstado,
                type: 'post',
                success: function (data) {
                    console.log(data.OperacionExitosa);
                    if (data.OperacionExitosa) {
                        showNotify("Actualización exitosa", 'El Estado del Storage se ha modificado correctamente', "success");
                        obtenerStorage(url_metodo);
                    } else {
                        showNotify("Error en la Actualización", 'No se ha podido modificar el Estado del Storage: ' + data.MensajeError, "error");
                    }
                }
            });
        } else {

        }
    });
}

////////////Función para evitar nombres de storage repetidos
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

function comprobarNick() {
    var nickStr = document.getElementById("NickStorage");
    nickStr.value = nickStr.value.toUpperCase();
    if (nickStr.value.length <= 0) {
        nickStr.style.borderColor = "#900C3F";
        $('#errorNickStorage').html('El campo nick no debe estar vacio').show();
        setTimeout("$('#errorNickStorage').html('').hide('slow')", 6000);
    } else {
        for (var i = 0; i < datosStorage.length; i++) {
            if ((datosStorage[i].NickStorage).toUpperCase() == nickStr.value) {
                nickStr.style.borderColor = "#900C3F";
                $('#errorNickStorage').html("El nick del Storage: " + nickStr.value + " ya existe").show();
                setTimeout("$('#errorNickStorage').html('').hide('slow')", 6000);
                nickStr.value = "";
                break;
            } else {
                nickStr.style.borderColor = "#ccc";
                $('#errorNickStorage').html('').hide();
            }
        }
    }


}

/////////////////////////Funciones para cargar el campo de autocompletado
function cargarNombresStorage() {
    for (var i = 0; i < datosStorage.length; i++) {
        nombresStorage[i] = datosStorage[i].NombreStorage;
        nicksStorage[i] = datosStorage[i].NickStorage;
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
function validarInputsVacios() {
    var esValido = true;
    
    var nomStr = document.getElementById("NombreStorage");
    //Valicación para el campo de texto nombre de categoria
    if (nomStr.value.length <= 0) {
        esValido = false;
        nomStr.style.borderColor = "#900C3F";
        $('#errorNombreStorage').html('El campo nombre no debe estar vacio').show();
        setTimeout("$('#errorNombreStorage').html('').hide('slow')", 6000);
    }else {
        nomStr.style.borderColor = "#ccc";
        $('#errorNombreStorage').html('').hide();
     }
    return esValido;
}

function validarNickVacios() {
    var esValido = true;

    var nickStr = document.getElementById("NickStorage");
    //Valicación para el campo de texto nombre de categoria
    if (nickStr.value.length <= 0) {
        esValido = false;
        nickStr.style.borderColor = "#900C3F";
        $('#errorNickStorage').html('El campo nick no debe estar vacio').show();
        setTimeout("$('#errorNickStorage').html('').hide('slow')", 6000);
    } else {
        nickStr.style.borderColor = "#ccc";
        $('#errorNickStorage').html('').hide();
    }
    return esValido;
}

//Función para validar disco duro y Ram
function validarNumero() {
    var esValido = true;
    var boton = document.getElementById("confirmarMV");
    var capa = document.getElementById("CapacidadStorage");
    //Validar memoria capa
    if (capa.value.length <= 0) {
        esValido = false;
        capa.value = "";
        capa.style.borderColor = "#900C3F";
        $('#errorCapacidadStorage').html('El campo capacidad no debe estar vacio').show();
        setTimeout("$('#errorCapacidadStorage').html('').hide('slow')", 6000);
    } else if (capa.value < 1) {
        esValido = false;
        capa.value = "";
        capa.style.borderColor = "#900C3F";
        $('#errorCapacidadStorage').html('El rango de capacidad es de 1 a 1000').show();
        setTimeout("$('#errorCapacidadStorage').html('').hide('slow')", 6000);
    } else if (capa.value > 1000) {
        esValido = false;
        capa.value = "";
        capa.style.borderColor = "#900C3F";
        $('#errorCapacidadStorage').html('No se puede ingresar un valor mayor a 1000').show();
        setTimeout("$('#errorCapacidadStorage').html('').hide('slow')", 6000);
    } else {
        capa.style.borderColor = "#ccc";
        $('#errorCapacidadStorage').html('').hide();
    }
    return esValido;
}



//Mensajes para los tooltips
function mensajesTooltipStorage() {
    document.getElementById("NombreStorage").title = "Máximo 80 caracteres en Mayúscula.\n Caracteres especiales permitidos - / _ .";
    document.getElementById("NickStorage").title = "Máximo 20 caracteres en Mayúscula y sin espacios.\n Caracteres especiales permitidos - / _ .";
    document.getElementById("CapacidadStorage").title = "Solo números. De 1 a 100 GB o TB";
    document.getElementById("DescripcionStorage").title = "Máximo 150 caracteres.\n Caracteres especiales permitidos - / _ .";
}
