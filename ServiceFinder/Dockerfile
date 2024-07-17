FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app


COPY ./ServiceFinder/ServiceFinder.API/ServiceFinder.API.csproj /app/ServiceFinder.API/
COPY ./ServiceFinder/ServiceFinder.BLL/ServiceFinder.BLL.csproj /app/ServiceFinder.BLL/
COPY ./ServiceFinder/ServiceFinder.DAL/ServiceFinder.DAL.csproj /app/ServiceFinder.DAL/
COPY ./ServiceFinder/ServiceFinder.UnitTest/ServiceFinder.UnitTest.csproj /app/ServiceFinder.UnitTest/
COPY ./ServiceFinder/ServiceFinder.IntegrationTest/ServiceFinder.IntegrationTest.csproj /app/ServiceFinder.IntegrationTest/

RUN dotnet restore /app/ServiceFinder.API/ServiceFinder.API.csproj

COPY ./ServiceFinder/ServiceFinder.API/ /app/ServiceFinder.API/
COPY ./ServiceFinder/ServiceFinder.BLL/ /app/ServiceFinder.BLL/
COPY ./ServiceFinder/ServiceFinder.DAL/ /app/ServiceFinder.DAL/
COPY ./ServiceFinder/ServiceFinder.UnitTest/ /app/ServiceFinder.UnitTest/
COPY ./ServiceFinder/ServiceFinder.IntegrationTest/ /app/ServiceFinder.IntegrationTest/

RUN dotnet build /app/ServiceFinder.API/ServiceFinder.API.csproj -c Release -o /app/build

CMD ["dotnet", "run", "--project", "/app/ServiceFinder.API/ServiceFinder.API.csproj"]
