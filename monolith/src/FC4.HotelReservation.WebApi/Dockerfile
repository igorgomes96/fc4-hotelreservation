FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/FC4.HotelReservation.WebApi/FC4.HotelReservation.WebApi.csproj", "src/FC4.HotelReservation.WebApi/"]
COPY ["src/FC4.HotelReservation.Infrastructure/FC4.HotelReservation.Infrastructure.csproj", "src/FC4.HotelReservation.Infrastructure/"]
COPY ["src/FC4.HotelReservation.Application/FC4.HotelReservation.Application.csproj", "src/FC4.HotelReservation.Application/"]
COPY ["src/FC4.HotelReservation.Domain/FC4.HotelReservation.Domain.csproj", "src/FC4.HotelReservation.Domain/"]
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
