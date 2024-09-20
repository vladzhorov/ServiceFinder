FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
EXPOSE 5004


COPY ./ServiceFinder.Auth/ServiceFinder.Auth/ServiceFinder.Auth.csproj /app/ServiceFinder.Auth/

RUN dotnet restore /app/ServiceFinder.Auth/ServiceFinder.Auth.csproj

COPY ./ServiceFinder.Auth/ServiceFinder.Auth/ /app/ServiceFinder.Auth/

RUN dotnet build /app/ServiceFinder.Auth/ServiceFinder.Auth.csproj -c Release -o /app/build

CMD ["dotnet", "run", "--project", "/app/ServiceFinder.Auth/ServiceFinder.Auth.csproj"]

