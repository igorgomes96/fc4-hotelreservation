FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/FC4.HotelReservation.WebApi/FC4.HotelReservation.WebApi.csproj", "src/FC4.HotelReservation.WebApi/"]
COPY ["modules/Catalog/FC4.HotelReservation.Catalog.Application/FC4.HotelReservation.Catalog.Application.csproj", "modules/Catalog/FC4.HotelReservation.Catalog.Application/"]
COPY ["shared/FC4.HotelReservation.Shared.Application/FC4.HotelReservation.Shared.Application.csproj", "shared/FC4.HotelReservation.Shared.Application/"]
COPY ["modules/Catalog/FC4.HotelReservation.Catalog.Domain/FC4.HotelReservation.Catalog.Domain.csproj", "modules/Catalog/FC4.HotelReservation.Catalog.Domain/"]
COPY ["shared/FC4.HotelReservation.Shared.Domain/FC4.HotelReservation.Shared.Domain.csproj", "shared/FC4.HotelReservation.Shared.Domain/"]
COPY ["modules/Catalog/FC4.HotelReservation.Catalog.Infra.Data/FC4.HotelReservation.Catalog.Infra.Data.csproj", "modules/Catalog/FC4.HotelReservation.Catalog.Infra.Data/"]
COPY ["shared/FC4.HotelReservation.Shared.Infrastructure/FC4.HotelReservation.Shared.Infrastructure.csproj", "shared/FC4.HotelReservation.Shared.Infrastructure/"]
RUN dotnet restore "src/FC4.HotelReservation.WebApi/FC4.HotelReservation.WebApi.csproj"
COPY . .
WORKDIR "/src/src/FC4.HotelReservation.WebApi"
RUN dotnet build "FC4.HotelReservation.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "FC4.HotelReservation.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FC4.HotelReservation.WebApi.dll"]
