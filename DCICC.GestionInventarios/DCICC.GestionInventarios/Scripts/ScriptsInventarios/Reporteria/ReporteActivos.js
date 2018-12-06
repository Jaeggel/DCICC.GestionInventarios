var url_idioma = obtenerIdioma();
var cmbEstados = listaEstadosActivos();
var datosActivos;
var cmbTipoActivo;
var cmbLaboratorio;
var cmbMarcas;
var fechas = [];
var maxDate;
var minDate;
//Método ajax para obtener los datos de los activos
function obtenerActivos(url) {
    url_metodo = url;
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'get',
        success: function (data) {
            console.log("Datos Exitosos");
            datosActivos = data;
            cargarActivosTabla();
            $('#dataTableActivos').DataTable({
                "language": {
                    "url": url_idioma
                },
                "order": [[1, "asc"]]
            });
        }
    });
}

//Método ajax para obtener los datos de tipos de activos
function datosTipoActivo(url) {
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'get',
        success: function (data) {
            cmbTipoActivo = data;
            cargarTipoActivoCmb();
        }
    });
}

//Método ajax para obtener los datos de los laboratorios
function datosLaboratorio(url) {
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'get',
        success: function (data) {
            cmbLaboratorio = data;
            cargarLaboratoriosCmb();
        }
    });
}

//Método ajax para obtener los datos de las marcas
function datosMarcas(url) {
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'get',
        success: function (data) {
            cmbMarcas = data;
            cargarMarcasCmb();
        }
    });
}

//Función para cargar el combobox de tipo de activo
function cargarTipoActivoCmb() {
    var str = '<select id="TipoActivo" class="form-control" name="TipoActivo">';
    str += '<option value="">Mostrar Todos</option>';
    for (var i = 0; i < cmbTipoActivo.length; i++) {
        str += '<option value="' + cmbTipoActivo[i].IdTipoActivo + '">' + cmbTipoActivo[i].NombreTipoActivo + '</option>';
    };
    str += '</select>';
    $("#cargarTipoActivo").html(str);
    ///////CAMBIO DEL COMBOBOX
    $('#TipoActivo').change(function () {
        var opcion = document.getElementById("TipoActivo");
        var tipoAct = opcion.options[opcion.selectedIndex];
        if (tipoAct.value == "") {
            cargarActivosTabla();
            $('#dataTableActivos').DataTable({
                "language": {
                    "url": url_idioma
                },
                "order": [[1, "asc"]]
            });
        } else {
            var td, i, txtValue;
            console.log(tipoAct);
            var table = document.getElementById("dataTableActivos");
            var tr = table.getElementsByTagName("tr");
            // Loop through all table rows, and hide those who don't match the search query
            for (i = 0; i < tr.length; i++) {
                td = tr[i].getElementsByTagName("td")[0];
                if (td) {
                    txtValue = td.textContent || td.innerText;
                    console.log(txtValue);
                    if (txtValue.toUpperCase().indexOf(tipoAct.text) > -1) {
                        tr[i].style.display = "";
                    } else {
                        tr[i].style.display = "none";
                    }
                }
            }
        }
    });
}

//Función para cargar el combobox de laboratorios
function cargarLaboratoriosCmb() {
    var str = '<select id="LaboratorioActivo" class="form-control" name="LaboratorioActivo"  required>';
    str += '<option value="">Mostrar Todos</option>';
    for (var i = 0; i < cmbLaboratorio.length; i++) {
        str += '<option value="' + cmbLaboratorio[i].IdLaboratorio + '">' + cmbLaboratorio[i].NombreLaboratorio + '</option>';
    };
    str += '</select>';
    $("#cargarLaboratorios").html(str);

    $('#LaboratorioActivo').change(function () {
        var opcion = document.getElementById("LaboratorioActivo");
        var tipoLab = opcion.options[opcion.selectedIndex];
        if (tipoLab.value == "") {
            cargarActivosTabla();
            $('#dataTableActivos').DataTable({
                "language": {
                    "url": url_idioma
                },
                "order": [[1, "asc"]]
            });
        } else {
            var td, i, txtValue;
            var table = document.getElementById("dataTableActivos");
            var tr = table.getElementsByTagName("tr");
            // Loop through all table rows, and hide those who don't match the search query
            for (i = 0; i < tr.length; i++) {
                td = tr[i].getElementsByTagName("td")[5];
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

//Función para cargar el combobox de Marcas
function cargarMarcasCmb() {
    var str = '<select id="MarcaActivo" class="form-control" name="MarcaActivo"   required>';
    str += '<option value="">Mostrar Todos</option>';
    for (var i = 0; i < cmbMarcas.length; i++) {
        str += '<option value="' + cmbMarcas[i].IdMarca + '">' + cmbMarcas[i].NombreMarca + '</option>';
    };
    str += '</select>';
    $("#cargarMarcas").html(str);

    //Método para la búsqueda en la tabla
    $('#MarcaActivo').change(function () {
        var opcion = document.getElementById("MarcaActivo");
        var tipoMarca = opcion.options[opcion.selectedIndex];
        if (tipoMarca.value == "") {
            cargarActivosTabla();
            $('#dataTableActivos').DataTable({
                "language": {
                    "url": url_idioma
                },
                "order": [[1, "asc"]]
            });
        } else {
            var td, i, txtValue;            
            var table = document.getElementById("dataTableActivos");
            var tr = table.getElementsByTagName("tr");
            // Loop through all table rows, and hide those who don't match the search query
            for (i = 0; i < tr.length; i++) {
                td = tr[i].getElementsByTagName("td")[2];
                if (td) {
                    txtValue = td.textContent || td.innerText;
                    console.log(txtValue.toUpperCase().indexOf(tipoMarca.text));
                    if (txtValue.toUpperCase().indexOf(tipoMarca.text) > -1) {
                        tr[i].style.display = "";
                    } else {
                        tr[i].style.display = "none";
                    }
                }
            }
        }
    });
}


//Función para cargar la tabla de Activos
function cargarActivosTabla() {
    var str = '<table id="dataTableActivos" class="table jambo_table bulk_action table-bordered " style="width:100%">';
    str += '<thead> <tr> <th>Tipo de Activo</th> <th>Nombre del Activo</th> <th>Marca</th> <th>Modelo</th> <th>Serial</th> <th>Laboratorio</th> <th>Fecha de Ingreso</th> <th>Estado del Activo</th></tr> </thead>';
    str += '<tbody>';
    for (var i = 0; i < datosActivos.length; i++) {
        if (datosActivos[i].EstadoActivo != "DE BAJA") {
            //Método para dar formato a la fecha y hora
            var fechaLog = new Date(parseInt((datosActivos[i].FechaIngresoActivo).substr(6)));
            //Fecha para ordenar el string mm/dd/yyyy
            var fechaordenar = (fechaLog.toLocaleDateString("en-US"));
            //fecha para la tabla y busquedas
            function pad(n) { return n < 10 ? "0" + n : n; }
            var fechaIngreso = pad(fechaLog.getMonth() + 1) + "/" + pad(fechaLog.getDate()) + "/" + fechaLog.getFullYear();
            
            fechas[i] = fechaordenar;

            str += '<tr><td>' + datosActivos[i].NombreTipoActivo +
                '</td><td>' + datosActivos[i].NombreActivo +
                '</td><td>' + datosActivos[i].NombreMarca +
                '</td><td>' + datosActivos[i].ModeloActivo +
                '</td><td>' + datosActivos[i].SerialActivo +
                '</td><td>' + datosActivos[i].NombreLaboratorio +
                '</td><td>' + fechaIngreso +
                '</td><td>' + datosActivos[i].EstadoActivo;
            str += '</td ></tr> ';
        }
    }
    str += '</tbody>' +
        '</table > ';
    $("#tablaReportesActivos").html(str);
    fechas = fechas.sort();
    console.log(fechas);
    var minDate = fechas[0];
    var maxDate = fechas[fechas.length - 1];
    inicioFecha(minDate, maxDate); 
    finFecha(minDate, maxDate);
}


function inicioFecha(minDate, maxDate) {
    $(function () {
        $('input[name="FechaInicio"]').daterangepicker({
            startDate: minDate,
            format: 'dd-mm-yyyy',
            singleDatePicker: true,
            showDropdowns: true,
            minDate: minDate,
            maxDate: maxDate
        });
    });
}

function finFecha(minDate, maxDate) {
    $(function () {
        $('input[name="FechaFin"]').daterangepicker({
            startDate: maxDate,
            dateFormat: 'dd-mm-yyyyy',
            singleDatePicker: true,
            showDropdowns: true,
            minDate: minDate,
            maxDate: maxDate
        });
    });
}

function consultarFechas() {
    var from = $('#FechaInicio').val();
    var to = $('#FechaFin').val();
    from.toString();
    to.toString();
    

    $("#dataTableActivos tr").each(function () {
        var row = $(this);
        var date = stringToDate(row.find("td").eq(6).text());

        //show all rows by default
        var show = true;

        //if from date is valid and row date is less than from date, hide the row
        if (from && date < from)
            show = false;

        //if to date is valid and row date is greater than to date, hide the row
        if (to && date > to)
            show = false;

        if (show)
            row.show();
        else
            row.hide();
    });



    //var td, i, txtValue;
    //var table = document.getElementById("dataTableActivos");
    //var tr = table.getElementsByTagName("tr");
    //// Loop through all table rows, and hide those who don't match the search query
    //for (i = 0; i < tr.length; i++) {
    //    td = tr[i].getElementsByTagName("td")[6];
    //    if (td) {
    //        txtValue = td.textContent || td.innerText;
            
    //        console.log(txtValue);
    //        console.log(txtValue.toUpperCase().indexOf(fechaInicio));
    //        console.log(txtValue.toUpperCase().indexOf(fechaFin));
    //        if (txtValue.toUpperCase().indexOf(fechaInicio) > -1 || txtValue.toUpperCase().indexOf(fechaFin) > -1) {
    //            console.log("mayor");
    //            tr[i].style.display = "";
    //        } else {
    //            tr[i].style.display = "none";
    //        }
    //    }
    //}

}

function stringToDate(s) {
    var ret = NaN;
    var parts = s.split("/");
    date = new Date(parts[2], parts[0], parts[1]);
    if (!isNaN(date.getTime())) {
        ret = date;
    }
    return ret;
}

//////////////////////////////////////////////////////MÉTODOS PARA TABLAS DE ACCESORIOS//////////////////////////////////




/////////////////////////////////////////////////////MÉTODOS PARA TABLA DE HISTORICOS////////////////////////////////////