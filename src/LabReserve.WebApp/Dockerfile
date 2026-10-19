FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["global.json", "Directory.Build.props", "Directory.Packages.props", "./"]
COPY ["src/LabReserve.Domain/LabReserve.Domain.csproj", "src/LabReserve.Domain/"]
COPY ["src/LabReserve.Application/LabReserve.Application.csproj", "src/LabReserve.Application/"]
COPY ["src/LabReserve.Persistence/LabReserve.Persistence.csproj", "src/LabReserve.Persistence/"]
COPY ["src/LabReserve.WebApp/LabReserve.WebApp.csproj", "src/LabReserve.WebApp/"]
RUN dotnet restore "src/LabReserve.WebApp/LabReserve.WebApp.csproj"
COPY src src
RUN dotnet publish "src/LabReserve.WebApp/LabReserve.WebApp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false --no-restore

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "LabReserve.WebApp.dll"]
