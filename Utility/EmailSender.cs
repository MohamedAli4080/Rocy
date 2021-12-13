using Mailjet.Client;
using Mailjet.Client.Resources;
using System;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Rocy.Utility
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Execute(email, subject, htmlMessage);
        }
        public async Task Execute(string email, string subject, string htmlMessage)
        {
            MailjetClient client = new MailjetClient("b22b9085d363a62deb73fa13b120ce1e","5c3eb5beae5d48d363973c058dc71ab2")
            {
                
            };
            
            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource,
            }
             .Property(Send.Messages, new JArray {
     new JObject {
      {
       "From",
       new JObject {
        {"Email", "mohamedali4080@gmail.com"},
        {"Name", "MOHAMED"}
       }
      }, {
       "To",
       new JArray {
        new JObject {
         {
          "Email",
         email
         }, {
          "Name",
          "Target"
         }
        }
       }
      }, {
       "Subject",
       subject
      }, {
       "HTMLPart",
       htmlMessage
      }, 
     }
             });
           // MailjetResponse response = await client.PostAsync(request);
              await client.PostAsync(request);
        }
    }
}