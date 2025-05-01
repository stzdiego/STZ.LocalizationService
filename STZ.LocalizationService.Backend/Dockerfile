FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["STZ.LocalizationService.Backend/STZ.LocalizationService.Backend.csproj", "STZ.LocalizationService.Backend/"]
COPY ["STZ.LocalizationService.Access/STZ.LocalizationService.Access.csproj", "STZ.LocalizationService.Access/"]
RUN dotnet restore "STZ.LocalizationService.Backend/STZ.LocalizationService.Backend.csproj"
COPY . .
WORKDIR "/src/STZ.LocalizationService.Backend"
RUN dotnet build "./STZ.LocalizationService.Backend.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./STZ.LocalizationService.Backend.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "STZ.LocalizationService.Backend.dll"]
