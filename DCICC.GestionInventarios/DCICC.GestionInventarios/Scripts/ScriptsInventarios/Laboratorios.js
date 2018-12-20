var url_idioma = obtenerIdioma();
var url_metodo;
var datosLaboratorios;
var idLaboratorio;
var nombreLabModificar;
var urlEstado;
var nombresLabs = [];
var rol;

/* --------------------------------------SECCIÓN PARA OBTENER DATOS DEL SERVIDOR---------------------------------*/
//Método ajax para obtener los datos de laboratorios
function obtenerLaboratorios(url) {
    url_metodo = url;
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            if (data.OperacionExitosa) {
                datosLaboratorios = data.ListaObjetoInventarios;
                cargarLaboratoriosTabla();
                $('#dataTableLaboratorios').DataTable({
                    "language": {
                        "url": url_idioma
                    }
                });
                cargarNombresLaboratorios();
            } else {
                showNotify("Error en la Consulta", 'No se ha podido mostrar los datos: ' + data.MensajeError, "error");
            }           
        }
    });
}

//Metodo para obtener la url de cambio de estado
function urlEstados(url) {
    urlEstado = url;
}

/* --------------------------------------SECCIÓN PARA CARGAR TABLAS Y COMBOBOX---------------------------------*/

//Función para cargar la tabla de Laboratorios
function cargarLaboratoriosTabla() {
    var str = '<table id="dataTableLaboratorios" class="table jambo_table bulk_action table-bordered" style="width:100%">';
    str += '<thead> <tr> <th>Nombre de Laboratorio</th> <th>Ubicación</th> <th>Descripción</th> <th>Estado</th> <th>Modificar</th> <th>Habilitar/<br>Deshabilitar</th> </tr> </thead>';
    str += '<tbody>';
    for (var i = 0; i < datosLaboratorios.length; i++) {
        str += '<tr><td>' + datosLaboratorios[i].NombreLaboratorio +
            '</td><td class="text-justify">' + datosLaboratorios[i].UbicacionLaboratorio +
            '</td><td class="text-justify">' + datosLaboratorios[i].DescripcionLaboratorio;

        if (datosLaboratorios[i].HabilitadoLaboratorio) {
            str += '</td><td> Habilitado';
        } else {
            str += '</td><td> Deshabilitado';
        }

        str += '</td><td><div class="text-center"><div class="col-md-12 col-sm-12 col-xs-12">' +
            '<button id="modificar" type="button" class="btn btn-info text-center" data-toggle="modal" data-target="#ModificarLaboratorios" onclick = "formUpdateLaboratorio(' + datosLaboratorios[i].IdLaboratorio + ');"> <strong><i class="fa fa-pencil-square-o"></i></strong></button> ' +
            '</div></div>' +
            '</td><td><div class=" text-center"><div class="col-md-12 col-sm-12 col-xs-12">';
        if (datosLaboratorios[i].HabilitadoLaboratorio) {
            str += '<button type = "button" class="btn btn-success text-center" onclick = "habilitarOdeshabilitar(' + datosLaboratorios[i].IdLaboratorio + ',' + datosLaboratorios[i].HabilitadoLaboratorio +');"> <strong><span class="fa fa-toggle-on"></span></strong></button> ';
        } else {
            str += '<button type = "button" class="btn btn-danger text-center" onclick = "habilitarOdeshabilitar(' + datosLaboratorios[i].IdLaboratorio + ',' + datosLaboratorios[i].HabilitadoLaboratorio +');"> <strong><i class="fa fa-toggle-off"></i></strong></button> ';
        }
         str +='</div></div></td></tr>';
    }
    str += '</tbody></table>';
    $("#tablaModificarLaboratorios").html(str);
}

/* --------------------------------------SECCIÓN PARA MODIFICACION DE DATOS---------------------------------*/

//Función para setear los valores en los inputs
function formUpdateLaboratorio(idLab) {
    idLaboratorio = idLab;
    for (var i = 0; i < datosLaboratorios.length; i++) {
        if (datosLaboratorios[i].IdLaboratorio == idLab) {
            nombreLabModificar = datosLaboratorios[i].NombreLaboratorio;
            //Métodos para setear los valores a modificar
            document.getElementById("NombreLaboratorio").value = datosLaboratorios[i].NombreLaboratorio;
            document.getElementById("UbicacionLaboratorio").value = datosLaboratorios[i].UbicacionLaboratorio;
            document.getElementById("DescripcionLaboratorio").value = datosLaboratorios[i].DescripcionLaboratorio;

            //Método para el check del update de laboratorios
            var valor = datosLaboratorios[i].HabilitadoLaboratorio;
            var estado = $('#HabilitadoLaboratorio').prop('checked');
            if (estado && valor == false) {
                document.getElementById("HabilitadoLaboratorio").click();
            }
            if (estado == false && valor == true) {
                document.getElementById("HabilitadoLaboratorio").click();
            }
        };
    };
}

//Función para modificar el laboratorio especificado
function modificarLaboratorio(url_modificar) {
    var nombreLab=document.getElementById("NombreLaboratorio").value;
    var ubicacionLab=document.getElementById("UbicacionLaboratorio").value;
    var descripcionLab=document.getElementById("DescripcionLaboratorio").value;
    var habilitadoLab = $('#HabilitadoLaboratorio').prop('checked');

    if (validarInputNombre() && validarInputUbicacion()) {
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
                //Método ajax para modificar el laboratorio
                $.ajax({
                    data: { "IdLaboratorio": idLaboratorio, "NombreLaboratorio": nombreLab, "UbicacionLaboratorio": ubicacionLab, "DescripcionLaboratorio": descripcionLab, "HabilitadoLaboratorio": habilitadoLab },
                    url: url_modificar,
                    type: 'post',
                    success: function (data) {
                        if (data.OperacionExitosa) {
                            $('#ModificarLaboratorios').modal('hide');
                            showNotify("Actualización exitosa", 'El Laboratorio "' + nombreLab.toUpperCase() + '" se ha modificado exitosamente', "success");
                            obtenerLaboratorios(url_metodo);
                        } else {
                            $('#ModificarLaboratorios').modal('hide');
                            showNotify("Error en la Actualización", 'Ocurrió un error al modificar el Laboratorio: ' + data.MensajeError, "error");
                        }
                    }
                });
            } else {
                $('#ModificarLaboratorios').modal('hide');
            }
        });
    }    
}

//Función para habilitar o deshabilitar la categoria
function habilitarOdeshabilitar(idLab, estadoLab) {
    var nuevoEstado = true;
    if (estadoLab) {
        nuevoEstado = false;
    } else {
        nuevoEstado = true;
    }
    console.log(nuevoEstado);
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
                data: { "IdLaboratorio": idLab, "HabilitadoLaboratorio": nuevoEstado },
                url: urlEstado,
                type: 'post',
                success: function (data) {
                    console.log(data.OperacionExitosa);
                    if (data.OperacionExitosa) {
                        showNotify("Actualización exitosa", 'El Estado del Laboratorio se ha modificado exitosamente', "success");
                        obtenerLaboratorios(url_metodo);
                    } else {                        
                        showNotify("Error en la Actualización", 'Ocurrió un error al modificar el estado del Laboratorio: ' + data.MensajeError, "error");
                    }
                   
                }
            });
        } else {

        }
    });
}

/* --------------------------------------SECCIÓN PARA CAMPOS DE AUTOCOMPLETE---------------------------------*/

//Funciones para cargar el campo de autocompletado
function cargarNombresLaboratorios() {
    for (var i = 0; i < datosLaboratorios.length; i++) {
        nombresLabs[i] = datosLaboratorios[i].NombreLaboratorio;
    }
}
//Función para cargar los nombres en el campo de nombre de laboratorios
$(function () {
    $("#NombreLaboratorio").autocomplete({
        source: nombresLabs
    });
});

/* --------------------------------------SECCIÓN PARA COMPROBACIONES Y VALIDACIONES---------------------------------*/

//Función para evitar nombres de laboratorios repetidos
function comprobarNombre() {
    var nombre = document.getElementById("NombreLaboratorio");
    nombre.value = nombre.value.toUpperCase();
    if (nombre.value.length <= 0) {
        nombre.focus();
        nombre.style.borderColor = "#900C3F";
        $('#errorNombreLab').html('El campo nombre no debe estar vacio').show();
        setTimeout("$('#errorNombreLab').html('').hide('slow')", 6000);
    } else {
        for (var i = 0; i < datosLaboratorios.length; i++) {
            if ((datosLaboratorios[i].NombreLaboratorio).toUpperCase() == nombre.value) {
                nombre.style.borderColor = "#900C3F";
                $('#errorNombreLab').html("El nombre del laboratorio: " + nombre.value + " ya existe").show();
                setTimeout("$('#errorNombreLab').html('').hide('slow')", 6000);
                nombre.value = "";
                break;
            } else {
                nombre.style.borderColor = "#ccc";
                $('#errorNombreLab').html('').hide();
            }
        }
    }
}

//Función para validar nombres repetidos en actualización de laboratorios 
function validarNombreModificación() {
    var nombre = document.getElementById("NombreLaboratorio");
    nombre.value = nombre.value.toUpperCase();
    if (nombre.value != nombreLabModificar.toUpperCase()) {
        for (var i = 0; i < datosLaboratorios.length; i++) {
            if ((datosLaboratorios[i].NombreLaboratorio).toUpperCase() == nombre.value) {
                nombre.style.borderColor = "#900C3F";
                $('#errorNombreLab').html("El nombre del laboratorio: " + nombre.value + " ya existe").show();
                setTimeout("$('#errorNombreLab').html('').hide('slow')", 6000);
                nombre.value = "";
                break;
            } else {
                nombre.style.borderColor = "#ccc";
                $('#errorNombreLab').html('').hide();
            }
        }
    } else {
        nombre.style.borderColor = "#ccc";
        $('#errorNombreLab').html('').hide();
    }
}


//Funciones para validaciones de campos de texto
function validarInputNombre() {
    var esValido = true;
    var nomLab = document.getElementById("NombreLaboratorio");
    //Validación para el campo de texto nombre de laboratorio
    if (nomLab.value.length <= 0) {
        esValido = false;
        nomLab.focus();
        nomLab.style.borderColor = "#900C3F";
        $('#errorNombreLab').html('El campo nombre no debe estar vacio').show();
        setTimeout("$('#errorNombreLab').html('').hide('slow')", 6000);
    } else {
        nomLab.style.borderColor = "#ccc";
        $('#errorNombreLab').html('').hide();
    }
    return esValido;
}

//Función para validar el campo Ubicación
function validarInputUbicacion() {
    var esValido = true;
    var ubicacionLab = document.getElementById("UbicacionLaboratorio");
    //Validación para el campo de texto ubicacion de laboratorio
    if (ubicacionLab.value.length <= 0) {
        esValido = false;
        ubicacionLab.style.borderColor = "#900C3F";
        $('#errorUbicacionLab').html('El campo ubicación no debe estar vacio').show();
        setTimeout("$('#errorUbicacionLab').html('').hide('slow')", 6000);
    } else {
        ubicacionLab.style.borderColor = "#ccc";
        $('#errorUbicacionLab').html('').hide();
    }
    return esValido;
}

/* --------------------------------------SECCIÓN PARA MENSAJES DE TOOLTIPS---------------------------------*/

//Mensajes para los tooltips
function mensajesTooltips() {
    document.getElementById("NombreLaboratorio").title = "Máximo 50 caracteres en Mayúscula, sin Espacios.\n Caracteres especiales permitidos - / _ .";
    document.getElementById("UbicacionLaboratorio").title = "Máximo 50 caracteres.\n Caracteres especiales permitidos - / _ .";
    document.getElementById("DescripcionLaboratorio").title = "Máximo 150 caracteres.\n Caracteres especiales permitidos - / _ .";
}

/* --------------------------------------SECCIÓN PARA OPERACIONES CON USUARIO INVITADO---------------------------------*/
//Función para bloquear botones cuando el usuario es invitado
function botones(url) {
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            rol = data;
            if (data == "Invitado") {
                $(':button').prop('disabled', true);
                desactivarBotonesTabla();
            }
        }
    });
}

function desactivarBotonesTabla() {
    //console.log(rol);
    var table = $('#dataTableLaboratorios').DataTable();
    //Metodo para bloquear los botones cuando sea usuario invitado
    if (rol == "Invitado") {
        var rows = table.rows({ 'search': 'applied' }).nodes();
        $('button', rows).attr("disabled", "disabled");
    }
}