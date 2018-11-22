/*Sistema de Gestión de inventarios y ticketing para soporte Técnico
Autores: Andres Chisaguano - Joel Ludeña
Descripción: Script para las funciones correspondientes al Login*/

//Método para la recuperación de contraseña
function enviarMailPass(url) {
    swal({
        title: 'Ingresar el Correo Electrónico',
        input: 'text',
        inputAttributes: {
            autocapitalize: 'off'
        },
        showCancelButton: true,
        confirmButtonText: 'Confirmar',
        cancelButtonText: 'Cancelar',
        showLoaderOnConfirm: true,
        preConfirm: (infoCorreo) => {
            $.ajax({
                data: '{infoCorreo: "' + infoCorreo + '"}',
                contentType: "application/json; charset=utf-8",
                type: "POST",
                dataType: "json",
                url: url,
                beforeSend: function () {
                    NProgress.start();                   
                },
                success: function (data) {
                    NProgress.done();
                    if (data === true) {
                        swal(
                            'Envío Exitoso',
                            'Las credenciales han sido enviadas a su correo electrónico.',
                            'success'
                        );
                    } else {
                        swal(
                            'Error en el Envío',
                            'El correo electrónico no corresponde a ningún usuario registrado.',
                            'error'
                        );
                    }
                },
                error: function () {
                    swal(
                        'Error en el Envío',
                        'No se ha podido enviar el correo electrónico.',
                        'error'
                    );
                }
            });
        },
        allowOutsideClick: () => !swal.isLoading()
    });
}