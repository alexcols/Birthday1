# ���������� �� ������������� ��������� ��������������

## ������ � EF

- ���������� ��������

dotnet ef migrations add UpX --project ../src/backend/Birthday.Infrastructure/Birthday.Infrastructure.csproj  --startup-project ../src/backend/Birthday.PublicApi/Birthday.PublicApi.csproj
dotnet ef  migrations add UpX --project .\src\backend\Birthday.Infrastructure\Birthday.Infrastructure.csproj --startup-project .\src\backend\Birthday.PublicAPI\Birthday.PublicAPI.csproj

- ���������� ��

dotnet ef database update --project ./src/backend/Birthday.Infrastructure/Birthday.Infrastructure.csproj  --startup-project ./src/backend/Birthday.PublicApi/Birthday.PublicApi.csproj


## ������������� �� � *docker*-����������

- ��� ������� ���������� � �� ��������� � �������� ��������:

	`docker-compose pull`

	`docker-compose up -d`
- ������� **persisted volumes** ������������� ������ ��� ������ � �� (PostgreSQL \ MS SQL Server), ������� �������� �� ��������� �����:
	PostgreSQL
		- 5432:5432
  
		credentials user/password123
	
  
- ����������: 
  
  `docker-compose down` 
  
  (����� ��������, �� **persisted volumes** ��������).