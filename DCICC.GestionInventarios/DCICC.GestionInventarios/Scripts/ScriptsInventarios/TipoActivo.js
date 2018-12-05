var url_idioma = obtenerIdioma();
var url_metodo;
var datosTipoActivo;
var cmbCategorias;
var cmbCategoriasComp;
var idTipoActivo;
var urlEstado;
var nombresTipoAcc=[];

//Método ajax para obtener los datos de tipo de activo
function obtenerTipoActivo(url) {
    url_metodo = url;
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            console.log("sii");
            datosTipoActivo = data;
            cargarTipoActTabla();    
            $('#dataTableTipoAct').DataTable({
                "language": {
                    "url": url_idioma
                }
            });
            cargarNombresTipo();
        }
    });
}

//Método ajax para obtener los datos de las categorias
function obtenerCategorias(url) {
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            console.log("siii");
            cmbCategorias = data;
            cargarCategoriasCmb();
        }
    });
}
//Método ajax para obtener los datos de las categorias completas
function obtenerCategoriasCompletas(url) {
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            console.log("siii");
            cmbCategoriasComp = data;
            cargarCategoriasCompCmb();
        }
    });
}

//Función para obtener la url de modificación
function urlEstados(url) {
    urlEstado = url;
}

//Función para cargar la tabla de Tipo de Activo
function cargarTipoActTabla() {
    var str = '<table id="dataTableTipoAct" class="table jambo_table bulk_action  table-bordered" style="width:100%">';
    str += '<thead> <tr> <th>Nombre Tipo Activo</th> <th>Categoría</th> <th>Descripción</th> <th>Vida Útil</th> <th>Estado</th> <th>Modificar</th> <th>Habilitar/<br>Deshabilitar</th> </tr> </thead>';
    str += '<tbody>';
    
    for (var i = 0; i < datosTipoActivo.length; i++) {
        
        str += '<tr><td>' + datosTipoActivo[i].NombreTipoActivo +
            '</td><td>' + datosTipoActivo[i].NombreCategoriaActivo +
            '</td><td>' + datosTipoActivo[i].DescripcionTipoActivo +
            '</td><td>' + datosTipoActivo[i].VidaUtilTipoActivo;

        if (datosTipoActivo[i].HabilitadoTipoActivo) {
            str += '</td><td> Habilitado';
        } else {
            str += '</td><td> Deshabilitado';
        }
        str += '</td><td><div class="text-center"><div class="col-md-12 col-sm-12 col-xs-12">' +
            '<button type="button" class="btn btn-info text-center" data-toggle="modal" data-target="#ModificarTipoActivo" onclick = "formUpdateTipoAct(' + datosTipoActivo[i].IdTipoActivo + ');"> <strong><i class="fa fa-pencil-square-o"></i></strong></button> ' +
            '</div></div>' +
            '</td><td><div class=" text-center"><div class="col-md-12 col-sm-12 col-xs-12">';
        if (datosTipoActivo[i].HabilitadoTipoActivo) {
            str += '<button type="button" class="btn btn-success text-center" onclick = "habilitarOdeshabilitar(' + datosTipoActivo[i].IdTipoActivo + ',' + datosTipoActivo[i].HabilitadoTipoActivo +');"> <strong><span class="fa fa-toggle-on"></span></strong></button>';
        } else {
            str += '<button type="button" class="btn btn-danger text-center" onclick = "habilitarOdeshabilitar(' + datosTipoActivo[i].IdTipoActivo + ',' + datosTipoActivo[i].HabilitadoTipoActivo +');"> <strong><i class="fa fa-toggle-off"></i></strong></button>';
        }
        str +='</div></div></td></tr>';
        
    };
    str += '</tbody></table>';
    $("#tablaModificarTipoActivo").html(str);
}

//Función para cargar el combobox de Categorias
function cargarCategoriasCmb() {
    var str = '<select id="IdCategoriaActivo" class="form-control" name="IdCategoriaActivo" onBlur="validarCmbVacios();" required>';
    str += '<option value="">Escoga una opción...</option>';
    for (var i = 0; i < cmbCategorias.length; i++) {
        str += '<option value="' + cmbCategorias[i].IdCategoriaActivo + '">' + cmbCategorias[i].NombreCategoriaActivo + '</option>';
    };
    str += '</select>';
    $("#cargarCategorias").html(str);
}

//Función para cargar el combobox de Categorias Completas
function cargarCategoriasCompCmb() {
    var str = '<select id="IdCategoriaComp" class="form-control" name="IdCategoriaComp" onBlur=" validarCmbTipoComp();" required>';
    str += '<option value="">Escoga una opción...</option>';
    for (var i = 0; i < cmbCategoriasComp.length; i++) {
        str += '<option value="' + cmbCategoriasComp[i].IdCategoriaActivo + '">' + cmbCategoriasComp[i].NombreCategoriaActivo + '</option>';
    };
    str += '</select>';
    $("#cargarCategorias").html(str);
}

//Función para setear los valores en los inputs
function formUpdateTipoAct(idTipoAct) {
    console.log(idTipoAct);
    idTipoActivo = idTipoAct;
    for (var i = 0; i < datosTipoActivo.length; i++) {

        if (datosTipoActivo[i].IdTipoActivo == idTipoAct) {
            //Métodos para setear los valores a modificar
            var element = document.getElementById("IdCategoriaComp");
            element.value = datosTipoActivo[i].IdCategoriaActivo;
            //element.options[element.options.length] = new Option(datosTipoActivo[i].NombreCategoriaActivo, 0);         
           
            document.getElementById("NombreTipoActivo").value = datosTipoActivo[i].NombreTipoActivo;
            document.getElementById("DescripcionTipoActivo").value = datosTipoActivo[i].DescripcionTipoActivo;
            document.getElementById("VidaUtilTipoActivo").value = datosTipoActivo[i].VidaUtilTipoActivo;

            //Método para el check del update de Tipo de Activo
            var valor = datosTipoActivo[i].HabilitadoTipoActivo;
            var estado = $('#HabilitadoTipoActivo').prop('checked');
            if (estado && valor == false) {
                document.getElementById("HabilitadoTipoActivo").click();
            }
            if (estado == false && valor == true) {
                document.getElementById("HabilitadoTipoActivo").click();
            }
            break;
        }
    };
}

//Función para modificar el Tipo de activo especificado
function modificarTipoActivo(url_modificar) {
    console.log(url_modificar);
    var cmbCategoria = document.getElementById("IdCategoriaComp");
    var idCategoria = cmbCategoria.options[cmbCategoria.selectedIndex].value;
    var nombreTipo=document.getElementById("NombreTipoActivo").value;
    var descripcionTipo=document.getElementById("DescripcionTipoActivo").value;
    var vidaUtil=document.getElementById("VidaUtilTipoActivo").value;
    var habilitadoTipo = $('#HabilitadoTipoActivo').prop('checked');

    if (validarCmbTipoComp() && validarInputsVaciosIngreso() && validarVidaUtil()) {
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
                    data: { "IdTipoActivo": idTipoActivo, "IdCategoriaActivo": idCategoria, "NombreTipoActivo": nombreTipo, "DescripcionTipoActivo": descripcionTipo, "VidaUtilTipoActivo": vidaUtil, "HabilitadoTipoActivo": habilitadoTipo },
                    url: url_modificar,
                    type: 'post',
                    success: function (data) {
                        if (data.OperacionExitosa) {
                            $('#ModificarTipoActivo').modal('hide');
                            showNotify("Actualización exitosa", 'El Tipo de Activo se ha modificado correctamente', "success");
                            obtenerTipoActivo(url_metodo);
                        } else {
                            $('#ModificarTipoActivo').modal('hide');
                            showNotify("Error en la Actualización", 'No se ha podido modificar el Tipo de Activo: ' + data.MensajeError, "error");
                        }
                    }
                });

            } else {
                $('#ModificarTipoActivo').modal('hide');
            }
        });
    }

    
}


//Función para habilitar o deshabilitar la categoria
function habilitarOdeshabilitar(idTipoAct, estadoTipoAct) {
    var nuevoEstado = true;
    if (estadoTipoAct) {
        nuevoEstado = false;
    } else {
        nuevoEstado = true;
    }
    console.log(nuevoEstado);
    swal({
        title: 'Confirmación de Cambio de Estado',
        text: "¿Está seguro de Cambiar de Estado del Tipo de Activo?",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#26B99A',
        cancelButtonColor: '#337ab7',
        confirmButtonText: 'Confirmar',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                data: { "IdTipoActivo": idTipoAct, "HabilitadoTipoActivo": nuevoEstado },
                url: urlEstado,
                type: 'post',
                success: function (data) {
                    console.log(data.OperacionExitosa);
                    if (data.OperacionExitosa) {
                        showNotify("Actualización exitosa", 'El Estado del Tipo de Activo se ha modificado correctamente', "success");
                        obtenerTipoActivo(url_metodo);
                    } else {
                        showNotify("Error en la Actualización", 'No se ha podido modificar el Estado del Tipo Activo: ' + data.MensajeError, "error");
                    }   
                }
            });
        } else {

        }
    });
}

////////Función para evitar nombres de tipo activo repetidos
function comprobarNombre() {
    var nombre = document.getElementById("NombreTipoActivo");
    nombre.value = nombre.value.toUpperCase();
    if (nombre.value.length <= 0) {
        nombre.style.borderColor = "#900C3F";
        $('#errorNombreTipo').html('El campo nombre no debe estar vacio').show();
        setTimeout("$('#errorNombreTipo').html('').hide('slow')", 6000);
    } else {
        for (var i = 0; i < datosTipoActivo.length; i++) {
            if ((datosTipoActivo[i].NombreTipoActivo).toUpperCase() == nombre.value) {
                nombre.style.borderColor = "#900C3F";
                $('#errorNombreTipo').html("El nombre del Tipo Activo: " + nombre.value + " ya existe").show();
                setTimeout("$('#errorNombreTipo').html('').hide('slow')", 8000);
                nombre.value = "";
                break;
            } else {
                nombre.style.borderColor = "#ccc";
                $('#errorNombreTipo').html('').hide();
            }
        }
    }
    
}

/////////////////////////Funciones para cargar el campo de autocompletado
function cargarNombresTipo() {
    for (var i = 0; i < datosTipoActivo.length; i++) {
        nombresTipoAcc[i]=datosTipoActivo[i].NombreTipoActivo;
    }
}
//Función para cargar los nombres en el campo de nombre de ingreso  de tipo
$(function () {
    $("#NombreTipoActivo").autocomplete({
        source: nombresTipoAcc
    });
});

/////////////Funciones para validaciones de campos de texto
function validarCmbVacios() {
    var boton = document.getElementById("confirmarTipo");
    var cmbCat = document.getElementById("IdCategoriaActivo");
    //Validación para el combobox de categorias
    if (cmbCat.value == "") {
        cmbCat.style.borderColor = "#900C3F";
        $('#errorCategoriaTipo').html('Debe seleccionar una opción').show();
        setTimeout("$('#errorCategoriaTipo').html('').hide('slow')", 6000);
        boton.disabled = true;
    } else {
        cmbCat.style.borderColor = "#ccc";
        $('#errorCategoriaTipo').html('').hide();
        boton.disabled = false;
    }
}

///Función para validar el combobox de modificar Tipo
function validarCmbTipoComp() {
    var esValido = true;
    var boton = document.getElementById("confirmarTipo");
    var cmbCat = document.getElementById("IdCategoriaComp");
    //Validación para el combobox de categorias
    if (cmbCat.value == "") {
        esValido = false;
        cmbCat.style.borderColor = "#900C3F";
        $('#errorCategoriaTipo').html('Debe seleccionar una opción').show();
        setTimeout("$('#errorCategoriaTipo').html('').hide('slow')", 6000);
        boton.disabled = true;
    } else {
        cmbCat.style.borderColor = "#ccc";
        $('#errorCategoriaTipo').html('').hide();
        boton.disabled = false;
    }
    return esValido;
}

function validarInputsVaciosIngreso() {
    var esValido = true;
    var boton = document.getElementById("confirmarTipo");
    var nomTipo = document.getElementById("NombreTipoActivo");
    //Validación para el campo de texto nombre de tipo
    if (nomTipo.value.length <= 0) {
        esValido = false;
        nomTipo.style.borderColor = "#900C3F";
        $('#errorNombreTipo').html('El campo nombre no debe estar vacio').show();
        setTimeout("$('#errorNombreTipo').html('').hide('slow')", 6000);
        boton.disabled = true;
    } else {
        nomTipo.style.borderColor = "#ccc";
        $('#errorNombreTipo').html('').hide();
        boton.disabled = false;
    }
    return esValido;

}
//Función para validar el campo de vida útil
function validarVidaUtil() {
    var esValido = true;
    var boton = document.getElementById("confirmarTipo");
    var vidaTipo = document.getElementById("VidaUtilTipoActivo");
    //Validación para el campo vida útil
    if (vidaTipo.value.length <= 0) {
        esValido = false;
        vidaTipo.style.borderColor = "#900C3F";
        $('#errorVidaTipo').html('El campo vida útil no debe estar vacio').show();
        setTimeout("$('#errorVidaTipo').html('').hide('slow')", 6000);
        boton.disabled = true;
    } else if (vidaTipo.value <1) {
        esValido = false;
        vidaTipo.style.borderColor = "#900C3F";
        vidaTipo.value = "";
        $('#errorVidaTipo').html('La vida útil debe estar en un rango de 1 a 100 años').show();
        setTimeout("$('#errorVidaTipo').html('').hide('slow')", 6000);
        boton.disabled = true;
    } else if (vidaTipo.value > 100) {
        esValido = false;
        vidaTipo.style.borderColor = "#900C3F";
        vidaTipo.value = "";
        $('#errorVidaTipo').html('La vida útil no debe ser mayor a 100').show();
        setTimeout("$('#errorVidaTipo').html('').hide('slow')", 6000);
        boton.disabled = true;
    } else {
        vidaTipo.style.borderColor = "#ccc";
        $('#errorVidaTipo').html('').hide();
        boton.disabled = false;
    }
    return esValido;
}

//Mensajes para los tooltips
function mensajesTooltips() {
    document.getElementById("NombreTipoActivo").title = "Máximo 50 caracteres en Mayúscula.\n No se puede ingresar caracteres especiales ni espacios.";
    document.getElementById("DescripcionTipoActivo").title = "Máximo 50 caracteres en Mayúscula.\n No se puede ingresar caracteres especiales.";
    document.getElementById("VidaUtilTipoActivo").title = "Rango de 1 a 100 años.";
   
}