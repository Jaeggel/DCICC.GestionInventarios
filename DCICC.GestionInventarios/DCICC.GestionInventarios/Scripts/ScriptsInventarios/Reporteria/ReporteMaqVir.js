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
    str += '<option value="">Mostrar Todos</option>';
    for (var i = 0; i < cmbSO.length; i++) {
        str += '<option value="' + cmbSO[i].IdSistOperativos + '">' + cmbSO[i].NombreSistOperativos + '</option>';
    };
    str += '</select>';
    $("#cargarSO").html(str);
    ///////CAMBIO DEL COMBOBOX
    $('#IdSistOperativos').change(function () {
        var opcion = document.getElementById("IdSistOperativos");
        var tipoSO = opcion.options[opcion.selectedIndex];
        if (tipoSO.value == "") {
            $('#dataTableMaquinaV').DataTable().column(3).search(
                ""
            ).draw(); 
        } else {
            $('#dataTableMaquinaV').DataTable().column(3).search(
                tipoSO.text
            ).draw();            
        }
    });
}

//Función para cargar el combobox de Propósitos
function cargarPropositosCmb() {
    var str = '<select id="PropositoMaqVirtuales" class="form-control" name="PropositoMaqVirtuales">';
    str += '<option value="">Mostrar Todos</option>';
    for (var i = 0; i < propositos.length; i++) {
        str += '<option value="' + propositos[i].NombreProposito + '">' + propositos[i].NombreProposito + '</option>';        
    };
    str += '</select>';
    $("#cargarPropositos").html(str);
    ///////CAMBIO DEL COMBOBOX
    $('#PropositoMaqVirtuales').change(function () {
        var opcion = document.getElementById("PropositoMaqVirtuales");
        var tipoPro = opcion.options[opcion.selectedIndex];
        if (tipoPro.value == "") {
            cargarMaquinaVTabla();
            $('#dataTableMaquinaV').DataTable({
                "language": {
                    "url": url_idioma
                },
                "order": [[1, "asc"]]
            });
        } else {
            $('#dataTableMaquinaV').DataTable().column(2).search(
                tipoPro.text
            ).draw();            
        }
    });
}

//Función para cargar la tabla de Máquinas Virtuales
function cargarMaquinaVTabla() {
    var str = '<table id="dataTableMaquinaV" class="table jambo_table bulk_action  table-bordered" style="width:100%">';
    str += '<thead> <tr> <th>Nombre Máquina Virtual</th> <th>Usuario/Encargado</th> <th>Propósito</th> <th>Sistema Operativo</th> <th>Dirección IP</th> <th>Tamaño en Disco (GB)</th> <th>Memoria RAM (GB)</th> <th>Descripción</th> <th>Estado</th></tr> </thead>';
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
            if (datosMaquinasV[i].HabilitadoMaqVirtuales) {
            str += '</td><td> Habilitado';
        } else {
            str += '</td><td> Deshabilitado';
        }
        str += '</td></tr>';

    };
    str += '</tbody></table>';
    $("#tablaReportesMaqVirtuales").html(str);
}