var url_idioma = obtenerIdioma();
var url_metodo;
var datosSO;
var idSOMod;

function obtenerSO(url) {
    url_metodo = url;
    console.log(url);
    //Método ajax para traer los roles de la base de datos
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            console.log(data);
            datosSO = data;
            console.log("siiiiii: ");
            cargarSOTabla();
            $('#dataTableSO').DataTable({
                "language": {
                    "url": url_idioma
                }
            });
        }
    });
}

//Función para cargar la tabla de Usuarios
function cargarSOTabla() {
    var str = '<table id="dataTableSO" class="table jambo_table bulk_action table-bordered" style="width:100%">';
    str += '<thead> <tr> <th>Nombre del Sistema Operativo</th> <th>Descripción</th> <th>Estado</th> <th>Modificar</th> <th>Habilitar/<br>Deshabilitar</th> </tr> </thead>';
    str += '<tbody>';
    for (var i = 0; i < datosSO.length; i++) {
        str += '<tr><td>' + datosSO[i].NombreSistOperativos +
            '</td><td>' + datosSO[i].DescripcionSistOperativos;

        if (datosSO[i].HabilitadoSistOperativos) {
            str += '</td><td> Habilitado';
        } else {
            str += '</td><td> Deshabilitado';
        }
        str += '</td><td><div class="text-center"><div class="col-md-12 col-sm-12 col-xs-12">' +
            '<button type="button" class="btn btn-info text-center" data-toggle="modal" data-target="#ModificarSo" onclick = "formUpdateSO(' + datosSO[i].IdSistOperativos + ');"> <strong><i class="fa fa-pencil-square-o"></i></strong></button> ' +
            '</div></div>' +
            '</td><td><div class=" text-center"><div class="col-md-12 col-sm-12 col-xs-12">' +
            '<button type = "button" class="btn btn-danger text-center" > <strong><i class="fa fa-times-circle"></i></strong></button> ' +
            '</div></div></td></tr>';
    };
    str += '</tbody></table>';
    $("#tablaModificarSistOperativo").html(str);
}

//Función para setear los valores en los inputs
function formUpdateSO(idSO) {
    console.log(idSO);
    idSOMod = idSO;
    for (var i = 0; i < datosSO.length; i++) {

        if (datosSO[i].IdSistOperativos == idSO) {
            //Métodos para setear los valores a modificar
            document.getElementById("NombreSistOperativos").value = datosSO[i].NombreSistOperativos;
            document.getElementById("DescripcionSistOperativos").value = datosSO[i].DescripcionSistOperativos;

            //Método para el check del update de Usuarios
            var valor = datosSO[i].HabilitadoSistOperativos;
            var estado = $('#HabilitadoSistOperativos').prop('checked');
            if (estado && valor == false) {
                document.getElementById("HabilitadoSistOperativos").click();
            }
            if (estado == false && valor == true) {
                document.getElementById("HabilitadoSistOperativos").click();
            }
        };
    };
}

//Función para modificar el laboratorio especificado
function modificarSO(url_modificar) {
    var nombreSO=document.getElementById("NombreSistOperativos").value;
    var descripcionSo=document.getElementById("DescripcionSistOperativos").value;
    var habilitadoSo = $('#HabilitadoSistOperativos').prop('checked');

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
            //Método ajax para modificar la categoria de la base de datos
            $.ajax({
                data: { "IdSistOperativos": idSOMod, "NombreSistOperativos": nombreSO, "DescripcionSistOperativos": descripcionSo, "HabilitadoSistOperativos": habilitadoSo },
                url: url_modificar,
                type: 'post',
                success: function () {
                    $('#ModificarSo').modal('hide');
                    showNotify("Actualización exitosa", 'El Sistema Operativo se ha modificado correctamente', "success");
                    obtenerSO(url_metodo);
                }, error: function () {
                    $('#ModificarSo').modal('hide');
                    showNotify("Error en la Actualización", 'No se ha podido modificar la Marca el Sistema Operativo', "error");
                }
            });
        } else {
            $('#ModificarSo').modal('hide');
        }
    });
}

//Función para evitar nombres de nick repetidos
function comprobarNombre(nombre) {
    nombre = nombre.toLowerCase();
    var comprobar = false;
    for (var i = 0; i < datosSO.length; i++) {
        if ((datosSO[i].NombreSistOperativos).toLowerCase() == nombre) {
            comprobar = true;
        }
    }

    console.log(comprobar);
    if (comprobar == true) {
        document.getElementById("NombreSistOperativos").setCustomValidity("El nombre del sistema operativo: " + nombre + " ya existe");
    } else {
        document.getElementById("NombreSistOperativos").setCustomValidity("");
    }
}