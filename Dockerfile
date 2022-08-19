#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Core.Services/Core.Services.csproj", "Core.Services/"]
RUN dotnet restore "Core.Services/Core.Services.csproj"
COPY . .
WORKDIR "/src/Core.Services"
RUN dotnet build "Core.Services.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Core.Services.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Core.Services.dll"]