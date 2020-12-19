#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Importacao.API/Importacao.API.csproj", "Importacao.API/"]
RUN dotnet restore "Importacao.API/Importacao.API.csproj"
COPY . .
WORKDIR "/src/Importacao.API"
RUN dotnet build "Importacao.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Importacao.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Importacao.API.dll"]