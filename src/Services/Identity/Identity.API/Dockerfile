#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Identity/Identity.API/Identity.API.csproj", "Services/Identity/Identity.API/"]
COPY ["Shared/Services/Auth.Extensions/OLM.Services.Auth.Extensions/Auth.Extensions.csproj", "Shared/Services/Auth.Extensions/OLM.Services.Auth.Extensions/"]
COPY ["Shared/Models/Identity/AccountAccessModels/AccountAccessModels.csproj", "Shared/Models/Identity/AccountAccessModels/"]
COPY ["Shared/Services/Services.SharedBases/Services.SharedBases.csproj", "Shared/Services/Services.SharedBases/"]
COPY ["Shared/Exceptions/APIResponse.Exceptions/APIResponse.Exceptions.csproj", "Shared/Exceptions/APIResponse.Exceptions/"]
COPY ["Shared/Services/Services.SharedBasses.APIErrors/Services.SharedBasses.APIErrors.csproj", "Shared/Services/Services.SharedBasses.APIErrors/"]
COPY ["Shared/Extensions/APIErrorExceptionHandler/APIErrorExceptionHandler.csproj", "Shared/Extensions/APIErrorExceptionHandler/"]
RUN dotnet restore "Services/Identity/Identity.API/Identity.API.csproj"
COPY . .
WORKDIR "/src/Services/Identity/Identity.API"
RUN dotnet build "Identity.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Identity.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Identity.API.dll"]