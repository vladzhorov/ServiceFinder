FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

COPY ./ServiceFinder.OrderService/ServiceFinder.OrderService.API/ServiceFinder.OrderService.API.csproj /app/ServiceFinder.OrderService.API/
COPY ./ServiceFinder.OrderService/ServiceFinder.OrderService.Application/ServiceFinder.OrderService.Application.csproj /app/ServiceFinder.OrderService.Application/
COPY ./ServiceFinder.OrderService/ServiceFinder.OrderService.Domain/ServiceFinder.OrderService.Domain.csproj /app/ServiceFinder.OrderService.Domain/
COPY ./ServiceFinder.OrderService/ServiceFinder.OrderService.Infrastructure/ServiceFinder.OrderService.Infrastructure.csproj /app/ServiceFinder.OrderService.Infrastructure/
COPY ./ServiceFinder.OrderService/Tests/ServiceFinder.OrderService.UnitTest/ServiceFinder.OrderService.UnitTest.csproj /app/ServiceFinder.OrderService.UnitTest/

RUN dotnet restore /app/ServiceFinder.OrderService.API/ServiceFinder.OrderService.API.csproj

COPY ./ServiceFinder.OrderService/ServiceFinder.OrderService.API/ /app/ServiceFinder.OrderService.API/
COPY ./ServiceFinder.OrderService/ServiceFinder.OrderService.Application/ /app/ServiceFinder.OrderService.Application/
COPY ./ServiceFinder.OrderService/ServiceFinder.OrderService.Domain/ /app/ServiceFinder.OrderService.Domain/
COPY ./ServiceFinder.OrderService/ServiceFinder.OrderService.Infrastructure/ /app/ServiceFinder.OrderService.Infrastructure/
COPY ./ServiceFinder.OrderService/Tests/ServiceFinder.OrderService.UnitTest/ /app/ServiceFinder.OrderService.UnitTest/

RUN dotnet build /app/ServiceFinder.OrderService.API/ServiceFinder.OrderService.API.csproj -c Release -o /app/build

CMD ["dotnet", "run", "--project", "/app/ServiceFinder.OrderService.API/ServiceFinder.OrderService.API.csproj"]
