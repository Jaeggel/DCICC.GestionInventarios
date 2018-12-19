/*Variables para Activos por estado*/
var datosActivos;
var datosUsuariosHab;


/***********************************************************************************
 *                SECCIÓN PARA MENÚ INFORMATIVO GENERAL
 ***********************************************************************************/

/* --------------------------------------SECCIÓN PARA ACTIVOS DE TI---------------------------------*/
//Método ajax para obtener los datos de los activos
function obtenerActivos(url) {
    url_metodo = url;
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            if (data.OperacionExitosa) {
                datosActivos = data.ListaObjetoInventarios;
                valoresActivos();
            } else {
                showNotify("Error en la Consulta", 'No se ha podido mostrar los datos: ' + data.MensajeError, "error");
            }
        }
    });
}

function valoresActivos() {
    var operativos=0;
    var noOperativos = 0;
    var deBaja = 0;
    for (var i = 0; i < datosActivos.length; i++) {
        if (datosActivos[i].EstadoActivo == "OPERATIVO") {
            operativos = operativos+1;
        }
        if (datosActivos[i].EstadoActivo == "NO OPERATIVO") {
            noOperativos = noOperativos+ 1;
        }
        if (datosActivos[i].EstadoActivo == "DE BAJA"){
            deBaja = deBaja+ 1;
        }
    }
    $('#numeroOperativos').html(operativos).show();
    $('#numeroNoOperativos').html(noOperativos).show();
    $('#numeroDeBaja').html(deBaja).show();

}

/* --------------------------------------SECCIÓN PARA USUARIOS HABILITADOS---------------------------------*/
//Método ajax para obtener los datos de los activos
function obtenerUsuariosHab(url) {
    url_metodo = url;
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            if (data.OperacionExitosa) {
                datosUsuariosHab = data.ListaObjetoInventarios;
                contarUsuarios();
            } else {
                showNotify("Error en la Consulta", 'No se ha podido mostrar los datos: ' + data.MensajeError, "error");
            }
        }
    });
}

function contarUsuarios() {
    console.log(datosUsuariosHab);
    var total = (datosUsuariosHab.length) - 1;
    $('#numeroHabilitados').html(total).show();
}