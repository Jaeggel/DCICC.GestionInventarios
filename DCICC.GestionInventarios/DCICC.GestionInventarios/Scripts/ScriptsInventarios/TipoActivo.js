var url_idioma = obtenerIdioma();
var url_metodo;
var datosTipoActivo;
var cmbCategorias;
var cmbCategoriasComp;
var idTipoActivo;

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
            str += '<button type="button" class="btn btn-success text-center" > <strong><span class="fa fa-toggle-on"></span></strong></button>';
        } else {
            str += '<button type="button" class="btn btn-danger text-center" > <strong><i class="fa fa-toggle-off"></i></strong></button>';
        }
        str +='</div></div></td></tr>';
        
    };
    str += '</tbody></table>';
    $("#tablaModificarTipoActivo").html(str);
}

//Función para cargar el combobox de Categorias
function cargarCategoriasCmb() {
    var str = '<select id="IdCategoriaActivo" class="form-control" name="IdCategoriaActivo" required>';
    str += '<option value="">Escoga una opción...</option>';
    for (var i = 0; i < cmbCategorias.length; i++) {
        str += '<option value="' + cmbCategorias[i].IdCategoriaActivo + '">' + cmbCategorias[i].NombreCategoriaActivo + '</option>';
    };
    str += '</select>';
    $("#cargarCategorias").html(str);
}

//Función para cargar el combobox de Categorias Completas
function cargarCategoriasCompCmb() {
    var str = '<select id="IdCategoriaComp" class="form-control" name="IdCategoriaComp" required>';
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
    var cmbCategoria = document.getElementById("IdCategoriaActivo");
    var idCategoria = cmbCategoria.options[cmbCategoria.selectedIndex].value;
    var nombreTipo=document.getElementById("NombreTipoActivo").value;
    var descripcionTipo=document.getElementById("DescripcionTipoActivo").value;
    var vidaUtil=document.getElementById("VidaUtilTipoActivo").value;
    var habilitadoTipo = $('#HabilitadoTipoActivo').prop('checked');

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
                success: function () {
                    $('#ModificarTipoActivo').modal('hide');
                    showNotify("Actualización exitosa", 'El Tipo de Activo se ha modificado correctamente', "success");
                    obtenerTipoActivo(url_metodo);
                }, error: function () {
                    $('#ModificarTipoActivo').modal('hide');
                    showNotify("Error en la Actualización", 'No se ha podido modificar el Tipo de Activo', "error");
                }
            });

        } else {
            $('#ModificarTipoActivo').modal('hide');
        }
    });
}

//Función para evitar nombres de tipo activo repetidos
function comprobarNombre(nombre) {
    nombre = nombre.toLowerCase();
    var comprobar = false;
    for (var i = 0; i < datosTipoActivo.length; i++) {
        if ((datosTipoActivo[i].NombreTipoActivo).toLowerCase() == nombre) {
            comprobar = true;
        }
    }

    console.log(comprobar);
    if (comprobar == true) {
        document.getElementById("NombreTipoActivo").setCustomValidity("El nombre del Tipo Activo: " + nombre + " ya existe");
    } else {
        document.getElementById("NombreTipoActivo").setCustomValidity("");
    }
}


