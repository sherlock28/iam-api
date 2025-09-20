FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS deps
ARG platform=linux-x64
WORKDIR /app
COPY ./nuget.config /root/.nuget/NuGet/NuGet.Config
COPY src/IamApi.API/*.csproj ./IamApi.API/
RUN dotnet restore -r ${platform} "./IamApi.API/IamApi.API.csproj"

FROM deps AS build
ARG platform=linux-x64
COPY src/ .
WORKDIR "/app/."
RUN dotnet build "./IamApi.API/IamApi.API.csproj" -c Release --no-restore -r ${platform}

FROM build AS publish
ARG platform=linux-x64
RUN dotnet publish "./IamApi.API/IamApi.API.csproj" -c Release --no-restore -r ${platform} -o /app/publish

FROM base AS final
ENV ASPNETCORE_URLS=http://*:5000
WORKDIR /app
COPY --from=publish /app/publish .
RUN chgrp -R 0 . && chmod -R g=u .
EXPOSE 5000
ENTRYPOINT ["dotnet", "IamApi.API.dll"]
