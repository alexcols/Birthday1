# ���������� �� ������������� ��������� ��������������


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

## ������ � EF
--�������1
� ������� Windows

- ���������� ��������

dotnet ef  migrations add UpX1 --project .\src\backend\Birthday.Infrastructure\Birthday.Infrastructure.csproj --startup-project .\src\backend\Birthday.PublicAPI\Birthday.PublicAPI.csproj

- ���������� ��

dotnet ef database update --project .\src\backend\Birthday.Infrastructure\Birthday.Infrastructure.csproj --startup-project .\src\backend\Birthday.PublicAPI\Birthday.PublicAPI.csproj

--�������2
� Package Manager ������� VS:

- ���������� ��������

Add-Migration UpX1

- ���������� ��

Update-Database