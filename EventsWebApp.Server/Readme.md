## Connect to database
In appsettings.json in "DefaultConnection" field specify your sql server name in the field "Server=Your server Name"

## Seed data to database
```powershell
dotnet run seeddata
Ctrl+C

## Add configuration options for SMTP server
```powershell
$smtp_email="Your Email Here"
$smtp_password="Your Password Here"
dotnet user-secrets set "SMTPServer" "Email=$smtp_email; Password=$smtp_password;"

## Configure front-end environment
Inside folder EventsWebApp.client copy .env file, rename it to .env.local and specify your server host URL (Both should be https://localhost:7127 as default configuration)