using Mailjet.Client;
using Newtonsoft.Json.Linq;
using Mailjet.Client.Resources;

namespace Event_Registration_System.Repository.Serveses
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;
        private readonly MailjetClient _client;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
            _client = new MailjetClient(
                _configuration["Mailjet:ApiKey"],
                _configuration["Mailjet:SecretKey"]);
        }


        public async Task<bool> SendEmailAsync(string toEmail, string toName, string htmlPart)
        {
            var request = new MailjetRequest
            {
                Resource = Send.Resource,
            }
            .Property(Send.FromEmail, "abedradwan84.5@gmail.com")
            .Property(Send.FromName, "Events Manager")
            .Property(Send.Subject, "Email Approved")
            .Property(Send.TextPart, "You Email Have Been Approved")
            .Property(Send.HtmlPart, htmlPart)
            .Property(Send.Recipients, new JArray {
            new JObject {
                {"Email", toEmail},
                {"Name", toName}
            }
            });


            MailjetResponse response = await _client.PostAsync(request);
            return response.IsSuccessStatusCode;
        }
    }
}
