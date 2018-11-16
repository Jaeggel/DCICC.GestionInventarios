var datosLaboratorios;

function obtenerMetodoRol(url) {
    console.log(url);
    //Método ajax para traer los roles de la base de datos
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            console.log(data);
            datosLaboratorios = data;
            console.log("siiiiii: ");
            cargarLaboratoriosTabla();
        }
    });
}

//Función para cargar la tabla de Usuarios
function cargarLaboratoriosTabla() {
    var str = '<table class="table  table-bordered">';
    str += '<thead> <tr> <th>Nombre de Laboratorio</th> <th>Ubicación</th> <th>Descripción</th> <th>Estado</th> <th>Modificar</th> <th>Deshabilitar</th> </tr> </thead>';
    str += '<tbody>';
    for (var i = 0; i < datosLaboratorios.length; i++) {
        var nom = "'" + datosLaboratorios[i].NombreLaboratorio+ "'";
        console.log(nom);

        str += '<tr><td>' + datosLaboratorios[i].NombreLaboratorio +
            '</td><td>' + datosLaboratorios[i].UbicacionLaboratorio +
            '</td><td>' + datosLaboratorios[i].DescripcionLaboratorio;

        if (datosLaboratorios[i].HabilitadoLaboratorio) {
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
    $("#tablaModificarLaboratorios").html(str);
}