var url_idioma = obtenerIdioma();
var url_metodo;
var datosSO;
var idSOMod;
var nombreSOModificar;
var urlEstado;
var nombresSO = [];

/* --------------------------------------SECCIÓN PARA OBTENER DATOS DEL SERVIDOR---------------------------------*/
//Método ajax para obtener los sistemas operativos
function obtenerSO(url) {
    url_metodo = url;
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            if (data.OperacionExitosa) {
                datosSO = data.ListaObjetoInventarios;
                cargarSOTabla();
                $('#dataTableSO').DataTable({
                    "language": {
                        "url": url_idioma
                    }
                });
                cargarNombresLaboratorios();
            } else{
                showNotify("Error en la Consulta", 'No se ha podido mostrar los datos: ' + data.MensajeError, "error");
            }          
        }
    });
}

//Función para obtener la url de modificación
function urlEstados(url) {
    urlEstado = url;
}

/* --------------------------------------SECCIÓN PARA CARGAR TABLAS Y COMBOBOX---------------------------------*/
//Función para cargar la tabla de Sistemas Operativos
function cargarSOTabla() {
    var str = '<table id="dataTableSO" class="table jambo_table bulk_action table-bordered" style="width:100%">';
    str += '<thead> <tr> <th>Nombre del Sistema Operativo</th> <th>Descripción</th> <th>Estado</th> <th>Modificar</th> <th>Habilitar/<br>Deshabilitar</th> </tr> </thead>';
    str += '<tbody>';
    for (var i = 0; i < datosSO.length; i++) {
        str += '<tr><td>' + datosSO[i].NombreSistOperativos +
            '</td><td class="text-justify">' + datosSO[i].DescripcionSistOperativos;

        if (datosSO[i].HabilitadoSistOperativos) {
            str += '</td><td> Habilitado';
        } else {
            str += '</td><td> Deshabilitado';
        }
        str += '</td><td><div class="text-center"><div class="col-md-12 col-sm-12 col-xs-12">' +
            '<button type="button" class="btn btn-info text-center" data-toggle="modal" data-target="#ModificarSo" onclick = "formUpdateSO(' + datosSO[i].IdSistOperativos + ');"> <strong><i class="fa fa-pencil-square-o"></i></strong></button> ' +
            '</div></div>' +
            '</td><td><div class=" text-center"><div class="col-md-12 col-sm-12 col-xs-12">';
        if (datosSO[i].HabilitadoSistOperativos) {
            str += '<button type="button" class="btn btn-success text-center" onclick = "habilitarOdeshabilitar(' + datosSO[i].IdSistOperativos + ',' + datosSO[i].HabilitadoSistOperativos +');"> <strong><span class="fa fa-toggle-on"></span></strong></button>';
        } else {
            str += '<button type="button" class="btn btn-danger text-center" onclick = "habilitarOdeshabilitar(' + datosSO[i].IdSistOperativos + ',' + datosSO[i].HabilitadoSistOperativos +');"> <strong><i class="fa fa-toggle-off"></i></strong></button>';
        }
         str += '</div></div></td></tr>';
    }
    str += '</tbody></table>';
    $("#tablaModificarSistOperativo").html(str);
}

/* --------------------------------------SECCIÓN PARA MODIFICACION DE DATOS---------------------------------*/
//Función para setear los valores en los inputs
function formUpdateSO(idSO) {
    idSOMod = idSO;
    for (var i = 0; i < datosSO.length; i++) {
        if (datosSO[i].IdSistOperativos == idSO) {
            nombreSOModificar = datosSO[i].NombreSistOperativos;
            //Métodos para setear los valores a modificar
            document.getElementById("NombreSistOperativos").value = datosSO[i].NombreSistOperativos;
            document.getElementById("DescripcionSistOperativos").value = datosSO[i].DescripcionSistOperativos;

            //Método para el check del update de Sistemas operativos
            var valor = datosSO[i].HabilitadoSistOperativos;
            var estado = $('#HabilitadoSistOperativos').prop('checked');
            if (estado && valor == false) {
                document.getElementById("HabilitadoSistOperativos").click();
            }
            if (estado == false && valor == true) {
                document.getElementById("HabilitadoSistOperativos").click();
            }
        };
    }
}

//Función para modificar el Sistema Operativo especificado
function modificarSO(url_modificar) {
    var nombreSO=document.getElementById("NombreSistOperativos").value;
    var descripcionSo=document.getElementById("DescripcionSistOperativos").value;
    var habilitadoSo = $('#HabilitadoSistOperativos').prop('checked');

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
                    data: { "IdSistOperativos": idSOMod, "NombreSistOperativos": nombreSO, "DescripcionSistOperativos": descripcionSo, "HabilitadoSistOperativos": habilitadoSo },
                    url: url_modificar,
                    type: 'post',
                    success: function (data) {
                        console.log(data.OperacionExitosa);
                        if (data.OperacionExitosa) {
                            $('#ModificarSo').modal('hide');
                            showNotify("Actualización exitosa", 'El Sistema Operativo " ' + nombreSO.toUpperCase() + ' " se ha modificado exitosamente', "success");
                            obtenerSO(url_metodo);
                        } else {
                            $('#ModificarSo').modal('hide');
                            showNotify("Error en la Actualización", 'Ocurrió un error al modificar el Sistema Operativo: ' + data.MensajeError, "error");
                        }

                    }
                });
            } else {
                $('#ModificarSo').modal('hide');
            }
        });
    }  
}

//Función para habilitar o deshabilitar un sistema operativo
function habilitarOdeshabilitar(idSistOpe, estadoSistOpe) {
    var nuevoEstado = true;
    if (estadoSistOpe) {
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
                data: { "IdSistOperativos": idSistOpe, "HabilitadoSistOperativos": nuevoEstado },
                url: urlEstado,
                type: 'post',
                success: function (data) {
                    console.log(data.OperacionExitosa);
                    if (data.OperacionExitosa) {
                       
                        showNotify("Actualización exitosa", 'El Estado del Sistema Operativo se ha modificado exitosamente', "success");
                        obtenerSO(url_metodo);
                    } else {
                      
                        showNotify("Error en la Actualización", 'Ocurrió un error al modificar el estado del Sistema Operativo: ' + data.MensajeError, "error");
                    }
                 
                }, error: function (e) {
                    console.log(e);
                }
            });
        } else {

        }
    });
}

/* --------------------------------------SECCIÓN PARA CAMPOS DE AUTOCOMPLETE---------------------------------*/
//Funciones para cargar el campo de autocompletado
function cargarNombresLaboratorios() {
    for (var i = 0; i < datosSO.length; i++) {
        nombresSO[i] = datosSO[i].NombreSistOperativos;
    }
}
//Función para cargar los nombres en el campo de nombre de Sistemas Operativos
$(function () {
    $("#NombreSistOperativos").autocomplete({
        source: nombresSO
    });
});

/* --------------------------------------SECCIÓN PARA COMPROBACIONES Y VALIDACIONES---------------------------------*/
//Función para evitar nombres de sistemas operativo repetidos
function comprobarNombre() { 
    var nomSO = document.getElementById("NombreSistOperativos");
    nomSO.value = nomSO.value.toUpperCase();
    //Validación para el campo de texto nombre de laboratorio
    if (nomSO.value.length <= 0) {
        nomSO.style.borderColor = "#900C3F";
        $('#errorNombreSO').html('El campo nombre no debe estar vacio').show();
        setTimeout("$('#errorNombreSO').html('').hide('slow')", 6000);
    } else {
        for (var i = 0; i < datosSO.length; i++) {
            if ((datosSO[i].NombreSistOperativos).toUpperCase() == nomSO.value) {
                nomSO.style.borderColor = "#900C3F";
                $('#errorNombreSO').html("El nombre: " + nomSO.value + " ya existe").show();
                setTimeout("$('#errorNombreSO').html('').hide('slow')", 6000);
                nomSO.value = "";
                break;
            } else {
                nomSO.style.borderColor = "#ccc";
                $('#errorNombreSO').html('').hide();
            }
        }
    }
}

//funcion para validar nombre repetido en la modificación
function validarNombreModificacion() {
    var nomCat = document.getElementById("NombreSistOperativos");
    nomCat.value = nomCat.value.toUpperCase();
    if (nomCat.value != nombreSOModificar.toUpperCase()) {
        for (var i = 0; i < datosSO.length; i++) {
            if ((datosSO[i].NombreSistOperativos).toUpperCase() == nomCat.value) {
                nomCat.style.borderColor = "#900C3F";
                $('#errorNombreSo').html("El nombre: " + nomCat.value + " ya existe").show();
                setTimeout("$('#errorNombreSo').html('').hide('slow')", 6000);
                nomCat.value = "";
                break;
            } else {
                nomCat.style.borderColor = "#ccc";
                $('#errorNombreSo').html('').hide();
            }
        }
    } else {
        nomCat.style.borderColor = "#ccc";
        $('#errorNombreSo').html('').hide();
    }
}

//Funciones para validaciones de campos de texto
function validarInputNombre() {
    var esValido = true;
    var nomSO = document.getElementById("NombreSistOperativos");
    //Validación para el campo de texto nombre de laboratorio
    if (nomSO.value.length <= 0) {
        esValido = false;
        nomSO.style.borderColor = "#900C3F";
        $('#errorNombreSo').html('El campo nombre no debe estar vacio').show();
        setTimeout("$('#errorNombreSo').html('').hide('slow')", 6000);
    } else {
        nomSO.style.borderColor = "#ccc";
        $('#errorNombreSo').html('').hide();
    }
    return esValido;
}

/* --------------------------------------SECCIÓN PARA MENSAJES DE TOOLTIPS---------------------------------*/
//Mensajes para los tooltips
function mensajesTooltips() {
    document.getElementById("NombreSistOperativos").title = "Máximo 80 caracteres en Mayúscula.\n Caracteres especiales permitidos - / _ .";
    document.getElementById("DescripcionSistOperativos").title = "Máximo 150 caracteres.\n Caracteres especiales permitidos - / _ .";
}