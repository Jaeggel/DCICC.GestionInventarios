var url_idioma = obtenerIdioma();
var cmbEstados = listaEstadosActivos();
var url_metodo;
var datosActivos;
var idActivoMod;
var NombreActMod;
var cmbTipoActivo;
var cmbLaboratorio;
var cmbMarcas;
var idAccesorioIng;
var nombreAccesorioIng;

var idCQRAccesorio;
var nombreCQRAccesorio;
var nombreActivoPert;

var nombresActivo = [];
var rolAct;
var url_bloquear;

var fechas = [];
var maxDate;
var minDate;

/***********************************************************************************
 *                SECCIÓN PARA OPERACIONES CON ACTIVOS
 * **********************************************************************************/

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
                    scrollX: true,                    
                    fixedColumns: {
                        leftColumns:2,
                        rightColumns: 3
                    },
                    "order": [[1, "asc"]]
                });
                              
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
                cargarTipoActivoCmb();
                TipoActivoFiltroAct();
            } else {
                showNotify("Error en la Consulta", 'No se ha podido mostrar los datos: ' + data.MensajeError, "error");
            }           
        }
    });
}

//Método ajax para obtener los datos de los laboratorios
function datosLaboratorio(url) {
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            if (data.OperacionExitosa) {
                cmbLaboratorio = data.ListaObjetoInventarios;
                cargarLaboratoriosCmb();
                LaboratoriosFiltroAct();
            } else {
                showNotify("Error en la Consulta", 'No se ha podido mostrar los datos: ' + data.MensajeError, "error");
            }

        }
    });
}

//Método ajax para obtener los datos de las marcas
function datosMarcas(url) {
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            if (data.OperacionExitosa) {
                cmbMarcas = data.ListaObjetoInventarios;
                cargarMarcasCmb();
                MarcasFiltroAct();
            } else {
                showNotify("Error en la Consulta", 'No se ha podido mostrar los datos: ' + data.MensajeError, "error");
            }
        }
    });
}

/* --------------------------------------SECCIÓN PARA CARGAR TABLAS Y COMBOBOX---------------------------------*/
//Función para cargar el combobox de tipo de activo
function cargarTipoActivoCmb() {   
    var str = '<select id="TipoActivo" class="form-control" name="TipoActivo"  required>';
    for (var i = 0; i < cmbTipoActivo.length; i++) {
            str += '<option value="' + cmbTipoActivo[i].IdTipoActivo + '">' + cmbTipoActivo[i].NombreTipoActivo + '</option>';
    }
    str += '</select>';
    $("#cargarTipoActivos").html(str);
}

//Función para cargar el combobox de laboratorios
function cargarLaboratoriosCmb() {
    var str = '<select id="LaboratorioActivo" class="form-control" name="LaboratorioActivo"  required>';
    for (var i = 0; i < cmbLaboratorio.length; i++) {
        str += '<option value="' + cmbLaboratorio[i].IdLaboratorio + '">' + cmbLaboratorio[i].NombreLaboratorio + '</option>';
    }
    str += '</select>';
    $("#cargarLaboratorios").html(str);
}

//Función para cargar el combobox de Marcas
function cargarMarcasCmb() {
    var str = '<select id="MarcaActivo" class="form-control" name="MarcaActivo"   required>';
    for (var i = 0; i < cmbMarcas.length; i++) {
        str += '<option value="' + cmbMarcas[i].IdMarca + '">' + cmbMarcas[i].NombreMarca + '</option>';
    }
    str += '</select>';
    $("#cargarMarcas").html(str);
}

//Función para cargar el combobox de estados
function cargarEstadosCmb() {
    var str = '<select id="EstadoActivo" class="form-control" name="EstadoActivo" onBlur=" validacionesCamposModificar();" required>';
    for (var i = 0; i < cmbEstados.length; i++) {
        str += '<option value="' + cmbEstados[i] + '">' + cmbEstados[i] + '</option>';
    }
    str += '</select>';
    $("#cargarEstados").html(str);
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

/* --------------------------------------SECCIÓN PARA CARGAR COMBOBOX DE FILTROS---------------------------------*/
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

//Función para cargar el combobox de laboratorios
function LaboratoriosFiltroAct() {
    var str = '<select id="LaboratoriosFiltroAct" class="form-control" name="LaboratoriosFiltroAct"  required>';
    str += '<option value="">Mostrar Todos</option>';
    for (var i = 0; i < cmbLaboratorio.length; i++) {
        str += '<option value="' + cmbLaboratorio[i].IdLaboratorio + '">' + cmbLaboratorio[i].NombreLaboratorio + '</option>';
    }
    str += '</select>';
    $("#cargarLaboratoriosFiltro").html(str);
    ///////CAMBIO DEL COMBOBOX
    $('#LaboratoriosFiltroAct').change(function () {
        var opcion = document.getElementById("LaboratoriosFiltroAct");
        var tipoAct = opcion.options[opcion.selectedIndex];
        if (tipoAct.value == "") {
            $('#dataTableActivos').DataTable().column(5).search(
                ""
            ).draw();
        } else {
            $('#dataTableActivos').DataTable().column(5).search(
                tipoAct.text
            ).draw();
        }
    });
}

//Función para cargar el combobox de Marcas
function MarcasFiltroAct() {
    var str = '<select id="MarcasFiltroAct" class="form-control" name="MarcasFiltroAct"  required>';
    str += '<option value="">Mostrar Todos</option>';
    for (var i = 0; i < cmbMarcas.length; i++) {
        str += '<option value="' + cmbMarcas[i].IdMarca + '">' + cmbMarcas[i].NombreMarca + '</option>';
    }
    str += '</select>';
    $("#cargarMarcasFiltro").html(str);
    ///////CAMBIO DEL COMBOBOX
    $('#MarcasFiltroAct').change(function () {
        var opcion = document.getElementById("MarcasFiltroAct");
        var tipoAct = opcion.options[opcion.selectedIndex];
        if (tipoAct.value == "") {
            $('#dataTableActivos').DataTable().column(2).search(
                ""
            ).draw();
        } else {
            $('#dataTableActivos').DataTable().column(2).search(
                tipoAct.text
            ).draw();
        }
    });

}

//Función para cargar el combobox de estados
function EstadosFiltroAct() {
    var str = '<select id="EstadosFiltroAct" class="form-control" name="EstadosFiltroAct" required>';
    str += '<option value="">Mostrar Todos</option>';
    for (var i = 0; i < cmbEstados.length; i++) {
        str += '<option value="' + cmbEstados[i] + '">' + cmbEstados[i] + '</option>';
    }
    str += '</select>';
    $("#cargarEstadosActivoFiltro").html(str);
    ///////CAMBIO DEL COMBOBOX
    $('#EstadosFiltroAct').change(function () {
        var opcion = document.getElementById("EstadosFiltroAct");
        var tipoAct = opcion.options[opcion.selectedIndex];
        if (tipoAct.value == "") {
            $('#dataTableActivos').DataTable().column(9).search(
                ""
            ).draw();
        } else {
            $('#dataTableActivos').DataTable().column(9).search('^' + tipoAct.text + '$', true, false                 
            ).draw();
        }
    });
}


//Función para cargar la tabla de Activos
function cargarActivosTabla() {
    var str = '<table id="dataTableActivos" class="table  jambo_table bulk_action  table-bordered " style="width:100%">';
    str += '<thead> <tr style="background-color: #405467;"> <th>Tipo de Activo</th> <th>Nombre de Activo</th> <th>Marca</th> <th>Modelo</th> <th>Serial</th> <th>Laboratorio</th> <th>Fecha Adquisición<br/>(mm/dd/yyyy)</th> <th>Código QR</th> <th>Custodio</th> <th>Estado de Activo</th> <th>Agregar Accesorio</th> <th>Modificar</th> <th>Cambiar Estado</th>  </tr> </thead>';
    str += '<tbody>';
    for (var i = 0; i < datosActivos.length; i++) {
        if (datosActivos[i].EstadoActivo != "DE BAJA") {
            //Método para dar formato a la fecha y hora
            var fechaLog = new Date(parseInt((datosActivos[i].FechaIngresoActivo).substr(6)));
            //var fechaIngreso = (fechaLog.toLocaleDateString("es-ES"));
            //fecha para la tabla y busquedas
            function pad(n) { return n < 10 ? "0" + n : n; }
            var fechaIngreso = pad(fechaLog.getMonth() + 1) + "/" + pad(fechaLog.getDate()) + "/" + fechaLog.getFullYear();
            var fechaordenar = (fechaLog.toLocaleDateString("en-US"));
            fechas.push(fechaordenar);

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
            str += '</td><td><div class="text-center"><div class="col-md-12 col-sm-12 col-xs-12">' +
                '<button id="modificar" type="button" class="btn btn-default text-center" data-toggle="modal" data-target="#IngresoNuevoAccesorio" onclick = "formIngresoAccesorio(' + datosActivos[i].IdActivo + ',\'' + datosActivos[i].NombreActivo + '\');" > <strong><i class="fa fa-plug"></i></strong></button> ' +
                '</div></div>';
            str += '</td><td><div class="text-center"><div class="col-md-12 col-sm-12 col-xs-12">' +
                '<button type="button" class="btn btn-info text-center" data-toggle="modal" data-target="#ModificarActivo" onclick = "formUpdateActivos(' + datosActivos[i].IdActivo + ');"> <strong><i class="fa fa-pencil-square-o"></i></strong></button> ' +
                '</div></div>' +
                '</td><td><div class="text-center"><div class="col-md-12 col-sm-12 col-xs-12">';
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
    BloquearbotonesActivos();
    //Cargar Datos Activos
    var minDate = fechas[0];
    inicioFechaAct(minDate);
    finFechaAct(minDate);
}

/* --------------------------------------SECCIÓN PARA OPERACIONES CON FECHAS---------------------------------*/
//Fecha de inicio
function inicioFechaAct(minDate) {
    $(function () {
        $('input[name="FechaInicio"]').daterangepicker({
            startDate: minDate,
            format: 'mm-dd-yyyy',
            singleDatePicker: true,
            showDropdowns: true,
            minDate: minDate,
            maxDate: new Date()
        });
    });
}
//Fecha de Fin
function finFechaAct(minDate) {
    $(function () {
        $('input[name="FechaFin"]').daterangepicker({
            startDate: 0,
            dateFormat: 'mm-dd-yyyyy',
            singleDatePicker: true,
            showDropdowns: true,
            minDate: minDate,
            maxDate: new Date()
        });
    });
}

//Función para obtener el filtro por rango de fechas
function consultarFechas() {
    var table = $('#dataTableActivos').DataTable();
    $.fn.DataTable.ext.search.push(
        function (settings, data, dataIndex) {
            if (settings.sTableId == 'dataTableActivos') {
                var min = new Date($("#FechaInicio").val()).getTime();
                var max = new Date($("#FechaFin").val()).getTime();
                var startDate = new Date(data[6]).getTime();
                if (min == null && max == null) { return true; }
                if (min == null && startDate <= max) { return true; }
                if (max == null && startDate >= min) { return true; }
                if (startDate <= max && startDate >= min) { return true; }
                return false;
            } else {
                return true;
            }

        }
    );
    table.draw();
}

/* --------------------------------------SECCIÓN PARA MODIFICACION DE DATOS---------------------------------*/
//Función para setear los valores en los inputs en modificaciones
function formUpdateActivos(idActivo) {
    idActivoMod = idActivo;
    for (var i = 0; i < datosActivos.length; i++) {
        if (datosActivos[i].IdActivo == idActivo) {
            NombreActMod = datosActivos[i].NombreActivo;
            //Obtener Valor del tipo de activo
            var element = document.getElementById("TipoActivo");
            element.value = datosActivos[i].IdTipoActivo;

            //Obtener valor del Laboratorio
            var element1 = document.getElementById("LaboratorioActivo");
            element1.value = datosActivos[i].IdLaboratorio;

            //Obtener valor de la marca
            var element2 = document.getElementById("MarcaActivo");
            element2.value = datosActivos[i].IdMarca;

            //Obtener valor del Estado
            var element3 = document.getElementById("EstadoActivo");
            element3.value = datosActivos[i].EstadoActivo;
            //Obtener valor del nombre de activo
            document.getElementById("NombreActivo").value = datosActivos[i].NombreActivo;
            //Obtener valor del serial de activo
            document.getElementById("SerialActivo").value = datosActivos[i].SerialActivo;
            //Obtener valor del modelo de activo
            document.getElementById("ModeloActivo").value = datosActivos[i].ModeloActivo;
            //Obtener valor del codigo UPS
            document.getElementById("CodigoUpsActivo").value = datosActivos[i].CodigoUpsActivo;
            //Obtener valor de la fecha de ingreso del activo
            //Obtener valor de la fecha de ingreso del activo
            var fechaLog = new Date(parseInt((datosActivos[i].FechaIngresoActivo).substr(6)));
            //var fechaIngreso = (fechaLog.toLocaleDateString("es-ES"));
            //fecha para la tabla y busquedas
            function pad(n) { return n < 10 ? "0" + n : n; }
            var fechaIngreso = pad(fechaLog.getMonth() + 1) + "/" + pad(fechaLog.getDate()) + "/" + fechaLog.getFullYear();
            $('#FechaIngresoActivo').val(fechaIngreso);
            
            //var fechaIngreso = document.getElementById("FechaIngresoActivo").value;
            //Obtener valor de la descripcion de activo
            document.getElementById("DescripcionActivo").value = datosActivos[i].DescripcionActivo;
            //Obtener valor de la descripcion de activo
            document.getElementById("ResponsableActivo").value = datosActivos[i].ResponsableActivo;
            //Obtener valor del ExpressServiceCodeActivo
            document.getElementById("ExpressServiceCodeActivo").value = datosActivos[i].ExpressServiceCodeActivo;
            //Obtener valor de la FechaManufacturaActivo
            document.getElementById("FechaManufacturaActivo").value = datosActivos[i].FechaManufacturaActivo;
            //Obtener valor del NumPuertosActivo
            document.getElementById("NumPuertosActivo").value = datosActivos[i].NumPuertosActivo;
            //Obtener valor del IosVersionActivo
            document.getElementById("IosVersionActivo").value = datosActivos[i].IosVersionActivo;
            //Obtener valor del ProductNameActivo
            document.getElementById("ProductNameActivo").value = datosActivos[i].ProductNameActivo;
            //Obtener valor del HpePartNumberActivo
            document.getElementById("HpePartNumberActivo").value = datosActivos[i].HpePartNumberActivo;
            //Obtener valor de CodBarras1Activo
            document.getElementById("CodBarras1Activo").value = datosActivos[i].CodBarras1Activo;
            //Obtener valor de CodBarras2Activo
            document.getElementById("CodBarras2Activo").value = datosActivos[i].CodBarras2Activo;
            //Obtener valor del CtActivo
            document.getElementById("CtActivo").value = datosActivos[i].CtActivo;
            //Obtener valor de CapacidadActivo
            document.getElementById("CapacidadActivo").value = datosActivos[i].CapacidadActivo;
            //Obtener valor de VelocidadTransfActivo
            document.getElementById("VelocidadTransfActivo").value = datosActivos[i].VelocidadTransfActivo;           
        }
    }
    
}

//Función para modificar el activo especificado
function actualizarActivo(url) {
    //Obtener Valor del tipo de activo
    var cmbTipoActivo = document.getElementById("TipoActivo");
    var idTipoActivo = cmbTipoActivo.options[cmbTipoActivo.selectedIndex].value;
    //Obtener valor del Laboratorio
    var cmbLaboratorio = document.getElementById("LaboratorioActivo");
    var idLaboratorio = cmbLaboratorio.options[cmbLaboratorio.selectedIndex].value;
    //Obtener valor de la marca
    var cmbMarca = document.getElementById("MarcaActivo");
    var idMarca = cmbMarca.options[cmbMarca.selectedIndex].value;
    //Obtener valor del Estado
    var cmbEstado = document.getElementById("EstadoActivo");
    var idEstado = cmbEstado.options[cmbEstado.selectedIndex].value;
    //Obtener valor del nombre de activo
    var nombreActivo = document.getElementById("NombreActivo").value;
    //Obtener valor del serial de activo
    var serialActivo = document.getElementById("SerialActivo").value;
    //Obtener valor del modelo de activo
    var modeloActivo = document.getElementById("ModeloActivo").value;
    //Obtener valor del codigo UPS
    var codigoUps = document.getElementById("CodigoUpsActivo").value;
    //Obtener valor de la fecha de ingreso del activo
    var fechaIngreso = $('#FechaIngresoActivo').val();
    console.log(fechaIngreso);
    var fechaPartes = fechaIngreso.split("/");
    var fechaNueva = fechaPartes[2] + "-" + fechaPartes[0] + "-" + fechaPartes[1];
    console.log(fechaNueva);
    //var fechaIngreso = document.getElementById("FechaIngresoActivo").value;
    //Obtener valor de la descripcion de activo
    var descripcionActivo = document.getElementById("DescripcionActivo").value;
    //Obtener valor del ExpressServiceCodeActivo
    //Obtener valor de la descripcion de activo
    var responsable=document.getElementById("ResponsableActivo").value;
    var expressCode = document.getElementById("ExpressServiceCodeActivo").value;
    //Obtener valor de la FechaManufacturaActivo
    var fechaManufactura = document.getElementById("FechaManufacturaActivo").value;
    //Obtener valor del NumPuertosActivo
    var numPuertos = document.getElementById("NumPuertosActivo").value;
    //Obtener valor del IosVersionActivo
    var iosVersion = document.getElementById("IosVersionActivo").value;
    //Obtener valor del ProductNameActivo
    var productName = document.getElementById("ProductNameActivo").value;
    //Obtener valor del HpePartNumberActivo
    var hpe = document.getElementById("HpePartNumberActivo").value;
    //Obtener valor de CodBarras1Activo
    var cod1 = document.getElementById("CodBarras1Activo").value;
    //Obtener valor de CodBarras2Activo
    var cod2 = document.getElementById("CodBarras2Activo").value;
    //Obtener valor del CtActivo
    var ct = document.getElementById("CtActivo").value;
    //Obtener valor de CapacidadActivo
    var capacidad = document.getElementById("CapacidadActivo").value;
    //Obtener valor de VelocidadTransfActivo
    var velocidadTransf = document.getElementById("VelocidadTransfActivo").value;

    if (validacionesCamposModificar()) {
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
                    data: {
                        "IdActivo": idActivoMod, "IdTipoActivo": idTipoActivo, "IdLaboratorio": idLaboratorio, "IdMarca": idMarca, "NombreActivo": nombreActivo, "EstadoActivo": idEstado,
                        "SerialActivo": serialActivo, "ModeloActivo": modeloActivo, "CodigoUpsActivo": codigoUps, "FechaIngresoActivo": fechaNueva,
                        "ResponsableActivo": responsable, "DescripcionActivo": descripcionActivo, "ExpressServiceCodeActivo": expressCode, "FechaManufacturaActivo": fechaManufactura,
                        "NumPuertosActivo": numPuertos, "IosVersionActivo": iosVersion, "ProductNameActivo": productName, "HpePartNumberActivo": hpe,
                        "CodBarras1Activo": cod1, "CodBarras2Activo": cod2, "CtActivo": ct, "CapacidadActivo": capacidad, "VelocidadTransfActivo": velocidadTransf
                    },
                    url: url,
                    type: 'post',
                    success: function (data) {
                        console.log(data.OperacionExitosa);
                        if (data.OperacionExitosa) {
                            $('#ModificarActivo').modal('hide');
                            showNotify("Actualización exitosa", 'El Activo "' + nombreActivo.toUpperCase() + '" se ha modificado exitosamente', "success");
                            obtenerActivos(url_metodo);
                        } else {
                            $('#ModificarActivo').modal('hide');
                            showNotify("Error en la Actualización", 'Ocurrió un error al modificar el Activo: ' + data.MensajeError, "error");
                        }

                    }
                });

            } else {
                $('#ModificarActivo').modal('hide');
            }
        });
    }
 
}

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
                            showNotify("Error en la Actualización", 'Ocurrió un error al modificar el estado del Activo: ' + data.MensajeError, "error");
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

/* --------------------------------------SECCIÓN PARA CAMPOS DE AUTOCOMPLETE---------------------------------*/
//Funciones para cargar el campo de autocompletado
function cargarNombresActivos(url) {
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            if (data.OperacionExitosa) {
                for (var i = 0; i < data.ListaObjetoInventarios.length; i++) {
                    nombresActivo[i] = data.ListaObjetoInventarios[i].NombreActivo;
                }
            } else {
                showNotify("Error en la Consulta", 'No se ha podido mostrar los datos: ' + data.MensajeError, "error");
            }          
        }
    });

}
//Función para cargar los nombres en el campo de nombre de activos
$(function () {
    $("#NombreActivo").autocomplete({
        source: nombresActivo
    });
});

/* --------------------------------------SECCIÓN PARA COMPROBACIONES Y VALIDACIONES---------------------------------*/
//Método de validación para validacion de información de activo
function validacionesCamposModificar() {
    var isValid = true;
    var boton = document.getElementById("confirmarActivo");
    var nombreActivo = document.getElementById("NombreActivo").value;
    var modeloActivo = document.getElementById("ModeloActivo").value;
    var serialActivo = document.getElementById("SerialActivo").value;
    var responsableActivo = document.getElementById("ResponsableActivo").value;
    //Validación para combobox tipo de Activo
    if (document.getElementById("TipoActivo").value == "") {
        isValid = false;
        document.getElementById("TipoActivo").style.borderColor = "#900C3F";
        $('#errorTipo').html('Debe seleccionar una Opción de Tipo de Activo').show();
        setTimeout("$('#errorTipo').html('').hide('slow')", 6000);
        boton.disabled = true;
    } else {
        document.getElementById("TipoActivo").style.borderColor = "#ccc";
        $('#errorTipo').html('').hide();
        boton.disabled = false;
    }
    //Validación para combobox laboratorio de Activo
    if (document.getElementById("LaboratorioActivo").value == "") {
        isValid = false;
        document.getElementById("LaboratorioActivo").style.borderColor = "#900C3F";
        $('#errorLaboratorio').html('Debe seleccionar una Opción de Laboratorio').show();
        setTimeout("$('#errorLaboratorio').html('').hide('slow')", 6000);
        boton.disabled = true;
    } else {
        document.getElementById("LaboratorioActivo").style.borderColor = "#ccc";
        $('#errorLaboratorio').html('').hide();
        boton.disabled = false;
    }
    //Validación para combobox marca de Activo
    if (document.getElementById("MarcaActivo").value == "") {
        isValid = false;
        document.getElementById("MarcaActivo").style.borderColor = "#900C3F";
        $('#errorMarca').html('Debe seleccionar una Opción de Marca').show();
        setTimeout("$('#errorMarca').html('').hide('slow')", 6000);
        boton.disabled = true;
    } else {
        document.getElementById("MarcaActivo").style.borderColor = "#ccc";
        $('#errorMarca').html('').hide();
        boton.disabled = false;
    }
    //Validación para combobox estado de Activo
    if (document.getElementById("EstadoActivo").value == "") {
        isValid = false;
        document.getElementById("EstadoActivo").style.borderColor = "#900C3F";
        $('#errorEstado').html('Debe seleccionar una Opción de Estado').show();
        setTimeout("$('#errorEstado').html('').hide('slow')", 6000);
        boton.disabled = true;
    } else {
        document.getElementById("EstadoActivo").style.borderColor = "#ccc";
        $('#errorEstado').html('').hide();
        boton.disabled = false;
    }
    //Validación para el nombre de Activo
    if (!nombreActivo && nombreActivo.length <= 0) {
        isValid = false;
        $("#NombreActivo").focus();
        document.getElementById("NombreActivo").style.borderColor = "#900C3F";
        $('#errorNombre').html('El campo Nombre de Activo es obligatorio').show();
        setTimeout("$('#errorNombre').html('').hide('slow')", 6000);
        boton.disabled = true;
    } else {
        document.getElementById("NombreActivo").style.borderColor = "#ccc";
        $('#errorNombre').html('').hide();
        boton.disabled = false;
    }
    //Validación para el modelo del activo
    if (!modeloActivo && modeloActivo.length <= 0) {
        isValid = false;
        $("#ModeloActivo").focus();
        document.getElementById("ModeloActivo").style.borderColor = "#900C3F";
        $('#errorModelo').html('El campo Modelo de Activo es obligatorio').show();
        setTimeout("$('#errorModelo').html('').hide('slow')", 6000);
        boton.disabled = true;
    } else {
        document.getElementById("ModeloActivo").style.borderColor = "#ccc";
        $('#errorModelo').html('').hide();
        boton.disabled = false;
    }
    //Validación para el serial del activo
    if (!serialActivo && serialActivo.length <= 0) {
        isValid = false;
        $("#SerialActivo").focus();
        document.getElementById("SerialActivo").style.borderColor = "#900C3F";
        $('#errorSerial').html('El campo Serial de Activo es obligatorio').show();
        setTimeout("$('#errorSerial').html('').hide('slow')", 6000);
        boton.disabled = true;
    } else {
        document.getElementById("SerialActivo").style.borderColor = "#ccc";
        $('#errorSerial').html('').hide();
        boton.disabled = false;
    }
    //Validación para el nombre del responsable
    if (!responsableActivo && responsableActivo.length <= 0) {
        isValid = false;
        $("#ResponsableActivo").focus();
        document.getElementById("ResponsableActivo").style.borderColor = "#900C3F";
        $('#errorResponsable').html('El campo Responsable es obligatorio').show();
        setTimeout("$('#errorResponsable').html('').hide('slow')", 6000);
    } else {
        document.getElementById("ResponsableActivo").style.borderColor = "#ccc";
        $('errorResponsable').html('').hide();
    }

    return isValid;
}

//Función para evitar nombres de activos repetidos
function validarNombreActModificar() {
    var nombreActivo = document.getElementById("NombreActivo");
    nombreActivo.value = nombreActivo.value.toUpperCase();
    console.log(nombreActivo.value + " - " + NombreActMod.toUpperCase());
    if (nombreActivo.value != NombreActMod.toUpperCase()) {
        for (var i = 0; i < nombresActivo.length; i++) {
            if ((nombresActivo[i]).toUpperCase() == nombreActivo.value) {
                nombreActivo.style.borderColor = "#900C3F";
                $('#errorNombre').html('El nombre de Activo: ' + nombreActivo.value + ' ya existe').show();
                setTimeout("$('#errorNombre').html('').hide('slow')", 6000);
                nombreActivo.value = "";
                break;
            } else {
                nombreActivo.style.borderColor = "#ccc";
                $('#errorNombre').html('').hide();
            }
        }
    } else {
        nombreActivo.style.borderColor = "#ccc";
        $('#errorNombre').html('').hide();
    }
}

//Función para evitar nombres de laboratorios repetidos
function comprobarNumPuertos() {
    var nPuertos = document.getElementById("NumPuertosActivo");
    //Validación para el campo vida útil
    if (nPuertos.value < 1) {
        esValido = false;
        nPuertos.style.borderColor = "#900C3F";
        nPuertos.value = "";
        $('#errorNumPuertos').html('Ingrese un valor mayor a 0').show();
        setTimeout("$('#errorNumPuertos').html('').hide('slow')", 6000);
    } else if (nPuertos.value > 500) {
        esValido = false;
        nPuertos.style.borderColor = "#900C3F";
        nPuertos.value = "";
        $('#errorNumPuertos').html('Ingrese un valor menor a 500').show();
        setTimeout("$('#errorNumPuertos').html('').hide('slow')", 6000);
    } else {
        nPuertos.style.borderColor = "#ccc";
        $('#errorNumPuertos').html('').hide();
    }
}

//Función para validar el estado de activo
function validacionesEstadoActivo() {
    var isValid = true;

    //Validación para combobox tipo de Activo
    if (document.getElementById("EstadoActivoModificar").value == "") {
        isValid = false;
        document.getElementById("EstadoActivoModificar").style.borderColor = "#900C3F";
        $('#errorEstadoActivo').html('Debe seleccionar una Opción de Estado de Activo').show();
        setTimeout("$('#errorEstadoActivo').html('').hide('slow')", 6000);
    } else {
        document.getElementById("EstadoActivoModificar").style.borderColor = "#ccc";
        $('#errorEstadoActivo').html('').hide();
    }
    return isValid;
}

//Función para setear valores N/A
function setearModeloAct() {
    var modeloActivo = document.getElementById("ModeloActivo").value;
    //Validación para el modelo del activo
    if (!modeloActivo && modeloActivo.length <= 0) {
        document.getElementById("ModeloActivo").value = "N/A";
    }
}

//Función para setear valores N/A
function setearSerialAct() {
    var serialActivo = document.getElementById("SerialActivo").value;;
    if (!serialActivo && serialActivo.length <= 0) {
        document.getElementById("SerialActivo").value = "N/A";
    }
}

/* --------------------------------------SECCIÓN PARA MENSAJES DE TOOLTIPS---------------------------------*/
//Mensajes para los tooltips
function mensajesTooltips() {
    document.getElementById("NombreActivo").title = "Máximo 50 caracteres en Mayúscula, sin Espacioss.\n Caracteres especiales permitidos - / _ .";
    document.getElementById("SerialActivo").title = "Máximo 80 caracteres, sin Espacios.\n  Caracteres especiales permitidos - / _ .";
    document.getElementById("ModeloActivo").title = "Máximo 80 caracteres.\n Caracteres especiales permitidos - / _ .";
    document.getElementById("CodigoUpsActivo").title = "Código de Barras otorgado por la UPS. Máximo 15 números.";
    document.getElementById("FechaIngresoActivo").title = "Fecha en la que se adquirió o se recibió el Activo de TI.";
    document.getElementById("ResponsableActivo").title = "Responsable Actual del Data Center";
    document.getElementById("DescripcionActivo").title = "Máximo 150 caracteres.\n  Caracteres especiales permitidos - / _ . , : ;";

    document.getElementById("ExpressServiceCodeActivo").title = "Máximo 50 caracteres.\n  Ejm: 20846125298";
    document.getElementById("FechaManufacturaActivo").title = "Máximo 50 caracteres.\n  Ejm: Apr.2017";
    document.getElementById("NumPuertosActivo").title = "Solo Números del 1 al 500.\n  Ejm: 24";
    document.getElementById("IosVersionActivo").title = "Máximo 20 caracteres.\n  Ejm: 12.2(85) SE5";
    document.getElementById("ProductNameActivo").title = "Máximo 20 caracteres.\n  Ejm: 789917-B21";
    document.getElementById("HpePartNumberActivo").title = "Máximo 20 caracteres.\n  Ejm: QR490-63007";
    document.getElementById("CodBarras1Activo").title = "Máximo 30 caracteres.\n  Ejm: CCD4028N02H";
    document.getElementById("CodBarras2Activo").title = "Máximo 30 caracteres.\n  Ejm: HP-6505-12-0R-80-1006124-06";
    document.getElementById("CtActivo").title = "Máximo 20 caracteres.\n  Ejm: EEAUBA2TF7429Y";
    document.getElementById("CapacidadActivo").title = "Máximo 8 caracteres.\n  Ejm: 1.2 TB";
    document.getElementById("VelocidadTransfActivo").title = "Máximo 10 caracteres.\n  Ejm: MLC/10k";
}


/**
 * *********************************************************************************
 *                SECCIÓN PARA OPERACIONES CON ACCESORIOS
 * *********************************************************************************
 */

/* --------------------------------------SECCIÓN PARA OBTENER DATOS DEL ACCESORIOS---------------------------------*/
//Función para obtener los valores del activo asociado con su accesorio
function formIngresoAccesorio(idAct, nombreAct) {
    idAccesorioIng = idAct;
    nombreAccesorioIng = nombreAct;
    document.getElementById("NombreActivoIngreso").value = nombreAccesorioIng;
   
}
/* --------------------------------------SECCIÓN PARA INSERTAR NUEVO ACCESORIO---------------------------------*/
//Función para ingresar el nuevo accesorio
function ingresarAccesorios(url,urlImagen,urlPdf) {
    console.log(url);
    //Obtener Valor del tipo de activo
    var cmbTipoAccesorio = document.getElementById("AccesorioIngreso");
    var idTipoAccesorio = cmbTipoAccesorio.options[cmbTipoAccesorio.selectedIndex].value;
    //Obtener Valor del estado de accesorio
    var cmbEstadoAccesorio = document.getElementById("EstadoAccesorioIng");
    var idEstadoAccesorio = cmbEstadoAccesorio.options[cmbEstadoAccesorio.selectedIndex].value;
    //Obtener valor del nombre de accesorio
    var activoAccesorio = document.getElementById("NombreActivoIngreso").value;
    var nombreAccesorio = document.getElementById("NombreAccesorioIngreso").value;
    //Obtener valor del serial de accesorio
    var serialAccesorio = document.getElementById("SerialAccesorioIngreso").value;
    //Obtener valor del modelo de accesorio
    var modeloAccesorio = document.getElementById("ModeloAccesorioIngreso").value
    //Obtener valor de la descripcion del accesorio
    var descripcionAccesorio = document.getElementById("DescripcionAccesorioIngreso").value;

    if (validacionesCamposAccesorio()) {
        swal({
            title: 'Confirmación de Ingreso',
            text: "¿Está seguro de ingresar el registro?",
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
                        "IdTipoAccesorio": idTipoAccesorio, "IdDetalleActivo": idAccesorioIng, "NombreAccesorio": nombreAccesorio,
                        "SerialAccesorio": serialAccesorio, "ModeloAccesorio": modeloAccesorio, "DescripcionAccesorio": descripcionAccesorio,
                        "EstadoAccesorio": idEstadoAccesorio, "NombreDetalleActivo": activoAccesorio
                    },
                    url: url,
                    dataType: 'json',
                    type: 'post',
                    success: function (data) {                       
                        var isValid = data.OperacionExitosa;
                        console.log(isValid);
                        if (isValid) {
                            document.getElementById("confirmarAccesorioNuevo").disabled = true;
                            datosCQRAccesorio(data.ObjetoInventarios);
                            var str = '<img src="' + urlImagen + '"/>';
                            $("#imgCQR").html(str);
                            $("#btnGenPDF").click(function () {
                                $('#GenPDFForm').attr('target', "_blank");
                                $('#GenPDFForm').attr('action', urlPdf).submit();
                            });
                            //obtenerActivos(url_metodo);
                            obtenerAccesorios(url_metodo_accesorio);
                            showNotify("Registro exitoso", 'El Accesorio "' + nombreAccesorio.toUpperCase() + '" se ha ingresado exitosamente', "success");
                        } else {
                            showNotify("Error de registro", "Ocurrió un error al ingresar el Accesorio: " + data.MensajeError, "error");
                        }
                    }, error: function (e) {
                        console.log(e);
                        console.log("fallo");
                    }
                });
            } else {
                $('#IngresoNuevoAccesorio').modal('hide');

            }
        });
    } 

}

//Función para mostrar el CQR del Accesorio Ingresados
function datosCQRAccesorio(data) {
    console.log(data);
    idCQRAccesorio = data.IdCQR;
    nombreCQRAccesorio = data.NombreAccesorio;
    nombreActivoPert = data.NombreDetalleActivo;
    $('#idCQRAccesorio').html(idCQRAccesorio).show();
    $('#nombreAccesorioIngresado').html(nombreCQRAccesorio).show();
    $('#nombreActivoPert').html(nombreActivoPert).show();
    $('#cqrAccesorio').show();  
}

//Función para limpiar los inputs y el  modal
function limpiarModal() {
    $('#IngresoNuevoAccesorio').modal('hide');
    $(".modal-body select").val("");
    $(".modal-body input").val("");
    $(".modal-body textarea").val("");
    $('#cqrAccesorio').hide();
}


/* --------------------------------------SECCIÓN PARA COMPROBACIONES Y VALIDACIONES---------------------------------*/
//Método de validación de información de accesorios
function validacionesCamposAccesorio() {
    var isValid = true;
    var nombreActivo = document.getElementById("NombreAccesorioIngreso").value;
    var modeloActivo = document.getElementById("ModeloAccesorioIngreso").value;
    var serialActivo = document.getElementById("SerialAccesorioIngreso").value;

    //Validación para combobox tipo de Accesorios
    if (document.getElementById("AccesorioIngreso").value == "") {
        isValid = false;
        document.getElementById("AccesorioIngreso").style.borderColor = "#900C3F";
        $('#errorTipoAccesorioIng').html('Debe seleccionar una Opción de Tipo de Activo').show();
        setTimeout("$('#errorTipoAccesorioIng').html('').hide('slow')", 6000);
    } else {
        document.getElementById("AccesorioIngreso").style.borderColor = "#ccc";
        $('#errorTipoAccesorioIng').html('').hide();
    }

    //Validación para combobox estado de Activo
    if (document.getElementById("EstadoAccesorioIng").value == "") {
        isValid = false;
        document.getElementById("EstadoAccesorioIng").style.borderColor = "#900C3F";
        $('#errorEstadoAccesorioIng').html('Debe seleccionar una Opción de Estado').show();
        setTimeout("$('#errorEstadoAccesorioIng').html('').hide('slow')", 6000);
    } else {
        document.getElementById("EstadoAccesorioIng").style.borderColor = "#ccc";
        $('#errorEstadoAccesorioIng').html('').hide();
    }

    //Validación para el nombre de Activo
    if (!nombreActivo && nombreActivo.length <= 0) {
        isValid = false;
        document.getElementById("NombreAccesorioIngreso").style.borderColor = "#900C3F";
        $('#errorNombreAccesorioIng').html('El campo Nombre de Accesorio es obligatorio').show();
        setTimeout("$('#errorNombreAccesorioIng').html('').hide('slow')", 6000);
    } else {
        document.getElementById("NombreAccesorioIngreso").style.borderColor = "#ccc";
        $('#errorNombreAccesorioIng').html('').hide();
    }
    //Validación para el modelo del activo
    if (!modeloActivo && modeloActivo.length <= 0) {
        isValid = false;
        document.getElementById("ModeloAccesorioIngreso").style.borderColor = "#900C3F";
        $('#errorModeloAccesorioIng').html('El campo Modelo de Accesorio es obligatorio').show();
        setTimeout("$('#errorModeloAccesorioIng').html('').hide('slow')", 6000);
    } else {
        document.getElementById("ModeloAccesorioIngreso").style.borderColor = "#ccc";
        $('#errorModeloAccesorioIng').html('').hide();
    }
    //Validación para el serial del activo
    if (!serialActivo && serialActivo.length <= 0) {
        isValid = false;
        document.getElementById("SerialAccesorioIngreso").style.borderColor = "#900C3F";
        $('#errorSerialAccesorioIng').html('El campo Serial de Accesorio es obligatorio').show();
        setTimeout("$('#errorSerialAccesorioIng').html('').hide('slow')", 6000);
    } else {
        document.getElementById("SerialAccesorioIngreso").style.borderColor = "#ccc";
        $('#errorSerialAccesorioIng').html('').hide();
    }

    return isValid;
}

/* --------------------------------------SECCIÓN PARA SETEAR VALORES---------------------------------*/
//Función para setear valores N/A en modificacion
function setearModelo() {
    var modeloActivo = document.getElementById("ModeloAccesorio").value;
    //Validación para el modelo del activo
    if (!modeloActivo && modeloActivo.length <= 0) {
        isValid = false;
        document.getElementById("ModeloAccesorio").value = "N/A";
    }
}

//Función para setear valores N/A en modificacion
function setearSerial() {
    var serialActivo = document.getElementById("SerialAccesorio").value;
    //Validación para el serial del activo
    if (!serialActivo && serialActivo.length <= 0) {
        document.getElementById("SerialAccesorio").value = "N/A";
    } 
}

//Función para setear valores N/A en Ingreso
function setearModeloIngreso() {
    var modeloActivo = document.getElementById("ModeloAccesorioIngreso").value;
    //Validación para el modelo del activo
    if (!modeloActivo && modeloActivo.length <= 0) {
        isValid = false;
        document.getElementById("ModeloAccesorioIngreso").value = "N/A";
    }
}

//Función para setear valores N/A en Ingreso
function setearSerialIngreso() {
    var serialActivo = document.getElementById("SerialAccesorioIngreso").value;
    //Validación para el serial del activo
    if (!serialActivo && serialActivo.length <= 0) {
        document.getElementById("SerialAccesorioIngreso").value = "N/A";
    }
}

/* --------------------------------------SECCIÓN PARA MENSAJES DE TOOLTIPS---------------------------------*/
//Mensajes para los tooltips
function mensajesTooltipsAccesorios() {
    document.getElementById("NombreAccesorioIngreso").title = "Máximo 50 caracteres en Mayúscula, sin Espacios.\n Caracteres especiales permitidos - / _ .";
    document.getElementById("SerialAccesorioIngreso").title = "Máximo 80 caracteres, sin Espacios.\n Caracteres especiales permitidos - / _ .";
    document.getElementById("ModeloAccesorioIngreso").title = "Máximo 80 caracteres.\n Caracteres especiales permitidos - / _ .";
    document.getElementById("DescripcionAccesorioIngreso").title = "Máximo 150 caracteres.\n Caracteres especiales permitidos - / _ . , : ;";
}

//Mensajes para los tooltips
function mensajesTooltipsAccesoriosMod() {
    document.getElementById("NombreAccesorio").title = "Máximo 50 caracteres en Mayúscula, sin Espacios.\n Caracteres especiales permitidos - / _ .";
    document.getElementById("SerialAccesorio").title = "Máximo 80 caracteres, sin Espacios.\n Caracteres especiales permitidos - / _ .";
    document.getElementById("ModeloAccesorio").title = "Máximo 80 caracteres.\n Caracteres especiales permitidos - / _ .";
    document.getElementById("DescripcionAccesorio").title = "Máximo 150 caracteres.\n Caracteres especiales permitidos - / _ . , : ;";
}

/**
 * *********************************************************************************
 *                SECCIÓN PARA OPERACIONES CON HISTORICOS
 * *********************************************************************************
 */

/* --------------------------------------SECCIÓN PARA OBTENER DATOS DEL SERVIDOR---------------------------------*/
//Método ajax para obtener accesorios
function obtenerHistoricos(url) {
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            if (data.OperacionExitosa) {
                datosHistoricos = data.ListaObjetoInventarios;
                cargarHistoricosTabla();
                $('#dataTableHistoricos').DataTable({
                    "language": {
                        "url": url_idioma
                    },
                    "order": [[3, "desc"]]
                });
            } else {
                showNotify("Error en la Consulta", 'No se ha podido mostrar los datos: ' + data.MensajeError, "error");
            }
        }
    });
}


/* --------------------------------------SECCIÓN PARA CARGAR TABLAS Y COMBOBOX---------------------------------*/

//Función para cargar la tabla de Activos
function cargarHistoricosTabla() {
    var str = '<table id="dataTableHistoricos" class="table jambo_table bulk_action table-bordered " style="width:100%">';
    str += '<thead> <tr> <th>Nombre de Activo o Accesorio</th> <th>Modelo de Activo o Accesorio</th> <th>Serial de Activo o Accesorio</th> <th>Fecha de Baja</th></tr> </thead>';
    str += '<tbody>';
    for (var i = 0; i < datosHistoricos.length; i++) {
        //Método para dar formato a la fecha y hora
        var fechaIng = new Date(parseInt((datosHistoricos[i].FechaModifHistActivos).substr(6)));
        //Fecha para ordenar el string mm/dd/yyyy
        var fechaordenar = (fechaIng.toLocaleDateString("en-US"));
        //fecha para la tabla y busquedas
        function pad(n) { return n < 10 ? "0" + n : n; }
        var fechaIngreso = pad(fechaIng.getMonth() + 1) + "/" + pad(fechaIng.getDate()) + "/" + fechaIng.getFullYear();

        if (datosHistoricos[i].IdActivo != 0) {
            str += '</td><td>' + datosHistoricos[i].NombreActivo +
                '</td><td>' + datosHistoricos[i].ModeloHistActivo +
                '</td><td>' + datosHistoricos[i].SerialHistActivo;
        } else {
            str += '</td><td>' + datosHistoricos[i].NombreAccesorio +
                '</td><td>' + datosHistoricos[i].ModeloHistAccesorio +
                '</td><td>' + datosHistoricos[i].SerialHistAccesorio;
        }
        str += '</td><td>' + fechaIngreso +
            '</td ></tr> ';
    }
    str += '</tbody>' +
        '</table > ';
    $("#tablaHistoricos").html(str);
}

/* --------------------------------------SECCIÓN PARA OPERACIONES CON USUARIO INVITADO---------------------------------*/
function botonesActivos(url) {
    url_bloquear = url;
}
//Función para bloquear botones cuando el usuario es invitado
function BloquearbotonesActivos() {
    $.ajax({
        dataType: 'json',
        url: url_bloquear,
        type: 'post',
        success: function (data) {
            rolAct = data;
            if (data == "Invitado") {
                $(':button').prop('disabled', true);
                var table = $('#dataTableActivos').DataTable();
                var rows = table.rows({ 'search': 'applied' }).nodes();
                $('button', rows).attr("disabled", "disabled");
            }
        }
    });
}



