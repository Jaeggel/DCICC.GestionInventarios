var datosRoles;

function obtenerMetodoRol(url) {
    console.log(url);
    //Método ajax para traer los roles de la base de datos
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            console.log(data);
            datosRoles = data;
            console.log("siiiiii: ");
            cargarRolesCmb();
        }
    });
}

//Función para cargar los elementos en el combobox de Roles de Usuario

/*$('#mySelect').append($('<option>',
    {
        value: i,
        text: "Option " + i
    }));*/

function cargarRolesCmb() {
    console.log("tabla: ");
    var str = '<select class="form-control" name="IdRol">';
    str += '<option value="">Escoga una opción...</option>';
    for (var i = 0; i < datosRoles.length; i++) {
        str += '<option value="' + datosRoles[i].IdRol + '">' + datosRoles[i].NombreRol + '</option>';
    };
    str += '</select>';
    $("#cargarRoles").html(str);
}
