using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace FFHRRequestSystem.Services.VietN
{
    public class EmailService
    {
        private readonly SmtpSettings _smtpSettings;

        public EmailService(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }

        public async Task SendEmailWithAttachmentAsync(string toEmail, string subject, string htmlBody, byte[] attachmentData, string attachmentFileName)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_smtpSettings.FromName, _smtpSettings.FromEmail));
            message.To.Add(new MailboxAddress("", toEmail));
            message.Subject = subject;

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = htmlBody
            };

            // Add attachment
            bodyBuilder.Attachments.Add(attachmentFileName, attachmentData);

            message.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();
            try
            {
                await client.ConnectAsync(_smtpSettings.Host, _smtpSettings.Port, 
                    _smtpSettings.UseStartTls ? SecureSocketOptions.StartTls : SecureSocketOptions.None);
                
                await client.AuthenticateAsync(_smtpSettings.User, _smtpSettings.Pass);
                await client.SendAsync(message);
            }
            finally
            {
                await client.DisconnectAsync(true);
            }
        }

        public string GenerateExportEmailHtml(string userName, int recordCount)
        {
            return $@"
<!DOCTYPE html>
<html lang='en'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <style>
        body {{
            font-family: 'Inter', 'Segoe UI', sans-serif;
            margin: 0;
            padding: 0;
            background-color: #f5f5f5;
        }}
        .email-container {{
            max-width: 600px;
            margin: 40px auto;
            background: white;
            border-radius: 12px;
            overflow: hidden;
            box-shadow: 0 4px 20px rgba(0, 0, 0, 0.1);
        }}
        .header {{
            background: linear-gradient(135deg, #FFD54F 0%, #FFC107 100%);
            padding: 40px 30px;
            text-align: center;
            color: white;
        }}
        .header-icon {{
            width: 80px;
            height: 80px;
            background: rgba(255, 255, 255, 0.2);
            border-radius: 50%;
            display: inline-flex;
            align-items: center;
            justify-content: center;
            margin-bottom: 20px;
            font-size: 40px;
        }}
        .header h1 {{
            margin: 0;
            font-size: 28px;
            font-weight: 700;
        }}
        .content {{
            padding: 40px 30px;
        }}
        .greeting {{
            font-size: 18px;
            color: #333;
            margin-bottom: 20px;
        }}
        .message {{
            color: #666;
            line-height: 1.6;
            margin-bottom: 30px;
        }}
        .info-box {{
            background: linear-gradient(135deg, #FFF8E1 0%, #FFECB3 100%);
            border-left: 4px solid #FFC107;
            padding: 20px;
            border-radius: 8px;
            margin: 20px 0;
        }}
        .info-row {{
            display: flex;
            justify-content: space-between;
            margin-bottom: 10px;
        }}
        .info-row:last-child {{
            margin-bottom: 0;
        }}
        .info-label {{
            color: #666;
            font-weight: 600;
        }}
        .info-value {{
            color: #333;
            font-weight: 700;
        }}
        .footer {{
            background: #f8f8f8;
            padding: 30px;
            text-align: center;
            color: #999;
            font-size: 14px;
        }}
        .button {{
            display: inline-block;
            padding: 14px 28px;
            background: linear-gradient(135deg, #FFD54F 0%, #FFC107 100%);
            color: white;
            text-decoration: none;
            border-radius: 8px;
            font-weight: 600;
            margin: 20px 0;
        }}
    </style>
</head>
<body>
    <div class='email-container'>
        <div class='header'>
            <div class='header-icon'>📊</div>
            <h1>Export Completed Successfully</h1>
        </div>
        
        <div class='content'>
            <p class='greeting'>Hi {userName},</p>
            
            <p class='message'>
                Your data export has been completed successfully! The Excel file containing all ticket processing records is attached to this email.
            </p>
            
            <div class='info-box'>
                <div class='info-row'>
                    <span class='info-label'>Export Date:</span>
                    <span class='info-value'>{DateTime.Now:dd/MM/yyyy HH:mm:ss}</span>
                </div>
                <div class='info-row'>
                    <span class='info-label'>Total Records:</span>
                    <span class='info-value'>{recordCount} records</span>
                </div>
                <div class='info-row'>
                    <span class='info-label'>File Format:</span>
                    <span class='info-value'>Microsoft Excel (.xlsx)</span>
                </div>
            </div>
            
            <p class='message'>
                The attached Excel file contains complete information about all ticket processing records including codes, references, actions, priorities, statuses, and more.
            </p>
            
            <p class='message'>
                If you have any questions or need assistance, please don't hesitate to contact our support team.
            </p>
        </div>
        
        <div class='footer'>
            <p>© {DateTime.Now.Year} Ticket Processing System. All rights reserved.</p>
            <p>This is an automated message. Please do not reply to this email.</p>
        </div>
    </div>
</body>
</html>";
        }
    }
}
