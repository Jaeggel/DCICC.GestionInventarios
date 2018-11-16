var datosCategorias;

function obtenerMetodoRol(url) {
    console.log(url);
    //Método ajax para traer los roles de la base de datos
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            console.log(data);
            datosCategorias = data;
            console.log("siiiiii: ");
            cargarCategoriaTabla();
        }
    });
}

//Función para cargar la tabla de Categorias
function cargarCategoriaTabla() {
    var str = '<table class="table  table-bordered">';
    str += '<thead> <tr> <th>Nombre Categoría</th> <th>Descripción</th> <th>Estado</th> <th>Modificar</th> <th>Deshabilitar</th> </tr> </thead>';
    str += '<tbody>';
    for (var i = 0; i < datosCategorias.length; i++) {
        var nom = "'" + datosCategorias[i].NombreCategoriaActivo + "'";
        console.log(nom);

        str += '<tr><td>' + datosCategorias[i].NombreCategoriaActivo +
            '</td><td>' + datosCategorias[i].DescripcionCategoriaActivo;

        if (datosCategorias[i].HabilitadoCategoriaActivoo) {
            str += '</td><td> Habilitado';
        } else {
            str += '</td><td> Deshabilitado';
        }

        str += '</td><td><button type="button" class="btn btn-info " ><strong><i class="fa fa-pencil-square-o"></i></strong></button>' +
            '</td><td><button type="button" class="btn btn-danger " ><strong><i class="fa fa-times-circle"></i></strong></button>' +
            '</td></tr>';
    };
    str += '</tbody></table>';
    $("#tablaModificarCategorias").html(str);
}