var url_idioma = obtenerIdioma();
var url_metodo;
var datosMarcas;
var idMarcaModificar;

function obtenerMarcas(url) {
    url_metodo = url;
    console.log(url);
    //Método ajax para traer las marcas de la base de datos
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            console.log(data);
            datosMarcas = data;
            cargarMarcasTabla();
            $('#dataTableMarcas').DataTable({
                "language": {
                    "url": url_idioma
                }
            });
        }
    });
}

//Función para cargar la tabla de las Marcas
function cargarMarcasTabla() {
    var str = '<table id="dataTableMarcas" class="table jambo_table bulk_action table-bordered" style="width:100%">';
    str += '<thead> <tr> <th>Nombre de la Marca</th> <th>Descripción</th> <th>Estado</th> <th>Modificar</th> <th>Habilitar/<br>Deshabilitar</th> </tr> </thead>';
    str += '<tbody>';
    for (var i = 0; i < datosMarcas.length; i++) {
        var nom = "'" + datosMarcas[i].NombreMarca + "'";
        console.log(nom);

        str += '<tr><td>' + datosMarcas[i].NombreMarca +
            '</td><td>' + datosMarcas[i].DescripcionMarca;

        if (datosMarcas[i].HabilitadoMarca) {
            str += '</td><td> Habilitado';
        } else {
            str += '</td><td> Deshabilitado';
        }
        str += '</td><td><div class="text-center"><div class="col-md-12 col-sm-12 col-xs-12">' +
            '<button type="button" class="btn btn-info text-center" data-toggle="modal" data-target="#ModificarMarca" onclick = "formUpdateMarca(' + datosMarcas[i].IdMarca + ');"> <strong><i class="fa fa-pencil-square-o"></i></strong></button> ' +
            '</div></div>' +
            '</td><td><div class=" text-center"><div class="col-md-12 col-sm-12 col-xs-12">' +
            '<button type = "button" class="btn btn-danger text-center" > <strong><i class="fa fa-times-circle"></i></strong></button> ' +
            '</div></div></td></tr>';

    };
    str += '</tbody></table>';
    $("#tablaModificarMarca").html(str);
}

//Función para setear los valores en los inputs
function formUpdateMarca(idMarca) {
    console.log(idMarca);
    idMarcaModificar = idMarca;
    for (var i = 0; i < datosMarcas.length; i++) {

        if (datosMarcas[i].IdMarca == idMarca) {
            //Métodos para setear los valores a modificar
            document.getElementById("NombreMarca").value = datosMarcas[i].NombreMarca;
            document.getElementById("DescripcionMarca").value = datosMarcas[i].DescripcionMarca;

            //Método para el check del update de Usuarios
            var valor = datosMarcas[i].HabilitadoMarca;
            var estado = $('#HabilitadoMarca').prop('checked');
            if (estado && valor == false) {
                document.getElementById("HabilitadoMarca").click();
            }
            if (estado == false && valor == true) {
                document.getElementById("HabilitadoMarca").click();
            }
        };
    };
}

//Función para modificar el laboratorio especificado
function modificarMarca(url_modificar) {
    console.log(url_modificar);
    var nombreMarca=document.getElementById("NombreMarca").value;
    var descripcionMarca=document.getElementById("DescripcionMarca").value;
    var habilitadoMarca = $('#HabilitadoMarca').prop('checked');

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
                data: { "IdMarca": idMarcaModificar, "NombreMarca": nombreMarca, "DescripcionMarca": descripcionMarca, "HabilitadoMarca": habilitadoMarca },
                url: url_modificar,
                type: 'post',
                success: function () {
                    $('#ModificarMarca').modal('hide');
                    showNotify("Actualización exitosa", 'La Marca se ha modificado correctamente', "success");
                    obtenerMarcas(url_metodo);
                }, error: function () {
                    $('#ModificarMarca').modal('hide');
                    showNotify("Error en la Actualización", 'No se ha podido modificar la Marca', "error");
                }
            });
        } else {
            $('#ModificarMarca').modal('hide');
        }
    });
}

//Función para evitar nombres de nick repetidos
function comprobarNombre(nombre) {
    nombre = nick.toLowerCase();
    var comprobar = false;
    for (var i = 0; i < datosMarcas.length; i++) {
        if ((datosMarcas[i].NombreMarca).toLowerCase() == nombre) {
            comprobar = true;
        }
    }

    console.log(comprobar);
    if (comprobar == true) {
        document.getElementById("NombreMarca").setCustomValidity("El nombre de la marca: " + nombre + " ya existe");
    } else {
        document.getElementById("NombreMarca").setCustomValidity("");
    }
}