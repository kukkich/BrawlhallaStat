#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BrawlhallaStat.Api/BrawlhallaStat.Api.csproj", "BrawlhallaStat.Api/"]
RUN dotnet restore "BrawlhallaStat.Api/BrawlhallaStat.Api.csproj"
COPY . .
WORKDIR "/src/BrawlhallaStat.Api"
RUN dotnet build "BrawlhallaStat.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BrawlhallaStat.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BrawlhallaStat.Api.dll"]