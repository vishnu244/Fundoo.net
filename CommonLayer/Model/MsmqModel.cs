using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace CommonLayer.Model
{
    public class MsmqModel
    {
        MessageQueue message = new MessageQueue();
        public void sendData2Queue(string token)
        {
            message.Path = @".\private$\token";
            if (!(MessageQueue.Exists(message.Path)))
            {
                MessageQueue.Create(message.Path);
            }

            message.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            message.ReceiveCompleted += Message_ReceiveCompleted;
            message.Send(token);
            message.BeginReceive();
            message.Close();
        }

        public void Message_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            var msg = message.EndReceive(e.AsyncResult);
            string token = msg.Body.ToString();
            string Subject = "Fundoo Notes Reset Token";
           
            string Body = "Your Password Reset Token is :\n" +token;
            var SMTP = new SmtpClient("Smtp.gmail.com")
            {
                Port = 587,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("vishnufundoo@gmail.com", "ydezbbglhfytcnip"),
                EnableSsl = true

            };
            SMTP.Send("vishnufundoo@gmail.com", "vishnufundoo@gmail.com", Subject, Body);
            message.BeginReceive();
        }
    }
}
