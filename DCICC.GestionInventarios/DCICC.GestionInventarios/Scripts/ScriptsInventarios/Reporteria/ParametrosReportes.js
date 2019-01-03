function obtenerParametros(url) {
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {           
            cargarDatosParametros(data);
        }
    });
}
//Función para cargar los datos
function cargarDatosParametros(datos) {
    document.getElementById("TituloCarrera").value = datos.TituloCarrera;
    document.getElementById("TituloSistema").value = datos.TituloSistema;
    document.getElementById("TituloReporte").value = datos.TituloReporte;
    document.getElementById("TituloSedeCampus").value = datos.TituloSedeCampus;
}