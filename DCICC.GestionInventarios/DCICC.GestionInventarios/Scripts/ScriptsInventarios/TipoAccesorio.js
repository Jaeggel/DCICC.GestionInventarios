var datosTipoAccesorio;

function obtenerMetodoRol(url) {
    console.log(url);
    //Método ajax para traer los roles de la base de datos
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            console.log(data);
            datosTipoAccesorio = data;
            console.log("siiiiii: ");
            cargarLaboratoriosTabla();
        }
    });
}

//Función para cargar la tabla de Usuarios
function cargarLaboratoriosTabla() {
    var str = '<table class="table  table-bordered">';
    str += '<thead> <tr> <th>Nombre del Tipo de Accesorio</th> <th>Descripción</th> <th>Estado</th> <th>Modificar</th> <th>Deshabilitar</th> </tr> </thead>';
    str += '<tbody>';
    for (var i = 0; i < datosTipoAccesorio.length; i++) {
        var nom = "'" + datosTipoAccesorio[i].NombreTipoAccesorio + "'";
        console.log(nom);

        str += '<tr><td>' + datosTipoAccesorio[i].NombreTipoAccesorio +
            '</td><td>' + datosTipoAccesorios[i].DescripcionTipoAccesorio;

        if (datosTipoAccesorio[i].HabilitadoTipoAccesorio) {
            str += '</td><td> Habilitado';
        } else {
            str += '</td><td> Deshabilitado';
        }

        //console.log(nom);
        str += '</td><td><button type="button" class="btn btn-info " ><strong><i class="fa fa-pencil-square-o"></i></strong></button>' +
            '</td><td><button type="button" class="btn btn-danger " ><strong><i class="fa fa-times-circle"></i></strong></button>' +
            '</td></tr>';

    };
    str += '</tbody></table>';
    $("#tablaModificarTipoAccesorio").html(str);
}