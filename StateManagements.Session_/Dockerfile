#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["StateManagements.Session_/StateManagements.Session_.csproj", "StateManagements.Session_/"]
RUN dotnet restore "StateManagements.Session_/StateManagements.Session_.csproj"
COPY . .
WORKDIR "/src/StateManagements.Session_"
RUN dotnet build "StateManagements.Session_.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StateManagements.Session_.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StateManagements.Session_.dll"]