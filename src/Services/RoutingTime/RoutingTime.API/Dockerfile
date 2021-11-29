#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/RoutingTime/RoutingTime.API/RoutingTime.API.csproj", "Services/RoutingTime/RoutingTime.API/"]
COPY ["Shared/Services/Auth.Extensions/OLM.Services.Auth.Extensions/Auth.Extensions.csproj", "Shared/Services/Auth.Extensions/OLM.Services.Auth.Extensions/"]
COPY ["Shared/Exceptions/DataManagerExceptions/DataManagerExceptions.csproj", "Shared/Exceptions/DataManagerExceptions/"]
COPY ["Shared/Services/Services.SharedBases/Services.SharedBases.csproj", "Shared/Services/Services.SharedBases/"]
COPY ["Shared/Exceptions/APIResponse.Exceptions/APIResponse.Exceptions.csproj", "Shared/Exceptions/APIResponse.Exceptions/"]
COPY ["Shared/Services/Services.SharedBasses.APIErrors/Services.SharedBasses.APIErrors.csproj", "Shared/Services/Services.SharedBasses.APIErrors/"]
COPY ["Shared/Extensions/OLM.Shared.Extensions.Pagination/Pagination.csproj", "Shared/Extensions/OLM.Shared.Extensions.Pagination/"]
COPY ["Shared/Extensions/APIErrorExceptionHandler/APIErrorExceptionHandler.csproj", "Shared/Extensions/APIErrorExceptionHandler/"]
COPY ["Shared/Models/RoutingTime/RoutingTime.SharedAPIModels/RoutingTime.SharedAPIModels.csproj", "Shared/Models/RoutingTime/RoutingTime.SharedAPIModels/"]
RUN dotnet restore "Services/RoutingTime/RoutingTime.API/RoutingTime.API.csproj"
COPY . .
WORKDIR "/src/Services/RoutingTime/RoutingTime.API"
RUN dotnet build "RoutingTime.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RoutingTime.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RoutingTime.API.dll"]