#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/DailyReport/BackgroundTasks/DailyReport.BackgroundTasks.Updater/DailyReport.BackgroundTasks.Updater.csproj", "Services/DailyReport/BackgroundTasks/DailyReport.BackgroundTasks.Updater/"]
RUN dotnet restore "Services/DailyReport/BackgroundTasks/DailyReport.BackgroundTasks.Updater/DailyReport.BackgroundTasks.Updater.csproj"
COPY . .
WORKDIR "/src/Services/DailyReport/BackgroundTasks/DailyReport.BackgroundTasks.Updater"
RUN dotnet build "DailyReport.BackgroundTasks.Updater.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DailyReport.BackgroundTasks.Updater.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DailyReport.BackgroundTasks.Updater.dll"]