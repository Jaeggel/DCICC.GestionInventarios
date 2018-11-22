using DCICC.GestionInventarios.AccesoDatos.InventariosBD;
using DCICC.GestionInventarios.Controllers;
using DCICC.GestionInventarios.Models.MensajesInventarios;
using log4net;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace DCICC.GestionInventarios.QR
{
    public class GeneracionCQR
    {
        //Instancia para la utilización de LOGS en la clase GeneracionCQR
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public string GenerarIdCodigoQR()
        {
            string mensajesCQR = string.Empty;
            string idCQR = "DCICC.CQR";
            MensajesCQR msjCQR = new MensajesCQR();
            try
            {
                ActivosAccDatos objCQRAccDatos = new ActivosAccDatos(LoginController.nickUsuarioSesion);
                msjCQR = objCQRAccDatos.ObtenerIdCQR();
                if (msjCQR.OperacionExitosa)
                {
                    if(msjCQR.ListaObjetoInventarios.Count==0)
                    {
                        idCQR +="1";
                    }
                    else
                    {
                        idCQR +="" + 1;
                    }
                    mensajesCQR = "La lista de los Id de CQR ha sido obtenido exitosamente.";
                    Logs.Info(mensajesCQR);
                }
                else
                {
                    mensajesCQR = "No se ha podido obtener los CQR: " + msjCQR.MensajeError;
                }
            }
            catch (Exception e)
            {
                Logs.Error(mensajesCQR + ": " + e.Message);
                return null;
            }
            return idCQR;
        }
        ///
        /// <summary>
        /// Método para generar el código QR en formato BitMap
        /// </summary>
        public Bitmap GenerarCodigoQR(string idCqr)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(idCqr, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(6);
            return qrCodeImage;
        }
        /// <summary>
        /// Método para transformar el código QR de Bitmap a byte[]
        /// </summary>
        /// <param name="datosQR"></param>
        /// <returns></returns>
        public byte[] GenQRBytes(Bitmap datosQR)
        {
            using (var memoryStream = new MemoryStream())
            {
                datosQR.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                return memoryStream.ToArray();
            }
        }
        /// <summary>
        /// Método para transformar el código QR de byte[] a Bitmap
        /// </summary>
        /// <param name="datosQR"></param>
        /// <returns></returns>
        public Bitmap GenQRBitmap(byte[] datosQR)
        {
            Bitmap bmp;
            using (var ms = new MemoryStream(datosQR))
            {
                bmp = new Bitmap(ms);
            }
            return bmp;
        }
    }
}