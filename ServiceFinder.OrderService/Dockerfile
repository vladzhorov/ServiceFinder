FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ServiceFinder.OrderService/ServiceFinder.OrderService.API/ServiceFinder.OrderService.API.csproj", "ServiceFinder.OrderService.API/"]
COPY ["ServiceFinder.OrderService/ServiceFinder.OrderService.Application/ServiceFinder.OrderService.Application.csproj", "ServiceFinder.OrderService.Application/"]
COPY ["ServiceFinder.OrderService/ServiceFinder.OrderService.Domain/ServiceFinder.OrderService.Domain.csproj", "ServiceFinder.OrderService.Domain/"]
COPY ["ServiceFinder.OrderService/ServiceFinder.OrderService.Infrastructure/ServiceFinder.OrderService.Infrastructure.csproj", "ServiceFinder.OrderService.Infrastructure/"]
RUN dotnet restore "ServiceFinder.OrderService.API/ServiceFinder.OrderService.API.csproj"
COPY . .
WORKDIR "/src/ServiceFinder.OrderService/ServiceFinder.OrderService.API"
RUN dotnet build "ServiceFinder.OrderService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ServiceFinder.OrderService.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ServiceFinder.OrderService.API.dll"]
