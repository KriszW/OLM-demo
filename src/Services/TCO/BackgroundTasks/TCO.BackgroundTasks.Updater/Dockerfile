#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/TCO/BackgroundTasks/TCO.BackgroundTasks.Updater/TCO.BackgroundTasks.Updater.csproj", "Services/TCO/BackgroundTasks/TCO.BackgroundTasks.Updater/"]
RUN dotnet restore "Services/TCO/BackgroundTasks/TCO.BackgroundTasks.Updater/TCO.BackgroundTasks.Updater.csproj"
COPY . .
WORKDIR "/src/Services/TCO/BackgroundTasks/TCO.BackgroundTasks.Updater"
RUN dotnet build "TCO.BackgroundTasks.Updater.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TCO.BackgroundTasks.Updater.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TCO.BackgroundTasks.Updater.dll"]