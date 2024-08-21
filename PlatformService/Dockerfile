FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

COPY PlatformService/PlatformService.generated.sln ./

COPY PlatformService/PlatformService.csproj PlatformService/
COPY CommandsService/CommandsService.csproj CommandsService/

RUN dotnet restore PlatformService.generated.sln

COPY PlatformService/. PlatformService/
COPY CommandsService/. CommandsService/

RUN dotnet publish PlatformService/PlatformService.csproj -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "PlatformService.dll"]
