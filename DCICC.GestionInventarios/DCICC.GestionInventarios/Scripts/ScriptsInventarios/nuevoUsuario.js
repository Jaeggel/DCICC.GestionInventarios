var datosRoles;
$(document).ready(function () {

    //Método ajax para traer los roles de la base de datos
    $.ajax({
        dataType: 'json',
        url: '../iglesia_controler/consultar_Min',
        type: 'post',
        success: function (data) {
            datosRoles = data;
            console.log("siiiiii: ");
            cargarRolesCmb();
        }
    });


});

//Función para cargar los elementos en el combobox de Roles de Usuario
function cargarRolesCmb() {
    console.log("tabla: ");
    var str = '';
    for (var i = 0; i < datosRoles.length; i++) {
        str += '<option value="' + datosRoles[i].id_rol+'">' + datosRoles[i].nombre_rol +'</option>';
    };
    $("#cargarRoles").html(str);
}
