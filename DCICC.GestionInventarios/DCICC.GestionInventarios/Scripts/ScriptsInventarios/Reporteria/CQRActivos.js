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
            if (data.OperacionExitosa) {
                console.log("Datos Exitosos");
                datosActivos = data.ListaObjetoInventarios;
                cargarActivosTabla();
                var table = $('#dataTableCQRActivos').DataTable({
                    "language": {
                        "url": url_idioma
                    },
                    "order": [[1, "asc"]]
                });

                // Handle click on "Select all" control
                $('#selecionar-activo').on('click', function () {
                    // Check/uncheck all checkboxes in the table
                    var rows = table.rows({ 'search': 'applied' }).nodes();
                    $('input[type="checkbox"]', rows).prop('checked', this.checked);
                });

                // Handle click on checkbox to set state of "Select all" control
                $('#dataTableCQRActivos tbody').on('change', 'input[type="checkbox"]', function () {
                    // If checkbox is not checked
                    if (!this.checked) {
                        var el = $('#selecionar-activo').get(0);
                        // If "Select all" control is checked and has 'indeterminate' property
                        if (el && el.checked && ('indeterminate' in el)) {
                            // Set visual state of "Select all" control 
                            // as 'indeterminate'
                            el.indeterminate = true;
                        }
                    }
                });
                cargarEstadosActivoCmb();
            } else {
                showNotify("Error en la Consulta", 'No se ha podido mostrar los datos: ' + data.MensajeError, "error");
            }
            
        }
    });

    
}
//Metodo 
function GenerarReportePDFActivosCQR(url,urlRPDF) {
    var table = $('#dataTableCQRActivos').DataTable();
    var rows = $(table.$('input[type="checkbox"]:checked').map(function () {
        return $(this).val();
    })).get();
    if (rows.length !== 0) {
        $.ajax({
            data: { "lstIdCQRActivos": rows },
            dataType: 'json',
            url: url,
            type: 'post'
        });
        window.open(urlRPDF);
    } else {
        showNotify("Error al generar el Reporte", "No se puede generar el reporte sin seleccionar activos", "error");
    }
}
function GenerarReportePDFAccesoriosCQR(url, urlRPDF) {
    var table = $('#dataTableAccesorios').DataTable();
    var rows = $(table.$('input[type="checkbox"]:checked').map(function () {
        return $(this).val();
    })).get();
    console.log(rows);
    if (rows.length !== 0) {
        $.ajax({
            data: { "lstIdCQRAccesorios": rows },
            dataType: 'json',
            url: url,
            type: 'post'
        });
        window.open(urlRPDF);
    } else {
        showNotify("Error al generar el Reporte", "No se puede generar el reporte sin seleccionar accesorios", "error");
    }
}

//Método ajax para obtener los datos de tipos de activos
function datosTipoActivo(url) {
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            if (data.OperacionExitosa) {
                cmbTipoActivo = data.ListaObjetoInventarios;
                cargarTipoActivoCmb();
            } else {
                showNotify("Error en la Consulta", 'No se ha podido mostrar los datos: ' + data.MensajeError, "error");
            }
            
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
            if (data.OperacionExitosa) {
                cmbLaboratorio = data.ListaObjetoInventarios;
                cargarLaboratoriosCmb();
            } else {
                showNotify("Error en la Consulta", 'No se ha podido mostrar los datos: ' + data.MensajeError, "error");
            }
           
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
            if (data.OperacionExitosa) {
                cmbMarcas = data.ListaObjetoInventarios;
                cargarMarcasCmb();
            } else {
                showNotify("Error en la Consulta", 'No se ha podido mostrar los datos: ' + data.MensajeError, "error");
            }
            
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
    }
    str += '</select>';
    $("#cargarTipoActivo").html(str);
    ///////CAMBIO DEL COMBOBOX
    $('#TipoActivo').change(function () {
        var opcion = document.getElementById("TipoActivo");
        var tipoAct = opcion.options[opcion.selectedIndex];
        if (tipoAct.value == "") {
            $('#dataTableCQRActivos').DataTable().column(2).search(
                ""
            ).draw();
        } else {
            $('#dataTableCQRActivos').DataTable().column(2).search(
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
    }
    str += '</select>';
    $("#cargarLaboratorios").html(str);

    $('#LaboratorioActivo').change(function () {
        var opcion = document.getElementById("LaboratorioActivo");
        var tipoLab = opcion.options[opcion.selectedIndex];
        if (tipoLab.value == "") {
            $('#dataTableCQRActivos').DataTable().column(5).search(
                ""
            ).draw();
        } else {
            $('#dataTableCQRActivos').DataTable().column(5).search(
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
    }
    str += '</select>';
    $("#cargarMarcas").html(str);

    //Método para la búsqueda en la tabla
    $('#MarcaActivo').change(function () {
        var opcion = document.getElementById("MarcaActivo");
        var tipoMarca = opcion.options[opcion.selectedIndex];
        if (tipoMarca.value == "") {
            $('#dataTableCQRActivos').DataTable().column(4).search(
                ""
            ).draw();
        } else {
            $('#dataTableCQRActivos').DataTable().column(4).search(
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
            $('#dataTableCQRActivos').DataTable().column(7).search(
                ""
            ).draw();
        } else {
            $('#dataTableCQRActivos').DataTable().column(7).search(
                tipoLab.text
            ).draw();
        }
    });
}


//Función para cargar la tabla de Activos
function cargarActivosTabla() {
    var str = '<table id="dataTableCQRActivos" class="table jambo_table bulk_action table-bordered " style="width:100%">';
    str += '<thead> <tr><th><input name="select_all" value="1" id="selecionar-activo" type="checkbox" /></th><th>Código QR</th> <th>Tipo de Activo</th> <th>Nombre del Activo</th> <th>Marca</th><th>Laboratorio</th><th>Custodio</th> <th>Estado del Activo</th><th>¿CQR Impreso?</th></tr> </thead>';
    str += '<tbody>';
    for (var i = 0; i < datosActivos.length; i++) {
        str += '<tr><td> <input id="chk" name="chk" value="' + datosActivos[i].IdCQR + '"  type="checkbox"/>' +
            '</td><td>' + datosActivos[i].IdCQR +
            '</td><td>' + datosActivos[i].NombreTipoActivo +
            '</td><td>' + datosActivos[i].NombreActivo +
            '</td><td>' + datosActivos[i].NombreMarca +
            '</td><td>' + datosActivos[i].NombreLaboratorio +
            '</td><td>' + datosActivos[i].ResponsableActivo +
            '</td><td>' + datosActivos[i].EstadoActivo;
        if (datosActivos[i].ImpresoCQR) {
            str +='</td><td> Impreso' ;
        } else {
            str += '</td><td> No Impreso';
        }   
        str += '</td ></tr> ';
    }
    str += '</tbody>' +
        '</table > ';
    $("#tablaReportesActivos").html(str);
}


/**
 * *********************************************************************************
 *                SECCIÓN PARA OPERACIONES CON ACCESORIOS
 * *********************************************************************************
 */

/* --------------------------------------SECCIÓN PARA OBTENER DATOS DEL SERVIDOR---------------------------------*/
//Método ajax para obtener accesorios
function obtenerAccesorios(url) {
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            if (data.OperacionExitosa) {
                console.log("Datos Exitosos");
                datosAccesorios = data.ListaObjetoInventarios;
                cargarAccesoriosTabla();
                var table = $('#dataTableAccesorios').DataTable({
                    "language": {
                        "url": url_idioma
                    },
                    "order": [[1, "asc"]]
                });

                // Handle click on "Select all" control
                $('#seleccionar-accesorio').on('click', function () {
                    // Check/uncheck all checkboxes in the table
                    var rows = table.rows({ 'search': 'applied' }).nodes();
                    $('input[type="checkbox"]', rows).prop('checked', this.checked);
                });

                // Handle click on checkbox to set state of "Select all" control
                $('#dataTableAccesorios tbody').on('change', 'input[type="checkbox"]', function () {
                    // If checkbox is not checked
                    if (!this.checked) {
                        var el = $('#seleccionar-accesorio').get(0);
                        // If "Select all" control is checked and has 'indeterminate' property
                        if (el && el.checked && ('indeterminate' in el)) {
                            // Set visual state of "Select all" control 
                            // as 'indeterminate'
                            el.indeterminate = true;
                        }
                    }
                });
            } else {
                showNotify("Error en la Consulta", 'No se ha podido mostrar los datos: ' + data.MensajeError, "error");
            }


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
            if (data.OperacionExitosa) {
                cmbTipoAccesorio = data.ListaObjetoInventarios;
                cargarAccesoriosCmb();
                cargarEstadosAccesoriosCmb();
            } else {
                showNotify("Error en la Consulta", 'No se ha podido mostrar los datos: ' + data.MensajeError, "error");
            }          
        }
    });
}

/* --------------------------------------SECCIÓN PARA CARGAR TABLAS Y COMBOBOX---------------------------------*/

//Función para cargar el combobox de accesorios
function cargarAccesoriosCmb() {
    var str = '<select id="AccesorioActivo" class="form-control" name="AccesorioActivo" required>';
    str += '<option value="">Mostrar Todos</option>';
    for (var i = 0; i < cmbTipoAccesorio.length; i++) {
        str += '<option value="' + cmbTipoAccesorio[i].IdTipoAccesorio + '">' + cmbTipoAccesorio[i].NombreTipoAccesorio + '</option>';
    }
    str += '</select>';
    $("#cargarTipoAccesorio").html(str);
    //Método para búsqueda con filtros
    $('#AccesorioActivo').change(function () {
        var opcion = document.getElementById("AccesorioActivo");
        var tipoLab = opcion.options[opcion.selectedIndex];
        if (tipoLab.value == "") {
            $('#dataTableAccesorios').DataTable().column(1).search(
                ""
            ).draw();
        } else {
            $('#dataTableAccesorios').DataTable().column(1).search(
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
            str += '<option value="' + cmbEstados[i] + '">' + cmbEstados[i] + '</option>';
    }
    str += '</select>';
    $("#cargarEstadosAccesorio").html(str);
    //Método para búsqueda con filtros
    $('#EstadoAccesorio').change(function () {
        var opcion = document.getElementById("EstadoAccesorio");
        var tipoLab = opcion.options[opcion.selectedIndex];
        if (tipoLab.value == "") {
            $('#dataTableAccesorios').DataTable().column(4).search(
                ""
            ).draw();
        } else {
            $('#dataTableAccesorios').DataTable().column(4).search(
                tipoLab.text
            ).draw();
        }
    });
}

//Función para cargar la tabla de Activos
function cargarAccesoriosTabla() {
    var str = '<table id="dataTableAccesorios" class="table jambo_table bulk_action  table-bordered " style="width:100%">';
    str += '<thead> <tr><th><input name="select_all" value="1" id="seleccionar-accesorio" type="checkbox" /></th> <th>Tipo de Accesorio</th> <th>Nombre de Accesorio</th> <th>Activo al que pertenece</th> <th>Estado de Accesorio</th><th>¿CQR Impreso?</th> </tr> </thead>';
    str += '<tbody>';
    for (var i = 0; i < datosAccesorios.length; i++) {
        str += '<tr><td> <input id="chk-accesorio" name="chk-accesorio" value="' + datosAccesorios[i].IdCQR + '"  type="checkbox"/>' +
            '</td><td>' + datosAccesorios[i].NombreTipoAccesorio +
            '</td><td>' + datosAccesorios[i].NombreAccesorio +
            '</td><td>' + datosAccesorios[i].NombreDetalleActivo +
            '</td><td>' + datosAccesorios[i].EstadoAccesorio;
        if (datosAccesorios[i].ImpresoCQR) {
            str += '</td><td> Impreso';
        } else {
            str += '</td><td> No Impreso';
        } 
        str += '</td></tr>';
    }
    str += '</tbody>' +
        '</table > ';
    $("#tablaAccesorios").html(str);
}