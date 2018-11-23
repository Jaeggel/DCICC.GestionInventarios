var url_idioma = obtenerIdioma();
var url_metodo;
var datosTipoAccesorio;
var idTipoAccesorio;
var urlEstado;

//Método ajax para obtener los datos de Tipo Accesorio
function obtenerTipoAccesorio(url) {
    url_metodo = url;
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            datosTipoAccesorio = data;
            console.log("siiiiii: ");
            cargarTipoAccTabla();
            $('#dataTableTipoAcc').DataTable({
                "language": {
                    "url": url_idioma
                }
            });
        }
    });
}

//Función para obtener la url de modificación
function urlEstados(url) {
    urlEstado = url;
}

//Función para cargar la tabla de Tipos de Accesorio
function cargarTipoAccTabla() {
    var str = '<table id="dataTableTipoAcc" class="table jambo_table bulk_action table-bordered" style="width:100%">';
    str += '<thead> <tr> <th>Nombre del Tipo de Accesorio</th> <th>Descripción</th> <th>Estado</th> <th>Modificar</th> <th>Habilitar/<br>Deshabilitar</th> </tr> </thead>';
    str += '<tbody>';
    for (var i = 0; i < datosTipoAccesorio.length; i++) {
        str += '<tr><td>' + datosTipoAccesorio[i].NombreTipoAccesorio +
            '</td><td>' + datosTipoAccesorio[i].DescripcionTipoAccesorio;

        if (datosTipoAccesorio[i].HabilitadoTipoAccesorio) {
            str += '</td><td> Habilitado';
        } else {
            str += '</td><td> Deshabilitado';
        }
        str += '</td><td><div class="text-center"><div class="col-md-12 col-sm-12 col-xs-12">' +
            '<button type="button" class="btn btn-info text-center" data-toggle="modal" data-target="#ModificarTipoAcc" onclick = "formUpdateTipoAcc(' + datosTipoAccesorio[i].IdTipoAccesorio + ');"> <strong><i class="fa fa-pencil-square-o"></i></strong></button> ' +
            '</div></div>' +
            '</td><td><div class=" text-center"><div class="col-md-12 col-sm-12 col-xs-12">';
        if (datosTipoAccesorio[i].HabilitadoTipoAccesorio) {
            str += '<button type="button" class="btn btn-success text-center" onclick = "habilitarOdeshabilitar(' + datosTipoAccesorio[i].IdTipoAccesorio + ',' + datosTipoAccesorio[i].HabilitadoTipoAccesorio +');"> <strong><span class="fa fa-toggle-on"></span></strong></button>';
        } else {
            str += '<button type="button" class="btn btn-danger text-center" onclick = "habilitarOdeshabilitar(' + datosTipoAccesorio[i].IdTipoAccesorio + ',' + datosTipoAccesorio[i].HabilitadoTipoAccesorio +');"> <strong><i class="fa fa-toggle-off"></i></strong></button>';
        }    
        str +='</div></div></td></tr>';

    };
    str += '</tbody></table>';
    $("#tablaModificarTipoAccesorio").html(str);
}

//Función para setear los valores en los inputs
function formUpdateTipoAcc(idTipo) {
    console.log(idTipo);
    idTipoAccesorio = idTipo;
    for (var i = 0; i < datosTipoAccesorio.length; i++) {

        if (datosTipoAccesorio[i].IdTipoAccesorio == idTipo) {
            //Métodos para setear los valores a modificar
            document.getElementById("NombreTipoAccesorio").value = datosTipoAccesorio[i].NombreTipoAccesorio;
            document.getElementById("DescripcionTipoAccesorio").value = datosTipoAccesorio[i].DescripcionTipoAccesorio;

            //Método para el check del update de Tipo Accesorio
            var valor = datosTipoAccesorio[i].HabilitadoTipoAccesorio;
            var estado = $('#HabilitadoTipoAccesorio').prop('checked');
            if (estado && valor == false) {
                document.getElementById("HabilitadoTipoAccesorio").click();
            }
            if (estado == false && valor == true) {
                document.getElementById("HabilitadoTipoAccesorio").click();
            }
        };
    };
}

//Función para modificar el Tipo Accesorio especificado
function modificarTipoAcc(url_modificar) {
    var nombreTipo=document.getElementById("NombreTipoAccesorio").value;
    var descripcionTipo=document.getElementById("DescripcionTipoAccesorio").value;
    var habilitadoTipo = $('#HabilitadoTipoAccesorio').prop('checked');
    
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
                data: { "IdTipoAccesorio": idTipoAccesorio, "NombreTipoAccesorio": nombreTipo, "DescripcionTipoAccesorio": descripcionTipo, "HabilitadoTipoAccesorio": habilitadoTipo },
                url: url_modificar,
                type: 'post',
                success: function () {
                    $('#ModificarTipoAcc').modal('hide');
                    showNotify("Actualización exitosa", 'El Tipo de Accesorio se ha modificado correctamente', "success");
                    obtenerTipoAccesorio(url_metodo);
                }, error: function () {
                    $('#ModificarTipoAcc').modal('hide');
                    showNotify("Error en la Actualización", 'No se ha podido modificar el Tipo de Accesorio', "error");
                }
            });
        } else {
            $('#ModificarTipoAcc').modal('hide');
        }
    });
}

//Función para evitar nombres de Tipo Accesorio repetidos
function comprobarNombre(nombre) {
    nombre = nombre.toLowerCase();
    var comprobar = false;
    for (var i = 0; i < datosTipoAccesorio.length; i++) {
        if ((datosTipoAccesorio[i].NombreTipoAccesorio).toLowerCase() == nombre) {
            comprobar = true;
        }
    }

    console.log(comprobar);
    if (comprobar == true) {
        document.getElementById("NombreTipoAccesorio").setCustomValidity("El nombre del tipo accesorio: " + nombre + " ya existe");
    } else {
        document.getElementById("NombreTipoAccesorio").setCustomValidity("");
    }
}

//Función para habilitar o deshabilitar la categoria
function habilitarOdeshabilitar(idTipoAcc, estadoTipoAcc) {
    var nuevoEstado = true;
    if (estadoTipoAcc) {
        nuevoEstado = false;
    } else {
        nuevoEstado = true;
    }
    console.log(nuevoEstado);
    swal({
        title: 'Confirmación de Cambio de Estado',
        text: "¿Está seguro de Cambiar de Estado el Tipo de Accesorio?",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#26B99A',
        cancelButtonColor: '#337ab7',
        confirmButtonText: 'Confirmar',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                data: { "IdTipoAccesorio": idTipoAcc, "HabilitadoTipoAccesorio": nuevoEstado },
                url: urlEstado,
                type: 'post',
                success: function () {           
                    obtenerTipoAccesorio(url_metodo);
                }, error: function () {

                }
            });
        } else {

        }
    });
}