var datosActivos;
var datosVidaUtil;
/*Variables para Activos por estado*/


/***********************************************************************************
 *                SECCIÓN PARA MENÚ INFORMATIVO GENERAL
 ***********************************************************************************/

/* --------------------------------------SECCIÓN PARA ACTIVOS DE TI---------------------------------*/
//Método ajax para obtener los datos de los activos
function obtenerValores(url) {
    url_metodo = url;
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            if (data.OperacionExitosa) {
                $('#numeroOperativos').html(data.ObjetoInventarios.ActivosOperativosCont).show('fast');
                $('#numeroNoOperativos').html(data.ObjetoInventarios.ActivosNoOperativosCont).show('fast');
                $('#numeroDeBaja').html(data.ObjetoInventarios.ActivosDeBajaCont).show('fast');
                $('#numeroHabilitados').html(data.ObjetoInventarios.UsuariosHabilitadosCont).show('fast');
                $('#sesionesIniciadas').html(data.ObjetoInventarios.SesionCont).show('fast');
            } else {
                showNotify("Error en la Consulta", 'No se ha podido mostrar los datos: ' + data.MensajeError, "error");
            }
        }
    });
}

/* --------------------------------------SECCIÓN PARA N° DE ACTIVOS VIDA UTIL---------------------------------*/
//Método ajax para obtener los datos de los activos
function obtenerVidaUtil(url) {
    url_metodo = url;
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            if (data.OperacionExitosa) {
                datosVidaUtil = data.ListaObjetoInventarios;
                contarVidaUtil();
            } else {
                showNotify("Error en la Consulta", 'No se ha podido mostrar los datos: ' + data.MensajeError, "error");
            }
        }
    });
}

function contarVidaUtil() {
    //console.log(datosUsuariosHab);
    var total = datosVidaUtil.length;
    //document.getElementById("numeroHabilitados").innerHTML = total;
    $('#numeroVidaUtil').html(total).show('fast');
}