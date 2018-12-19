﻿var url_idioma = obtenerIdioma();
var cmbEstados = listaEstadosActivos();
var url_metodo;
var datosActivos;
var idActivoMod;

/* --------------------------------------SECCIÓN PARA OBTENER DATOS DEL SERVIDOR---------------------------------*/
//Método ajax para obtener los datos de los activos
function obtenerActivos(url) {
    url_metodo = url;
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            if (data.OperacionExitosa) {
                datosActivos = data.ListaObjetoInventarios;
                cargarActivosTabla();
                $('#dataTableActivos').DataTable({
                    "language": {
                        "url": url_idioma
                    },
                    "order": [[1, "asc"]]
                });
            } else {
                showNotify("Error en la Consulta", 'No se ha podido mostrar los datos: ' + data.MensajeError, "error");
            }
        }
    });
}


/* --------------------------------------SECCIÓN PARA CARGAR TABLAS Y COMBOBOX---------------------------------*/
//Función para cargar la tabla de Activos
function cargarActivosTabla() {
    var str = '<table id="dataTableActivos" class="table jambo_table bulk_action  table-bordered " style="width:100%">';
    str += '<thead> <tr> <th>Tipo de Activo</th> <th>Nombre del Activo</th> <th>Marca</th> <th>Modelo</th> <th>Serial</th> <th>Laboratorio</th> <th>Fecha Adquisición<br/>(mm/dd/yyyy)</th> <th>Código QR</th> <th>Custodio</th> <th>Estado del Activo</th><th>Cambiar Estado</th>  </tr> </thead>';
    str += '<tbody>';
    for (var i = 0; i < datosActivos.length; i++) {
        if (datosActivos[i].EstadoActivo != "DE BAJA") {
            //Método para dar formato a la fecha y hora
            var fechaLog = new Date(parseInt((datosActivos[i].FechaIngresoActivo).substr(6)));
            //var fechaIngreso = (fechaLog.toLocaleDateString("es-ES"));

            //fecha para la tabla y busquedas
            function pad(n) { return n < 10 ? "0" + n : n; }
            var fechaIngreso = pad(fechaLog.getMonth() + 1) + "/" + pad(fechaLog.getDate()) + "/" + fechaLog.getFullYear();

            str += '<tr><td>' + datosActivos[i].NombreTipoActivo +
                '</td><td>' + datosActivos[i].NombreActivo +
                '</td><td>' + datosActivos[i].NombreMarca +
                '</td><td>' + datosActivos[i].ModeloActivo +
                '</td><td>' + datosActivos[i].SerialActivo +
                '</td><td>' + datosActivos[i].NombreLaboratorio +
                '</td><td>' + fechaIngreso +
                '</td><td>' + datosActivos[i].IdCQR +
                '</td><td>' + datosActivos[i].ResponsableActivo +
                '</td><td>' + datosActivos[i].EstadoActivo;
            str += '</td><td><div class="text-center"><div class="col-md-12 col-sm-12 col-xs-12">';
            if (datosActivos[i].EstadoActivo == "OPERATIVO") {
                str += '<button type = "button" class="btn btn-success text-center" data-toggle="modal" data-target="#ModificarEstadoActivo" onclick = "habilitarOdeshabilitar(' + datosActivos[i].IdActivo + ');" > <strong><span class="fa fa-check"></span></strong></button> ';
            } else if (datosActivos[i].EstadoActivo == "NO OPERATIVO") {
                str += '<button type = "button" class="btn btn-warning text-center" data-toggle="modal" data-target="#ModificarEstadoActivo" onclick = "habilitarOdeshabilitar(' + datosActivos[i].IdActivo + ');" > <strong><span class="fa fa-warning"></span></strong></button> ';
            }
            str += '</div></div>' +
                '</td ></tr > ';
        }

    }
    str += '</tbody>' +
        '</table > ';
    $("#tablaActivos").html(str);

    //Metodo para bloquear los botones cuando sea usuario invitado
    if (rol == "Invitado") {
        $("#dataTableActivos :button").attr("disabled", "disabled");
    }
}

//Función para cargar el combobox de estados para modificar
function cargarEstadosModificarCmb() {
    var str = '<select id="EstadoActivoModificar" class="form-control" name="EstadoActivoModificar" required>';
    str += '<option value="">Escoga una opción...</option>';
    for (var i = 0; i < cmbEstados.length; i++) {
        str += '<option value="' + cmbEstados[i] + '">' + cmbEstados[i] + '</option>';
    }
    str += '</select>';
    $("#cargarEstadosActivo").html(str);
}

/* --------------------------------------SECCIÓN PARA MODIFICACION DE DATOS---------------------------------*/
//Función para cambiar de estado al Activo
function habilitarOdeshabilitar(idActivo) {
    idActivoMod = idActivo;
}

//Función para cambiar de estado al Activo
function actualizarEstadoActivo(url) {

    var cmbEstado = document.getElementById("EstadoActivoModificar");
    var idEstado = cmbEstado.options[cmbEstado.selectedIndex].value;

    if (validacionesEstadoActivo()) {
        swal({
            title: 'Confirmación de Actualización',
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
                    data: {
                        "IdActivo": idActivoMod, "EstadoActivo": idEstado
                    },
                    url: url,
                    type: 'post',
                    success: function (data) {
                        if (data.OperacionExitosa) {
                            $('#ModificarEstadoActivo').modal('hide');
                            showNotify("Actualización exitosa", 'El Estado del Activo se ha modificado exitosamente', "success");
                            obtenerActivos(url_metodo);
                        } else {
                            $('#ModificarEstadoActivo').modal('hide');
                            showNotify("Error en la Actualización", 'Ocurrió un error al modificar el estado del Activo' + data.MensajeError, "error");
                        }

                    }, error: function () {

                    }
                });
            } else {
                $('#ModificarEstadoActivo').modal('hide');
            }
        });
    }
}