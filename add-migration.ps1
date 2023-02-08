dotnet ef migrations add Initialize_$(Get-Date -Format "ddMMyyyHHmmss") --project StateManagments.Models --startup-project StateManagements.Session_ --output-dir Migrations

dotnet ef database update --project StateManagments.Models --startup-project StateManagements.Session_