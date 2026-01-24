# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to DAQBridge.CleanSight.MQTTSource your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["DWIS.DAQBridge.CleanSight.MQTTSource/DWIS.DAQBridge.CleanSight.MQTTSource.csproj", "DWIS.DAQBridge.CleanSight.MQTTSource/"]
RUN dotnet restore "DWIS.DAQBridge.CleanSight.MQTTSource/DWIS.DAQBridge.CleanSight.MQTTSource.csproj"
COPY . .
WORKDIR "/src/DWIS.DAQBridge.CleanSight.MQTTSource"
RUN dotnet build "DWIS.DAQBridge.CleanSight.MQTTSource.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "DWIS.DAQBridge.CleanSight.MQTTSource.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
VOLUME /home
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "DWIS.DAQBridge.CleanSight.MQTTSource.dll"]
