var datosActivos = [];
var datosVidaUtil;
/*Variables para Activos por estado*/
var nombresTipoActivo = [];
var valorNombresTipoActivo = [];
/*valores para Tickets*/
var estadosTickets = listaEstadosTicket();
var valoresTickets = [];


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
                valoresTickets=[data.ObjetoInventarios.TicketsAbiertosCont, data.ObjetoInventarios.TicketsEnProcesoCont, data.ObjetoInventarios.TicketsEnEsperaCont, data.ObjetoInventarios.TicketsResueltosCont];                
                graficaTickets();
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

/* --------------------------------------SECCIÓN PARA GRÁFICOS DE ACTIVOS X TIPO---------------------------------*/
//Método ajax para obtener los datos de tipo de activo
function obtenerTipoActivo(url) {
    url_metodo = url;
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            if (data.OperacionExitosa) {
                var datosTipoActivo = data.ListaObjetoInventarios;
                for (var i = 0; i < data.ListaObjetoInventarios.length; i++) {
                    nombresTipoActivo[i] = datosTipoActivo[i].NombreTipoActivo;
                }
                //console.log(nombresTipoActivo);
            } else {
                showNotify("Error en la Consulta", 'No se ha podido mostrar los datos: ' + data.MensajeError, "error");
            }

        }
    });
}

//Método ajax para obtener los datos de los activos
function obtenerActivos(url) {
    url_metodo = url;
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            if (data.OperacionExitosa) {
                var aux = data.ListaObjetoInventarios;
                for (var i = 0; i < aux.length; i++) {
                    datosActivos[i] = aux[i].NombreTipoActivo;
                }
                contarActivosTipo();
            } else {
                showNotify("Error en la Consulta", 'No se ha podido mostrar los datos: ' + data.MensajeError, "error");
            }
        }
    });
}

function contarActivosTipo() {
    //for (var i = 0; i < datosActivos.length; i++) {
        
    for (var j = 0; j < nombresTipoActivo.length; j++) {
        var ocurrencia = $.grep(datosActivos, function (element) {
            return element === nombresTipoActivo[j];
        }).length;
        valorNombresTipoActivo[j] = ocurrencia;
        }
    //console.log(valorNombresTipoActivo);
    graficaActivos();
}

////////funcion para graficar
function graficaActivos() {

    $('#graficoActivos').highcharts({
            title: {
                style: {
                    fontFamily: 'Helvetica Neue,Roboto,Arial,Droid Sans,sans-serif',
                    color:'#73879C'
                },
                text: 'N° de Activos de TI por Tipo de Activo'
            },

            subtitle: {
                style: {
                    fontFamily: 'Helvetica Neue,Roboto,Arial,Droid Sans,sans-serif',
                    color: '#73879C'
                },
                text: 'Sistema de Gestión de Activos y Ticketing para Soporte Técnico'
            },
            yAxis: {
                title: {
                    text: 'N° de Activos'
                }
            },
            xAxis: {
                title: {
                    text: 'Tipos de Activos'
                },
                categories: nombresTipoActivo
            },

            series: [{
                type: 'column',
                colorByPoint: true,
                data: valorNombresTipoActivo,
                showInLegend: false
            }],
            credits: false
        });
    
}

function graficaTickets() {
    var completo = [];
    for (var i = 0; i < estadosTickets.length; i++) {      
        if (i != 0) {
            completo.push(new Array(estadosTickets[i], valoresTickets[i], false));           
        } else {
            completo.push(new Array(estadosTickets[i], valoresTickets[i], true, true));
        }
    }
    console.log(completo);
    $('#graficoTickets').highcharts({
        chart: {
            styledMode: true
        },

        title: {
            style: {
                fontFamily: 'Helvetica Neue,Roboto,Arial,Droid Sans,sans-serif',
                color: '#73879C'
            },
            text: 'N° de Tickets para Soporte Técnico'
        },
        subtitle: {
            style: {
                fontFamily: 'Helvetica Neue,Roboto,Arial,Droid Sans,sans-serif',
                color: '#73879C'
            },
            text: 'Sistema de Gestión de Activos y Ticketing para Soporte Técnico'
        },
        series: [{
            type: 'pie',
            allowPointSelect: true,
            keys: ['name', 'y', 'selected', 'sliced'],
            colors: ['#7cb5ec', '#f7a35c', '#FA413A','#1ABB9C'],
            data: completo,
            showInLegend: true
        }],
        credits: false
    });

}