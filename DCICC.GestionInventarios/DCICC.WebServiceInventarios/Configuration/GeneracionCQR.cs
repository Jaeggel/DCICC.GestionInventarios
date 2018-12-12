using log4net;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DCICC.WebServiceInventarios.Configuration
{
    public class GeneracionCQR
    {
        //Instancia para la utilización de LOGS en la clase ActivosController
        private static readonly ILog Logs = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        ///
        /// <summary>
        /// Método para generar el código QR en formato BitMap
        /// </summary>
        public Bitmap GenerarCodigoQR(string idCqr)
        {
            Bitmap qrCodeImage = null;    
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(idCqr, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            qrCodeImage = qrCode.GetGraphic(6);
            Logs.Info("El bitmap para el Código QR ha sido generado correctamente.");
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
                Logs.Info("Los bytes para el Código QR ha sido generado correctamente.");
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
