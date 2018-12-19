/*Variables para Activos por estado*/
var datosActivos;
var datosUsuariosHab;
var datosInicioSesion;


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
    $('#numeroOperativos').html(operativos).show('fast');
    $('#numeroNoOperativos').html(noOperativos).show('fast');
    $('#numeroDeBaja').html(deBaja).show('fast');

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
    //console.log(datosUsuariosHab);
    var total = datosUsuariosHab.length;
    //document.getElementById("numeroHabilitados").innerHTML = total;
    $('#numeroHabilitados').html(total).show('fast');
}

/* --------------------------------------SECCIÓN PARA N° DE INICIOS DE SESION---------------------------------*/
//Método ajax para obtener los datos de los activos
function obtenerNumLogs(url) {
    url_metodo = url;
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            if (data.OperacionExitosa) {
                datosInicioSesion = data.ValorLong;
                contarSesiones();
            } else {
                showNotify("Error en la Consulta", 'No se ha podido mostrar los datos: ' + data.MensajeError, "error");
            }
        }
    });
}

function contarSesiones() {
    //console.log(datosUsuariosHab);
    //var total = datosUsuariosHab.length;
    //document.getElementById("sesionesIniciadas").innerHTML = datosInicioSesion;
    $('#sesionesIniciadas').html(datosInicioSesion).show('fast');
}