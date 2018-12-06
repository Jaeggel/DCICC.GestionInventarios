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
        var tipoLab = opcion.options[opcion.selectedIndex];
        if (tipoLab.value == "") {
            cargarMaquinaVTabla();
            $('#dataTableMaquinaV').DataTable({
                "language": {
                    "url": url_idioma
                },
                "order": [[1, "asc"]]
            });
        } else {
            var td, i, txtValue;
            var table = document.getElementById("dataTableMaquinaV");
            var tr = table.getElementsByTagName("tr");
            // Loop through all table rows, and hide those who don't match the search query
            for (i = 0; i < tr.length; i++) {
                td = tr[i].getElementsByTagName("td")[3];
                if (td) {
                    txtValue = td.textContent || td.innerText;
                    if (txtValue.toUpperCase().indexOf(tipoLab.text) > -1) {
                        tr[i].style.display = "";
                    } else {
                        tr[i].style.display = "none";
                    }
                }
            }
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
        var tipoLab = opcion.options[opcion.selectedIndex];
        if (tipoLab.value == "") {
            cargarMaquinaVTabla();
            $('#dataTableMaquinaV').DataTable({
                "language": {
                    "url": url_idioma
                },
                "order": [[1, "asc"]]
            });
        } else {
            var td, i, txtValue;
            var table = document.getElementById("dataTableMaquinaV");
            var tr = table.getElementsByTagName("tr");
            // Loop through all table rows, and hide those who don't match the search query
            for (i = 0; i < tr.length; i++) {
                td = tr[i].getElementsByTagName("td")[2];
                if (td) {
                    txtValue = td.textContent || td.innerText;
                    if (txtValue.toUpperCase().indexOf(tipoLab.text) > -1) {
                        tr[i].style.display = "";
                    } else {
                        tr[i].style.display = "none";
                    }
                }
            }
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