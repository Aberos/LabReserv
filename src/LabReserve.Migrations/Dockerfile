FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["global.json", "Directory.Build.props", "Directory.Packages.props", "./"]
COPY ["src/LabReserve.Migrations/LabReserve.Migrations.csproj", "src/LabReserve.Migrations/"]
RUN dotnet restore "src/LabReserve.Migrations/LabReserve.Migrations.csproj"
COPY src/LabReserve.Migrations src/LabReserve.Migrations
RUN dotnet publish "src/LabReserve.Migrations/LabReserve.Migrations.csproj" -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/runtime:10.0 AS final
USER $APP_UID
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "LabReserve.Migrations.dll"]
