# ���������� �� ������������� ��������� ��������������

## ������ � EF

- ���������� ��������

dotnet ef migrations add AddFields --project ../src/backend/Advertisement.Infrastructure/Advertisement.Infrastructure.csproj  --startup-project ../src/backend/Advertisement.PublicApi/Advertisement.PublicApi.csproj
Build started...

- ���������� ��

dotnet ef database update --project ./src/backend/Advertisement.Infrastructure/Advertisement.Infrastructure.csproj  --startup-project ./src/backend/Advertisement.PublicApi/Advertisement.
PublicApi.csproj


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