#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Bundles/BackgroundTasks/Bundles.BackgroundTasks/Bundles.BackgroundTasks.Updater.csproj", "Services/Bundles/BackgroundTasks/Bundles.BackgroundTasks/"]
COPY ["Shared/Models/GateWay/SPA/SPAGtw.SharedModels/SPAGtw.SharedModels.csproj", "Shared/Models/GateWay/SPA/SPAGtw.SharedModels/"]
COPY ["Shared/Models/CategoryBulbs/CategoryBulbs.APIResponses.Models/CategoryBulbs.APIResponses.csproj", "Shared/Models/CategoryBulbs/CategoryBulbs.APIResponses.Models/"]
COPY ["Shared/Models/Routing/Routing.SharedAPIModels/Routing.SharedAPIModels.csproj", "Shared/Models/Routing/Routing.SharedAPIModels/"]
COPY ["Shared/Models/TCO/OLM.Shared.Models.TCO.SharedAPIModels/TCO.SharedAPIModels.csproj", "Shared/Models/TCO/OLM.Shared.Models.TCO.SharedAPIModels/"]
COPY ["Shared/Models/Bundles/OLM.Shared.Models.Bundles.BundleAPIModels/Bundles.APIResponses.csproj", "Shared/Models/Bundles/OLM.Shared.Models.Bundles.BundleAPIModels/"]
COPY ["Shared/Services/Services.SharedBases/Services.SharedBases.csproj", "Shared/Services/Services.SharedBases/"]
COPY ["Shared/Exceptions/APIResponse.Exceptions/APIResponse.Exceptions.csproj", "Shared/Exceptions/APIResponse.Exceptions/"]
COPY ["Shared/Services/Services.SharedBasses.APIErrors/Services.SharedBasses.APIErrors.csproj", "Shared/Services/Services.SharedBasses.APIErrors/"]
RUN dotnet restore "Services/Bundles/BackgroundTasks/Bundles.BackgroundTasks/Bundles.BackgroundTasks.Updater.csproj"
COPY . .
WORKDIR "/src/Services/Bundles/BackgroundTasks/Bundles.BackgroundTasks"
RUN dotnet build "Bundles.BackgroundTasks.Updater.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Bundles.BackgroundTasks.Updater.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bundles.BackgroundTasks.Updater.dll"]