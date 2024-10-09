## Prerequisites
* MSSQL server and database should be set up

## Configure back-end environment

Run in PowerShell
```powershell
cd EventsWebApp.Server
$smtp_email="<Your Email>"
$smtp_password="<Your Password>"
$client_url="<Client Url>"
$jwt_secret_key="<Your Jwt Secret>"
$sql_connection_string="Server=<Your Server Name>;Database=<Your Database Name>;Trusted_Connection=True;TrustServerCertificate=True"
dotnet user-secrets set "SMTPServerEmail" "$smtp_email"
dotnet user-secrets set "SMTPServerPassword" "$smtp_password" 
dotnet user-secrets set "ClientUrl" "$client_url"
dotnet user-secrets set "JWTSecretKey" "$jwt_secret_key"
dotnet user-secrets set "SqlConnectionString" "$sql_connection_string"
```
Replace \<Your Email> and \<Your Password> with credentials provided (or your Gmail email and app key). \<Your Jwt Secret> is an arbitrary key (for example: `S1u*p7e_r+S2e/c4r6e7t*0K/e7yS1u*p7e_r+S2e/c4r6e7t*0K/e7yS1u*p7e_r+S2e/c4r6e7t*0K/e7y`). \<Client Url> with url of your client app (it's https://localhost:5173 by default). Replace \<Your Database Name> and \<Your Server Name> with your MSSQL server and database name


Run in PowerShell:
```powershell
cd EventsWebApp.Server
dotnet run seeddata
```
Press Ctrl+C when script is finished

## Configure front-end environment
Fill fields in _EventsWebApp.client/.env_ (all fields should be https://localhost:7127 by default)
