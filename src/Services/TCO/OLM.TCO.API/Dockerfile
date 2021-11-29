#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/TCO/OLM.TCO.API/TCO.API.csproj", "Services/TCO/OLM.TCO.API/"]
COPY ["Shared/Services/Auth.Extensions/OLM.Services.Auth.Extensions/Auth.Extensions.csproj", "Shared/Services/Auth.Extensions/OLM.Services.Auth.Extensions/"]
COPY ["Shared/Exceptions/DataManagerExceptions/DataManagerExceptions.csproj", "Shared/Exceptions/DataManagerExceptions/"]
COPY ["Shared/Extensions/HttpMessage.URIHelpers/HttpMessage.URIHelpers.csproj", "Shared/Extensions/HttpMessage.URIHelpers/"]
COPY ["Shared/Models/BundlePrices/Bundle.Prices.APIResponses/Bundle.Prices.APIResponses.csproj", "Shared/Models/BundlePrices/Bundle.Prices.APIResponses/"]
COPY ["Shared/Services/Services.SharedBases/Services.SharedBases.csproj", "Shared/Services/Services.SharedBases/"]
COPY ["Shared/Exceptions/APIResponse.Exceptions/APIResponse.Exceptions.csproj", "Shared/Exceptions/APIResponse.Exceptions/"]
COPY ["Shared/Services/Services.SharedBasses.APIErrors/Services.SharedBasses.APIErrors.csproj", "Shared/Services/Services.SharedBasses.APIErrors/"]
COPY ["Shared/Extensions/OLM.Shared.Extensions.Pagination/Pagination.csproj", "Shared/Extensions/OLM.Shared.Extensions.Pagination/"]
COPY ["Shared/Extensions/APIErrorExceptionHandler/APIErrorExceptionHandler.csproj", "Shared/Extensions/APIErrorExceptionHandler/"]
COPY ["Shared/Models/TCO/OLM.Shared.Models.TCO.SharedAPIModels/TCO.SharedAPIModels.csproj", "Shared/Models/TCO/OLM.Shared.Models.TCO.SharedAPIModels/"]
RUN dotnet restore "Services/TCO/OLM.TCO.API/TCO.API.csproj"
COPY . .
WORKDIR "/src/Services/TCO/OLM.TCO.API"
RUN dotnet build "TCO.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TCO.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TCO.API.dll"]