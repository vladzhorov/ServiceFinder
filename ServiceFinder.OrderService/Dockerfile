# Установка базового образа
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Установка рабочей директории внутри образа
WORKDIR /app

# Копирование файлов проекта и восстановление зависимостей
COPY ./ServiceFinder.OrderService/ServiceFinder.OrderService.API/ServiceFinder.OrderService.API.csproj ./ServiceFinder.OrderService.API/
COPY ./ServiceFinder.OrderService/ServiceFinder.OrderService.Application/ServiceFinder.OrderService.Application.csproj ./ServiceFinder.OrderService.Application/
COPY ./ServiceFinder.OrderService/ServiceFinder.OrderService.Domain/ServiceFinder.OrderService.Domain.csproj ./ServiceFinder.OrderService.Domain/
COPY ./ServiceFinder.OrderService/ServiceFinder.OrderService.Infrastructure/ServiceFinder.OrderService.Infrastructure.csproj ./ServiceFinder.OrderService.Infrastructure/
COPY ./Tests/ServiceFinder.OrderService.UnitTest/ServiceFinder.OrderService.UnitTest.csproj ./Tests/ServiceFinder.OrderService.UnitTest/

# Восстановление зависимостей для проекта
RUN dotnet restore ./ServiceFinder.OrderService/ServiceFinder.OrderService.API/ServiceFinder.OrderService.API.csproj

# Копирование всех файлов проекта
COPY ./ServiceFinder.OrderService/ServiceFinder.OrderService.API/ ./ServiceFinder.OrderService.API/
COPY ./ServiceFinder.OrderService/ServiceFinder.OrderService.Application/ ./ServiceFinder.OrderService.Application/
COPY ./ServiceFinder.OrderService/ServiceFinder.OrderService.Domain/ ./ServiceFinder.OrderService.Domain/
COPY ./ServiceFinder.OrderService/ServiceFinder.OrderService.Infrastructure/ ./ServiceFinder.OrderService.Infrastructure/
COPY ./Tests/ServiceFinder.OrderService.UnitTest/ ./Tests/ServiceFinder.OrderService.UnitTest/

# Сборка проекта
RUN dotnet build ./ServiceFinder.OrderService/ServiceFinder.OrderService.API/ServiceFinder.OrderService.API.csproj -c Release -o /app/build

# Определение точки входа для контейнера
CMD ["dotnet", "run", "--project", "./ServiceFinder.OrderService/ServiceFinder.OrderService.API/ServiceFinder.OrderService.API.csproj"]
