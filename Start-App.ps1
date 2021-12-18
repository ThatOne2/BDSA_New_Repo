$project = "$PSScriptRoot/Server/TrialProject.Server.csproj"

$password = New-Guid

Write-Host "Starting SQL Server"
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=$password" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest
$database = "BDSA"
$connectionString = "Server=localhost;Database=$database;User Id=sa;Password=$password;Trusted_Connection=False;Encrypt=False"

Write-Host "Configuring Connection String"
dotnet user-secrets init --project $project
dotnet user-secrets set "ConnectionStrings:BDSA" "$connectionString" --project $project

dotnet dev-certs https -ep %USERPROFILE%.aspnet\https\aspnetapp.pfx -p localhost
dotnet dev-certs https --trust

dotnet ef migrations add InitialMigration --project $project
dotnet ef database update --project $project
<# dotnet ef database update --project Server/TrialProject.Server.csproj#>
dotnet run --project $project