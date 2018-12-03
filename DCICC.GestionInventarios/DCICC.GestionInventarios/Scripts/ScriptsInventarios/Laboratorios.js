var url_idioma = obtenerIdioma();
var url_metodo;
var datosLaboratorios;
var idLaboratorio;
var urlEstado;
var nombresLabs = [];

//Método ajax para obtener los datos de laboratorios
function obtenerLaboratorios(url) {
    url_metodo = url;
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'get',
        success: function (data) {
            console.log("Datos Exitos");
            datosLaboratorios = data;
            cargarLaboratoriosTabla();
            $('#dataTableLaboratorios').DataTable({
                "language": {
                    "url": url_idioma
                }
            });
            cargarNombresLaboratorios();
        }
    });
}

//Metodo para obtener la url de cambio de estado
function urlEstados(url) {
    urlEstado = url;
}

//Función para cargar la tabla de Laboratorios
function cargarLaboratoriosTabla() {
    var str = '<table id="dataTableLaboratorios" class="table jambo_table bulk_action table-bordered" style="width:100%">';
    str += '<thead> <tr> <th>Nombre de Laboratorio</th> <th>Ubicación</th> <th>Descripción</th> <th>Estado</th> <th>Modificar</th> <th>Habilitar/<br>Deshabilitar</th> </tr> </thead>';
    str += '<tbody>';
    for (var i = 0; i < datosLaboratorios.length; i++) {
        str += '<tr><td>' + datosLaboratorios[i].NombreLaboratorio +
            '</td><td>' + datosLaboratorios[i].UbicacionLaboratorio +
            '</td><td>' + datosLaboratorios[i].DescripcionLaboratorio;

        if (datosLaboratorios[i].HabilitadoLaboratorio) {
            str += '</td><td> Habilitado';
        } else {
            str += '</td><td> Deshabilitado';
        }

        str += '</td><td><div class="text-center"><div class="col-md-12 col-sm-12 col-xs-12">' +
            '<button type="button" class="btn btn-info text-center" data-toggle="modal" data-target="#ModificarLaboratorios" onclick = "formUpdateLaboratorio(' + datosLaboratorios[i].IdLaboratorio + ');"> <strong><i class="fa fa-pencil-square-o"></i></strong></button> ' +
            '</div></div>' +
            '</td><td><div class=" text-center"><div class="col-md-12 col-sm-12 col-xs-12">';
        if (datosLaboratorios[i].HabilitadoLaboratorio) {
            str += '<button type = "button" class="btn btn-success text-center" onclick = "habilitarOdeshabilitar(' + datosLaboratorios[i].IdLaboratorio + ',' + datosLaboratorios[i].HabilitadoLaboratorio +');"> <strong><span class="fa fa-toggle-on"></span></strong></button> ';
        } else {
            str += '<button type = "button" class="btn btn-danger text-center" onclick = "habilitarOdeshabilitar(' + datosLaboratorios[i].IdLaboratorio + ',' + datosLaboratorios[i].HabilitadoLaboratorio +');"> <strong><i class="fa fa-toggle-off"></i></strong></button> ';
        }
         str +='</div></div></td></tr>';
    };
    str += '</tbody></table>';
    $("#tablaModificarLaboratorios").html(str);
}

//Función para setear los valores en los inputs
function formUpdateLaboratorio(idLab) {
    console.log(url_metodo);
    console.log(idLab);
    idLaboratorio = idLab;
    for (var i = 0; i < datosLaboratorios.length; i++) {
        if (datosLaboratorios[i].IdLaboratorio == idLab) {
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
    console.log(url_modificar);
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
                        console.log(data.OperacionExitosa);
                        if (data.OperacionExitosa) {
                            $('#ModificarLaboratorios').modal('hide');
                            showNotify("Actualización exitosa", 'El Laboratorio se ha modificado correctamente', "success");
                            obtenerLaboratorios(url_metodo);
                        } else {
                            $('#ModificarLaboratorios').modal('hide');
                            showNotify("Error en la Actualización", 'No se ha podido modificar el Laboratorio: ' + data.MensajeError, "error");
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
        text: "¿Está seguro de Cambiar de Estado el Laboratorio?",
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
                        showNotify("Actualización exitosa", 'El Estado del Laboratorio se ha modificado correctamente', "success");
                        obtenerLaboratorios(url_metodo);
                    } else {                        
                        showNotify("Error en la Actualización", 'No se ha podido modificar el Estado del Laboratorio: ' + data.MensajeError, "error");
                    }
                   
                }
            });
        } else {

        }
    });
}

//Función para evitar nombres de laboratorios repetidos
function comprobarNombre(nombre) {
    nombre = nombre.toUpperCase();
    var comprobar = false;
    for (var i = 0; i < datosLaboratorios.length; i++) {
        if ((datosLaboratorios[i].NombreLaboratorio).toUpperCase() == nombre) {
            comprobar = true;
        }
    }

    console.log(comprobar);
    if (comprobar == true) {
        document.getElementById("NombreLaboratorio").setCustomValidity("El nombre del laboratorio: " + nombre + " ya existe");
    } else {
        document.getElementById("NombreLaboratorio").setCustomValidity("");
    }
}

/////////////////////////Funciones para cargar el campo de autocompletado
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

/////////////Funciones para validaciones de campos de texto

function validarInputNombre() {
    var esValido = true;
    var boton = document.getElementById("confirmarLab");
    var nomLab = document.getElementById("NombreLaboratorio");
    //Validación para el campo de texto nombre de laboratorio
    if (nomLab.value.length <= 0) {
        esValido = false;
        nomLab.style.borderColor = "#900C3F";
        $('#errorNombreLab').html('El campo nombre no debe estar vacio').show();
        setTimeout("$('#errorNombreLab').html('').hide('slow')", 6000);
        boton.disabled = true;
    } else {
        nomLab.style.borderColor = "#ccc";
        $('#errorNombreLab').html('').hide();
        boton.disabled = false;
    }
    return esValido;
}

function validarInputUbicacion() {
    var esValido = true;
    var boton = document.getElementById("confirmarLab");
    var ubicacionLab = document.getElementById("UbicacionLaboratorio");
    //Validación para el campo de texto ubicacion de laboratorio
    if (ubicacionLab.value.length <= 0) {
        esValido = false;
        ubicacionLab.style.borderColor = "#900C3F";
        $('#errorUbicacionLab').html('El campo ubicación no debe estar vacio').show();
        setTimeout("$('#errorUbicacionLab').html('').hide('slow')", 6000);
        boton.disabled = true;
    } else {
        ubicacionLab.style.borderColor = "#ccc";
        $('#errorUbicacionLab').html('').hide();
        boton.disabled = false;
    }
    return esValido;
}
