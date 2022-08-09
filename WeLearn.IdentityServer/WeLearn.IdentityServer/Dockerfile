#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WeLearn.IdentityServer/WeLearn.IdentityServer/WeLearn.IdentityServer.csproj", "WeLearn.IdentityServer/WeLearn.IdentityServer/"]
COPY ["WeLearn.Auth/WeLearn.Auth/WeLearn.Auth.csproj", "WeLearn.Auth/WeLearn.Auth/"]
COPY ["WeLearn.Data/WeLearn.Data/WeLearn.Data.csproj", "WeLearn.Data/WeLearn.Data/"]
RUN dotnet restore "WeLearn.IdentityServer/WeLearn.IdentityServer/WeLearn.IdentityServer.csproj"
COPY . .
WORKDIR "/src/WeLearn.IdentityServer/WeLearn.IdentityServer"
# NPM
# Install Excubo.WebCompiler ---
RUN dotnet new tool-manifest --force
RUN dotnet tool install Excubo.WebCompiler
RUN dotnet tool restore
# --- Installed Excubo.WebCompiler
# Install npm ---
RUN apt-get update -yq && apt-get upgrade -yq && apt-get install -yq curl git nano
RUN curl -sL https://deb.nodesource.com/setup_18.x | bash - && apt-get install -yq nodejs build-essential
RUN npm install -g npm
# --- Installed npm
RUN npm -v
RUN npm install
RUN npm audit
RUN npm audit fix
RUN npm list
# --- NPM
RUN dotnet build "WeLearn.IdentityServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WeLearn.IdentityServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WeLearn.IdentityServer.dll"]