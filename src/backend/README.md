# Инструкция по развертыванию локальной инфраструктуры


## Развертывание БД в *docker*-контейнере

- Для запуска контейнера с БД выполнить в корневом каталоге:

	`docker-compose pull`

	`docker-compose up -d`
- Создает **persisted volumes** персистентный сервис для работы с БД (PostgreSQL \ MS SQL Server), который биндится на дефолтные порты:
	PostgreSQL
		- 5432:5432
  
		credentials user/password123
	
  
- Отключение: 
  
  `docker-compose down` 
  
  (порты свободны, но **persisted volumes** остаются).

## Работа с EF
--Вариант1
В консоли Windows

- Добавление миграции

dotnet ef  migrations add UpX1 --project .\src\backend\Birthday.Infrastructure\Birthday.Infrastructure.csproj --startup-project .\src\backend\Birthday.PublicAPI\Birthday.PublicAPI.csproj

- Обновление БД

dotnet ef database update --project .\src\backend\Birthday.Infrastructure\Birthday.Infrastructure.csproj --startup-project .\src\backend\Birthday.PublicAPI\Birthday.PublicAPI.csproj

--Вариант2
в Package Manager консоли VS:

- Добавление миграции

Add-Migration UpX1

- Обновление БД

Update-Database