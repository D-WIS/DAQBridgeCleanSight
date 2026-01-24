# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to DAQBridge.CleanSight.Server your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

RUN mkdir -p /app/config
RUN chmod -R 755 /app/config

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["DWIS.DAQBridge.CleanSight.Server/config/Quickstarts.ReferenceClient.Config.xml", "/app/config/"]
COPY ["DWIS.DAQBridge.CleanSight.Server/DWIS.DAQBridge.CleanSight.Server.csproj", "DWIS.DAQBridge.CleanSight.Server/"]
RUN dotnet restore "DWIS.DAQBridge.CleanSight.Server/DWIS.DAQBridge.CleanSight.Server.csproj"
COPY . .
WORKDIR "/src/DWIS.DAQBridge.CleanSight.Server"
RUN dotnet build "DWIS.DAQBridge.CleanSight.Server.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "DWIS.DAQBridge.CleanSight.Server.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
VOLUME /home
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "DWIS.DAQBridge.CleanSight.Server.dll"]
