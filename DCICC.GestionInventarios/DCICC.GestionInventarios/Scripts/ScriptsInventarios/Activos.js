var url_idioma = obtenerIdioma();
var cmbEstados = listaEstadosActivos();
var url_metodo;
var cmbCategoria;
var cmbTipoActivo;
var cmbLaboratorio;
var cmbMarcas;
var cmbTipoAccesorio;


function datosTipoActivo(url) {
    //url_metodo = url;
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'get',
        success: function (data) {
            cmbTipoActivo = data;

        }
    });
}

function datosCategoria(url) {
    //url_metodo = url;
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'get',
        success: function (data) {
            cmbCategoria = data;
            cargarCategoriasCmb();
            
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


function cargarCategoriasCmb() {
    var str = '<select id="CategoriaActivo" class="form-control" name="CategoriaActivo" required>';
    str += '<option value="">Escoga una opción...</option>';
    for (var i = 0; i < cmbCategoria.length; i++) {
        str += '<option value="' + cmbCategoria[i].IdCategoriaActivo + '">' + cmbCategoria[i].NombreCategoriaActivo + '</option>';
        
    };
    str += '</select>';
    $("#cargarCategorias").html(str);

    $('#CategoriaActivo').change(function () {
        $('#TipoActivo option').remove()
        var opcion = document.getElementById("CategoriaActivo");
        var valorCat = opcion.options[opcion.selectedIndex].value;
        cargarTipoActivoCmb(valorCat);
    });
    
}

function cargarTipoActivoCmb(idCategoria) {
    console.log(idCategoria);
    //$("#selectBox").append('<option value="option6">option6</option>');
    //var str = '<select id="TipoActivo" class="form-control" name="TipoActivo" required>';
    //str += '<option value="">Escoga una opción...</option>';
    for (var i = 0; i < cmbTipoActivo.length; i++) {
        if (cmbTipoActivo[i].IdCategoriaActivo == idCategoria) {
            console.log(cmbTipoActivo[i].NombreTipoActivo);
            //str += '<option value="' + cmbTipoActivo[i].IdTipoActivo + '">' + cmbTipoActivo[i].NombreTipoActivo + '</option>';
            $("#TipoActivo").append('<option value="' + cmbTipoActivo[i].IdTipoActivo + '">' + cmbTipoActivo[i].NombreTipoActivo + '</option>');
        }     
    };
    //str += '</select>';
    //$("#cargarTipoActivos").html(str);
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

function cargarAccesoriosCmb() {
    var str = '<select id="AccesorioActivo" class="form-control" name="AccesorioActivo" required>';
    str += '<option value="">Escoga una opción...</option>';
    for (var i = 0; i < cmbTipoAccesorio.length; i++) {
        str += '<option value="' + cmbTipoAccesorio[i].IdTipoAccesorio + '">' + cmbTipoAccesorio[i].NombreTipoAccesorio + '</option>';
    };
    str += '</select>';
    $("#cargarAccesorios").html(str);
}

//function ingresarActivo(url) {
//    console.log(url);
//    var confirmarIngreso=false;
//    //Obtener Valor del tipo de activo
//    var cmbTipoActivo = document.getElementById("TipoActivo");
//    var idTipoActivo = cmbTipoActivo.options[cmbTipoActivo.selectedIndex].value;
//    //Obtener valor del Laboratorio
//    var cmbLaboratorio = document.getElementById("LaboratorioActivo");
//    var idLaboratorio = cmbLaboratorio.options[cmbLaboratorio.selectedIndex].value;
//    //Obtener valor de la marca
//    var cmbMarca = document.getElementById("MarcaActivo");
//    var idMarca = cmbMarca.options[cmbMarca.selectedIndex].value;
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
//    var codigoUps = document.getElementById("CodigoUpsActivo").value;
//    //Obtener valor de la fecha de ingreso del activo
//    var fechaIngreso = document.getElementById("FechaIngresoActivo").value;
//    //Obtener valor de la descripcion de activo
//    var descripcionActivo = document.getElementById("DescripcionActivo").value;
//    //Obtener valor del ExpressServiceCodeActivo
//    var expressCode = document.getElementById("ExpressServiceCodeActivo").value;
//    //Obtener valor de la FechaManufacturaActivo
//    var fechaManufactura = document.getElementById("FechaManufacturaActivo").value;
//    //Obtener valor del NumPuertosActivo
//    var numPuertos = document.getElementById("NumPuertosActivo").value;
//    //Obtener valor del IosVersionActivo
//    var iosVersion = document.getElementById("IosVersionActivo").value;
//    //Obtener valor del ProductNameActivo
//    var productName = document.getElementById("ProductNameActivo").value;
//    //Obtener valor del HpePartNumberActivo
//    var hpe = document.getElementById("HpePartNumberActivo").value;
//    //Obtener valor de CodBarras1Activo
//    var cod1 = document.getElementById("CodBarras1Activo").value;
//    //Obtener valor de CodBarras2Activo
//    var cod2 = document.getElementById("CodBarras2Activo").value;
//    //Obtener valor del CtActivo
//    var ct = document.getElementById("CtActivo").value;
//    //Obtener valor de CapacidadActivo
//    var capacidad = document.getElementById("CapacidadActivo").value;
//    //Obtener valor de VelocidadTransfActivo
//    var velocidadTransf = document.getElementById("VelocidadTransfActivo").value;

//    $.ajax({
//        data: {
//            "IdTipoActivo": idTipoActivo, "IdLaboratorio": idLaboratorio, "IdMarca": idMarca, "NombreActivo": nombreActivo, "EstadoActivo": idEstado,
//            "SerialActivo": serialActivo, "ModeloActivo": modeloActivo, "CodigoUpsActivo": codigoUps, "FechaIngresoActivo": fechaIngreso,
//            "DescripcionActivo": descripcionActivo, "ExpressServiceCodeActivo": expressCode, "FechaManufacturaActivo": fechaManufactura,
//            "NumPuertosActivo": numPuertos, "IosVersionActivo": iosVersion, "ProductNameActivo": productName, "HpePartNumberActivo": hpe,
//            "CodBarras1Activo": cod1, "CodBarras2Activo": cod2, "CtActivo": ct, "CapacidadActivo": capacidad, "VelocidadTransfActivo": velocidadTransf
//        },
//        url: url,
//        type: 'post',
//        success: function () {
//            console.log("Inserto");
//            confirmarIngreso = true;
//            console.log(confirmarIngreso);

//        }, error: function () {
//            console.log("fallo");
//            confirmarIngreso = false;
//        }
//    });
//    console.log(confirmarIngreso);
//    return confirmarIngreso;
//}



