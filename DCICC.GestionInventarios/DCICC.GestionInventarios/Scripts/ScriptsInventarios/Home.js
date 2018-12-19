var permisosRol;
function definirMenuPorRolActual(url) {
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            definicionMenu(data.ObjetoInventarios);
        }
    });
}
function definicionMenu(permisos) {
    //$('#menuActivos').remove();
    //$('#menuConsultaActivos').remove();
    //$('#menuReportes').remove();
    //$('#menuMaqVirtuales').remove();
    //$('#menuTickets').remove();
    if (!permisos.PermisoActivos) {
        $('#menuActivos').remove();
        $('#menuConsultaActivos').remove();
    }
    if (!permisos.PermisoMaqVirtuales) {
        $('#menuMaqVirtuales').remove();
    }
    if (!permisos.PermisoTickets) {
        $('#menuTickets').remove();
    }
    if (!permisos.PermisoReportes) {
        $('#menuReportes').remove();
    }
}