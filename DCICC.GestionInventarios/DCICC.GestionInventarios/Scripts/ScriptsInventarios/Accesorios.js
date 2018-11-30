var url_idioma = obtenerIdioma();
var cmbEstados = listaEstadosActivos();
var url_metodo_accesorio;
var cmbTipoAccesorio;
var datosAccesorios;
var idAccesorioMod;

//Método ajax para obtener los datos de categorias
function obtenerAccesorios(url) {
    url_metodo_accesorio = url;
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


function cargarEstadosAccesoriosMod() {
    var str = '<select id="EstadoAccesorioMod" class="form-control" name="EstadoAccesorioMod" required>';
    str += '<option value="">Escoga una opción...</option>';
    for (var i = 0; i < cmbEstados.length; i++) {
        str += '<option value="' + cmbEstados[i] + '">' + cmbEstados[i] + '</option>';
    };
    str += '</select>';
    $("#cargarEstadosAccesorioMod").html(str);
}

function cargarAccesoriosCmb() {
    var str = '<select id="AccesorioActivo" class="form-control" name="AccesorioActivo" required>';
    str += '<option value="">Escoga una opción...</option>';
    for (var i = 0; i < cmbTipoAccesorio.length; i++) {
        str += '<option value="' + cmbTipoAccesorio[i].IdTipoAccesorio + '">' + cmbTipoAccesorio[i].NombreTipoAccesorio + '</option>';
    };
    str += '</select>';
    $("#cargarTipoAccesorio").html(str);
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
            '</td><td>' + datosAccesorios[i].ModeloAccesorio +
            '</td><td>' + datosAccesorios[i].EstadoAccesorio;
        str += '</td><td><div class="text-center"><div class="col-md-12 col-sm-12 col-xs-12">' +
            '<button type="button" class="btn btn-info text-center" data-toggle="modal" data-target="#ModificarAccesorio" onclick = "formUpdateAccesorio(' + datosAccesorios[i].IdAccesorio + ');"> <strong><i class="fa fa-pencil-square-o"></i></strong></button> ' +
            '</div></div>' +
            '</td><td><div class="text-center"><div class="col-md-12 col-sm-12 col-xs-12">';
        if (datosAccesorios[i].EstadoAccesorio == "OPERATIVO") {
            str += '<button type = "button" class="btn btn-success text-center" data-toggle="modal" data-target="#ModificarEstadoAccesorio" onclick = "habilitarOdeshabilitarAcc(' + datosAccesorios[i].IdAccesorio + ');" > <strong><span class="fa fa-check"></span></strong></button> ';
        } else if (datosAccesorios[i].EstadoAccesorio == "NO OPERATIVO") {
            str += '<button type = "button" class="btn btn-warning text-center" data-toggle="modal" data-target="#ModificarEstadoAccesorio" onclick = "habilitarOdeshabilitarAcc(' + datosAccesorios[i].IdAccesorio + ');" > <strong><span class="fa fa-warning"></span></strong></button> ';
        } else {
            str += '<button type = "button" class="btn btn-danger text-center" data-toggle="modal" data-target="#ModificarEstadoAccesorio" onclick = "habilitarOdeshabilitarAcc(' + datosAccesorios[i].IdAccesorio + ');" > <strong><span class="fa fa-close"></span></strong></button> ';
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
            
            
            document.getElementById("NombreActivoAccesorio").value = datosAccesorios[i].NombreDetalleActivo;
            
            document.getElementById("NombreAccesorio").value = datosAccesorios[i].NombreAccesorio;
            
            document.getElementById("SerialAccesorio").value = datosAccesorios[i].SerialAccesorio;

            document.getElementById("ModeloAccesorio").value = datosAccesorios[i].ModeloAccesorio;

            document.getElementById("DescripcionAccesorio").value = datosAccesorios[i].DescripcionAccesorio;

            var element3 = document.getElementById("EstadoAccesorio");
            element3.value = datosAccesorios[i].EstadoAccesorio;

            
            
        }
    };

}

function actualizarAccesorio(url) {

    //Obtener Valor del tipo de activo
    var cmbTipoAccesorio = document.getElementById("AccesorioActivo");
    var idTipoAccesorio = cmbTipoAccesorio.options[cmbTipoAccesorio.selectedIndex].value;
    
    var nombreAccesorio=document.getElementById("NombreAccesorio").value;

    var serialAccesorio=document.getElementById("SerialAccesorio").value;

    var modeloAccesorio=document.getElementById("ModeloAccesorio").value;

    var descripcionAccesorio = document.getElementById("DescripcionAccesorio").value;

    //Obtener valor del Estado
    var cmbEstadoAccesorio = document.getElementById("EstadoAccesorio");
    var idEstadoAccesorio = cmbEstadoAccesorio.options[cmbEstadoAccesorio.selectedIndex].value;
    console.log(idEstadoAccesorio);
  
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
                    "IdAccesorio": idAccesorioMod, "IdTipoAccesorio": idTipoAccesorio, "EstadoAccesorio": idEstadoAccesorio, "NombreAccesorio": nombreAccesorio,
                    "SerialAccesorio": serialAccesorio, "ModeloAccesorio": modeloAccesorio, "DescripcionAccesorio": descripcionAccesorio
                },
                url: url,
                type: 'post',
                success: function () {
                    $('#ModificarAccesorio').modal('hide');
                    showNotify("Actualización exitosa", 'Se ha modificado el accesorio', "success");
                    obtenerAccesorios(url_metodo_accesorio);

                }, error: function () {
                    $('#ModificarAccesorio').modal('hide');
                    showNotify("Error en la Actualización", 'No se ha podido modificar el usuario', "error");
                }
            });

        } else {
            $('#ModificarAccesorio').modal('hide');
        }
    });

}

function habilitarOdeshabilitarAcc(idActivo) {
    idAccesorioMod = idActivo;
}

function actualizarEstadoAccesorios(urlAccesorio) {

    var cmbEstado = document.getElementById("EstadoAccesorioMod");
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
                    "IdAccesorio": idAccesorioMod, "EstadoAccesorio": idEstado
                },
                url: urlAccesorio,
                type: 'post',
                success: function () {
                    $('#ModificarEstadoAccesorio').modal('hide');
                    obtenerAccesorios(url_metodo_accesorio);
                }, error: function () {

                }
            });
        } else {
        }
    });

}


//////////////////////////////////////FUNCIONES PARA GUARDAR ACCESORIO/////////////////////////////////////

function validarPaso3() {
    var isValid = validar3('@Url.Action("NuevoAccesorio", "Activos")');
    console.log(isValid);
    return isValid;

}

function validar3(url) {
    var isValid = false;

    console.log(url);
    //Obtener Valor del tipo de activo
    var cmbTipoAccesorio = document.getElementById("AccesorioActivo");
    var idTipoAccesorio = cmbTipoAccesorio.options[cmbTipoAccesorio.selectedIndex].value;
    //Obtener Valor del estado de accesorio
    var cmbEstadoAccesorio = document.getElementById("EstadoAccesorios");
    var idEstadoAccesorio = cmbEstadoAccesorio.options[cmbEstadoAccesorio.selectedIndex].value;
    //Obtener valor del nombre de activo
    var nombreAccesorio = document.getElementById("NombreAccesorio").value;
    //Obtener valor del serial de activo
    var serialAccesorio = document.getElementById("SerialAccesorio").value;
    //Obtener valor del modelo de activo
    var modeloAccesorio = document.getElementById("ModeloAccesorio").value
    //Obtener valor de la descripcion del accesorio
    var descripcionAccesorio = document.getElementById("DescripcionAccesorio").value;

    if (document.getElementById("AccesorioActivo").value == "") {
        isValid = true;
    } else {
        $.ajax({
            data: {
                "IdTipoAccesorio": idTipoAccesorio, "IdDetalleActivo": idActivo, "NombreAccesorio": nombreAccesorio, "SerialAccesorio": serialAccesorio, "ModeloAccesorio": modeloAccesorio, "DescripcionAccesorio": descripcionAccesorio, "EstadoAccesorio": idEstadoAccesorio
            },
            async: false,
            url: url,
            type: 'post',
            success: function () {
                console.log("accesorio bienn");
                isValid = true;
            }, error: function (e) {
                console.log(e);
                console.log("fallo");

                isValid = false;
            }
        });

    }
    return isValid;
}