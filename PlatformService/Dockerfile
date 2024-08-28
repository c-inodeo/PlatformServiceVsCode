FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

EXPOSE 5000

COPY PlatformService.generated.sln ./
COPY PlatformService.csproj ./

RUN dotnet restore PlatformService.generated.sln

COPY . .

RUN dotnet publish PlatformService.csproj -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "PlatformService.dll"]
