#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 17020
ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["APP-QTHT-GIS/App.Qtht.Gis.Api/App.Qtht.Gis.Api.csproj", "APP-QTHT-GIS/App.Qtht.Gis.Api/"]
COPY ["APP_CORE/App.Core.Common/App.Core.Common.csproj", "APP_CORE/App.Core.Common/"]
COPY ["APP-QTHT-GIS/APP_QTHT_GIS_DAL/DatabaseGeneric/App.Qtht.Gis.Dal.csproj", "APP-QTHT-GIS/APP_QTHT_GIS_DAL/DatabaseGeneric/"]
COPY ["APP-QTHT-GIS/APP_QTHT_GIS_DAL/DatabaseSpecific/App.Qtht.Gis.DalDBSpecific.csproj", "APP-QTHT-GIS/APP_QTHT_GIS_DAL/DatabaseSpecific/"]
COPY ["APP-QTHT-GIS/App.Qtht.Gis.Services/App.Qtht.Gis.Services.csproj", "APP-QTHT-GIS/App.Qtht.Services/"]
#COPY ["APP_QTHT/App.Qtht.Repository/App.Qtht.Repository.csproj", "APP_QTHT/App.Qtht.Repository/"]
RUN dotnet nuget add source "https://nuget.telerik.com/v3/index.json" --name telerik.com --username thenn.stttt@phutho.gov.vn --password Stttt@123 --store-password-in-clear-text
RUN dotnet restore "APP-QTHT-GIS/App.Qtht.Gis.Api/App.Qtht.Gis.Api.csproj"
COPY . .
WORKDIR "/src/APP-QTHT-GIS/App.Qtht.Gis.Api"
RUN dotnet build "App.Qtht.Gis.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "App.Qtht.Gis.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "App.Qtht.Gis.Api.dll"]