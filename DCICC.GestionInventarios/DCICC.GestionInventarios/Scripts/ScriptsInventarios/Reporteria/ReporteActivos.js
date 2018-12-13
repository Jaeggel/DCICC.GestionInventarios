var url_idioma = obtenerIdioma();
var cmbEstados = listaEstadosActivos();
var datosActivos;
var cmbTipoActivo;
var cmbLaboratorio;
var cmbMarcas;
var fechas = [];
var maxDate;
var minDate;

//accesorios
var datosAccesorios;
var cmbTipoAccesorio;
//Historicos
var datosHistoricos;
var fechasHist = [];

//Método ajax para obtener los datos de los activos
function obtenerActivos(url) {
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
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
        type: 'post',
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
        type: 'post',
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
        type: 'post',
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
            $('#dataTableActivos').DataTable().column(0).search(
                ""
            ).draw();
        } else {
            $('#dataTableActivos').DataTable().column(0).search(
                tipoAct.text
            ).draw();           
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
            $('#dataTableActivos').DataTable().column(5).search(
                ""
            ).draw();
        } else {
            $('#dataTableActivos').DataTable().column(5).search(
                tipoLab.text
            ).draw();
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
            $('#dataTableActivos').DataTable().column(2).search(
                ""
            ).draw();
        } else {
            $('#dataTableActivos').DataTable().column(2).search(
                tipoMarca.text
            ).draw();            
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
            var fechaIng = new Date(parseInt((datosActivos[i].FechaIngresoActivo).substr(6)));
            //Fecha para ordenar el string mm/dd/yyyy
            var fechaordenar = (fechaIng.toLocaleDateString("en-US"));
            //fecha para la tabla y busquedas
            function pad(n) { return n < 10 ? "0" + n : n; }
            var fechaIngreso = pad(fechaIng.getMonth() + 1) + "/" + pad(fechaIng.getDate()) + "/" + fechaIng.getFullYear();
            
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
    inicioFechaAct(minDate, maxDate); 
    finFechaAct(minDate, maxDate);

}


function inicioFechaAct(minDate, maxDate) {
    $(function () {
        $('input[name="FechaInicio"]').daterangepicker({
            startDate: minDate,
            format: 'mm-dd-yyyy',
            singleDatePicker: true,
            showDropdowns: true,
            minDate: minDate,
            maxDate: maxDate
        });
    });
}

function finFechaAct(minDate, maxDate) {
    $(function () {
        $('input[name="FechaFin"]').daterangepicker({
            startDate: 0,
            dateFormat: 'mm-dd-yyyyy',
            singleDatePicker: true,
            showDropdowns: true,
            minDate: minDate,
            maxDate: 0
        });
    });
}


function consultarFechas() {
    var table = $('#dataTableActivos').DataTable();
    $.fn.DataTable.ext.search.push(
        function (settings, data, dataIndex) {
            if (settings.sTableId == 'dataTableActivos') {
                var min = new Date($("#FechaInicio").val()).getTime();
                var max = new Date($("#FechaFin").val()).getTime();
                var startDate = new Date(data[6]).getTime();
                if (min == null && max == null) { return true; }
                if (min == null && startDate <= max) { return true; }
                if (max == null && startDate >= min) { return true; }
                if (startDate <= max && startDate >= min) { return true; }
                return false;
            } else {
                return true;
            }
            
        }
    ); 
    table.draw();
}

//function consultarDatos() {
//    var table = $('#dataTableActivos').DataTable();

//    var cols=table.rows({ filter: 'applied' }).nodes();

//    console.log(cols);

//    var title = table.columns().header();
//    console.log(title);
    
//}

//Función para limpiar las fechas
function limpiarFechas() {
    var table = $('#dataTableActivos').DataTable();
    
}

//////////////////////////////////////////////////////MÉTODOS PARA TABLAS DE ACCESORIOS//////////////////////////////////

//Método ajax para obtener accesorios
function obtenerAccesorios(url) {
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            console.log("Datos Exitosos");
            datosAccesorios = data;
            cargarAccesoriosTabla();
            $('#dataTableAccesorios').DataTable({
                "language": {
                    "url": url_idioma
                },
                "order": [[1, "asc"]]
            });
        }
    });
}

//Método ajax para obtener los tipo de accesorios
function datosTipoAccesorio(url) {
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            cmbTipoAccesorio = data;
            cargarAccesoriosCmb();
            cargarEstadosAccesoriosCmb();
        }
    });
}

//Función para cargar el combobox de accesorios
function cargarAccesoriosCmb() {
    var str = '<select id="AccesorioActivo" class="form-control" name="AccesorioActivo" required>';
    str += '<option value="">Mostrar Todos</option>';
    for (var i = 0; i < cmbTipoAccesorio.length; i++) {
        str += '<option value="' + cmbTipoAccesorio[i].IdTipoAccesorio + '">' + cmbTipoAccesorio[i].NombreTipoAccesorio + '</option>';
    };
    str += '</select>';
    $("#cargarTipoAccesorio").html(str);
    //Método para búsqueda con filtros
    $('#AccesorioActivo').change(function () {
        var opcion = document.getElementById("AccesorioActivo");
        var tipoLab = opcion.options[opcion.selectedIndex];
        if (tipoLab.value == "") {
            $('#dataTableAccesorios').DataTable().column(0).search(
                ""
            ).draw();
        } else {
            $('#dataTableAccesorios').DataTable().column(0).search(
                tipoLab.text
            ).draw();
        }
    });
}

//Función para cargar estados de accesorios
function cargarEstadosAccesoriosCmb() {
    var str = '<select id="EstadoAccesorio" class="form-control" name="EstadoAccesorio" required>';
    str += '<option value="">Mostrar Todos</option>';
    for (var i = 0; i < cmbEstados.length; i++) {
        if (cmbEstados[i]!="DE BAJA") {
            str += '<option value="' + cmbEstados[i] + '">' + cmbEstados[i] + '</option>';
        }
        
    };
    str += '</select>';
    $("#cargarEstadosAccesorio").html(str);
    //Método para búsqueda con filtros
    $('#EstadoAccesorio').change(function () {
        var opcion = document.getElementById("EstadoAccesorio");
        var tipoLab = opcion.options[opcion.selectedIndex];
        if (tipoLab.value == "") {
            $('#dataTableAccesorios').DataTable().column(5).search(
                ""
            ).draw();
        } else {
            $('#dataTableAccesorios').DataTable().column(5).search(
                tipoLab.text
            ).draw();
        }
    });
}

//Función para cargar la tabla de Activos
function cargarAccesoriosTabla() {
    var str = '<table id="dataTableAccesorios" class="table jambo_table bulk_action  table-bordered " style="width:100%">';
    str += '<thead> <tr> <th>Tipo de Accesorio</th> <th>Nombre de Accesorio</th> <th>Activo al que pertenece</th> <th>Serial de Accesorio</th> <th>Modelo de Accesorio</th> <th>Estado de Accesorio</th> </tr> </thead>';
    str += '<tbody>';
    for (var i = 0; i < datosAccesorios.length; i++) {
        if (datosAccesorios[i].EstadoAccesorio != "DE BAJA") {
            str += '<tr><td>' + datosAccesorios[i].NombreTipoAccesorio +
                '</td><td>' + datosAccesorios[i].NombreAccesorio +
                '</td><td>' + datosAccesorios[i].NombreDetalleActivo +
                '</td><td>' + datosAccesorios[i].SerialAccesorio +
                '</td><td>' + datosAccesorios[i].ModeloAccesorio +
                '</td><td>' + datosAccesorios[i].EstadoAccesorio;
            str += '</td></tr>';
        }       
    }
    str += '</tbody>' +
        '</table > ';
    $("#tablaAccesorios").html(str);
}

/////////////////////////////////////////////////////MÉTODOS PARA TABLA DE HISTORICOS////////////////////////////////////

//Método ajax para obtener accesorios
function obtenerHistoricos(url) {
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            console.log("Datos Exitosos");
            datosHistoricos = data;
            cargarHistoricosTabla();
            $('#dataTableHistoricos').DataTable({
                "language": {
                    "url": url_idioma
                },
                "order": [[1, "asc"]]
            });
        }
    });
}

//Función para cargar la tabla de Activos
function cargarHistoricosTabla() {
    var str = '<table id="dataTableHistoricos" class="table jambo_table bulk_action table-bordered " style="width:100%">';
    str += '<thead> <tr> <th>Nombre del Activo o Accesorio</th> <th>Modelo del Activo o Accesorio</th> <th>Serial del Activo o Accesorio</th> <th>Fecha de Baja</th></tr> </thead>';
    str += '<tbody>';
    for (var i = 0; i < datosHistoricos.length; i++) {
            //Método para dar formato a la fecha y hora
        var fechaIng = new Date(parseInt((datosHistoricos[i].FechaModifHistActivos).substr(6)));
            //Fecha para ordenar el string mm/dd/yyyy
            var fechaordenar = (fechaIng.toLocaleDateString("en-US"));
            //fecha para la tabla y busquedas
            function pad(n) { return n < 10 ? "0" + n : n; }
            var fechaIngreso = pad(fechaIng.getMonth() + 1) + "/" + pad(fechaIng.getDate()) + "/" + fechaIng.getFullYear();

            fechasHist[i] = fechaordenar;

        if (datosHistoricos[i].IdActivo!= 0) {
            str += '</td><td>' + datosHistoricos[i].NombreActivo +
                '</td><td>' + datosHistoricos[i].ModeloHistActivo +
                '</td><td>' + datosHistoricos[i].SerialHistActivo ;
        } else {
            str += '</td><td>' + datosHistoricos[i].NombreAccesorio +
                '</td><td>' + datosHistoricos[i].ModeloHistAccesorio +
                '</td><td>' + datosHistoricos[i].SerialHistAccesorio ;
        }
        str += '</td><td>' + fechaIngreso +
                '</td ></tr> ';
    }
    str += '</tbody>' +
        '</table > ';
    $("#tablaHistoricos").html(str);
    fechasHist = fechasHist.sort();
    var minDate = fechasHist[0];
    var maxDate = fechasHist[fechasHist.length - 1];
     inicioFechaHist(minDate, maxDate);
    finFechaHist(minDate, maxDate);

}

function inicioFechaHist(minDate, maxDate) {
    $(function () {
        $('input[name="FechaInicioHist"]').daterangepicker({
            startDate: minDate,
            format: 'mm-dd-yyyy',
            singleDatePicker: true,
            showDropdowns: true,
            minDate: minDate,
            maxDate: maxDate
        });
    });
}

function finFechaHist(minDate, maxDate) {
    $(function () {
        $('input[name="FechaFinHist"]').daterangepicker({
            startDate: 0,
            dateFormat: 'mm-dd-yyyyy',
            singleDatePicker: true,
            showDropdowns: true,
            minDate: minDate,
            maxDate: 0
        });
    });
}


function consultarFechasHist() {
    var table = $('#dataTableHistoricos').DataTable();
    $.fn.dataTable.ext.search.push(
        function (settings, data, dataIndex) {
            if (settings.sTableId == 'dataTableHistoricos') {
                var min = new Date($("#FechaInicioHist").val()).getTime();
                var max = new Date($("#FechaFinHist").val()).getTime();
                var startDate = new Date(data[3]).getTime();
                if (min == null && max == null) { return true; }
                if (min == null && startDate <= max) { return true; }
                if (max == null && startDate >= min) { return true; }
                if (startDate <= max && startDate >= min) { return true; }
                return false;
            } else {
                return true;
            }
            
        }
    );
    table.draw();
}