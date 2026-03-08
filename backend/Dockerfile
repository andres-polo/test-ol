FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY src/ComercioApi.Web/ComercioApi.Web.csproj src/ComercioApi.Web/
RUN dotnet restore src/ComercioApi.Web/ComercioApi.Web.csproj

COPY . .
WORKDIR /src/src/ComercioApi.Web
RUN dotnet build ComercioApi.Web.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish ComercioApi.Web.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ComercioApi.Web.dll"]