var url_idioma = obtenerIdioma();
var url_metodo;
var datosLuns;
var cmbStorage;
var cmbStorageComp;
var idLunModificar;
var nombreLunModificar;
var urlEstadoLuns;
var nombresLun = [];

/* --------------------------------------SECCIÓN PARA OBTENER DATOS DEL SERVIDOR---------------------------------*/
//Método ajax para obtener los datos de Luns
function obtenerLuns(url) {
    url_metodo = url;
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            if (data.OperacionExitosa) {
                datosLuns = data.ListaObjetoInventarios;
                cargarLunTabla();
                $('#dataTableLun').DataTable({
                    "language": {
                        "url": url_idioma
                    }
                });
                cargarNombresLun();
            } else {
                showNotify("Error en la Consulta", 'No se ha podido mostrar los datos: ' + data.MensajeError, "error");
            }           
        }
    });
}

//Método ajax para obtener los datos de StorageHab
function obtenerCmbStorageHab(url) {    
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            if (data.OperacionExitosa) {
                console.log("Datos Exitosos");
                cmbStorage = data.ListaObjetoInventarios;
                cargarStorageCmb(); 
            } else {
                showNotify("Error en la Consulta", 'No se ha podido mostrar los datos: ' + data.MensajeError, "error");
            }
                      
        }
    });
}

//Función para cargar el combobox de StorageHab
function cargarStorageCmb() {
    var str = '<select id="IdStorage" class="form-control" name="IdStorage" required>';
    str += '<option value="">Escoga una opción...</option>';
    for (var i = 0; i < cmbStorage.length; i++) {
        str += '<option value="' + cmbStorage[i].IdStorage + '">' + cmbStorage[i].NickStorage + '</option>';
    }
    str += '</select>';
    $("#cargarStorage").html(str);
}

//Método ajax para obtener los datos de todos Storage
function obtenerCmbStorageComp(url) {
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            if (data.OperacionExitosa) {
                console.log("Datos Exitosos");
                cmbStorageComp = data.ListaObjetoInventarios;
                cargarStorageCompCmb();
            } else {
                showNotify("Error en la Consulta", 'No se ha podido mostrar los datos: ' + data.MensajeError, "error");
            }        
        }
    });
}

/* --------------------------------------SECCIÓN PARA CARGAR TABLAS Y COMBOBOX---------------------------------*/
//Función para cargar el combobox de Storage
function cargarStorageCompCmb() {
    var str = '<select id="IdStorage" class="form-control" name="IdStorage" required>';
    for (var i = 0; i < cmbStorageComp.length; i++) {
        str += '<option value="' + cmbStorageComp[i].IdStorage + '">' + cmbStorageComp[i].NickStorage + '</option>';
    }
    str += '</select>';
    $("#cargarStorage").html(str);
}

//Función para obtener la url de modificación
function urlEstadosLun(url) {
    urlEstadoLuns = url;
}

//Función para cargar la tabla de Luns
function cargarLunTabla() {
    var str = '<table id="dataTableLun" class="table jambo_table bulk_action  table-bordered " style="width:100%">';
    str += '<thead> <tr> <th>Nombre de LUN</th> <th>Nombre de Storage</th> <th>Capacidad (GB/TB)</th> <th>RAID/Tipo Conexión</th> <th>Descripcion</th> <th>Estado</th> <th>Modificar</th> <th>Habilitar/<br>Deshabilitar</th> </tr> </thead>';
    str += '<tbody>';
    for (var i = 0; i < datosLuns.length; i++) {
        str += '<tr><td>' + datosLuns[i].NombreLUN +
            '</td><td>' + datosLuns[i].NombreStorage +
            '</td><td>' + datosLuns[i].CapacidadLUN +
            '</td><td>' + datosLuns[i].RaidTPLUN +
            '</td><td>' + datosLuns[i].DescripcionLUN;

        if (datosLuns[i].HabilitadoLUN) {
            str += '</td><td> Habilitado';
        } else {
            str += '</td><td> Deshabilitado';
        }
        str += '</td><td><div class="text-center"><div class="col-md-12 col-sm-12 col-xs-12">' +
            '<button type="button" class="btn btn-info text-center" data-toggle="modal" data-target="#ModificarLuns" onclick = "formUpdateLun(' + datosLuns[i].IdLUN + ');"> <strong><i class="fa fa-pencil-square-o"></i></strong></button> ' +
            '</div></div>' +
            '</td><td><div class="text-center"><div class="col-md-12 col-sm-12 col-xs-12">';
        if (datosLuns[i].HabilitadoLUN) {
            str += '<button type = "button" class="btn btn-success text-center" onclick = "habilitarOdeshabilitarLun(' + datosLuns[i].IdLUN + ',' + datosLuns[i].HabilitadoLUN + ');" > <strong><span class="fa fa-toggle-on"></span></strong></button> ';
        } else {
            str += '<button type = "button" class="btn btn-danger text-center" onclick = "habilitarOdeshabilitarLun(' + datosLuns[i].IdLUN + ',' + datosLuns[i].HabilitadoLUN + ');"> <strong><i class="fa fa-toggle-off"></i></strong></button> ';
        }
        str += '</div></div></td></tr>';
    }
    str += '</tbody></table>';
    $("#tablaModificarLuns").html(str);
}

/* --------------------------------------SECCIÓN PARA MODIFICACION DE DATOS---------------------------------*/
//Función para setear los valores en los inputs en modificaciones
function formUpdateLun(idLun) {
    idLunModificar = idLun;
    for (var i = 0; i < datosLuns.length; i++) {
        if (datosLuns[i].IdLUN == idLun) {
            nombreLunModificar = datosLuns[i].NombreLUN;
            //Métodos para setear los valores a modificar
            document.getElementById("NombreLUN").value = datosLuns[i].NombreLUN;
            //Método para setear combobox
            var element = document.getElementById("IdStorage");
            element.value = datosLuns[i].IdStorage;
            //Método para setear inputs
            document.getElementById("CapacidadLUN").value = datosLuns[i].SizeLUN;

            var element1 = document.getElementById("UnidadLUN");
            element1.value = datosLuns[i].UnidadLUN;

            var element1 = document.getElementById("RaidTPLUN");
            element1.value = datosLuns[i].RaidTPLUN;

            document.getElementById("DescripcionLUN").value = datosLuns[i].DescripcionLUN;

            //Método para el check del update de Categorias
            var valor = datosLuns[i].HabilitadoLUN;
            var estado = $('#HabilitadoLUN').prop('checked');
            if (estado && valor == false) {
                document.getElementById("HabilitadoLUN").click();
            }
            if (estado == false && valor == true) {
                document.getElementById("HabilitadoLUN").click();
            }
        }
    }
}

//Función para modificar la categoria especificada
function modificarLun(url_modificar) {
    //Métodos para setear los valores a modificar
    var nombre= document.getElementById("NombreLUN").value;
    //Método para setear combobox
    //Obtener valor del combobox
    var cmbStor = document.getElementById("IdStorage");
    var idStor = cmbStor.options[cmbStor.selectedIndex].value;

    //Método para setear inputs
    var capacidad=document.getElementById("CapacidadLUN").value;

    var cmbUnidad = document.getElementById("UnidadLUN");
    var unidad = cmbUnidad.options[cmbUnidad.selectedIndex].value;

    var cmbRaid = document.getElementById("RaidTPLUN");
    var raid = cmbRaid.options[cmbRaid.selectedIndex].value;

    var descripcion=document.getElementById("DescripcionLUN").value;
    var habilitadoLun = $('#HabilitadoLUN').prop('checked');

    if (validarNombreLun() && validarNumeroLun()) {
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
                        "IdLUN": idLunModificar, "IdStorage": idStor, "NombreLUN": nombre,
                        "CapacidadLUN": capacidad, "UnidadLUN": unidad, "RaidTPLUN": raid,
                        "DescripcionLUN": descripcion, "HabilitadoLUN": habilitadoLun
                    },
                    url: url_modificar,
                    type: 'post',
                    success: function (data) {
                        console.log(data.OperacionExitosa);
                        if (data.OperacionExitosa) {
                            $('#ModificarLuns').modal('hide');
                            showNotify("Actualización exitosa", 'La LUN " ' + nombre.toUpperCase() + ' " se ha modificado exitosamente', "success");
                            obtenerLuns(url_metodo);
                        } else {
                            $('#ModificarLuns').modal('hide');
                            showNotify("Error en la Actualización", 'Ocurrió un error al modificar la LUN: ' + data.MensajeError, "error");
                        }

                    }
                });
            } else {
                $('#ModificarLuns').modal('hide');
            }
        });
    }

}

//Función para habilitar o deshabilitar la categoria
function habilitarOdeshabilitarLun(idLun, estadoLun) {
    var nuevoEstado = true;
    if (estadoLun) {
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
                data: { "IdLUN": idLun, "HabilitadoLUN": nuevoEstado },
                url: urlEstadoLuns,
                type: 'post',
                success: function (data) {
                    console.log(data.OperacionExitosa);
                    if (data.OperacionExitosa) {
                        showNotify("Actualización exitosa", 'El Estado de la LUN se ha modificado exitosamente', "success");
                        obtenerLuns(url_metodo);
                    } else {
                        showNotify("Error en la Actualización", 'Ocurrió un error al modificar el estado de la LUN: ' + data.MensajeError, "error");
                    }
                }
            });
        } else {

        }
    });
}

/* --------------------------------------SECCIÓN PARA CAMPOS DE AUTOCOMPLETE---------------------------------*/
//Funciones para cargar el campo de autocompletado
function cargarNombresLun() {
    for (var i = 0; i < datosLuns.length; i++) {
        nombresLun[i] = datosLuns[i].NombreLUN;
    }
}
//Función para cargar los nombres en el campo de nombre de ingreso de luns
$(function () {
    $("#NombreLUN").autocomplete({
        source: nombresLun
    });
});

/* --------------------------------------SECCIÓN PARA COMPROBACIONES Y VALIDACIONES---------------------------------*/
///Función para evitar nombres de luns repetidos
function comprobarNombreLun() {
    var nomLun = document.getElementById("NombreLUN");
    nomLun.value = nomLun.value.toUpperCase();
    if (nomLun.value.length <= 0) {
        nomLun.style.borderColor = "#900C3F";
        $('#errorNombreLUN').html('El campo nombre no debe estar vacio').show();
        setTimeout("$('#errorNombreLUN').html('').hide('slow')", 6000);
    } else {
        for (var i = 0; i < datosLuns.length; i++) {
            if ((datosLuns[i].NombreLUN).toUpperCase() == nomLun.value) {
                nomLun.style.borderColor = "#900C3F";
                $('#errorNombreLUN').html("El nombre del Storage: " + nomLun.value + " ya existe").show();
                setTimeout("$('#errorNombreLUN').html('').hide('slow')", 6000);
                nomLun.value = "";
                break;
            } else {
                nomLun.style.borderColor = "#ccc";
                $('#errorNombreLUN').html('').hide();
            }
        }
    }
}

///Función para evitar nombres de luns repetidos
function validarNombreModificar() {
    var nomLun = document.getElementById("NombreLUN");
    nomLun.value = nomLun.value.toUpperCase();
    if (nomLun.value != nombreLunModificar.toUpperCase()) {
        for (var i = 0; i < datosLuns.length; i++) {
            if ((datosLuns[i].NombreLUN).toUpperCase() == nomLun.value) {
                nomLun.style.borderColor = "#900C3F";
                $('#errorNombreLUN').html("El nombre del Storage: " + nomLun.value + " ya existe").show();
                setTimeout("$('#errorNombreLUN').html('').hide('slow')", 6000);
                nomLun.value = "";
                break;
            } else {
                nomLun.style.borderColor = "#ccc";
                $('#errorNombreLUN').html('').hide();
            }
        }
    } else {
        nomLun.style.borderColor = "#ccc";
        $('#errorNombreLUN').html('').hide();
    }
}

//Función para evitar campos vacios
function validarNombreLun() {
    var esValido = true;
    var nomLun = document.getElementById("NombreLUN");
    nomLun.value = nomLun.value.toUpperCase();
    if (nomLun.value.length <= 0) {
        esValido = false;
        nomLun.style.borderColor = "#900C3F";
        $('#errorNombreLUN').html('El campo nombre no debe estar vacio').show();
        setTimeout("$('#errorNombreLUN').html('').hide('slow')", 6000);
    } else {
        nomLun.style.borderColor = "#ccc";
        $('#errorNombreLUN').html('').hide();
    }
    return esValido;

}


//Función para validar disco duro 
function validarNumeroLun() {
    var esValido = true;
    var capa = document.getElementById("CapacidadLUN");
    //Validar memoria capa
    if (capa.value.length <= 0) {
        esValido = false;
        capa.value = "";
        capa.style.borderColor = "#900C3F";
        $('#errorCapacidadLun').html('El campo capacidad no debe estar vacio').show();
        setTimeout("$('#errorCapacidadLun').html('').hide('slow')", 6000);
    } else if (capa.value < 1) {
        esValido = false;
        capa.value = "";
        capa.style.borderColor = "#900C3F";
        $('#errorCapacidadLun').html('El rango de capacidad es de 1 a 1000').show();
        setTimeout("$('#errorCapacidadLun').html('').hide('slow')", 6000);
    } else if (capa.value > 1000) {
        esValido = false;
        capa.value = "";
        capa.style.borderColor = "#900C3F";
        $('#errorCapacidadLun').html('No se puede ingresar un valor mayor a 1000').show();
        setTimeout("$('#errorCapacidadLun').html('').hide('slow')", 6000);
    } else {
        capa.style.borderColor = "#ccc";
        $('#errorCapacidadLun').html('').hide();
    }
    return esValido;
}

/* --------------------------------------SECCIÓN PARA MENSAJES DE TOOLTIPS---------------------------------*/
//Mensajes para los tooltips
function mensajesTooltipLun() {
    document.getElementById("NombreLUN").title = "Máximo 80 caracteres en Mayúscula.\n Caracteres especiales permitidos - / _ .";
    document.getElementById("CapacidadLUN").title = "Solo Números. De 1 a 100 GB o TB";
    document.getElementById("DescripcionLUN").title = "Máximo 150 caracteres.\n Caracteres especiales permitidos - / _ .";
}