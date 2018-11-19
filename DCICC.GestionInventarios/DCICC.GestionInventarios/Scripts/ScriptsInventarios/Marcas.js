var datosMarcas;
var idMarcaModificar;

function obtenerMarcas(url) {
    console.log(url);
    //Método ajax para traer las marcas de la base de datos
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            console.log(data);
            datosMarcas = data;
            cargarLaboratoriosTabla();
        }
    });
}

//Función para cargar la tabla de las Marcas
function cargarLaboratoriosTabla() {
    var str = '<table class="table table-striped jambo_table bulk_action table-responsive table-bordered">';
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
    console.log(idUsuario);
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
    var nombreMarca=document.getElementById("NombreMarca").value;
    var descripcionMarca=document.getElementById("DescripcionMarca").value;
    var habilitadoMarca = $('#HabilitadoMarca').prop('checked');

    //Método ajax para modificar el usuario de la base de datos
    $.ajax({
        data: { "IdMarca": idMarcaModificar, "NombreMarca": nombreMarca, "DescripcionMarca": descripcionMarca, "HabilitadoMarca": habilitadoMarca },
        dataType: 'json',
        url: url_modificar,
        type: 'post',
        success: function (data) {
            console.log(data);
            console.log("siiiiii: ");
        }
    });
}

//Función para evitar nombres de nick repetidos
function comprobarNombre(nombre) {
    nombre = nick.toLowerCase();
    var comprobar = false;
    for (var i = 0; i < datosMarcas.length; i++) {
        if (datosMarcas[i].NombreMarca == nombre) {
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