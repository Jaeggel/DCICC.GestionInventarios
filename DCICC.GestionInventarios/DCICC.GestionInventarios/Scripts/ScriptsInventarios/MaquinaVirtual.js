var url_idioma = obtenerIdioma();
var url_metodo;
var propositos;
var datosMaquinasV;
var cmbSO;
var datosLuns;
var idMaquinaV;
var nombreMVModificar;
var urlEstado;
var nombresMV = [];

/* --------------------------------------SECCIÓN PARA OBTENER DATOS DEL SERVIDOR---------------------------------*/
//Método ajax para obtener los datos de Máquinas virtuales
function obtenerMaquinaV(url) {
    url_metodo = url;
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            if (data.OperacionExitosa) {
                datosMaquinasV = data.ListaObjetoInventarios;
                cargarMaquinaVTabla();
                $('#dataTableMaquinaV').DataTable({
                    "language": {
                        "url": url_idioma
                    }
                });
                cargarNombresMV();
            } else {
                showNotify("Error en la Consulta", 'No se ha podido mostrar los datos: ' + data.MensajeError, "error");
            }           
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
            if (data.OperacionExitosa) {
                cmbSO = data.ListaObjetoInventarios;
                cargarSOCmb();
                cargarSOModificarCmb();
            } else {
                showNotify("Error en la Actualización", 'No se ha podido modificar el Estado de la LUN: ' + data.MensajeError, "error");
            }         
        }
    });
}

//Método ajax para obtener los datos de propósitos
function listaPropositos(url) {
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            propositos = data;
            cargarPropositosCmb();
            cargarPropositosCmbModificar();
        }
    });
}

//Método ajax para obtener los datos de las luns
function obtenerLuns(url) {
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            if (data.OperacionExitosa) {
                datosLuns = data.ListaObjetoInventarios;
                cargarLunsCmb();
                cargarLunsCmbModificar();
            } else {
                showNotify("Error en la Actualización", 'No se ha podido modificar el Estado de la LUN: ' + data.MensajeError, "error");
            }         
        }
    });
}

//Función para obtener la url de modificación
function urlEstados(url) {
    urlEstado = url;
}

/* --------------------------------------SECCIÓN PARA CARGAR TABLAS Y COMBOBOX---------------------------------*/

//Función para cargar la tabla de Máquinas Virtuales
function cargarMaquinaVTabla() {
    var str = '<table id="dataTableMaquinaV" class="table jambo_table bulk_action  table-bordered" style="width:100%">';
    str += '<thead> <tr> <th>Nombre Máquina Virtual</th> <th>Nombre LUN</th> <th>Usuario/Encargado</th> <th>Propósito</th> <th>Sistema Operativo</th> <th>Dirección IP</th> <th>Tamaño en Disco (GB/TB)</th> <th>Memoria RAM (GB)</th> <th>Descripción</th> <th>Estado</th> <th>Modificar</th> <th>Habilitar/<br>Deshabilitar</th> </tr> </thead>';
    str += '<tbody>';
    for (var i = 0; i < datosMaquinasV.length; i++) {
        str += '<tr><td>' + datosMaquinasV[i].NombreMaqVirtuales +
            '</td><td>' + datosMaquinasV[i].NombreLUN +
            '</td><td>' + datosMaquinasV[i].UsuarioMaqVirtuales +
            '</td><td>' + datosMaquinasV[i].PropositoMaqVirtuales +
            '</td><td>' + datosMaquinasV[i].NombreSistOperativos +
            '</td><td>' + datosMaquinasV[i].DireccionIPMaqVirtuales +
            '</td><td>' + datosMaquinasV[i].DiscoMaqVirtuales +
            '</td><td>' + datosMaquinasV[i].RamMaqVirtuales +
            '</td><td class="text-justify">' + datosMaquinasV[i].DescripcionMaqVirtuales;

        if (datosMaquinasV[i].HabilitadoMaqVirtuales) {
            str += '</td><td> Habilitado';
        } else {
            str += '</td><td> Deshabilitado';
        }
        str += '</td><td><div class="text-center"><div class="col-md-12 col-sm-12 col-xs-12">' +
            '<button type="button" class="btn btn-info text-center" data-toggle="modal" data-target="#ModificarMaquinaV" onclick = "formUpdateMaquinaV(' + datosMaquinasV[i].IdMaqVirtuales + ');"> <strong><i class="fa fa-pencil-square-o"></i></strong></button> ' +
            '</div></div>' +
            '</td><td><div class=" text-center"><div class="col-md-12 col-sm-12 col-xs-12">';
        if (datosMaquinasV[i].HabilitadoMaqVirtuales) {
            str += '<button type = "button" class="btn btn-success text-center" onclick = "habilitarOdeshabilitar(' + datosMaquinasV[i].IdMaqVirtuales + ',' + datosMaquinasV[i].HabilitadoMaqVirtuales+');"> <strong><span class="fa fa-toggle-on"></span></strong></button> ';
        } else {
            str += '<button type = "button" class="btn btn-danger text-center" onclick = "habilitarOdeshabilitar(' + datosMaquinasV[i].IdMaqVirtuales + ',' + datosMaquinasV[i].HabilitadoMaqVirtuales +');"> <strong><i class="fa fa-toggle-off"></i></strong></button> ';
        }
           str +=   '</div></div></td></tr>';

    }
    str += '</tbody></table>';
    $("#tablaModificarMaquinaV").html(str);
}

//Función para cargar el combobox de Sistemas Operativos
function cargarSOCmb() {
    var str = '<select id="IdSistOperativos" class="form-control" name="IdSistOperativos" required>';
    str += '<option value="">Escoga una opción...</option>';
    for (var i = 0; i < cmbSO.length; i++) {
        str += '<option value="' + cmbSO[i].IdSistOperativos + '">' + cmbSO[i].NombreSistOperativos + '</option>';
    }
    str += '</select>';
    $("#cargarSO").html(str);
}

//Función para cargar el combobox de Sistemas Operativos para modificar
function cargarSOModificarCmb() {
    var str = '<select id="IdSistOperativos" class="form-control" name="IdSistOperativos" required>';
    for (var i = 0; i < cmbSO.length; i++) {
        str += '<option value="' + cmbSO[i].IdSistOperativos + '">' + cmbSO[i].NombreSistOperativos + '</option>';
    }
    str += '</select>';
    $("#cargarSOModificar").html(str);
}

//Función para cargar el combobox de Propósitos
function cargarPropositosCmb() {
    var str = '<select id="PropositoMaqVirtuales" class="form-control" name="PropositoMaqVirtuales" onBlur="validarCmbMV();" required>';
    str += '<option value="">Escoga una opción...</option>';
    for (var i = 0; i < propositos.length; i++) {
        str += '<option value="' + propositos[i].NombreProposito + '">' + propositos[i].NombreProposito + '</option>';
    }
    str += '</select>';
    $("#cargarPropositos").html(str);
}

//Función para cargar el combobox de Propositos Modificados
function cargarPropositosCmbModificar() {
    var str = '<select id="PropositoMaqVirtuales" class="form-control" name="PropositoMaqVirtuales" onBlur="validarCmbMV();" required>';
    for (var i = 0; i < propositos.length; i++) {
        str += '<option value="' + propositos[i].NombreProposito + '">' + propositos[i].NombreProposito + '</option>';
    }
    str += '</select>';
    $("#cargarPropositosModificar").html(str);
}

//Función para cargar el combobox de Luns
function cargarLunsCmb() {
    var str = '<select id="IdLUN" class="form-control" name="IdLUN" required>';
    str += '<option value="">Escoga una opción...</option>';
    for (var i = 0; i < datosLuns.length; i++) {
        if (datosLuns[i].HabilitadoLUN) {
            str += '<option value="' + datosLuns[i].IdLUN + '">' + datosLuns[i].NombreLUN + '</option>';
        }   
    }
    str += '</select>';
    $("#cargarLuns").html(str);
}

//Función para cargar el combobox de Luns modificadas
function cargarLunsCmbModificar() {
    var str = '<select id="IdLUN" class="form-control" name="IdLUN" required>';
    for (var i = 0; i < datosLuns.length; i++) {
      
            str += '<option value="' + datosLuns[i].IdLUN + '">' + datosLuns[i].NombreLUN + '</option>';
    }
    str += '</select>';
    $("#cargarLunsModificar").html(str);
}

/* --------------------------------------SECCIÓN PARA MODIFICACION DE DATOS---------------------------------*/

//Función para setear los valores en los inputs
function formUpdateMaquinaV(idMV) {
    idMaquinaV = idMV;
    for (var i = 0; i < datosMaquinasV.length; i++) {
        if (datosMaquinasV[i].IdMaqVirtuales == idMV) {
            nombreMVModificar = datosMaquinasV[i].NombreMaqVirtuales;
            //Métodos para setear los valores a modificar
            var element4 = document.getElementById("IdLUN");
            element4.value = datosMaquinasV[i].IdLUN;

            var element = document.getElementById("IdSistOperativos");
            element.value = datosMaquinasV[i].IdSistOperativos;
            document.getElementById("NombreMaqVirtuales").value = datosMaquinasV[i].NombreMaqVirtuales;
            document.getElementById("UsuarioMaqVirtuales").value = datosMaquinasV[i].UsuarioMaqVirtuales;
            var element2 = document.getElementById("PropositoMaqVirtuales");
            element2.value = datosMaquinasV[i].PropositoMaqVirtuales;
            
            document.getElementById("DireccionIPMaqVirtuales").value = datosMaquinasV[i].DireccionIPMaqVirtuales;
            document.getElementById("DiscoMaqVirtuales").value = datosMaquinasV[i].SizeMaqVirtuales;
            var element3 = document.getElementById("UnidadMaqVirtuales");
            element3.value = datosMaquinasV[i].UnidadMaqVirtuales;

            document.getElementById("RamMaqVirtuales").value = datosMaquinasV[i].RamMaqVirtuales;
            document.getElementById("DescripcionMaqVirtuales").value = datosMaquinasV[i].DescripcionMaqVirtuales;


            //Método para el check del update de Máquinas Virtuales
            var valor = datosMaquinasV[i].HabilitadoMaqVirtuales;
            var estado = $('#HabilitadoMaqVirtuales').prop('checked');
            if (estado && valor == false) {
                document.getElementById("HabilitadoMaqVirtuales").click();
            }
            if (estado == false && valor == true) {
                document.getElementById("HabilitadoMaqVirtuales").click();
            }
        }
    }
}

//Función para modificar el la Máquina virtual
function modificarMaquinaV(url_modificar) {
    var cmbLun = document.getElementById("IdLUN");
    var idLun = cmbLun.options[cmbLun.selectedIndex].value;
    var cmbSO = document.getElementById("IdSistOperativos");
    var idSO = cmbSO.options[cmbSO.selectedIndex].value;
    var nombreMV= document.getElementById("NombreMaqVirtuales").value;
    var usuarioMV = document.getElementById("UsuarioMaqVirtuales").value;
    var cmbProposito = document.getElementById("PropositoMaqVirtuales");
    var propositoMV = cmbProposito.options[cmbProposito.selectedIndex].value;
    var direccionIP= document.getElementById("DireccionIPMaqVirtuales").value;
    var disco = document.getElementById("DiscoMaqVirtuales").value;

    var cmbUnidad = document.getElementById("UnidadMaqVirtuales");
    var idUnidad = cmbUnidad.options[cmbUnidad.selectedIndex].value;

    var ram= document.getElementById("RamMaqVirtuales").value;
    var descripcion= document.getElementById("DescripcionMaqVirtuales").value;
    var habilitadoMV = $('#HabilitadoMaqVirtuales').prop('checked');

    if (validarInputNombre() && validarIP() && validarDisco() && validarRam() && validarCmbMV() && validarInputUsuario()) {
        swal({
            title: 'Confirmación de Actualización',
            text: "¿Está seguro de modificar el registro?",
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#26B99A',
            cancelButtonColor: '#337ab7',
            confirmButtonText: 'Confirmar',
            cancelButtonText: 'Cancelar'
        }).then((result) => {
            if (result.value) {
                $.ajax({
                    data: {
                        "IdMaqVirtuales": idMaquinaV, "IdLUN": idLun, "IdSistOperativos": idSO, "UsuarioMaqVirtuales": usuarioMV,
                        "NombreMaqVirtuales": nombreMV, "PropositoMaqVirtuales": propositoMV, "DireccionIPMaqVirtuales": direccionIP,
                        "DiscoMaqVirtuales": disco, "RamMaqVirtuales": ram, "DescripcionMaqVirtuales": descripcion,
                        "UnidadMaqVirtuales": idUnidad, "HabilitadoMaqVirtuales": habilitadoMV
                    },
                    url: url_modificar,
                    type: 'post',
                    success: function (data) {
                        console.log(data.OperacionExitosa);
                        if (data.OperacionExitosa) {
                            $('#ModificarMaquinaV').modal('hide');
                            showNotify("Actualización exitosa", 'La Máquina Virtual " ' + nombreMV.toUpperCase() + ' " se ha modificado exitosamente', "success");
                            obtenerMaquinaV(url_metodo);
                        } else {
                            $('#ModificarMaquinaV').modal('hide');
                            showNotify("Error en la Actualización", 'Ocurrió un error al modificar la Máquina Virtual: ' + data.MensajeError, "error");
                        }

                    }
                });

            } else {
                $('#ModificarTipoActivo').modal('hide');
            }
        });
    }  
}

//Función para habilitar o deshabilitar la maquina virtual
function habilitarOdeshabilitar(idMaqVir, estadoMv) {
    var nuevoEstado = true;
    if (estadoMv) {
        nuevoEstado = false;
    } else {
        nuevoEstado = true;
    }
    swal({
        title: 'Confirmación de Cambio de Estado',
        text: "¿Está seguro de cambiar el estado del registro?",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#26B99A',
        cancelButtonColor: '#337ab7',
        confirmButtonText: 'Confirmar',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.value) {
            $.ajax({
                data: { "IdMaqVirtuales": idMaqVir, "HabilitadoMaqVirtuales": nuevoEstado },
                url: urlEstado,
                type: 'post',
                success: function (data) {
                    console.log(data.OperacionExitosa);
                    if (data.OperacionExitosa) {
                        showNotify("Actualización exitosa", 'El Estado de la Máquina Virtual se ha modificado exitosamente', "success");
                        obtenerMaquinaV(url_metodo);
                    } else {
                        showNotify("Error en la Actualización", 'Ocurrió un error al modificar el estado de la Máquina Virtual: ' + data.MensajeError, "error");
                    }                  
                }, error: function (e) {
                    console.log(e);
                }
            });
        } else {

        }
    });
}

/* --------------------------------------SECCIÓN PARA CAMPOS DE AUTOCOMPLETE---------------------------------*/

//Funciones para cargar el campo de autocompletado
function cargarNombresMV() {
    for (var i = 0; i < datosMaquinasV.length; i++) {
        nombresMV[i] = datosMaquinasV[i].NombreMaqVirtuales;
    }
}
//Función para cargar los nombres en el campo de nombre de Máquinas Virtuales
$(function () {
    $("#NombreMaqVirtuales").autocomplete({
        source: nombresMV
    });
});

/* --------------------------------------SECCIÓN PARA COMPROBACIONES Y VALIDACIONES---------------------------------*/
//Función para evitar nombres de máquinas virtuales repetidos
function comprobarNombre() {
    var nomMV = document.getElementById("NombreMaqVirtuales");
    nomMV.value = nomMV.value.toUpperCase();
    //Validación para el campo de texto nombre de Máquina virtual
    if (nomMV.value.length <= 0) {
        nomMV.style.borderColor = "#900C3F";
        $('#errorNombreMV').html('El campo nombre de Máquina Virtual no debe estar vacio').show();
        setTimeout("$('#errorNombreMV').html('').hide('slow')", 6000);
    } else {
        for (var i = 0; i < datosMaquinasV.length; i++) {
            if ((datosMaquinasV[i].NombreMaqVirtuales).toUpperCase() == nomMV.value) {
                nomMV.style.borderColor = "#900C3F";
                $('#errorNombreMV').html("El nombre de la Máquina Virtual: " + nomMV.value + " ya existe").show();
                setTimeout("$('#errorNombreMV').html('').hide('slow')", 6000);
                nomMV.value = "";
            } else {
                nomMV.style.borderColor = "#ccc";
                $('#errorNombreMV').html('').hide();
            }
        }
    }
}


//Función para evitar nombres de máquinas virtuales repetidos modificacion
function validarNombreModificacion() {
    var nomMV = document.getElementById("NombreMaqVirtuales");
    nomMV.value = nomMV.value.toUpperCase();
    //Validación para el campo de texto nombre de Máquina virtual
    if (nomMV.value != nombreMVModificar.toUpperCase()) {
        for (var i = 0; i < datosMaquinasV.length; i++) {
            if ((datosMaquinasV[i].NombreMaqVirtuales).toUpperCase() == nomMV.value) {
                nomMV.style.borderColor = "#900C3F";
                $('#errorNombreMV').html("El nombre de la Máquina Virtual: " + nomMV.value + " ya existe").show();
                setTimeout("$('#errorNombreMV').html('').hide('slow')", 6000);
                nomMV.value = "";
            } else {
                nomMV.style.borderColor = "#ccc";
                $('#errorNombreMV').html('').hide();
            }
        }
    } else {
        nomMV.style.borderColor = "#ccc";
        $('#errorNombreMV').html('').hide();
    }
}

//Funciones para validaciones de campos de texto
function validarInputNombre() {
    var esValido = true;
    var nomMV = document.getElementById("NombreMaqVirtuales");
   
    //Validación para el campo de texto nombre de Máquina virtual
    if (nomMV.value.length <= 0) {
        esValido = false;
        nomMV.value = "";
        nomMV.style.borderColor = "#900C3F";
        $('#errorNombreMV').html('El campo nombre de Máquina Virtual no debe estar vacio').show();
        setTimeout("$('#errorNombreMV').html('').hide('slow')", 6000);
    } else {
        nomMV.style.borderColor = "#ccc";
        $('#errorNombreMV').html('').hide();
    }
    return esValido;

}

//Función para validar el nombre de Usuario
function validarInputUsuario() {
    var esValido = true;
    var boton = document.getElementById("confirmarMV");
    var nomUsuario = document.getElementById("UsuarioMaqVirtuales");

    if (nomUsuario.value.length <= 0) {
        esValido = false;
        nomUsuario.value = "";
        nomUsuario.style.borderColor = "#900C3F";
        $('#errorNombreUsuario').html('El campo nombre de usuario no debe estar vacio').show();
        setTimeout("$('#errorNombreUsuario').html('').hide('slow')", 6000);
        boton.disabled = true;
    } else {
        nomUsuario.style.borderColor = "#ccc";
        $('#errorNombreUsuario').html('').hide();
        boton.disabled = false;
    }
    return esValido;
}

//Función para validar dirección IP
function validarIP() {
    var esValido = true;
    var boton = document.getElementById("confirmarMV");
    var ip = document.getElementById("DireccionIPMaqVirtuales");
    //Validación para el campo de texto nombre de Máquina virtual
    if (ip.value.length <= 0) {
        esValido = false;
        ip.style.borderColor = "#900C3F";
        $('#errorIpMv').html('El campo IP no debe estar vacio').show();
        setTimeout("$('#errorIpMv').html('').hide('slow')", 6000);
        boton.disabled = true;
    } else if (!/^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$/.test(ip.value)) {
        esValido = false;
        ip.value = "";
        ip.style.borderColor = "#900C3F";
        $('#errorIpMv').html('La dirección IP es incorrecta').show();
        setTimeout("$('#errorIpMv').html('').hide('slow')", 6000);
        boton.disabled = true;
    } else {
        ip.style.borderColor = "#ccc";
        $('#errorIpMv').html('').hide();
        boton.disabled = false;
    }
    return esValido;
}

//Función para validar disco duro y Ram
function validarDisco() {
    var esValido = true;
    var boton = document.getElementById("confirmarMV");
    var disco = document.getElementById("DiscoMaqVirtuales");
    //Validación para disco de Máquina virtual
    if (disco.value.length <= 0) {
        esValido = false;
        disco.value = "";
        disco.style.borderColor = "#900C3F";
        $('#errorDiscoMv').html('El campo Disco Duro no debe estar vacio').show();
        setTimeout("$('#errorDiscoMv').html('').hide('slow')", 6000);
        boton.disabled = true;
    } else if (disco.value <1) {
        esValido = false;
        disco.value = "";
        disco.style.borderColor = "#900C3F";
        $('#errorDiscoMv').html('El rango de Disco Duro es de 1 a 10000').show();
        setTimeout("$('#errorDiscoMv').html('').hide('slow')", 6000);
        boton.disabled = true;
    } else if (disco.value > 10000) {
        esValido = false;
        disco.value = "";
        disco.style.borderColor = "#900C3F";
        $('#errorDiscoMv').html('No se puede asignar mas de 10000 de Disco Duro').show();
        setTimeout("$('#errorDiscoMv').html('').hide('slow')", 6000);
        boton.disabled = true;
    }else{
        disco.style.borderColor = "#ccc";
        $('#errorDiscoMv').html('').hide();
        boton.disabled = false;
    }

    return esValido;
}

//Función para validar disco duro y Ram
function validarRam() {
    var esValido = true;
    var boton = document.getElementById("confirmarMV");
    var ram = document.getElementById("RamMaqVirtuales");
    //Validar memoria Ram
    if (ram.value.length <= 0) {
        esValido = false;
        ram.value = "";
        ram.style.borderColor = "#900C3F";
        $('#errorRamMv').html('El campo RAM no debe estar vacio').show();
        setTimeout("$('#errorRamMv').html('').hide('slow')", 6000);
        boton.disabled = true;
    } else if (ram.value < 1) {
        esValido = false;
        ram.value = "";
        ram.style.borderColor = "#900C3F";
        $('#errorRamMv').html('El rango de Memoria Ram es de 1 a 1000 GB').show();
        setTimeout("$('#errorRamMv').html('').hide('slow')", 6000);
        boton.disabled = true;
    } else if (ram.value > 1000) {
        esValido = false;
        ram.value = "";
        ram.style.borderColor = "#900C3F";
        $('#errorRamMv').html('No se puede asignar mas de 1000 de Memoria Ram').show();
        setTimeout("$('#errorRamMv').html('').hide('slow')", 6000);
        boton.disabled = true;
    } else {
        ram.style.borderColor = "#ccc";
        $('#errorRamMv').html('').hide();
        boton.disabled = false;
    }
    return esValido;
}

///Función para validar los combobox de maquinas virtuales
function validarCmbMV() {
    var esValido = true;
    var boton = document.getElementById("confirmarMV");
    var cmbSO = document.getElementById("IdSistOperativos");
    var cmbEs = document.getElementById("PropositoMaqVirtuales");
    //Validación para el combobox de SO
    if (cmbSO.value == "") {
        esValido = false;
        cmbSO.style.borderColor = "#900C3F";
        $('#errorCmbSo').html('Debe seleccionar una opción').show();
        setTimeout("$('#errorCmbSo').html('').hide('slow')", 6000);
        boton.disabled = true;
    } else {
        cmbSO.style.borderColor = "#ccc";
        $('#errorCmbSo').html('').hide();
        boton.disabled = false;
    }

    //Validación para el combobox de estados
    if (cmbEs.value == "") {
        esValido = false;
        cmbEs.style.borderColor = "#900C3F";
        $('#errorCmbEstado').html('Debe seleccionar una opción').show();
        setTimeout("$('#errorCmbEstado').html('').hide('slow')", 6000);
        boton.disabled = true;
    } else {
        cmbEs.style.borderColor = "#ccc";
        $('#errorCmbEstado').html('').hide();
        boton.disabled = false;
    }
    return esValido;
}

/* --------------------------------------SECCIÓN PARA MENSAJES DE TOOLTIPS---------------------------------*/
//Mensajes para los tooltips
function mensajesTooltips() {
    document.getElementById("NombreMaqVirtuales").title = "Máximo 80 caracteres en Mayúscula.\n Caracteres especiales permitidos - / _ .";
    document.getElementById("UsuarioMaqVirtuales").title = "Máximo 80 caracteres.\n Caracteres especiales permitidos - / _ .";
    document.getElementById("DireccionIPMaqVirtuales").title = "Cuadro de texto para IpV4.\n Formato: 000.000.000.000";
    document.getElementById("DiscoMaqVirtuales").title = "Solo Números. Rango de 1 a 10000";
    document.getElementById("RamMaqVirtuales").title = "Solo Números. Rango de 1 a 100GB";
    document.getElementById("DescripcionMaqVirtuales").title = "Máximo 150 caracteres.\n Caracteres especiales permitidos - / _ .";
}