FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
EXPOSE 5003


COPY ./ServiceFinder.NotificationsService/ServiceFinder.NotificationService/ServiceFinder.NotificationService.csproj /app/ServiceFinder.NotificationService/
COPY ./ServiceFinder.NotificationsService/ServiceFinder.NotificationsService.Application/ServiceFinder.NotificationsService.Application.csproj /app/ServiceFinder.NotificationsService.Application/
COPY ./ServiceFinder.NotificationsService/ServiceFinder.NotificationsService.Domain/ServiceFinder.NotificationsService.Domain.csproj /app/ServiceFinder.NotificationsService.Domain/
COPY ./ServiceFinder.NotificationsService/ServiceFinder.NotificationsService.Infrastructure/ServiceFinder.NotificationsService.Infrastructure.csproj /app/ServiceFinder.NotificationsService.Infrastructure/
COPY ./ServiceFinder.NotificationsService/ServiceFinder.NotificationsService.UnitTest/ServiceFinder.NotificationsService.UnitTest.csproj /app/ServiceFinder.NotificationsService.UnitTest/

RUN dotnet restore /app/ServiceFinder.NotificationService/ServiceFinder.NotificationService.csproj

COPY ./ServiceFinder.NotificationsService/ServiceFinder.NotificationService/ /app/ServiceFinder.NotificationService/
COPY ./ServiceFinder.NotificationsService/ServiceFinder.NotificationsService.Application/ /app/ServiceFinder.NotificationsService.Application/
COPY ./ServiceFinder.NotificationsService/ServiceFinder.NotificationsService.Domain/ /app/ServiceFinder.NotificationsService.Domain/
COPY ./ServiceFinder.NotificationsService/ServiceFinder.NotificationsService.Infrastructure/ /app/ServiceFinder.NotificationsService.Infrastructure/
COPY ./ServiceFinder.NotificationsService/ServiceFinder.NotificationsService.UnitTest/ /app/ServiceFinder.NotificationsService.UnitTest/

RUN dotnet build /app/ServiceFinder.NotificationService/ServiceFinder.NotificationService.csproj -c Release -o /app/build

CMD ["dotnet", "run", "--project", "/app/ServiceFinder.NotificationService/ServiceFinder.NotificationService.csproj"]



