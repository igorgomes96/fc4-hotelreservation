FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8090
ENV ASPNETCORE_URLS=http://*:8090

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/FC4.HotelReservation.Guests.WebApi/FC4.HotelReservation.Guests.WebApi.csproj", "src/FC4.HotelReservation.Guests.WebApi/"]
COPY ["modules/Guests/FC4.HotelReservation.Guests.Infra.Data/FC4.HotelReservation.Guests.Infra.Data.csproj", "modules/Guests/FC4.HotelReservation.Guests.Infra.Data/"]
COPY ["modules/Guests/FC4.HotelReservation.Guests.Domain/FC4.HotelReservation.Guests.Domain.csproj", "modules/Guests/FC4.HotelReservation.Guests.Domain/"]
COPY ["modules/Guests/FC4.HotelReservation.Guests.Application/FC4.HotelReservation.Guests.Application.csproj", "modules/Guests/FC4.HotelReservation.Guests.Application/"]
RUN dotnet restore "src/FC4.HotelReservation.Guests.WebApi/FC4.HotelReservation.Guests.WebApi.csproj"
COPY . .
WORKDIR "/src/src/FC4.HotelReservation.Guests.WebApi"
RUN dotnet build "FC4.HotelReservation.Guests.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "FC4.HotelReservation.Guests.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FC4.HotelReservation.Guests.WebApi.dll"]
