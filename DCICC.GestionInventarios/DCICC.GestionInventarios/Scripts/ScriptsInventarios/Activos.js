var url_idioma = obtenerIdioma();
var cmbEstados = listadoEstados();
var url_metodo;
var cmbCategoria;
var cmbTipoActivo;
var cmbLaboratorio;
var cmbMarcas;
var cmbTipoAccesorio;

function datosCategoria() {
    url_metodo = url;
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

function datosTipoActivo() {
    url_metodo = url;
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'get',
        success: function (data) {
            cmbTipoActivo = data;

        }
    });
}

function datosLaboratorio() {
    url_metodo = url;
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

function datosMarcas() {
    url_metodo = url;
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

function datosTipoAccesorio() {
    url_metodo = url;
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
        cargarTipoActivoCmb(cmbCategoria[i].IdCategoriaActivo);
    };
    str += '</select>';
    $("#cargarCategorias").html(str);
    
}

function cargarTipoActivoCmb(idCategoria) {
    //$("#selectBox").append('<option value="option6">option6</option>');
    var str = '<select id="TipoActivo" class="form-control" name="TipoActivo" required>';
    str += '<option value="">Escoga una opción...</option>';
    for (var i = 0; i < cmbTipoActivo.length; i++) {
        if (cmbTipoActivo[i].IdCategoriaActivo == idCategoria) {
            str += '<option value="' + cmbTipoActivo[i].IdTipoActivo + '">' + cmbTipoActivo[i].NombreCategoriaActivo + '</option>';
        }     
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

function cargarAccesoriosCmb() {
    var str = '<select id="AccesorioActivo" class="form-control" name="AccesorioActivo" required>';
    str += '<option value="">Escoga una opción...</option>';
    for (var i = 0; i < cmbTipoAccesorio.length; i++) {
        str += '<option value="' + cmbTipoAccesorio[i].IdTipoAccesorio + '">' + cmbTipoAccesorio[i].NombreTipoAccesorio + '</option>';
    };
    str += '</select>';
    $("#cargarAccesorios").html(str);
}

//Método de validación para el paso 1
function validarPaso1() {
    var isValid = true;
    var nombreActivo = document.getElementById("NombreActivo").value;
    var modeloActivo = document.getElementById("ModeloActivo").value;
    var serialActivo = document.getElementById("SerialActivo").value;
    if (!nombreActivo && nombreActivo.length <= 0) {
        isValid = false;
        document.getElementById("NombreActivo").setCustomValidity("El campo no debe estar vacio");
        $("#NombreActivo").focus();
        $('#msg_username').html('El campo Nombre del Activo es obligatorio').show();
    } else {
        isValid = true;
    }

    if (!modeloActivo && modeloActivo.length <= 0) {
        isValid = false;
        document.getElementById("NombreActivo").setCustomValidity("El campo no debe estar vacio");
        $("#ModeloActivo").focus();
        $('#msg_username').html('El campo Modelo de Activo es obligatorio').show();
    } else {
        isValid = true;
    }

    if (!serialActivo && serialActivo.length <= 0) {
        isValid = false;
        document.getElementById("SerialActivo").setCustomValidity("El campo no debe estar vacio");
        $("#SerialActivo").focus();
        $('#msg_username').html('El campo Serial de Activo es obligatorio').show();
    } else {
        isValid = true;
    }

    if (document.getElementById("CategoriaActivo").value == "") {
        isValid = false;
        $("#SerialActivo").focus();
        $('#msg_username').html('Debe seleccionar una Opción').show();

    } else {
    isValid = true;
    }

    return isValid;
}

