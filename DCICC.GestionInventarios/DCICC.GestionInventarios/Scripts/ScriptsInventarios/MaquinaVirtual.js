var url_idioma = obtenerIdioma();
var url_metodo;
var datosMaquinasV;
var cmbSO;
var idMaquinaV;
var propositos;

function obtenerMaquinaV(url) {
    url_metodo = url;
    console.log(url);
    //Método ajax para traer las marcas de la base de datos
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            console.log(data);
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

function obtenerSO(url) {
    console.log(url);
    //Método ajax para traer las marcas de la base de datos
    $.ajax({
        dataType: 'json',
        url: url,
        type: 'post',
        success: function (data) {
            console.log(data);
            cmbSO = data;
            cargarSOCmb();
        }
    });
}

function obtenerPropositos() {

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
            '</td><td>' + datosMaquinasV[i].NombreSistOperativo +
            '</td><td>' + datosMaquinasV[i].DireccionIPMaqVirtuales +
            '</td><td>' + datosMaquinasV[i].DiscoMaqVirtuales +
            '</td><td>' + datosMaquinasV[i].RamMaqVirtuales +
            '</td><td>' + datosMaquinasV[i].DescripcionMaqVirtuales;

        if (datosMaquinasV[i].HabilitadoMaqVirtuales) {
            str += '</td><td> Habilitado';
        } else {
            str += '</td><td> Deshabilitado';
        }
        str += '</td><td><div class="text-center"><div class="col-md-12 col-sm-12 col-xs-12">' +
            '<button type="button" class="btn btn-info text-center" data-toggle="modal" data-target="#ModificarMaquinaV" onclick = "formUpdateMaquinaV(' + datosMaquinasV[i].IdMaqVirtuales + ');"> <strong><i class="fa fa-pencil-square-o"></i></strong></button> ' +
            '</div></div>' +
            '</td><td><div class=" text-center"><div class="col-md-12 col-sm-12 col-xs-12">' +
            '<button type = "button" class="btn btn-danger text-center" > <strong><i class="fa fa-times-circle"></i></strong></button> ' +
            '</div></div></td></tr>';

    };
    str += '</tbody></table>';
    $("#tablaModificarMaquinaV").html(str);
}

//Función para cargar el combobox de Categorias
function cargarSOCmb() {
    var str = '<select id="IdSistOperativo" class="form-control" name="IdSistOperativo" required>';
    str += '<option value="">Escoga una opción...</option>';
    for (var i = 0; i < cmbSO.length; i++) {
        str += '<option value="' + cmbSO[i].IdSistOperativo + '">' + cmbSO[i].NombreSistOperativo + '</option>';
    };
    str += '</select>';
    $("#cargarSO").html(str);
}

//Función para setear los valores en los inputs
function formUpdateMaquinaV(idMV) {
    console.log(idMV);
    idMaquinaV = idMV;
    for (var i = 0; i < datosMaquinasV.length; i++) {

        if (datosMaquinasV[i].IdMaqVirtuales == idMV) {
            //Métodos para setear los valores a modificar
            var element = document.getElementById("IdSistOperativo");
            element.value = datosMaquinasV[i].IdSistOperativo;
            document.getElementById("NombreMaqVirtuales").value = datosMaquinasV[i].NombreMaqVirtuales;
            document.getElementById("UsuarioMaqVirtuales").value = datosMaquinasV[i].UsuarioMaqVirtuales;
            document.getElementById("PropositoMaqVirtuales").value = datosMaquinasV[i].PropositoMaqVirtuales;
            document.getElementById("DireccionIPMaqVirtuales").value = datosMaquinasV[i].DireccionIPMaqVirtuales;
            document.getElementById("DiscoMaqVirtuales").value = datosMaquinasV[i].DiscoMaqVirtuales;
            document.getElementById("RamMaqVirtuales").value = datosMaquinasV[i].RamMaqVirtuales;
            document.getElementById("DescripcionMaqVirtuales").value = datosMaquinasV[i].DescripcionMaqVirtuales;


            //Método para el check del update de Usuarios
            var valor = datosMaquinasV[i].HabilitadoMaqVirtuales;
            var estado = $('#HabilitadoMaqVirtuales').prop('checked');
            if (estado && valor == false) {
                document.getElementById("HabilitadoMaqVirtuales").click();
            }
            if (estado == false && valor == true) {
                document.getElementById("HabilitadoMaqVirtuales").click();
            }
        };
    };
}

//Función para modificar el Tipo de activo especificado
function modificarMaquinaV(url_modificar) {
    console.log(url_modificar);
    var cmbSO = document.getElementById("IdCategoriaActivo");
    var idSO = cmbSO.options[cmbSO.selectedIndex].value;
    var nombreMV= document.getElementById("NombreMaqVirtuales").value = datosMaquinasV[i].NombreMaqVirtuales;
    var usuarioMV= document.getElementById("UsuarioMaqVirtuales").value = datosMaquinasV[i].UsuarioMaqVirtuales;
    var propositoMV= document.getElementById("PropositoMaqVirtuales").value = datosMaquinasV[i].PropositoMaqVirtuales;
    var direccionIP= document.getElementById("DireccionIPMaqVirtuales").value = datosMaquinasV[i].DireccionIPMaqVirtuales;
    var disco= document.getElementById("DiscoMaqVirtuales").value = datosMaquinasV[i].DiscoMaqVirtuales;
    var ram= document.getElementById("RamMaqVirtuales").value = datosMaquinasV[i].RamMaqVirtuales;
    var descripcion= document.getElementById("DescripcionMaqVirtuales").value = datosMaquinasV[i].DescripcionMaqVirtuales;
    var habilitadoMV = $('#HabilitadoMaqVirtuales').prop('checked');

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
            //Método ajax para modificar el usuario de la base de datos
            $.ajax({
                data: { "IdMaqVirtuales": idMaquinaV, "IdSistOperativo": idSO, "UsuarioMaqVirtuales": usuarioMV, "NombreMaqVirtuales": nombreMV, "PropositoMaqVirtuales": propositoMV, "DireccionIPMaqVirtuales": direccionIP, "DiscoMaqVirtuales": disco, "RamMaqVirtuales": ram, "DescripcionMaqVirtuales": descripcion, "HabilitadoMaqVirtuales": habilitadoMV },
                url: url_modificar,
                type: 'post',
                success: function () {
                    $('#ModificarTipoActivo').modal('hide');
                    showNotify("Actualización exitosa", 'La Máquina Virtual se ha modificado correctamente', "success");
                    obtenerUsuariosUpdate(url_Usu);
                }, error: function () {
                    $('#ModificarTipoActivo').modal('hide');
                    showNotify("Error en la Actualización", 'No se ha podido modificar la Máquina Virtual', "error");
                }
            });

        } else {
            $('#ModificarTipoActivo').modal('hide');
        }
    });
}

//Función para evitar nombres de máquinas virtuales repetidos
function comprobarNombre(nombre) {
    nombre = nombre.toLowerCase();
    var comprobar = false;
    for (var i = 0; i < datosMaquinasV.length; i++) {
        if ((datosMaquinasV[i].NombreMaqVirtuales).toLowerCase() == nombre) {
            comprobar = true;
        }
    }

    console.log(comprobar);
    if (comprobar == true) {
        document.getElementById("NombreMarca").setCustomValidity("El nombre de la Máquina Virtual: " + nombre + " ya existe");
    } else {
        document.getElementById("NombreMarca").setCustomValidity("");
    }
}