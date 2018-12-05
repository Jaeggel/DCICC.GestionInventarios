var url_idioma = obtenerIdioma();
var cmbEstados = listaEstadosActivos();
var datosActivos;
var cmbTipoActivo;
var cmbLaboratorio;
var cmbMarcas;

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
    var str = '<select id="TipoActivo" class="form-control" name="TipoActivo"  required>';
    str += '<option value="">Escoga una opción...</option>';
    for (var i = 0; i < cmbTipoActivo.length; i++) {
        console.log(cmbTipoActivo[i].NombreTipoActivo);
        str += '<option value="' + cmbTipoActivo[i].IdTipoActivo + '">' + cmbTipoActivo[i].NombreTipoActivo + '</option>';
    };
    str += '</select>';
    $("#cargarTipoActivos").html(str);
}

//Función para cargar el combobox de laboratorios
function cargarLaboratoriosCmb() {
    var str = '<select id="LaboratorioActivo" class="form-control" name="LaboratorioActivo"  required>';
    str += '<option value="">Escoga una opción...</option>';
    for (var i = 0; i < cmbLaboratorio.length; i++) {
        str += '<option value="' + cmbLaboratorio[i].IdLaboratorio + '">' + cmbLaboratorio[i].NombreLaboratorio + '</option>';
    };
    str += '</select>';
    $("#cargarLaboratorios").html(str);
}

//Función para cargar el combobox de Marcas
function cargarMarcasCmb() {
    var str = '<select id="MarcaActivo" class="form-control" name="MarcaActivo"   required>';
    str += '<option value="">Escoga una opción...</option>';
    for (var i = 0; i < cmbMarcas.length; i++) {
        str += '<option value="' + cmbMarcas[i].IdMarca + '">' + cmbMarcas[i].NombreMarca + '</option>';
    };
    str += '</select>';
    $("#cargarMarcas").html(str);
}

//Función para cargar el combobox de estados
function cargarEstadosCmb() {
    var str = '<select id="EstadoActivo" class="form-control" name="EstadoActivo" onBlur=" validacionesCamposModificar();" required>';
    str += '<option value="">Escoga una opción...</option>';
    for (var i = 0; i < cmbEstados.length; i++) {
        str += '<option value="' + cmbEstados[i] + '">' + cmbEstados[i] + '</option>';
    };
    str += '</select>';
    $("#cargarEstados").html(str);
}


//Función para cargar la tabla de Activos
function cargarActivosTabla() {
    var str = '<table id="dataTableActivos" class="table jambo_table bulk_action  table-bordered " style="width:100%">';
    str += '<thead> <tr> <th>Tipo de Activo</th> <th>Nombre del Activo</th> <th>Modelo</th> <th>Serial</th> <th>Laboratorio</th> <th>Fecha de Ingreso</th> <th>Código QR</th> <th>Estado del Activo</th> <th>Agregar Accesorio</th> <th>Modificar</th> <th>Cambiar Estado</th>  </tr> </thead>';
    str += '<tbody>';
    for (var i = 0; i < datosActivos.length; i++) {
        //Método para dar formato a la fecha y hora
        var fechaLog = new Date(parseInt((datosActivos[i].FechaIngresoActivo).substr(6)));
        var fechaIngreso = (fechaLog.toLocaleDateString("es-ES"));

        str += '<tr><td>' + datosActivos[i].NombreTipoActivo +
            '</td><td>' + datosActivos[i].NombreActivo +
            '</td><td>' + datosActivos[i].ModeloActivo +
            '</td><td>' + datosActivos[i].SerialActivo +
            '</td><td>' + datosActivos[i].NombreLaboratorio +
            '</td><td>' + fechaIngreso +
            '</td><td>' + datosActivos[i].IdCQR +
            '</td><td>' + datosActivos[i].EstadoActivo;
        str += '</td ></tr > ';
    };
    str += '</tbody>' +
        '<tfoot><tr> <th>Tipo de Activo</th> <th>Nombre del Activo</th> <th>Modelo</th> <th>Serial</th> <th>Laboratorio</th> <th>Fecha de Ingreso</th> <th>Código QR</th><th>Estado del Activo</th> <th>Agregar Accesorio</th> <th>Modificar</th> <th>Cambiar Estado</th>  </tr> </thead></tfoot>' +
        '</table > ';
    $("#tablaReportesActivos").html(str);
}