var url_idioma = obtenerIdioma();
var cmbEstados = listaEstadosActivos();
var datosActivos;
var cmbTipoActivo;
var cmbLaboratorio;
var cmbMarcas;
var fechas = [];
var maxDate;
var minDate;

/**
 * *********************************************************************************
 *                          SECCIÓN PARA OPERACIONES CON ACTIVOS
 * *********************************************************************************
 * /

/* --------------------------------------SECCIÓN PARA OBTENER DATOS DEL SERVIDOR---------------------------------*/
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
            var table = $('#dataTableCQRActivos').DataTable({
                "language": {
                    "url": url_idioma
                },
                "order": [[1, "asc"]]
            });


            // Handle click on "Select all" control
            $('#example-select-all').on('click', function () {
                // Check/uncheck all checkboxes in the table
                var rows = table.rows({ 'search': 'applied' }).nodes();
                $('input[type="checkbox"]', rows).prop('checked', this.checked);
            });

            // Handle click on checkbox to set state of "Select all" control
            $('#dataTableCQRActivos tbody').on('change', 'input[type="checkbox"]', function () {
                // If checkbox is not checked
                if (!this.checked) {
                    var el = $('#example-select-all').get(0);
                    // If "Select all" control is checked and has 'indeterminate' property
                    if (el && el.checked && ('indeterminate' in el)) {
                        // Set visual state of "Select all" control 
                        // as 'indeterminate'
                        el.indeterminate = true;
                    }
                }
            });         
            cargarEstadosActivoCmb();
        }
    });

    
}

//Metodo chekbox
function ver() {

    var id = $('#dataTableCQRActivos tbody input[type="checkbox"]:checked');
    console.log(id.outerHTML);
        var checks = $('#dataTableCQRActivos tbody input[type="checkbox"]:checked').map(function () {
            return $(this).val();
        }).get()
    console.log(checks);
    
        
    
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

/* --------------------------------------SECCIÓN PARA CARGAR TABLAS Y COMBOBOX---------------------------------*/
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
            $('#dataTableActivos').DataTable().column(3).search(
                ""
            ).draw();
        } else {
            $('#dataTableActivos').DataTable().column(3).search(
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

//Función para cargar estados de activos
function cargarEstadosActivoCmb() {
    var str = '<select id="EstadoActivo" class="form-control" name="EstadoActivo" required>';
    str += '<option value="">Mostrar Todos</option>';
    for (var i = 0; i < cmbEstados.length; i++) {
       str += '<option value="' + cmbEstados[i] + '">' + cmbEstados[i] + '</option>';
    }
    str += '</select>';
    $("#cargarEstadosActivo").html(str);
    //Método para búsqueda con filtros
    $('#EstadoActivo').change(function () {
        var opcion = document.getElementById("EstadoActivo");
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


//Función para cargar la tabla de Activos
function cargarActivosTabla() {
    var str = '<table id="dataTableCQRActivos" class="table jambo_table bulk_action table-bordered " style="width:100%">';
    str += '<thead> <tr><th><input name="select_all" value="1" id="example-select-all" type="checkbox" /></th> <th>Tipo de Activo</th> <th>Nombre del Activo</th> <th>Marca</th><th>Laboratorio</th><th>Custodio</th> <th>Estado del Activo</th> <th>Código QR</th></tr> </thead>';
    str += '<tbody>';
    for (var i = 0; i < datosActivos.length; i++) {
        str += '<tr><td> <input id="chk" name="chk" value="' + datosActivos[i].IdCQR+'"  type="checkbox"/>'+
                '</td><td>' + datosActivos[i].NombreTipoActivo +
                '</td><td>' + datosActivos[i].NombreActivo +
                '</td><td>' + datosActivos[i].NombreMarca +
                '</td><td>' + datosActivos[i].NombreLaboratorio +
                '</td><td>' + datosActivos[i].ResponsableActivo +
                '</td><td>' + datosActivos[i].EstadoActivo +
                '</td><td>' + datosActivos[i].IdCQR;
            str += '</td ></tr> ';
    }
    str += '</tbody>' +
        '</table > ';
    $("#tablaReportesActivos").html(str);
}
