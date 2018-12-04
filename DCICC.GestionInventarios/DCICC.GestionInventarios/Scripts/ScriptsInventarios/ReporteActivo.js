﻿var url_idioma = obtenerIdioma();
var cmbEstados = listaEstadosActivos();
var url_metodo;
var datosActivos;
var idActivoMod;
var cmbTipoActivo;
var cmbLaboratorio;
var cmbMarcas;
var idAccesorioIng;
var nombreAccesorioIng;

var idCQRAccesorio;
var nombreCQRAccesorio;

var nombresActivo = [];


//Método ajax para obtener los datos de los activos
function obtenerActivos(url) {
    url_metodo = url;
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'get',
        success: function (data) {
            console.log("Datos Exitosos");
            datosActivos = data;
            cargarActivosTabla();
            $('#dataTableActivos').DataTable({
                "language": {
                    "url": url_idioma
                },
                "order": [[1,"asc"]]
            });
        }
    });
}

function datosTipoActivo(url) {
    //url_metodo = url;
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'get',
        success: function (data) {
            cmbTipoActivo = data;
            cargarTipoActivoCmb();

        }
    });
}

function datosLaboratorio(url) {
    //url_metodo = url;
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'get',
        success: function (data) {
            cmbLaboratorio = data;
            cargarLaboratoriosCmb();
        }
    });
}

function datosMarcas(url) {
    //url_metodo = url;
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'get',
        success: function (data) {
            cmbMarcas = data;
            cargarMarcasCmb();
        }
    });
}

function cargarTipoActivoCmb() {
    
    var str = '<select id="TipoActivo" class="form-control" name="TipoActivo"  required>';
    str += '<option value="">Escoga una opción...</option>';
    for (var i = 0; i < cmbTipoActivo.length; i++) {
            console.log(cmbTipoActivo[i].NombreTipoActivo);
            str += '<option value="' + cmbTipoActivo[i].IdTipoActivo + '">' + cmbTipoActivo[i].NombreTipoActivo + '</option>';
    };
    str += '</select>';
    $("#cargarTipoActivos").html(str);
}

function cargarLaboratoriosCmb() {
    var str = '<select id="LaboratorioActivo" class="form-control" name="LaboratorioActivo"  required>';
    str += '<option value="">Escoga una opción...</option>';
    for (var i = 0; i < cmbLaboratorio.length; i++) {
        str += '<option value="' + cmbLaboratorio[i].IdLaboratorio + '">' + cmbLaboratorio[i].NombreLaboratorio + '</option>';
    };
    str += '</select>';
    $("#cargarLaboratorios").html(str);
}

function cargarMarcasCmb() {
    var str = '<select id="MarcaActivo" class="form-control" name="MarcaActivo"   required>';
    str += '<option value="">Escoga una opción...</option>';
    for (var i = 0; i < cmbMarcas.length; i++) {
        str += '<option value="' + cmbMarcas[i].IdMarca + '">' + cmbMarcas[i].NombreMarca + '</option>';
    };
    str += '</select>';
    $("#cargarMarcas").html(str);
}

function cargarEstadosCmb() {
    var str = '<select id="EstadoActivo" class="form-control" name="EstadoActivo" onBlur=" validacionesCamposModificar();" required>';
    str += '<option value="">Escoga una opción...</option>';
    for (var i = 0; i < cmbEstados.length; i++) {
        str += '<option value="' + cmbEstados[i] + '">' + cmbEstados[i] + '</option>';
    };
    str += '</select>';
    $("#cargarEstados").html(str);
}

function cargarEstadosModificarCmb() {
    var str = '<select id="EstadoActivoModificar" class="form-control" name="EstadoActivoModificar" required>';
    str += '<option value="">Escoga una opción...</option>';
    for (var i = 0; i < cmbEstados.length; i++) {
        str += '<option value="' + cmbEstados[i] + '">' + cmbEstados[i] + '</option>';
    };
    str += '</select>';
    $("#cargarEstadosActivo").html(str);
}


//Función para cargar la tabla de Activos
function cargarActivosTabla() {
    var str = '<table id="dataTableActivos" class="table jambo_table bulk_action  table-bordered " style="width:100%">';
    str += '<thead> <tr> <th>Tipo de Activo</th> <th>Nombre del Activo</th> <th>Modelo</th> <th>Serial</th> <th>Laboratorio</th> <th>Fecha de Ingreso</th> <th>Código QR</th> <th>Estado del Activo</th> <th>Agregar Accesorio</th> <th>Modificar</th> <th>Cambiar Estado</th>  </tr> </thead>';
    str += '<tbody>';
    for (var i = 0; i < datosActivos.length; i++) {
        //Método para dar formato a la fecha y hora
        var fechaLog = new Date(parseInt((datosActivos[i].FechaIngresoActivo).substr(6)));
        var fechaIngreso = (fechaLog.toLocaleDateString("es-ES"));

        str += '<tr><td>' + datosActivos[i].NombreTipoActivo +
            '</td><td>' + datosActivos[i].NombreActivo +
            '</td><td>' + datosActivos[i].ModeloActivo +
            '</td><td>' + datosActivos[i].SerialActivo +
            '</td><td>' + datosActivos[i].NombreLaboratorio +
            '</td><td>' + fechaIngreso +
            '</td><td>' + datosActivos[i].IdCQR +
            '</td><td>' + datosActivos[i].EstadoActivo;
        str += '</td><td><div class="text-center"><div class="col-md-12 col-sm-12 col-xs-12">' +
            '<button type="button" class="btn btn-default text-center" data-toggle="modal" data-target="#IngresoNuevoAccesorio" onclick = "formIngresoAccesorio(' + datosActivos[i].IdActivo + ',\'' + datosActivos[i].NombreActivo +'\');" > <strong><i class="fa fa-plug"></i></strong></button> ' +
            '</div></div>';
        str += '</td><td><div class="text-center"><div class="col-md-12 col-sm-12 col-xs-12">' +
            '<button type="button" class="btn btn-info text-center" data-toggle="modal" data-target="#ModificarActivo" onclick = "formUpdateActivos(' + datosActivos[i].IdActivo + ');"> <strong><i class="fa fa-pencil-square-o"></i></strong></button> ' +
            '</div></div>' +
            '</td><td><div class="text-center"><div class="col-md-12 col-sm-12 col-xs-12">';
        if (datosActivos[i].EstadoActivo == "OPERATIVO") {
            str += '<button type = "button" class="btn btn-success text-center" data-toggle="modal" data-target="#ModificarEstadoActivo" onclick = "habilitarOdeshabilitar(' + datosActivos[i].IdActivo + ');" > <strong><span class="fa fa-check"></span></strong></button> ';
        } else if (datosActivos[i].EstadoActivo == "NO OPERATIVO") {
            str += '<button type = "button" class="btn btn-warning text-center" data-toggle="modal" data-target="#ModificarEstadoActivo" onclick = "habilitarOdeshabilitar(' + datosActivos[i].IdActivo +  ');" > <strong><span class="fa fa-warning"></span></strong></button> ';
        } else {
            str += '<button type = "button" class="btn btn-danger text-center" data-toggle="modal" data-target="#ModificarEstadoActivo" onclick = "habilitarOdeshabilitar(' + datosActivos[i].IdActivo + ');" > <strong><span class="fa fa-close"></span></strong></button> ';
        }

        
        str += '</div></div>'+
        
        '</td ></tr > ';
    };
    str += '</tbody>' +
        '<tfoot><tr> <th>Tipo de Activo</th> <th>Nombre del Activo</th> <th>Modelo</th> <th>Serial</th> <th>Laboratorio</th> <th>Fecha de Ingreso</th> <th>Código QR</th><th>Estado del Activo</th> <th>Agregar Accesorio</th> <th>Modificar</th> <th>Cambiar Estado</th>  </tr> </thead></tfoot>' +
        '</table > ';
    $("#tablaActivos").html(str);
}

function formUpdateActivos(idActivo) {
    idActivoMod = idActivo;
    for (var i = 0; i < datosActivos.length; i++) {
        if (datosActivos[i].IdActivo == idActivo) {
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
            var fechaIngreso = (fechaLog.toLocaleDateString("es-ES"));
             $('#single_cal4').val(fechaIngreso);
            console.log(fechaIngreso);
            //var fechaIngreso = document.getElementById("FechaIngresoActivo").value;
            //Obtener valor de la descripcion de activo
            document.getElementById("DescripcionActivo").value = datosActivos[i].DescripcionActivo;
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
    };
    
}


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
    var fechaIngreso = $('#single_cal4').val();
    console.log(fechaIngreso);
    //var fechaIngreso = document.getElementById("FechaIngresoActivo").value;
    //Obtener valor de la descripcion de activo
    var descripcionActivo = document.getElementById("DescripcionActivo").value;
    //Obtener valor del ExpressServiceCodeActivo
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
                        "SerialActivo": serialActivo, "ModeloActivo": modeloActivo, "CodigoUpsActivo": codigoUps, "FechaIngresoActivo": fechaIngreso,
                        "DescripcionActivo": descripcionActivo, "ExpressServiceCodeActivo": expressCode, "FechaManufacturaActivo": fechaManufactura,
                        "NumPuertosActivo": numPuertos, "IosVersionActivo": iosVersion, "ProductNameActivo": productName, "HpePartNumberActivo": hpe,
                        "CodBarras1Activo": cod1, "CodBarras2Activo": cod2, "CtActivo": ct, "CapacidadActivo": capacidad, "VelocidadTransfActivo": velocidadTransf
                    },
                    url: url,
                    type: 'post',
                    success: function (data) {
                        console.log(data.OperacionExitosa);
                        if (data.OperacionExitosa) {
                            $('#ModificarActivo').modal('hide');
                            showNotify("Actualización exitosa", 'El Activo se a modificado correctamente', "success");
                            obtenerActivos(url_metodo);
                        } else {
                            $('#ModificarActivo').modal('hide');
                            showNotify("Error en la Actualización", 'No se ha podido modificar el Activo' + data.MensajeError, "error");
                        }

                    }
                });

            } else {
                $('#ModificarActivo').modal('hide');
            }
        });
    }
 
}

function habilitarOdeshabilitar(idActivo) {
    idActivoMod = idActivo;
}

function actualizarEstadoActivo(url) {

    var cmbEstado = document.getElementById("EstadoActivoModificar");
    var idEstado = cmbEstado.options[cmbEstado.selectedIndex].value;

    if (validacionesEstadoActivo()) {
        swal({
            title: 'Confirmación de Actualización',
            text: "¿Está seguro de modificar el estado del Activo?",
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
                            showNotify("Actualización exitosa", 'El Estado del Activo se a modificado correctamente', "success");
                            obtenerActivos(url_metodo);
                        } else {
                            $('#ModificarEstadoActivo').modal('hide');
                            showNotify("Error en la Actualización", 'No se ha podido modificar el Estado del Activo' + data.MensajeError, "error");
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


//Método de validación para validacion de información de activo
function validacionesCamposModificar() {
    var isValid = true;
    var boton = document.getElementById("confirmarActivo");
    var nombreActivo = document.getElementById("NombreActivo").value;
    var modeloActivo = document.getElementById("ModeloActivo").value;
    var serialActivo = document.getElementById("SerialActivo").value;

    //Validación para combobox tipo de Activo
    if (document.getElementById("TipoActivo").value == "") {
        isValid = false;
        document.getElementById("TipoActivo").style.borderColor = "#900C3F";
        $('#errorTipo').html('Debe seleccionar una Opción del Tipo de Activo').show();
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
        $('#errorNombre').html('El campo Nombre del Activo es obligatorio').show();
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

    return isValid;
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


//Mensajes para los tooltips
function mensajesTooltips() {
    document.getElementById("NombreActivo").title = "Máximo 50 caracteres en Mayúscula.\n No se puede ingresar caracteres especiales ni espacios.";
    document.getElementById("SerialActivo").title = "Máximo 80 caracteres.\n No se puede ingresar caracteres especiales ni espacios.";
    document.getElementById("ModeloActivo").title = "Máximo 80 caracteres.\n No se puede ingresar caracteres especiales.";
    document.getElementById("CodigoUpsActivo").title = "Máximo 15 caracteres numéricos.\n No se puede ingresar caracteres especiales ni espacios.";
    document.getElementById("single_cal4").title = "Fecha en la que se adquirio o se recibio el Activo de TI.";
    document.getElementById("DescripcionActivo").title = "Máximo 150 caracteres.\n No se puede ingresar caracteres especiales.";
}

//////////////////////////////////////FUNCIONES PARA GUARDAR ACCESORIO/////////////////////////////////////
//Función para obtener los valores del activo asociado con su accesorio
function formIngresoAccesorio(idAct, nombreAct) {
    idAccesorioIng = idAct;
    nombreAccesorioIng = nombreAct;
    document.getElementById("NombreActivoIngreso").value = nombreAccesorioIng;
   
}

//Función para ingresar el nuevo accesorio
function ingresarAccesorios(url,urlImagen,urlPdf) {

    console.log(url);
    //Obtener Valor del tipo de activo
    var cmbTipoAccesorio = document.getElementById("AccesorioIngreso");
    var idTipoAccesorio = cmbTipoAccesorio.options[cmbTipoAccesorio.selectedIndex].value;
    //Obtener Valor del estado de accesorio
    var cmbEstadoAccesorio = document.getElementById("EstadoAccesorioIng");
    var idEstadoAccesorio = cmbEstadoAccesorio.options[cmbEstadoAccesorio.selectedIndex].value;
    //Obtener valor del nombre de activo
    var nombreAccesorio = document.getElementById("NombreAccesorioIngreso").value;
    //Obtener valor del serial de activo
    var serialAccesorio = document.getElementById("SerialAccesorioIngreso").value;
    //Obtener valor del modelo de activo
    var modeloAccesorio = document.getElementById("ModeloAccesorioIngreso").value
    //Obtener valor de la descripcion del accesorio
    var descripcionAccesorio = document.getElementById("DescripcionAccesorioIngreso").value;

    if (validacionesCamposAccesorio()) {
        swal({
            title: 'Confirmación de Ingreso',
            text: "¿Está seguro de ingresar el Accesorio?",
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
                        "IdTipoAccesorio": idTipoAccesorio, "IdDetalleActivo": idAccesorioIng, "NombreAccesorio": nombreAccesorio, "SerialAccesorio": serialAccesorio, "ModeloAccesorio": modeloAccesorio, "DescripcionAccesorio": descripcionAccesorio, "EstadoAccesorio": idEstadoAccesorio
                    },
                    url: url,
                    dataType: 'json',
                    type: 'post',
                    success: function (data) {
                        console.log("Inserto");
                        var isValid = data.OperacionExitosa;

                        if (isValid) {
                            document.getElementById("confirmarAccesorioNuevo").disabled = true;
                            datosCQRAccesorio(data.ObjetoInventarios);
                            var str = '<img src="' + urlImagen + '"/>';
                            $("#imgCQR").html(str);
                            $("#btnGenPDF").click(function () {
                                $('#GenPDFForm').attr('target', "_blank");
                                $('#GenPDFForm').attr('action', urlPdf).submit();
                            });
                            obtenerActivos(url_metodo);
                            obtenerAccesorios(url_metodo_accesorio);
                            showNotify("Registro exitoso", 'El accesorio se ha ingresado correctamente', "success");
                        } else {
                            showNotify("Error al ingresar accesorio", "error");
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
    idCQRAccesorio = data.IdCQR;
    nombreCQRAccesorio = data.NombreAccesorio;
    $('#idCQRAccesorio').html(idCQRAccesorio).show();
    console.log(nombreCQRAccesorio);
    $('#nombreAccesorioIngresado').html(nombreCQRAccesorio ).show();
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
        $('#errorTipoAccesorioIng').html('Debe seleccionar una Opción del Tipo de Activo').show();
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
        $('#errorNombreAccesorioIng').html('El campo Nombre del Activo es obligatorio').show();
        setTimeout("$('#errorNombreAccesorioIng').html('').hide('slow')", 6000);
    } else {
        document.getElementById("NombreAccesorioIngreso").style.borderColor = "#ccc";
        $('#errorNombreAccesorioIng').html('').hide();
    }
    //Validación para el modelo del activo
    if (!modeloActivo && modeloActivo.length <= 0) {
        isValid = false;
        document.getElementById("ModeloAccesorioIngreso").style.borderColor = "#900C3F";
        $('#errorModeloAccesorioIng').html('El campo Modelo de Activo es obligatorio').show();
        setTimeout("$('#errorModeloAccesorioIng').html('').hide('slow')", 6000);
    } else {
        document.getElementById("ModeloAccesorioIngreso").style.borderColor = "#ccc";
        $('#errorModeloAccesorioIng').html('').hide();
    }
    //Validación para el serial del activo
    if (!serialActivo && serialActivo.length <= 0) {
        isValid = false;
        document.getElementById("SerialAccesorioIngreso").style.borderColor = "#900C3F";
        $('#errorSerialAccesorioIng').html('El campo Serial de Activo es obligatorio').show();
        setTimeout("$('#errorSerialAccesorioIng').html('').hide('slow')", 6000);
    } else {
        document.getElementById("SerialAccesorioIngreso").style.borderColor = "#ccc";
        $('#errorSerialAccesorioIng').html('').hide();
    }

    return isValid;
}

//Función para setear valores N/A
function setearModelo() {
    var modeloActivo = document.getElementById("ModeloAccesorioIngreso").value;
    //Validación para el modelo del activo
    if (!modeloActivo && modeloActivo.length <= 0) {
        isValid = false;
        document.getElementById("ModeloAccesorioIngreso").value = "N/A";
    }
}

//Función para setear valores N/A
function setearSerial() {
    var serialActivo = document.getElementById("SerialAccesorioIngreso").value;
    //Validación para el serial del activo
    if (!serialActivo && serialActivo.length <= 0) {
        document.getElementById("SerialAccesorioIngreso").value = "N/A";
    } 
}

/////////////////////////Funciones para cargar el campo de autocompletado
function cargarNombresActivos(url) {
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'get',
        success: function (data) {
            for (var i = 0; i < data.length; i++) {
                nombresActivo[i] = data[i].NombreActivo;
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

//Mensajes para los tooltips
function mensajesTooltipsAccesorios() {
    document.getElementById("NombreAccesorioIngreso").title = "Máximo 50 caracteres en Mayúscula.\n No se puede ingresar caracteres especiales ni espacios.";
    document.getElementById("SerialAccesorioIngreso").title = "Máximo 80 caracteres.\n No se puede ingresar caracteres especiales ni espacios.";
    document.getElementById("ModeloAccesorioIngreso").title = "Máximo 80 caracteres.\n No se puede ingresar caracteres especiales.";
    document.getElementById("DescripcionAccesorioIngreso").title = "Máximo 150 caracteres.\n No se puede ingresar caracteres especiales.";
}

//Mensajes para los tooltips
function mensajesTooltipsAccesoriosMod() {
    document.getElementById("NombreAccesorio").title = "Máximo 50 caracteres en Mayúscula.\n No se puede ingresar caracteres especiales ni espacios.";
    document.getElementById("SerialAccesorio").title = "Máximo 80 caracteres.\n No se puede ingresar caracteres especiales ni espacios.";
    document.getElementById("ModeloAccesorio").title = "Máximo 80 caracteres.\n No se puede ingresar caracteres especiales.";
    document.getElementById("DescripcionAccesorio").title = "Máximo 150 caracteres.\n No se puede ingresar caracteres especiales.";
}




