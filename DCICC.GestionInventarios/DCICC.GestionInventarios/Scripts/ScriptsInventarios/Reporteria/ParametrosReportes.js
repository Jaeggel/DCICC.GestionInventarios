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
    var str;
    if (datos.ImagenPNG) {
        str = '<img src="/Inventarios/Content/Images/LogoReporte/LogoNuevo.png" style="text-align:center;width: 100%;">';
    } else if (datos.ImagenJPG) {
        str = '<img src="/Inventarios/Content/Images/LogoReporte/LogoNuevo.jpg" style="text-align:center;width: 100%;">';
    } else {
        str = '<img src="/Inventarios/Content/Images/LogoReporte/LogoUPS.png" style="text-align:center;width: 100%;">';
    }
    $("#ImagenLogo").html(str);
}