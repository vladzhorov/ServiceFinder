FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY ["ServiceFinder/ServiceFinder.API/ServiceFinder.API.csproj", "ServiceFinder.API/"]
COPY ["ServiceFinder/ServiceFinder.BLL/ServiceFinder.BLL.csproj", "ServiceFinder.BLL/"]
COPY ["ServiceFinder/ServiceFinder.DAL/ServiceFinder.DAL.csproj", "ServiceFinder.DAL/"]
COPY ["ServiceFinder/ServiceFinder.UnitTest/ServiceFinder.UnitTest.csproj", "ServiceFinder.UnitTest/"]
COPY ["ServiceFinder/ServiceFinder.IntegrationTest/ServiceFinder.IntegrationTest.csproj", "ServiceFinder.IntegrationTest/"]

RUN dotnet restore "ServiceFinder.API/ServiceFinder.API.csproj"

COPY . .

WORKDIR "/src/ServiceFinder/ServiceFinder.API"

RUN dotnet build "ServiceFinder.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ServiceFinder.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

WORKDIR /app

EXPOSE 5002

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "ServiceFinder.API.dll"]
