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
var nombreActivoRegis;
var nombresActivo = [];

/* --------------------------------------SECCIÓN PARA OBTENER DATOS DEL SERVIDOR---------------------------------*/

//Método ajax para recibir los datos del Tipo Activo
function datosTipoActivo(url) {
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            cmbTipoActivo = data;

        }
    });
}

//Método ajax para obtener los datos de las categorias
function datosCategoria(url) {
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            cmbCategoria = data;
            cargarCategoriasCmb();
            
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
            cmbLaboratorio = data;
            cargarLaboratoriosCmb();
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
            cmbMarcas = data;
            cargarMarcasCmb();
        }
    });
}

//Método ajax para obtener los datos de los tipos de accesorio
function datosTipoAccesorio(url) {
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            cmbTipoAccesorio = data;
            cargarAccesoriosCmb();

        }
    });
}

//Método ajax para obtener los datos del Responsable
function obtenerResponsable(url) {
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            console.log(data);
            document.getElementById("ResponsableActivo").value = data;

        }, error: function (request,status,error) {
            console.log(request);
            console.log(status);
            console.log(error);
        }
    });
}

//Método ajax para modificar los datos del Responsable
function modificarResponsable(url) {
    var nombre = document.getElementById("ResponsableActivo").value;
    $.ajax({
        data: {"NombreResponsable":nombre },
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            console.log(data);
            document.getElementById("ResponsableActivo").value = data;

        }, error: function (request, status, error) {
            console.log(request);
            console.log(status);
            console.log(error);
        }
    });
}

//Método para obtener los datos del activo de TI ingresado para generar el código CQR
function datosActivoSeleccionado(data) {
    idActivo = data.IdActivo;
    idCQR = data.IdCQR;
    nombreActivoRegis = data.NombreActivo;
}

/* --------------------------------------SECCIÓN PARA CARGAR COMBOBOX PARA ACTIVOS---------------------------------*/

//Función para cargar el combobox de categorias de activo
function cargarCategoriasCmb() {
    var str = '<select id="CategoriaActivo" class="form-control" name="CategoriaActivo" required>';
    str += '<option value="">Escoga una opción...</option>';
    for (var i = 0; i < cmbCategoria.length; i++) {
        str += '<option value="' + cmbCategoria[i].IdCategoriaActivo + '">' + cmbCategoria[i].NombreCategoriaActivo + '</option>';
        
    };
    str += '</select>';
    $("#cargarCategorias").html(str);
    //Método para obtener los tipos de activo dependiendo de su categoria
    $('#CategoriaActivo').change(function () {
        $('#TipoActivo option').remove()
        var opcion = document.getElementById("CategoriaActivo");
        var valorCat = opcion.options[opcion.selectedIndex].value;
        cargarTipoActivoCmb(valorCat);
    });
    
}

//Función para cargar el combobox de tipo de activos
function cargarTipoActivoCmb(idCategoria) {
    for (var i = 0; i < cmbTipoActivo.length; i++) {
        if (cmbTipoActivo[i].IdCategoriaActivo == idCategoria) {
            $("#TipoActivo").append('<option value="' + cmbTipoActivo[i].IdTipoActivo + '">' + cmbTipoActivo[i].NombreTipoActivo + '</option>');
        }     
    };
}

//Función para cargar el combobox de laboratorios de activo
function cargarLaboratoriosCmb() {
    var str = '<select id="LaboratorioActivo" class="form-control" name="LaboratorioActivo" required>';
    str += '<option value="">Escoga una opción...</option>';
    for (var i = 0; i < cmbLaboratorio.length; i++) {
        str += '<option value="' + cmbLaboratorio[i].IdLaboratorio + '">' + cmbLaboratorio[i].NombreLaboratorio + '</option>';
    };
    str += '</select>';
    $("#cargarLaboratorios").html(str);
}

//Función para cargar el combobox de marcas de activo
function cargarMarcasCmb() {
    var str = '<select id="MarcaActivo" class="form-control" name="MarcaActivo" required>';
    str += '<option value="">Escoga una opción...</option>';
    for (var i = 0; i < cmbMarcas.length; i++) {
        str += '<option value="' + cmbMarcas[i].IdMarca + '">' + cmbMarcas[i].NombreMarca + '</option>';
    };
    str += '</select>';
    $("#cargarMarcas").html(str);
}

//Función para cargar el combobox  del estado de un activo 
function cargarEstadosCmb() {
    var str = '<select id="EstadoActivo" class="form-control" name="EstadoActivo" required>';
    str += '<option value="">Escoga una opción...</option>';
    for (var i = 0; i < cmbEstados.length; i++) {
        str += '<option value="' + cmbEstados[i] + '">' + cmbEstados[i] + '</option>';
    };
    str += '</select>';
    $("#cargarEstados").html(str);
}

//Función para cargar el combobox de los estados de un accesorio
function cargarEstadosAccesorioCmb() {
    var str = '<select id="EstadoAccesorios" class="form-control" name="EstadoAccesorios" required>';
    str += '<option value="">Escoga una opción...</option>';
    for (var i = 0; i < cmbEstados.length; i++) {
        str += '<option value="' + cmbEstados[i] + '">' + cmbEstados[i] + '</option>';
    };
    str += '</select>';
    $("#cargarEstadosAccesorio").html(str);
}

//Función para cargar el combobox de accesorios para un activo
function cargarAccesoriosCmb() {
    var str = '<select id="AccesorioActivo" class="form-control" name="AccesorioActivo" required>';
    str += '<option value="">Escoga una opción...</option>';
    for (var i = 0; i < cmbTipoAccesorio.length; i++) {
        str += '<option value="' + cmbTipoAccesorio[i].IdTipoAccesorio + '">' + cmbTipoAccesorio[i].NombreTipoAccesorio + '</option>';
    };
    str += '</select>';
    $("#cargarAccesorios").html(str);
}

//Función para cargar el CQR y nombre del activo creado
function cargarCQRActivo() {
    $('#idCQR').html(idCQR).show();
    console.log(nombreActivoRegis);
    $('#nombreActivoIngresado').html(nombreActivoRegis).show();
}

/* -------------------------------------SECCIÓN PARA VALIDACIÓN DEL PASO 1 Y 2 DEL SMARTWIZARD---------------------------------*/

//Método de validación para el paso 1
function validarPaso1() {
    var isValid = true;
    var nombreActivo = document.getElementById("NombreActivo").value;
    var modeloActivo = document.getElementById("ModeloActivo").value;
    var serialActivo = document.getElementById("SerialActivo").value;
    var responsableActivo = document.getElementById("ResponsableActivo").value;
    //Validación para combobox categoria de Activo
    if (document.getElementById("CategoriaActivo").value == "") {
        isValid = false;
        document.getElementById("CategoriaActivo").style.borderColor = "#900C3F";
        $('#errorCategoria').html('Debe seleccionar una Opción de la Categoría').show();
        setTimeout("$('#errorCategoria').html('').hide('slow')", 6000);

    } else {
        document.getElementById("CategoriaActivo").style.borderColor = "#ccc";
        $('#errorCategoria').html('').hide();
    }
     //Validación para combobox tipo de Activo
    if (document.getElementById("TipoActivo").value == "") {
        isValid = false;
        document.getElementById("TipoActivo").style.borderColor = "#900C3F";
        $('#errorTipo').html('Debe seleccionar una Opción del Tipo de Activo').show();
        setTimeout("$('#errorTipo').html('').hide('slow')", 6000);

    } else {
        document.getElementById("TipoActivo").style.borderColor = "#ccc";
        $('#errorTipo').html('').hide();
    }
     //Validación para combobox laboratorio de Activo
    if (document.getElementById("LaboratorioActivo").value == "") {
        isValid = false;
        document.getElementById("LaboratorioActivo").style.borderColor = "#900C3F";
        $('#errorLaboratorio').html('Debe seleccionar una Opción de Laboratorio').show();
        setTimeout("$('#errorLaboratorio').html('').hide('slow')", 6000);

    } else {
        document.getElementById("LaboratorioActivo").style.borderColor = "#ccc";
        $('#errorLaboratorio').html('').hide();
    }
    //Validación para combobox marca de Activo
    if (document.getElementById("MarcaActivo").value == "") {
        isValid = false;
        document.getElementById("MarcaActivo").style.borderColor = "#900C3F";
        $('#errorMarca').html('Debe seleccionar una Opción de Marca').show();
        setTimeout("$('#errorMarca').html('').hide('slow')", 6000);

    } else {
        document.getElementById("MarcaActivo").style.borderColor = "#ccc";
        $('#errorMarca').html('').hide();
    }
    //Validación para combobox estado de Activo
    if (document.getElementById("EstadoActivo").value == "") {
        isValid = false;
        document.getElementById("EstadoActivo").style.borderColor = "#900C3F";
        $('#errorEstado').html('Debe seleccionar una Opción de Estado').show();
        setTimeout("$('#errorEstado').html('').hide('slow')", 6000);

    } else {
        document.getElementById("EstadoActivo").style.borderColor = "#ccc";
        $('#errorEstado').html('').hide();
    }
    //Validación para el nombre de Activo
    if (!nombreActivo && nombreActivo.length <= 0) {
        isValid = false;
        $("#NombreActivo").focus();
        document.getElementById("NombreActivo").style.borderColor = "#900C3F";
        $('#errorNombre').html('El campo Nombre del Activo es obligatorio').show();
        setTimeout("$('#errorNombre').html('').hide('slow')", 6000);
    } else {
        document.getElementById("NombreActivo").style.borderColor = "#ccc";
        $('#errorNombre').html('').hide();
    }
    //Validación para el modelo del activo
    if (!modeloActivo && modeloActivo.length <= 0) {
        isValid = false;
        $("#ModeloActivo").focus();
        document.getElementById("ModeloActivo").style.borderColor = "#900C3F";
        $('#errorModelo').html('El campo Modelo de Activo es obligatorio').show();
        setTimeout("$('#errorModelo').html('').hide('slow')", 6000);
    } else {
        document.getElementById("ModeloActivo").style.borderColor = "#ccc";
        $('#errorModelo').html('').hide();
    }
    //Validación para el serial del activo
    if (!serialActivo && serialActivo.length <= 0) {
        isValid = false;
        $("#SerialActivo").focus();
        document.getElementById("SerialActivo").style.borderColor = "#900C3F";
        $('#errorSerial').html('El campo Serial de Activo es obligatorio').show();
        setTimeout("$('#errorSerial').html('').hide('slow')", 6000);
    } else {
        document.getElementById("SerialActivo").style.borderColor = "#ccc";
        $('#errorSerial').html('').hide();
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

//Método de validación para el paso 2
function validarPaso2(urlMetodo,urlImagen,urlPdf) {
    var isValid = false;
    //url = '@Url.Action("NuevoActivo", "Activos")';
    //var confirmarIngreso = false;
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

    $.ajax({
        data: {
            "IdTipoActivo": idTipoActivo, "IdLaboratorio": idLaboratorio, "IdMarca": idMarca, "NombreActivo": nombreActivo, "EstadoActivo": idEstado,
            "SerialActivo": serialActivo, "ModeloActivo": modeloActivo, "CodigoUpsActivo": codigoUps, "FechaIngresoActivo": fechaIngreso,
            "DescripcionActivo": descripcionActivo, "ExpressServiceCodeActivo": expressCode, "FechaManufacturaActivo": fechaManufactura,
            "NumPuertosActivo": numPuertos, "IosVersionActivo": iosVersion, "ProductNameActivo": productName, "HpePartNumberActivo": hpe,
            "CodBarras1Activo": cod1, "CodBarras2Activo": cod2, "CtActivo": ct, "CapacidadActivo": capacidad, "VelocidadTransfActivo": velocidadTransf
        },
        url: urlMetodo,
        async: false,
        dataType: 'json',
        type: 'post',
        success: function (data) {
            console.log("Inserto");
            isValid = data.OperacionExitosa;
            if (isValid) {
                datosActivoSeleccionado(data.ObjetoInventarios);
                var str = '<img src="' + urlImagen+'")"/>';
                $("#imgCQR").html(str);
                $("#btnGenPDF").click(function () {
                    $('#GenPDFForm').attr('target', "_blank");
                    $('#GenPDFForm').attr('action', urlPdf ).submit();
                });
                cargarCQRActivo();
            } else {
                isvalid = false;
            }

        }, error: function (e) {
            isvalid = false;
        }
    });
    return isValid;

}

/* --------------------------------------SECCIÓN PARA CAMPOS DE AUTOCOMPLETE---------------------------------*/
//Funciones para cargar el campo de autocompletado
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

/* --------------------------------------SECCIÓN PARA COMPROBACIONES Y VALIDACIONES---------------------------------*/
//Función para evitar nombres de laboratorios repetidos
function comprobarNombre() {
    var nombreActivo = document.getElementById("NombreActivo"); 
    if (nombreActivo.value.length <= 0) {
        $("#NombreActivo").focus();
        nombreActivo.style.borderColor = "#900C3F";
        $('#errorNombre').html('El campo Nombre del Activo es obligatorio').show();
        setTimeout("$('#errorNombre').html('').hide('slow')", 6000);
    } else {
        for (var i = 0; i < nombresActivo.length; i++) {
            if ((nombresActivo[i]).toUpperCase() == nombreActivo.value) {
                nombreActivo.style.borderColor = "#900C3F";
                $('#errorNombre').html('El Nombre del Activo ya existe').show();
                setTimeout("$('#errorNombre').html('').hide('slow')", 6000);
                nombreActivo.value = "";
                break;
            } else {
                nombreActivo.style.borderColor = "#ccc";
                $('#errorNombre').html('').hide();
            }
        }
    }
}

//Función para setear valores N/A
function setearModelo() {
    var modeloActivo = document.getElementById("ModeloActivo").value;
    //Validación para el modelo del activo
    if (!modeloActivo && modeloActivo.length <= 0) {
        document.getElementById("ModeloActivo").value = "N/A";
    } 
}

//Función para setear valores N/A
function setearSerial() {
    var serialActivo = document.getElementById("SerialActivo").value;;
    if (!serialActivo && serialActivo.length <= 0) {
        document.getElementById("SerialActivo").value = "N/A";
    }  
}

/* --------------------------------------SECCIÓN PARA MENSAJES DE TOOLTIPS---------------------------------*/

//Mensajes para los tooltips
function mensajesTooltips() {
    document.getElementById("NombreActivo").title = "Máximo 50 caracteres en Mayúscula, sin Espacios ni Números.\n Caracteres especiales permitidos - / _ .";
    document.getElementById("SerialActivo").title = "Máximo 80 caracteres, sin Espacios.\n  Caracteres especiales permitidos - / _ .";
    document.getElementById("ModeloActivo").title = "Máximo 80 caracteres.\n Caracteres especiales permitidos - / _ .";
    document.getElementById("CodigoUpsActivo").title = "Código de Barras otorgado por la UPS. Máximo 15 números.";
    document.getElementById("FechaIngresoActivo").title = "Fecha en la que se adquirio o se recibio el Activo de TI.";
    document.getElementById("ResponsableActivo").title = "Máximo 50 caracteres. \n  Caracteres especiales permitidos - / _ .";
    document.getElementById("DescripcionActivo").title = "Máximo 150 caracteres.\n  Caracteres especiales permitidos - / _ .";
}