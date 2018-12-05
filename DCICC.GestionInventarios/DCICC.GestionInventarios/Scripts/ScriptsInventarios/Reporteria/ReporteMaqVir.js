var url_idioma = obtenerIdioma();
var url_metodo;
var propositos;
var datosMaquinasV;
var cmbSO;

//Método ajax para obtener los datos de Máquinas virtuales
function obtenerMaquinaV(url) {
    url_metodo = url;
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            datosMaquinasV = data;
            cargarMaquinaVTabla();
            $('#dataTableMaquinaV').DataTable({
                "language": {
                    "url": url_idioma
                }
            });
            cargarNombresMV();
        }
    });
}

//Método ajax para obtener los datos de Sistemas operativos
function obtenerSO(url) {
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            console.log("siiii");
            cmbSO = data;
            cargarSOCmb();
        }
    });
}

//Método ajax para obtener los datos de propósitos
function listaPropositos(url) {
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'get',
        success: function (data) {
            console.log("entrooo");
            propositos = data;
            cargarPropositosCmb();

        }, error: function (e) {
            console.log(e);
        }
    });
}

//Función para cargar el combobox de Sistemas Operativos
function cargarSOCmb() {
    var str = '<select id="IdSistOperativos" class="form-control" name="IdSistOperativos" required>';
    str += '<option value="">Escoga una opción...</option>';
    for (var i = 0; i < cmbSO.length; i++) {
        str += '<option value="' + cmbSO[i].IdSistOperativos + '">' + cmbSO[i].NombreSistOperativos + '</option>';
    };
    str += '</select>';
    $("#cargarSO").html(str);
}

//Función para cargar el combobox de Propósitos
function cargarPropositosCmb() {
    var str = '<select id="PropositoMaqVirtuales" class="form-control" name="PropositoMaqVirtuales" onBlur="validarCmbMV();" required>';
    str += '<option value="">Escoga una opción...</option>';
    for (var i = 0; i < propositos.length; i++) {
        str += '<option value="' + propositos[i].NombreProposito + '">' + propositos[i].NombreProposito + '</option>';
        nombresPropositos[i] = propositos[i].NombreProposito;
    };
    str += '</select>';
    $("#cargarPropositos").html(str);
}

//Función para cargar la tabla de Máquinas Virtuales
function cargarMaquinaVTabla() {
    var str = '<table id="dataTableMaquinaV" class="table jambo_table bulk_action  table-bordered" style="width:100%">';
    str += '<thead> <tr> <th>Nombre Máquina Virtual</th> <th>Usuario/Encargado</th> <th>Propósito</th> <th>Sistema Operativo</th> <th>Dirección IP</th> <th>Tamaño en Disco</th> <th>Memoria RAM</th> <th>Descripción</th> <th>Estado</th> <th>Modificar</th> <th>Habilitar/<br>Deshabilitar</th> </tr> </thead>';
    str += '<tbody>';
    for (var i = 0; i < datosMaquinasV.length; i++) {

        str += '<tr><td>' + datosMaquinasV[i].NombreMaqVirtuales +
            '</td><td>' + datosMaquinasV[i].UsuarioMaqVirtuales +
            '</td><td>' + datosMaquinasV[i].PropositoMaqVirtuales +
            '</td><td>' + datosMaquinasV[i].NombreSistOperativos +
            '</td><td>' + datosMaquinasV[i].DireccionIPMaqVirtuales +
            '</td><td>' + datosMaquinasV[i].DiscoMaqVirtuales +
            '</td><td>' + datosMaquinasV[i].RamMaqVirtuales +
            '</td><td>' + datosMaquinasV[i].DescripcionMaqVirtuales;
        str += '</td></tr>';

    };
    str += '</tbody></table>';
    $("#tablaReportesMaqVirtuales").html(str);
}