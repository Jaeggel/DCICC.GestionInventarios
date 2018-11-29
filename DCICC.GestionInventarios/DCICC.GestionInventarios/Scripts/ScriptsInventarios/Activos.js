var url_idioma = obtenerIdioma();
var cmbEstados = listaEstadosActivos();
var url_metodo;
var cmbCategoria;
var cmbTipoActivo;
var cmbLaboratorio;
var cmbMarcas;
var cmbTipoAccesorio;
var datosActivo;
var idActivo;
var idCQR;
var nombreActivo;


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

function datosActivoSeleccionado(data) {
    idActivo = data.IdActivo;
    console.log(idActivo);
    idCQR = data.IdCQR;
    nombreActivo = data.nombreActivo;
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

function cargarEstadosAccesorioCmb() {
    var str = '<select id="EstadoAccesorios" class="form-control" name="EstadoAccesorio" required>';
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
                "IdTipoAccesorio": idTipoAccesorio, "IdDetalleActivo": idActivo, "NombreAccesorio": nombreAccesorio, "SerialAccesorio": serialAccesorio, "ModeloAccesorio": modeloAccesorio, "DescripcionAccesorio": descripcionAccesorio, " EstadoAccesorio": idEstadoAccesorio
            },
            url: url,
            async: false,
            dataType: 'json',
            url: url,
            type: 'post',
            success: function (data) {
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

