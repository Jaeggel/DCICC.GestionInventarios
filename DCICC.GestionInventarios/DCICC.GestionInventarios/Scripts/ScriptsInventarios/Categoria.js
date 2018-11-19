var datosCategorias;
var idCategoriaModificar;

function obtenerCategorias(url) {
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
    var str = '<table class="table table-striped jambo_table bulk_action table-responsive table-bordered">';
    str += '<thead> <tr> <th>Nombre Categoría</th> <th>Descripción</th> <th>Estado</th> <th>Modificar</th> <th>Habilitar/<br>Deshabilitar</th> </tr> </thead>';
    str += '<tbody>';
    for (var i = 0; i < datosCategorias.length; i++) {
        var nom = "'" + datosCategorias[i].NombreCategoriaActivo + "'";
        console.log(nom);

        str += '<tr><td>' + datosCategorias[i].NombreCategoriaActivo +
            '</td><td>' + datosCategorias[i].DescripcionCategoriaActivo;

        if (datosCategorias[i].HabilitadoCategoriaActivo) {
            str += '</td><td> Habilitado';
        } else {
            str += '</td><td> Deshabilitado';
        }
        str += '</td><td><div class="text-center"><div class="col-md-12 col-sm-12 col-xs-12">' +
            '<button type="button" class="btn btn-info text-center" data-toggle="modal" data-target="#ModificarCategoria" onclick = "formUpdateCategoria(' + datosCategorias[i].IdCategoriaActivo + ');"> <strong><i class="fa fa-pencil-square-o"></i></strong></button> ' +
            '</div></div>' +
            '</td><td><div class=" text-center"><div class="col-md-12 col-sm-12 col-xs-12">' +
            '<button type = "button" class="btn btn-danger text-center" > <strong><i class="fa fa-times-circle"></i></strong></button> ' +
            '</div></div></td></tr>';
    };
    str += '</tbody></table>';
    $("#tablaModificarCategorias").html(str);
}

//Función para setear los valores en los inputs
function formUpdateCategoria(idCategoria) {
    idCategoriaModificar = idCategoria;
    console.log(idCategoria);
    for (var i = 0; i < datosCategorias.length; i++) {

        if (datosCategorias[i].IdUsuario == idCategoria) {
            //Métodos para setear los valores a modificar
            document.getElementById("NombreCategoriaActivo").value = datosCategorias[i].NombreCategoriaActivo;
            document.getElementById("DescripcionCategoriaActivo").value = datosCategorias[i].DescripcionCategoriaActivo;

            //Método para el check del update de Categorias
            var valor = datosCategorias[i].HabilitadoCategoriaActivo;
            var estado = $('#HabilitadoCategoriaActivo').prop('checked');
            if (estado && valor == false) {
                document.getElementById("HabilitadoCategoriaActivo").click();
            }
            if (estado == false && valor == true) {
                document.getElementById("HabilitadoCategoriaActivo").click();
            }
        };
    };
}

//Función para modificar la categoria especificada
function modificarCategoria(url_modificar) {
    var nombreCategoria=document.getElementById("NombreCategoriaActivo").value;
    var descripcionCategoria=document.getElementById("DescripcionCategoriaActivo").value;
    var habilitadoCategoria = $('#HabilitadoCategoriaActivo').prop('checked');

    //Método ajax para modificar la categoria de la base de datos
    $.ajax({
        data: { "IdCategoriaActivo": idCategoriaModificar, "NombreCategoriaActivo": nombreCategoria, "DescripcionCategoriaActivo": descripcionCategoria, "HabilitadoCategoriaActivo": habilitadoCategoria },
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
    nombre = nombre.toLowerCase();
    var comprobar = false;
    for (var i = 0; i < datosCategorias.length; i++) {
        if (datosCategorias[i].NombreCategoriaActivo == nombre) {
            comprobar = true;
        }
    }

    console.log(comprobar);
    if (comprobar == true) {
        document.getElementById("NombreCategoriaActivo").setCustomValidity("El nombre de la categoria: " + nombre + " ya existe");
    } else {
        document.getElementById("NombreCategoriaActivo").setCustomValidity("");
    }
}