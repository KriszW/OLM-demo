#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Tram/Tram.API/Tram.API.csproj", "Services/Tram/Tram.API/"]
COPY ["Shared/Services/Auth.Extensions/OLM.Services.Auth.Extensions/Auth.Extensions.csproj", "Shared/Services/Auth.Extensions/OLM.Services.Auth.Extensions/"]
COPY ["Shared/Exceptions/DataManagerExceptions/DataManagerExceptions.csproj", "Shared/Exceptions/DataManagerExceptions/"]
COPY ["Shared/Models/Tram/Tram.SharedAPIModels/Tram.SharedAPIModels.csproj", "Shared/Models/Tram/Tram.SharedAPIModels/"]
COPY ["Shared/Services/Services.SharedBases/Services.SharedBases.csproj", "Shared/Services/Services.SharedBases/"]
COPY ["Shared/Exceptions/APIResponse.Exceptions/APIResponse.Exceptions.csproj", "Shared/Exceptions/APIResponse.Exceptions/"]
COPY ["Shared/Services/Services.SharedBasses.APIErrors/Services.SharedBasses.APIErrors.csproj", "Shared/Services/Services.SharedBasses.APIErrors/"]
COPY ["Shared/Extensions/OLM.Shared.Extensions.Pagination/Pagination.csproj", "Shared/Extensions/OLM.Shared.Extensions.Pagination/"]
COPY ["Shared/Extensions/APIErrorExceptionHandler/APIErrorExceptionHandler.csproj", "Shared/Extensions/APIErrorExceptionHandler/"]
RUN dotnet restore "Services/Tram/Tram.API/Tram.API.csproj"
COPY . .
WORKDIR "/src/Services/Tram/Tram.API"
RUN dotnet build "Tram.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tram.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tram.API.dll"]