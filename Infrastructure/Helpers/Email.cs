
using System.Net;
using System.Net.Mail;

namespace AnniesTech.Infrastructure.Helpers
{
    public class Email
    {
        public void Enviar(string correo, string token)
        {
            Correo(correo,token);
        }

        private void Correo(string correo_receptor, string token)
        {
            /*
            string correo_emisor = "juanacbu128@gmail.com";
            string clave_emisor = "JN/1193427712+"; 

            MailAddress receptor = new(correo_receptor);
            MailAddress emisor = new(correo_emisor);

            MailMessage email = new(emisor,receptor);
            email.Subject = "AnniesTech: Activacion de cuenta";
            email.Body = @"<!DOCTYPE html>
                            <html>
                            <head>
                            <title>Document</title>
                            </head>
                            <body>
                                <h2>Activacio de cuenta</h2>
                                <p>Para activar su cuenta haga click en el siguiente enlace <a href='http://localhost:5105/Cuenta/Token?valor="+ token +"'>Activar cuenta</a></p></body></html>";

            SmtpClient smtp = new();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.Credentials = new NetworkCredential(correo_emisor,clave_emisor);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.EnableSsl = true;

            try{
                smtp.Send(email);

            }catch(Exception ex){
                Console.WriteLine(ex.Message+"Holsssssssssss");
            }
            
        }*/
            string correo_emisor = "jaacero@uts.edu.co";
            string clave_emisor = "Jn1193427712+"; 

            MailAddress receptor = new(correo_receptor);
            MailAddress emisor = new(correo_emisor);

            MailMessage email = new(emisor,receptor);
        email.Subject = "AnniesTech: Activacion de cuenta";
        email.Body = @"<!DOCTYPE html>
                            <html>
                            <head>
                            <title>Document</title>
                            </head>
                            <body>
                                <h2>Activacio de cuenta</h2>
                                <p>Para activar su cuenta haga click en el siguiente enlace <a href='http://localhost:5105/Cuenta/Token?valor="+ token +"'>Activar cuenta</a></p></body></html>";
        email.IsBodyHtml = true;

        SmtpClient smtp = new SmtpClient("smtp.office365.com", 587);
        smtp.EnableSsl = true;
        smtp.UseDefaultCredentials = false;
        smtp.Credentials = new NetworkCredential(correo_emisor,clave_emisor);

        try
        {
            smtp.Send(email);
            Console.WriteLine("Correo enviado correctamente.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al enviar el correo: " + ex.Message);
        }
        finally
        {
            email.Dispose(); // Liberar recursos
        }
    }
        
    }
}