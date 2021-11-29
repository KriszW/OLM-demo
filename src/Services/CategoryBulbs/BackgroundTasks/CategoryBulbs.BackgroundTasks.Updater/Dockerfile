#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/CategoryBulbs/BackgroundTasks/CategoryBulbs.BackgroundTasks.Updater/CategoryBulbs.BackgroundTasks.Updater.csproj", "Services/CategoryBulbs/BackgroundTasks/CategoryBulbs.BackgroundTasks.Updater/"]
RUN dotnet restore "Services/CategoryBulbs/BackgroundTasks/CategoryBulbs.BackgroundTasks.Updater/CategoryBulbs.BackgroundTasks.Updater.csproj"
COPY . .
WORKDIR "/src/Services/CategoryBulbs/BackgroundTasks/CategoryBulbs.BackgroundTasks.Updater"
RUN dotnet build "CategoryBulbs.BackgroundTasks.Updater.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CategoryBulbs.BackgroundTasks.Updater.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CategoryBulbs.BackgroundTasks.Updater.dll"]