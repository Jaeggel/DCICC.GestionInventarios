﻿var url_idioma = obtenerIdioma();
var url_metodo;
var datosLaboratorios;
var idLaboratorio;

function obtenerLaboratorios(url) {
    url_metodo = url;
    console.log(url_metodo);
    //Método ajax para traer los roles de la base de datos
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            console.log(data);
            datosLaboratorios = data;
            console.log("siiiiii: ");
            cargarLaboratoriosTabla();
            $('#dataTableLaboratorios').DataTable({
                "language": {
                    "url": url_idioma
                }
            });
        }
    });
}

//Función para cargar la tabla de Usuarios
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
            '</td><td><div class=" text-center"><div class="col-md-12 col-sm-12 col-xs-12">' +
            '<button type = "button" class="btn btn-danger text-center" > <strong><i class="fa fa-times-circle"></i></strong></button> ' +
            '</div></div></td></tr>';
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

            //Método para el check del update de Usuarios
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
    console.log(url_metodo);

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
                data: { "IdLaboratorio": idLaboratorio, "NombreLaboratorio": nombreLab, "UbicacionLaboratorio": ubicacionLab, "DescripcionLaboratorio": descripcionLab, "HabilitadoLaboratorio": habilitadoLab },
                url: url_modificar,
                type: 'post',
                success: function () {
                    $('#ModificarLaboratorios').modal('hide');
                    showNotify("Actualización exitosa", 'La Categoria de Activo se ha modificado correctamente', "success");
                    obtenerLaboratorios(url_metodo);
                }, error: function () {
                    $('#ModificarLaboratorios').modal('hide');
                    showNotify("Error en la Actualización", 'No se ha podido modificar la Categoría del Activo', "error");
                }
            });
        } else {
            $('#ModificarLaboratorios').modal('hide');
        }
    });
}

//Función para evitar nombres de nick repetidos
function comprobarNombre(nombre) {
    nombre = nombre.toLowerCase();
    var comprobar = false;
    for (var i = 0; i < datosLaboratorios.length; i++) {
        if ((datosLaboratorios[i].NombreLaboratorio).toLowerCase() == nombre) {
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