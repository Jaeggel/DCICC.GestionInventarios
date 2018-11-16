var datosSO;

function obtenerMetodoRol(url) {
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
            cargarRolesTabla();
        }
    });
}

//Función para cargar la tabla de Usuarios
function cargarRolesTabla() {
    var str = '<table class="table  table-bordered">';
    str += '<thead> <tr> <th>Nombre del Sistema Operativo</th> <th>Descripción</th> <th>Estado</th> <th>Modificar</th> <th>Deshabilitar</th> </tr> </thead>';
    str += '<tbody>';
    for (var i = 0; i < datosSO.length; i++) {
        var nom = "'" + datosSO[i].NombreSistOperativos + "'";
        console.log(nom);

        str += '<tr><td>' + datosSO[i].NombreSistOperativos +
            '</td><td>' + datosSO[i].DescripcionSistOperativos;

        if (datosSO[i].HabilitadoSistOperativos) {
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
    $("#tablaModificarSistOperativos").html(str);
}