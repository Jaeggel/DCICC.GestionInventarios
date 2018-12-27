var url_idioma = obtenerIdioma();
var cmbEstados = listaEstadosActivos();
var url_metodo;
var datosActivos;
var idActivoMod;
var cmbTipoActivo;

/* --------------------------------------SECCIÓN PARA OBTENER DATOS DEL SERVIDOR---------------------------------*/
//Método ajax para obtener los datos de los activos
function obtenerVidaUtil(url) {
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
                    "order": [[2, "asc"]]
                });
                cargarEstadosModificarCmb();
            } else {
                showNotify("Error en la Consulta", 'No se ha podido mostrar los datos: ' + data.MensajeError, "error");
            }
        }
    });
}

//Método ajax para obtener los datos de tipos de activos
function datosTipoActivo(url) {
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            if (data.OperacionExitosa) {
                cmbTipoActivo = data.ListaObjetoInventarios;
                TipoActivoFiltroAct();
            } else {
                showNotify("Error en la Consulta", 'No se ha podido mostrar los datos: ' + data.MensajeError, "error");
            }
        }
    });
}


/* --------------------------------------SECCIÓN PARA CARGAR TABLAS Y COMBOBOX---------------------------------*/

//Función para cargar el combobox de tipo de activo
function TipoActivoFiltroAct() {
    var str = '<select id="TipoActivoFiltroAct" class="form-control" name="TipoActivoFiltroAct"  required>';
    str += '<option value="">Mostrar Todos</option>';
    for (var i = 0; i < cmbTipoActivo.length; i++) {
        str += '<option value="' + cmbTipoActivo[i].IdTipoActivo + '">' + cmbTipoActivo[i].NombreTipoActivo + '</option>';
    }
    str += '</select>';
    $("#cargarTipoActivosFiltro").html(str);
    ///////CAMBIO DEL COMBOBOX
    $('#TipoActivoFiltroAct').change(function () {
        var opcion = document.getElementById("TipoActivoFiltroAct");
        var tipoAct = opcion.options[opcion.selectedIndex];
        if (tipoAct.value == "") {
            $('#dataTableActivos').DataTable().column(0).search(
                ""
            ).draw();
        } else {
            $('#dataTableActivos').DataTable().column(0).search(
                tipoAct.text
            ).draw();
        }
    });
}


//Función para cargar el combobox de estados para ingreso de accesorios
function EstadosAccesoriosFiltro() {
    var str = '<select id="EstadosAccesoriosFiltro" class="form-control" name="EstadosAccesoriosFiltro" required>';
    str += '<option value="">Mostrar Todos</option>';
    for (var i = 0; i < cmbEstados.length; i++) {
        if (cmbEstados[i] != "DE BAJA") {
            str += '<option value="' + cmbEstados[i] + '">' + cmbEstados[i] + '</option>';
        }
    }
    str += '</select>';
    $("#cargarEstadosAccesorioFiltro").html(str);
    //Método para búsqueda con filtros
    $('#EstadosAccesoriosFiltro').change(function () {
        var opcion = document.getElementById("EstadosAccesoriosFiltro");
        var tipoLab = opcion.options[opcion.selectedIndex];
        if (tipoLab.value == "") {
            $('#dataTableActivos').DataTable().column(8).search(
                ""
            ).draw();
        } else {
            $('#dataTableActivos').DataTable().column(8).search('^' + tipoLab.text + '$', true, false
            ).draw();
        }
    });
}

//Función para cargar la tabla de Activos
function cargarActivosTabla() {
    var str = '<table id="dataTableActivos" class="table jambo_table bulk_action  table-bordered " style="width:100%">';
    str += '<thead> <tr> <th>Tipo de Activo</th> <th>Vida Útil</th> <th>Fecha Adquisición<br/>(mm/dd/yyyy)</th> <th>Nombre del Activo</th><th>Modelo</th> <th>Serial</th> <th>Laboratorio</th> <th>Custodio</th> <th>Estado del Activo</th> <th>Fecha de finalización de Vida Útil</th><th>Cambiar Estado</th>  </tr> </thead>';
    str += '<tbody>';
    for (var i = 0; i < datosActivos.length; i++) {
        
            //Método para dar formato a la fecha y hora
            var fechaLog = new Date(parseInt((datosActivos[i].FechaIngresoActivo).substr(6)));
            //fecha para la tabla y busquedas
            function pad(n) { return n < 10 ? "0" + n : n; }
            var fechaIngreso = pad(fechaLog.getMonth() + 1) + "/" + pad(fechaLog.getDate()) + "/" + fechaLog.getFullYear();

            var fechaFin = new Date(parseInt((datosActivos[i].VidaFinalTipoActivo).substr(6)));
            var fechaFinActivo = pad(fechaFin.getMonth() + 1) + "/" + pad(fechaFin.getDate()) + "/" + fechaFin.getFullYear();


            str += '<tr><td>' + datosActivos[i].NombreTipoActivo +
                '</td><td>' + datosActivos[i].VidaUtilTipoActivo +
                '</td><td>' + fechaIngreso +
                '</td><td>' + datosActivos[i].NombreActivo +
                '</td><td>' + datosActivos[i].ModeloActivo +
                '</td><td>' + datosActivos[i].SerialActivo +
                '</td><td>' + datosActivos[i].NombreLaboratorio +

                '</td><td>' + datosActivos[i].ResponsableActivo +
                '</td><td>' + datosActivos[i].EstadoActivo +
                '</td><td>' + fechaFinActivo;
            str += '</td><td><div class="text-center"><div class="col-md-12 col-sm-12 col-xs-12">';
            if (datosActivos[i].EstadoActivo == "OPERATIVO") {
                str += '<button type = "button" class="btn btn-success text-center" data-toggle="modal" data-target="#ModificarEstadoActivo" onclick = "habilitarOdeshabilitar(' + datosActivos[i].IdActivo + ');" > <strong><span class="fa fa-check"></span></strong></button> ';
            } else if (datosActivos[i].EstadoActivo == "NO OPERATIVO") {
                str += '<button type = "button" class="btn btn-warning text-center" data-toggle="modal" data-target="#ModificarEstadoActivo" onclick = "habilitarOdeshabilitar(' + datosActivos[i].IdActivo + ');" > <strong><span class="fa fa-warning"></span></strong></button> ';
            }
            str += '</div></div>' +
                '</td ></tr > ';

    }
    str += '</tbody>' +
        '</table > ';
    $("#tablaActivosVidaUtil").html(str);

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
                            obtenerVidaUtil(url_metodo);
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

//Función para validar el estado de activo
function validacionesEstadoActivo() {
    var isValid = true;

    //Validación para combobox tipo de Activo
    if (document.getElementById("EstadoActivoModificar").value == "") {
        isValid = false;
        document.getElementById("EstadoActivoModificar").style.borderColor = "#900C3F";
        $('#errorEstadoActivo').html('Debe seleccionar una Opción del Estado de Activo').show();
        setTimeout("$('#errorEstadoActivo').html('').hide('slow')", 6000);
    } else {
        document.getElementById("EstadoActivoModificar").style.borderColor = "#ccc";
        $('#errorEstadoActivo').html('').hide();
    }
    return isValid;
}