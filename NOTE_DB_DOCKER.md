# DB Docker note

Use these commands when setting up the project on another machine.

## 1. Run SQL Server with Docker

```powershell
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=StrongPass!123" -e "MSSQL_PID=developer" -p 1433:1433 --name sqlserver -d mcr.microsoft.com/mssql/server:2022-latest
```

If the container already exists, start it:

```powershell
docker start sqlserver
```

Check container status:

```powershell
docker ps
```

## 2. Create database schema from EF migrations

Run from the repository root:

```powershell
cd BackendAPI\BackendAPI
dotnet tool install --global dotnet-ef --version 6.0.10
dotnet ef database update --connection "Server=localhost,1433;Database=BackendAPI_Rookies_Final;User Id=sa;Password=StrongPass!123;TrustServerCertificate=True;MultipleActiveResultSets=true"
cd ..\..
```

If `dotnet-ef` is already installed, the install command may fail. In that case, continue with the `dotnet ef database update` command.

## 3. Seed sample data

Run from the repository root after the schema is created:

```powershell
$seed = Join-Path $env:TEMP 'database_seed_backendapi_rookies_final.sql'
Get-Content database.sql | Select-Object -Skip 280 | Set-Content -Encoding UTF8 $seed
docker cp $seed sqlserver:/tmp/database_seed_backendapi_rookies_final.sql
docker exec sqlserver /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "StrongPass!123" -C -d BackendAPI_Rookies_Final -i /tmp/database_seed_backendapi_rookies_final.sql
```

## 4. DBeaver connection

```text
Host: localhost
Port: 1433
Database: BackendAPI_Rookies_Final
Authentication: SQL Server Authentication
Username: sa
Password: StrongPass!123
Trust Server Certificate: checked
```

Do not use Windows Authentication for this Docker SQL Server container.

## 5. Backend connection string

Use this connection string in `BackendAPI/BackendAPI/appsettings.json` if the backend connects to Docker SQL Server:

```json
"UserDbContextConnection": "Server=localhost,1433;Database=BackendAPI_Rookies_Final;User Id=sa;Password=StrongPass!123;TrustServerCertificate=True;MultipleActiveResultSets=true"
```

## 6. Admin dashboard login

Default admin user created for local development:

```text
Email: admin@gmail.com
Password: Admin@123
Role: Admin
```
