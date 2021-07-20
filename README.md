# Инструкция по развертыванию локальной инфраструктуры

## Работа с EF

- Добавление миграции

dotnet ef migrations add AddFields --project ../src/backend/Advertisement.Infrastructure/Advertisement.Infrastructure.csproj  --startup-project ../src/backend/Advertisement.PublicApi/Advertisement.PublicApi.csproj
Build started...

- Обновление БД

dotnet ef database update --project ./src/backend/Advertisement.Infrastructure/Advertisement.Infrastructure.csproj  --startup-project ./src/backend/Advertisement.PublicApi/Advertisement.
PublicApi.csproj


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