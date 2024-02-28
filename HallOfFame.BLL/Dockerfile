FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["HallOfFame.BLL/HallOfFame.BLL.csproj", "HallOfFame.BLL/"]
COPY ["HallOfFame.DAL/HallOfFame.DAL.csproj", "HallOfFame.DAL/"]
COPY ["HallOfFame.Common/HallOfFame.Common.csproj", "HallOfFame.Common/"]
RUN dotnet restore "HallOfFame.BLL/HallOfFame.BLL.csproj"
COPY . .
WORKDIR "/src/HallOfFame.BLL"
RUN dotnet build "HallOfFame.BLL.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "HallOfFame.BLL.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HallOfFame.BLL.dll"]
