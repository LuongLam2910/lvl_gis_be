#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 16000
ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["API.GateWay/Api.Gateway.Api.csproj", "API.GateWay/"]
COPY ["APP_CORE/App.Core.Common/App.Core.Common.csproj", "APP_CORE/App.Core.Common/"]
COPY ["APP_QTHT/APP_QTHT_DAL/DatabaseGeneric/App.Qtht.Dal.csproj", "APP_QTHT/APP_QTHT_DAL/DatabaseGeneric/"]
COPY ["APP_QTHT/APP_QTHT_DAL/DatabaseSpecific/App.Qtht.DalDBSpecific.csproj", "APP_QTHT/APP_QTHT_DAL/DatabaseSpecific/"]
RUN dotnet restore "API.GateWay/Api.Gateway.Api.csproj"
COPY . .
WORKDIR "/src/API.GateWay"
RUN dotnet build "Api.Gateway.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Api.Gateway.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.Gateway.Api.dll"]