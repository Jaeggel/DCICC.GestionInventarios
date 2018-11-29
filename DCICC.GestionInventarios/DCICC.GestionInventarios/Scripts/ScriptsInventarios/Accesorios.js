var url_idioma = obtenerIdioma();
var cmbEstados = listaEstadosActivos();
var url_metodo;
var cmbTipoAccesorio;
var datosAccesorios;
var idAccesorioMod;

//Método ajax para obtener los datos de categorias
function obtenerAccesorios(url) {
    url_metodo = url;
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'get',
        success: function (data) {
            console.log("Datos Exitosos");
            datosAccesorios = data;
            cargarAccesoriosTabla();
            $('#dataTableAccesorios').DataTable({
                "language": {
                    "url": url_idioma
                },
                "order": [[1, "asc"]]
            });
        }
    });
}

function datosTipoAccesorio(url) {
    //url_metodo = url;
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'get',
        success: function (data) {
            cmbTipoAccesorio = data;
            cargarAccesoriosCmb();

        }
    });
}

function cargarEstadosAccesoriosCmb() {
    var str = '<select id="EstadoAccesorio" class="form-control" name="EstadoAccesorio" required>';
    str += '<option value="">Escoga una opción...</option>';
    for (var i = 0; i < cmbEstados.length; i++) {
        str += '<option value="' + cmbEstados[i] + '">' + cmbEstados[i] + '</option>';
    };
    str += '</select>';
    $("#cargarEstadosAccesorio").html(str);
}

function cargarAccesoriosCmb() {
    var str = '<select id="AccesorioActivo" class="form-control" name="AccesorioActivo" required>';
    str += '<option value="">Escoga una opción...</option>';
    for (var i = 0; i < cmbTipoAccesorio.length; i++) {
        str += '<option value="' + cmbTipoAccesorio[i].IdTipoAccesorio + '">' + cmbTipoAccesorio[i].NombreTipoAccesorio + '</option>';
    };
    str += '</select>';
    $("#cargarAccesorios").html(str);
}

//Función para cargar la tabla de Activos
function cargarAccesoriosTabla() {
    var str = '<table id="dataTableAccesorios" class="table jambo_table bulk_action  table-bordered " style="width:100%">';
    str += '<thead> <tr> <th>Tipo de Accesorio</th> <th>Nombre de Accesorio</th> <th>Activo al que pertenece:</th> <th>Serial de Accesorio</th> <th>Modelo de Accesorio</th> <th>Estado de Accesorio</th> <th>Modificar</th> <th>Cambiar Estado</th> </tr> </thead>';
    str += '<tbody>';
    for (var i = 0; i < datosAccesorios.length; i++) {

        str += '<tr><td>' + datosAccesorios[i].NombreTipoAccesorio +
            '</td><td>' + datosAccesorios[i].NombreAccesorio +
            '</td><td>' + datosAccesorios[i].NombreDetalleActivo +            
            '</td><td>' + datosAccesorios[i].SerialAccesorio +
            '</td><td>' + datosActivos[i].ModeloAccesorio +
            '</td><td>' + datosActivos[i].EstadoAccesorio;
        str += '</td><td><div class="text-center"><div class="col-md-12 col-sm-12 col-xs-12">' +
            '<button type="button" class="btn btn-info text-center" data-toggle="modal" data-target="#ModificarAccesorio" onclick = "formUpdateAccesorio(' + datosAccesorios[i].IdAccesorio + ');"> <strong><i class="fa fa-pencil-square-o"></i></strong></button> ' +
            '</div></div>' +
            '</td><td><div class="text-center"><div class="col-md-12 col-sm-12 col-xs-12">';
        if (datosActivos[i].EstadoActivo == "OPERATIVO") {
            str += '<button type = "button" class="btn btn-success text-center" data-toggle="modal" data-target="#ModificarEstadoActivo" onclick = "habilitarOdeshabilitar(' + datosAccesorios[i].IdAccesorio + ');" > <strong><span class="fa fa-toggle-on"></span></strong></button> ';
        } else if (datosActivos[i].EstadoActivo == "NO OPERATIVO") {
            str += '<button type = "button" class="btn btn-warning text-center" data-toggle="modal" data-target="#ModificarEstadoActivo" onclick = "habilitarOdeshabilitar(' + datosAccesorios[i].IdTipoAccesorio + ');" > <strong><span class="fa fa-toggle-on"></span></strong></button> ';
        } else {
            str += '<button type = "button" class="btn btn-danger text-center" data-toggle="modal" data-target="#ModificarEstadoActivo" onclick = "habilitarOdeshabilitar(' + datosAccesorios[i].IdTipoAccesorio + ');" > <strong><span class="fa fa-toggle-on"></span></strong></button> ';
        }

        str += '</div></div></td></tr>';
    };
    str += '</tbody>' +
        '</table > ';
    $("#tablaAccesorios").html(str);
}

function formUpdateAccesorio(idAccesorio) {
    idAccesorioMod = idAccesorio;
    for (var i = 0; i < datosAccesorios.length; i++) {
        if (datosAccesorios[i].IdAccesorio == idAccesorio) {
            
            var element = document.getElementById("AccesorioActivo");
            element.value = datosAccesorios[i].IdTipoAccesorio;
            
            
            document.getElementById("NombreActivo").value = datosAccesorios[i].NombreActivo;
            
            document.getElementById("NombreAccesorio").value = datosAccesorios[i].NombreAccesorio;
            
            document.getElementById("SerialAccesorio").value = datosAccesorios[i].SerialAccesorio;

            document.getElementById("ModeloAccesorio").value = datosAccesorios[i].ModeloAccesorio;

            document.getElementById("DescripcionAccesorio").value = datosAccesorios[i].DescripcionAccesorio;

            var element3 = document.getElementById("EstadoAccesorio");
            element3.value = datosAccesorios[i].EstadoAccesorio;

            
            
        }
    };

}


//function actualizarActivo(url) {
//    //Obtener Valor del tipo de activo
//    var cmbTipoActivo = document.getElementById("TipoActivo");
//    var idTipoActivo = cmbTipoActivo.options[cmbTipoActivo.selectedIndex].value;

//    //Obtener valor del Estado
//    var cmbEstado = document.getElementById("EstadoActivo");
//    var idEstado = cmbEstado.options[cmbEstado.selectedIndex].value;
//    //Obtener valor del nombre de activo
//    var nombreActivo = document.getElementById("NombreActivo").value;
//    //Obtener valor del serial de activo
//    var serialActivo = document.getElementById("SerialActivo").value;
//    //Obtener valor del modelo de activo
//    var modeloActivo = document.getElementById("ModeloActivo").value;
//    //Obtener valor del codigo UPS
    

//    swal({
//        title: 'Confirmación de Actualización',
//        text: "¿Está seguro de modificar el registro?",
//        type: 'warning',
//        showCancelButton: true,
//        confirmButtonColor: '#26B99A',
//        cancelButtonColor: '#337ab7',
//        confirmButtonText: 'Confirmar',
//        cancelButtonText: 'Cancelar'
//    }).then((result) => {
//        if (result.value) {
//            $.ajax({
//                data: {
//                    "IdActivo": idActivoMod, "IdTipoActivo": idTipoActivo, "IdLaboratorio": idLaboratorio, "IdMarca": idMarca, "NombreActivo": nombreActivo, "EstadoActivo": idEstado,
//                    "SerialActivo": serialActivo, "ModeloActivo": modeloActivo, "CodigoUpsActivo": codigoUps, "FechaIngresoActivo": fechaIngreso,
//                    "DescripcionActivo": descripcionActivo, "ExpressServiceCodeActivo": expressCode, "FechaManufacturaActivo": fechaManufactura,
//                    "NumPuertosActivo": numPuertos, "IosVersionActivo": iosVersion, "ProductNameActivo": productName, "HpePartNumberActivo": hpe,
//                    "CodBarras1Activo": cod1, "CodBarras2Activo": cod2, "CtActivo": ct, "CapacidadActivo": capacidad, "VelocidadTransfActivo": velocidadTransf
//                },
//                url: url,
//                type: 'post',
//                success: function () {
//                    $('#ModificarActivo').modal('hide');
//                    showNotify("Actualización exitosa", 'El usuario se ha modificado correctamente', "success");
//                    obtenerActivos(url_metodo);

//                }, error: function () {
//                    $('#ModificarActivo').modal('hide');
//                    showNotify("Error en la Actualización", 'No se ha podido modificar el usuario', "error");
//                }
//            });

//        } else {
//            $('#ModificarActivo').modal('hide');
//        }
//    });

//}

//function habilitarOdeshabilitar(idActivo) {
//    idActivoMod = idActivo;
//}

//function actualizarEstadoActivo(url) {

//    var cmbEstado = document.getElementById("EstadoActivoModificar");
//    var idEstado = cmbEstado.options[cmbEstado.selectedIndex].value;

//    swal({
//        title: 'Confirmación de Actualización',
//        text: "¿Está seguro de modificar el estado del Activo?",
//        type: 'warning',
//        showCancelButton: true,
//        confirmButtonColor: '#26B99A',
//        cancelButtonColor: '#337ab7',
//        confirmButtonText: 'Confirmar',
//        cancelButtonText: 'Cancelar'
//    }).then((result) => {
//        if (result.value) {
//            $.ajax({
//                data: {
//                    "IdActivo": idActivoMod, "EstadoActivo": idEstado
//                },
//                url: url,
//                type: 'post',
//                success: function () {
//                    $('#ModificarEstadoActivo').modal('hide');
//                    obtenerActivos(url_metodo);
//                }, error: function () {

//                }
//            });
//        } else {
//        }
//    });

//}