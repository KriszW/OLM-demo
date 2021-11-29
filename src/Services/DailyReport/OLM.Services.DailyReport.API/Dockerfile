#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/DailyReport/OLM.Services.DailyReport.API/DailyReport.API.csproj", "Services/DailyReport/OLM.Services.DailyReport.API/"]
COPY ["Shared/Services/Auth.Extensions/OLM.Services.Auth.Extensions/Auth.Extensions.csproj", "Shared/Services/Auth.Extensions/OLM.Services.Auth.Extensions/"]
COPY ["Shared/Exceptions/DataManagerExceptions/DataManagerExceptions.csproj", "Shared/Exceptions/DataManagerExceptions/"]
COPY ["Shared/Extensions/HttpMessage.URIHelpers/HttpMessage.URIHelpers.csproj", "Shared/Extensions/HttpMessage.URIHelpers/"]
COPY ["Shared/Models/DailyReport/DailyReport.SharedAPIModels/DailyReport.SharedAPIModels.csproj", "Shared/Models/DailyReport/DailyReport.SharedAPIModels/"]
COPY ["Shared/Services/Services.SharedBases/Services.SharedBases.csproj", "Shared/Services/Services.SharedBases/"]
COPY ["Shared/Exceptions/APIResponse.Exceptions/APIResponse.Exceptions.csproj", "Shared/Exceptions/APIResponse.Exceptions/"]
COPY ["Shared/Services/Services.SharedBasses.APIErrors/Services.SharedBasses.APIErrors.csproj", "Shared/Services/Services.SharedBasses.APIErrors/"]
COPY ["Shared/Extensions/OLM.Shared.Extensions.Pagination/Pagination.csproj", "Shared/Extensions/OLM.Shared.Extensions.Pagination/"]
COPY ["Shared/Extensions/APIErrorExceptionHandler/APIErrorExceptionHandler.csproj", "Shared/Extensions/APIErrorExceptionHandler/"]
RUN dotnet restore "Services/DailyReport/OLM.Services.DailyReport.API/DailyReport.API.csproj"
COPY . .
WORKDIR "/src/Services/DailyReport/OLM.Services.DailyReport.API"
RUN dotnet build "DailyReport.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DailyReport.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DailyReport.API.dll"]