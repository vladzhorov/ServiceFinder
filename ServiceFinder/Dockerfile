# Установка базового образа
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Установка рабочей директории внутри образа
WORKDIR /app

# Копирование файлов проекта и восстановление зависимостей
COPY ./ServiceFinder/ServiceFinder.API/ServiceFinder.API.csproj ./ServiceFinder.API/
COPY ./ServiceFinder/ServiceFinder.BLL/ServiceFinder.BLL.csproj ./ServiceFinder.BLL/
COPY ./ServiceFinder/ServiceFinder.DAL/ServiceFinder.DAL.csproj ./ServiceFinder.DAL/
COPY ./ServiceFinder/ServiceFinder.UnitTest/ServiceFinder.UnitTest.csproj ./ServiceFinder.UnitTest/
COPY ./ServiceFinder/ServiceFinder.IntegrationTest/ServiceFinder.IntegrationTest.csproj ./ServiceFinder.IntegrationTest/

# Восстановление зависимостей для проекта
RUN dotnet restore ./ServiceFinder.API/ServiceFinder.API.csproj

# Копирование всех файлов проекта
COPY ./ServiceFinder/ServiceFinder.API/ ./ServiceFinder.API/
COPY ./ServiceFinder/ServiceFinder.BLL/ ./ServiceFinder.BLL/
COPY ./ServiceFinder/ServiceFinder.DAL/ ./ServiceFinder.DAL/
COPY ./ServiceFinder/ServiceFinder.UnitTest/ ./ServiceFinder.UnitTest/
COPY ./ServiceFinder/ServiceFinder.IntegrationTest/ ./ServiceFinder.IntegrationTest/

# Сборка проекта
RUN dotnet build ./ServiceFinder.API/ServiceFinder.API.csproj -c Release -o /app/build

# Определение точки входа для контейнера
CMD ["dotnet", "run", "--project", "./ServiceFinder.API/ServiceFinder.API.csproj"]
