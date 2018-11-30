var url_idioma = obtenerIdioma();
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


//Método ajax para obtener los datos de categorias
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
    
    var str = '<select id="TipoActivo" class="form-control" name="TipoActivo" required>';
    str += '<option value="">Escoga una opción...</option>';
    for (var i = 0; i < cmbTipoActivo.length; i++) {
            console.log(cmbTipoActivo[i].NombreTipoActivo);
            str += '<option value="' + cmbTipoActivo[i].IdTipoActivo + '">' + cmbTipoActivo[i].NombreTipoActivo + '</option>';
    };
    str += '</select>';
    $("#cargarTipoActivos").html(str);
}

function cargarLaboratoriosCmb() {
    var str = '<select id="LaboratorioActivo" class="form-control" name="LaboratorioActivo" required>';
    str += '<option value="">Escoga una opción...</option>';
    for (var i = 0; i < cmbLaboratorio.length; i++) {
        str += '<option value="' + cmbLaboratorio[i].IdLaboratorio + '">' + cmbLaboratorio[i].NombreLaboratorio + '</option>';
    };
    str += '</select>';
    $("#cargarLaboratorios").html(str);
}

function cargarMarcasCmb() {
    var str = '<select id="MarcaActivo" class="form-control" name="MarcaActivo" required>';
    str += '<option value="">Escoga una opción...</option>';
    for (var i = 0; i < cmbMarcas.length; i++) {
        str += '<option value="' + cmbMarcas[i].IdMarca + '">' + cmbMarcas[i].NombreMarca + '</option>';
    };
    str += '</select>';
    $("#cargarMarcas").html(str);
}

function cargarEstadosCmb() {
    var str = '<select id="EstadoActivo" class="form-control" name="EstadoActivo" required>';
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
                    "IdActivo":idActivoMod,"IdTipoActivo": idTipoActivo, "IdLaboratorio": idLaboratorio, "IdMarca": idMarca, "NombreActivo": nombreActivo, "EstadoActivo": idEstado,
                    "SerialActivo": serialActivo, "ModeloActivo": modeloActivo, "CodigoUpsActivo": codigoUps, "FechaIngresoActivo": fechaIngreso,
                    "DescripcionActivo": descripcionActivo, "ExpressServiceCodeActivo": expressCode, "FechaManufacturaActivo": fechaManufactura,
                    "NumPuertosActivo": numPuertos, "IosVersionActivo": iosVersion, "ProductNameActivo": productName, "HpePartNumberActivo": hpe,
                    "CodBarras1Activo": cod1, "CodBarras2Activo": cod2, "CtActivo": ct, "CapacidadActivo": capacidad, "VelocidadTransfActivo": velocidadTransf
                },
                url: url,
                type: 'post',
                success: function () {
                    $('#ModificarActivo').modal('hide');
                    showNotify("Actualización exitosa", 'El usuario se ha modificado correctamente', "success");
                    obtenerActivos(url_metodo);  

                }, error: function () {
                    $('#ModificarActivo').modal('hide');
                    showNotify("Error en la Actualización", 'No se ha podido modificar el usuario', "error");
                }
            });

        } else {
            $('#ModificarActivo').modal('hide');
        }
    });
 
}

function habilitarOdeshabilitar(idActivo) {
    idActivoMod = idActivo;
}

function actualizarEstadoActivo(url) {

    var cmbEstado = document.getElementById("EstadoActivoModificar");
    var idEstado = cmbEstado.options[cmbEstado.selectedIndex].value;

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
                success: function () {
                    $('#ModificarEstadoActivo').modal('hide');                   
                    obtenerActivos(url_metodo);
                }, error: function () {

                }
            });
        } else {
            $('#ModificarEstadoActivo').modal('hide');
        }
    });

}


//////////////////////////////////////FUNCIONES PARA GUARDAR ACCESORIO/////////////////////////////////////

function formIngresoAccesorio(idAct, nombreAct) {
    idAccesorioIng = idAct;
    nombreAccesorioIng = nombreAct;
    console.log(nombreAccesorioIng);
    document.getElementById("NombreActivoIngreso").value = nombreAccesorioIng;
   
}

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
                        datosCQRAccesorio(data.ObjetoInventarios);
                        var str = '<img src="' + urlImagen +'"/>';
                        $("#imgCQR").html(str);
                        $("#btnGenPDF").click(function () {
                            $('#GenPDFForm').attr('target', "_blank");
                            $('#GenPDFForm').attr('action', urlPdf).submit();
                        });
                        obtenerActivos(url_metodo);
                        obtenerAccesorios(url_metodo_accesorio);
                        showNotify("Actualización exitosa", 'El accesorio se ha ingresado correctamente', "success");
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

function datosCQRAccesorio(data) {
    idCQRAccesorio = data.IdCQR;
    nombreCQRAccesorio = data.NombreAccesorio;
    $('#idCQRAccesorio').html(idCQRAccesorio).show();
    console.log(nombreCQRAccesorio);
    $('#nombreAccesorioIngresado').html(nombreCQRAccesorio ).show();
    $('#cqrAccesorio').show();  
}

function limpiarModal() {
    $('#IngresoNuevoAccesorio').modal('hide');
    $(".modal-body select").val("");
    $(".modal-body input").val("");
    $(".modal-body textarea").val("");
    $('#cqrAccesorio').hide();
}
