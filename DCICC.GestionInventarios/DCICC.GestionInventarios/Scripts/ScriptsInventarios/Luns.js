var url_idioma = obtenerIdioma();
var url_metodo;
var datosLuns;
var cmbStorage;
var idLunModificar;
var urlEstado;
var nombresLun = [];

//Método ajax para obtener los datos de categorias
function obtenerLuns(url) {
    url_metodo = url;
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            console.log("Datos Exitosos");
            datosLuns = data;
            cargarLunTabla();
            $('#dataTableLun').DataTable({
                "language": {
                    "url": url_idioma
                }
            });
            //cargarNombresStorage();
        }
    });
}

//Método ajax para obtener los datos de categorias
function obtenerCmbStorageHab(url) {    
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            console.log("Datos Exitosos");
            cmbStorage = data;
            cargarStorageCmb();           
        }
    });
}

//Función para cargar el combobox de Storage
function cargarStorageCmb() {
    var str = '<select id="IdStorage" class="form-control" name="IdStorage" required>';
    str += '<option value="">Escoga una opción...</option>';
    for (var i = 0; i < cmbStorage.length; i++) {
        str += '<option value="' + cmbStorage[i].IdStorage + '">' + cmbStorage[i].NickStorage + '</option>';
    };
    str += '</select>';
    $("#cargarStorage").html(str);
}


//Función para obtener la url de modificación
function urlEstados(url) {
    urlEstado = url;
}

//Función para cargar la tabla de Categorias
function cargarLunTabla() {
    var str = '<table id="dataTableLun" class="table jambo_table bulk_action  table-bordered " style="width:100%">';
    str += '<thead> <tr> <th>Nombre de LUN</th> <th>Nombre de Storage</th> <th>Capacidad (GB/TB)</th> <th>RAID/Tipo Conexión</th> <th>Descripcion</th> <th>Estado</th> <th>Modificar</th> <th>Habilitar/<br>Deshabilitar</th> </tr> </thead>';
    str += '<tbody>';
    for (var i = 0; i < datosLuns.length; i++) {
        str += '<tr><td>' + datosLuns[i].NombreLUN +
            '</td><td>' + datosLuns[i].NombreStorage +
            '</td><td>' + datosLuns[i].CapacidadLUN +
            '</td><td>' + datosLuns[i].RaidTPLUN +
            '</td><td>' + datosLuns[i].DescripcionLUN;

        if (datosLuns[i].HabilitadoLUN) {
            str += '</td><td> Habilitado';
        } else {
            str += '</td><td> Deshabilitado';
        }
        str += '</td><td><div class="text-center"><div class="col-md-12 col-sm-12 col-xs-12">' +
            '<button type="button" class="btn btn-info text-center" data-toggle="modal" data-target="#ModificarStorage" onclick = "formUpdateStorage(' + datosLuns[i].IdLUN + ');"> <strong><i class="fa fa-pencil-square-o"></i></strong></button> ' +
            '</div></div>' +
            '</td><td><div class="text-center"><div class="col-md-12 col-sm-12 col-xs-12">';
        if (datosLuns[i].HabilitadoLUN) {
            str += '<button type = "button" class="btn btn-success text-center" onclick = "habilitarOdeshabilitar(' + datosLuns[i].IdLUN + ',' + datosLuns[i].HabilitadoLUN + ');" > <strong><span class="fa fa-toggle-on"></span></strong></button> ';
        } else {
            str += '<button type = "button" class="btn btn-danger text-center" onclick = "habilitarOdeshabilitar(' + datosLuns[i].IdLUN + ',' + datosLuns[i].HabilitadoLUN + ');"> <strong><i class="fa fa-toggle-off"></i></strong></button> ';
        }
        str += '</div></div></td></tr>';
    };
    str += '</tbody></table>';
    $("#tablaModificarLuns").html(str);
}


/////////////////////////Funciones para cargar el campo de autocompletado
function cargarNombresLun() {
    for (var i = 0; i < datosLuns.length; i++) {
        nombresLun[i] = datosLuns[i].NombreLUN;
    }
}
//Función para cargar los nombres en el campo de nombre de ingreso  de storage
$(function () {
    $("#NombreLUN").autocomplete({
        source: nombresLun
    });
});


////////////Función para evitar nombres de storage repetidos
function comprobarNombreLun() {
    var nomLun = document.getElementById("NombreLUN");
    nomLun.value = nomLun.value.toUpperCase();
    if (nomLun.value.length <= 0) {
        nomLun.style.borderColor = "#900C3F";
        $('#errorNombreLUN').html('El campo nombre no debe estar vacio').show();
        setTimeout("$('#errorNombreLUN').html('').hide('slow')", 6000);
    } else {
        for (var i = 0; i < datosLuns.length; i++) {
            if ((datosLuns[i].NombreLUN).toUpperCase() == nomLun.value) {
                nomLun.style.borderColor = "#900C3F";
                $('#errorNombreLUN').html("El nombre del Storage: " + nomLun.value + " ya existe").show();
                setTimeout("$('#errorNombreLUN').html('').hide('slow')", 6000);
                nomLun.value = "";
                break;
            } else {
                nomLun.style.borderColor = "#ccc";
                $('#errorNombreLUN').html('').hide();
            }
        }
    }
}


//Función para validar disco duro 
function validarNumeroLun() {
    var esValido = true;
    var capa = document.getElementById("CapacidadLUN");
    //Validar memoria capa
    if (capa.value.length <= 0) {
        esValido = false;
        capa.value = "";
        capa.style.borderColor = "#900C3F";
        $('#errorCapacidadLun').html('El campo capacidad no debe estar vacio').show();
        setTimeout("$('#errorCapacidadLun').html('').hide('slow')", 6000);
    } else if (capa.value < 1) {
        esValido = false;
        capa.value = "";
        capa.style.borderColor = "#900C3F";
        $('#errorCapacidadLun').html('El rango de capacidad es de 1 a 100').show();
        setTimeout("$('#errorCapacidadLun').html('').hide('slow')", 6000);
    } else if (capa.value > 100) {
        esValido = false;
        capa.value = "";
        capa.style.borderColor = "#900C3F";
        $('#errorCapacidadLun').html('No se puede ingresar un valor mayor a 100').show();
        setTimeout("$('#errorCapacidadLun').html('').hide('slow')", 6000);
    } else {
        capa.style.borderColor = "#ccc";
        $('#errorCapacidadLun').html('').hide();
    }
    return esValido;
}

//Mensajes para los tooltips
function mensajesTooltipLun() {
    document.getElementById("NombreLUN").title = "Máximo 80 caracteres en Mayúscula.\n Caracteres especiales permitidos - / _ .";
    document.getElementById("CapacidadLUN").title = "Solo números. De 1 a 100 GB o TB";
    document.getElementById("DescripcionLUN").title = "Máximo 150 caracteres.\n Caracteres especiales permitidos - / _ .";
}