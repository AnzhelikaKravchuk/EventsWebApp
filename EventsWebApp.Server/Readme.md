## Add configuration options for SMTP server
```powershell
$smtp_email="Your Email Here"
$smtp_password="Your Password Here"
dotnet user-secrets set "SMTPServer" "Email=$smtp_email; Password=$smtp_password;"