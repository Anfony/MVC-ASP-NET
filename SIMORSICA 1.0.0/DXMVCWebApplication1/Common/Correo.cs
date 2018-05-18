using System.Net.Mail;
using System.Threading.Tasks;

namespace DXMVCWebApplication1.Common
{
    public class Correo
    {
        private bool estado = true;
        private string mError = "";

        private string destinatario = "";
        private string asunto = "";
        private string mensaje = "";

        public Correo(string destinatario, string asunto, string mensaje)
        {
            estado = true;
            this.destinatario = destinatario;
            this.asunto = asunto;
            this.mensaje = mensaje;
        }

        /// <summary>
        /// Envía el correo electrónico al usuario, regresando true si el envío fue satisfactoriamente
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Enviar()
        {
            MailMessage correo = new MailMessage();
            SmtpClient protocolo = new SmtpClient();

            correo.To.Add(destinatario);
            correo.From = new MailAddress(ConstantesGlobales.EMAIL_SOPORTE, "Soporte SIMOSICA", System.Text.Encoding.UTF8);
            correo.Subject = asunto;
            correo.SubjectEncoding = System.Text.Encoding.UTF8;
            correo.Body = mensaje;
            correo.BodyEncoding = System.Text.Encoding.UTF8;
            correo.IsBodyHtml = false;

            protocolo.Credentials = new System.Net.NetworkCredential(ConstantesGlobales.EMAIL_SOPORTE, "admingadai");
            protocolo.Port = 587;
            protocolo.Host = "smtp.gmail.com";
            protocolo.EnableSsl = true;

            try
            {
                await protocolo.SendMailAsync(correo);
            }
            catch (SmtpException error)
            {
                estado = false;
                mError = error.Message.ToString();
            }

            return estado;
        }

        public string Error()
        {
            return mError;
        }
    }
}