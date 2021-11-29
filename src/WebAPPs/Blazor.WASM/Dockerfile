FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Shared/Services/Auth.Extensions/OLM.Services.Auth.Extensions/Auth.Extensions.csproj", "Shared/Services/Auth.Extensions/OLM.Services.Auth.Extensions/"]
COPY ["Shared/Models/GateWay/SPA/SPAGtw.SharedModels/SPAGtw.SharedModels.csproj", "Shared/Models/GateWay/SPA/SPAGtw.SharedModels/"]
COPY ["Shared/Models/Bundles/OLM.Shared.Models.Bundles.BundleAPIModels/Bundles.APIResponses.csproj", "Shared/Models/Bundles/OLM.Shared.Models.Bundles.BundleAPIModels/"]
COPY ["Shared/Models/TCO/OLM.Shared.Models.TCO.SharedAPIModels/TCO.SharedAPIModels.csproj", "Shared/Models/TCO/OLM.Shared.Models.TCO.SharedAPIModels/"]
COPY ["Shared/Models/CategoryBulbs/CategoryBulbs.APIResponses.Models/CategoryBulbs.APIResponses.csproj", "Shared/Models/CategoryBulbs/CategoryBulbs.APIResponses.Models/"]
COPY ["Shared/Services/Services.SharedBases/Services.SharedBases.csproj", "Shared/Services/Services.SharedBases/"]
COPY ["Shared/Models/Identity/AccountAccessModels/AccountAccessModels.csproj", "Shared/Models/Identity/AccountAccessModels/"]
COPY ["WebAPPs/Blazor.WASM/Blazor.WASM.csproj", "WebAPPs/Blazor.WASM/"]
RUN dotnet restore "WebAPPs/Blazor.WASM/Blazor.WASM.csproj"
COPY . .
WORKDIR "/src/WebAPPs/Blazor.WASM"
RUN dotnet build "Blazor.WASM.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Blazor.WASM.csproj" -c Release -o /app/publish

FROM nginx:alpine AS final
COPY --from=build /src/WebAPPs/Blazor.WASM/nginx.conf /etc/nginx/nginx.conf
COPY --from=publish /app/publish/wwwroot /usr/share/nginx/html