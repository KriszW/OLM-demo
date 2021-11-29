#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ApiGateWays/SPA/OLM.Web.Gtw.SPA/Web.Gtw.SPA.csproj", "ApiGateWays/SPA/OLM.Web.Gtw.SPA/"]
COPY ["Shared/Services/Auth.Extensions/OLM.Services.Auth.Extensions/Auth.Extensions.csproj", "Shared/Services/Auth.Extensions/OLM.Services.Auth.Extensions/"]
COPY ["Shared/Extensions/HttpMessage.URIHelpers/HttpMessage.URIHelpers.csproj", "Shared/Extensions/HttpMessage.URIHelpers/"]
COPY ["Shared/Models/DailyReport/DailyReport.SharedAPIModels/DailyReport.SharedAPIModels.csproj", "Shared/Models/DailyReport/DailyReport.SharedAPIModels/"]
COPY ["Shared/Models/Tram/Tram.SharedAPIModels/Tram.SharedAPIModels.csproj", "Shared/Models/Tram/Tram.SharedAPIModels/"]
COPY ["Shared/Models/GateWay/SPA/SPAGtw.SharedModels/SPAGtw.SharedModels.csproj", "Shared/Models/GateWay/SPA/SPAGtw.SharedModels/"]
COPY ["Shared/Models/CategoryBulbs/CategoryBulbs.APIResponses.Models/CategoryBulbs.APIResponses.csproj", "Shared/Models/CategoryBulbs/CategoryBulbs.APIResponses.Models/"]
COPY ["Shared/Models/Routing/Routing.SharedAPIModels/Routing.SharedAPIModels.csproj", "Shared/Models/Routing/Routing.SharedAPIModels/"]
COPY ["Shared/Models/TCO/OLM.Shared.Models.TCO.SharedAPIModels/TCO.SharedAPIModels.csproj", "Shared/Models/TCO/OLM.Shared.Models.TCO.SharedAPIModels/"]
COPY ["Shared/Models/Bundles/OLM.Shared.Models.Bundles.BundleAPIModels/Bundles.APIResponses.csproj", "Shared/Models/Bundles/OLM.Shared.Models.Bundles.BundleAPIModels/"]
COPY ["Shared/Models/BundlePrices/Bundle.Prices.APIResponses/Bundle.Prices.APIResponses.csproj", "Shared/Models/BundlePrices/Bundle.Prices.APIResponses/"]
COPY ["Shared/Services/Services.SharedBases/Services.SharedBases.csproj", "Shared/Services/Services.SharedBases/"]
COPY ["Shared/Exceptions/APIResponse.Exceptions/APIResponse.Exceptions.csproj", "Shared/Exceptions/APIResponse.Exceptions/"]
COPY ["Shared/Services/Services.SharedBasses.APIErrors/Services.SharedBasses.APIErrors.csproj", "Shared/Services/Services.SharedBasses.APIErrors/"]
COPY ["Shared/Extensions/HttpExtensions.RequestHelper/HttpExtensions.RequestHelper.csproj", "Shared/Extensions/HttpExtensions.RequestHelper/"]
COPY ["Shared/Extensions/APIErrorExceptionHandler/APIErrorExceptionHandler.csproj", "Shared/Extensions/APIErrorExceptionHandler/"]
COPY ["Shared/Extensions/Caching/Caching.csproj", "Shared/Extensions/Caching/"]
COPY ["Shared/Models/MoneyExchangeRate/MoneyExchangeRate.SharedAPIModels/MoneyExchangeRate.SharedAPIModels.csproj", "Shared/Models/MoneyExchangeRate/MoneyExchangeRate.SharedAPIModels/"]
RUN dotnet restore "ApiGateWays/SPA/OLM.Web.Gtw.SPA/Web.Gtw.SPA.csproj"
COPY . .
WORKDIR "/src/ApiGateWays/SPA/OLM.Web.Gtw.SPA"
RUN dotnet build "Web.Gtw.SPA.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Web.Gtw.SPA.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web.Gtw.SPA.dll"]