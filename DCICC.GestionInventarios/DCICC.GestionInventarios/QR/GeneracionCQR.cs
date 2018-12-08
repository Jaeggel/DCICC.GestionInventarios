using DCICC.GestionInventarios.AccesoDatos.InventariosBD;
using DCICC.GestionInventarios.Models.MensajesInventarios;
using log4net;
using QRCoder;
using System;
using System.Drawing;
using System.IO;

namespace DCICC.GestionInventarios.QR
{
    public class GeneracionCQR
    {
        //Instancia para la utilización de LOGS en la clase ActivosController
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Método para generar el Id del código QR
        /// </summary>
        /// <returns></returns>
        public string GenerarIdCodigoQR(string NickUsuarioSesion)
        {
            string mensajesCQR = string.Empty;
            string idCQR = "DCICC.CQR";
            try
            {
                MensajesCQR msjCQR = new MensajesCQR();
                ActivosAccDatos objCQRAccDatos = new ActivosAccDatos(NickUsuarioSesion);
                msjCQR = objCQRAccDatos.ObtenerIdCQR();
                if (msjCQR.OperacionExitosa)
                {
                    int sizeLst = msjCQR.ListaObjetoInventarios.Count;
                    if (sizeLst == 0)
                    {
                        idCQR += "1";
                    }
                    else
                    {
                        idCQR += sizeLst + 1;
                    }
                    Logs.Info("El ID para el código QR ha sido generado correctamente.");
                }
                else
                {
                    idCQR += "0";
                }
            }
            catch(Exception e)
            {
                Logs.Error(string.Format("No se ha podido generar el Id para el código QR: {0}",e.Message));
            }
            return idCQR;
        }
        ///
        /// <summary>
        /// Método para generar el código QR en formato BitMap
        /// </summary>
        public Bitmap GenerarCodigoQR(string idCqr)
        {
            Bitmap qrCodeImage = null;
            try
            {
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(idCqr, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                qrCodeImage = qrCode.GetGraphic(6);
                Logs.Info("El bitmap para el Código QR ha sido generado correctamente.");
            }
            catch(Exception e)
            {
                Logs.Error(string.Format("No se ha podido generar el Bitmap para el código QR: {0}",e.Message));
            }
            return qrCodeImage;
        }
        /// <summary>
        /// Método para transformar el código QR de Bitmap a byte[]
        /// </summary>
        /// <param name="datosQR"></param>
        /// <returns></returns>
        public byte[] GenQRBytes(Bitmap datosQR)
        {
            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    datosQR.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    Logs.Info("Los bytes para el Código QR ha sido generado correctamente.");
                    return memoryStream.ToArray();
                }
            }
            catch (Exception e)
            {
                Logs.Error(string.Format("No se han podido generar los bytes para el código QR: {0}",e.Message));
                return null;
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