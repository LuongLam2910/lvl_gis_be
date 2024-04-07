#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 17010
ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["APP-GIS/App.ConganGis.Api/App.ConganGis.Api.csproj", "APP-GIS/App.ConganGis.Api/"]
COPY ["APP-GIS/App.ConganGis.Services/App.ConganGis.Services.csproj", "APP-GIS/App.ConganGis.Services/"]
COPY ["APP_CORE/App.Core.Common/App.Core.Common.csproj", "APP_CORE/App.Core.Common/"]
COPY ["APP-QTHT-GIS/APP_QTHT_GIS_DAL/DatabaseGeneric/App.Qtht.Gis.Dal.csproj", "APP-QTHT-GIS/APP_QTHT_GIS_DAL/DatabaseGeneric/"]
COPY ["APP-QTHT-GIS/APP_QTHT_GIS_DAL/DatabaseSpecific/App.Qtht.Gis.DalDBSpecific.csproj", "APP-QTHT-GIS/APP_QTHT_GIS_DAL/DatabaseSpecific/"]
COPY ["APP-GIS/APP_GIS_DAL/DatabaseGeneric/App.CongAnGis.Dal.csproj", "APP-GIS/APP_GIS_DAL/DatabaseGeneric/"]
COPY ["APP-GIS/APP_GIS_DAL/DatabaseSpecific/App.CongAnGis.DalDBSpecific.csproj", "APP-GIS/APP_GIS_DAL/DatabaseSpecific/"]
RUN dotnet nuget add source "https://nuget.telerik.com/v3/index.json" --name telerik.com --username thenn.stttt@phutho.gov.vn --password Stttt@123 --store-password-in-clear-text
RUN dotnet restore "APP-GIS/App.ConganGis.Api/App.ConganGis.Api.csproj"
COPY . .
WORKDIR "/src/APP-GIS/App.ConganGis.Api"
RUN dotnet build "App.ConganGis.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "App.ConganGis.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "App.ConganGis.Api.dll"]