# NuGet restore
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY *.sln .
COPY ProfanityCheckLib/*.csproj ProfanityCheckLib/
COPY ProfanityCheckLib.Tests/*.csproj ProfanityCheckLib.Tests/
COPY ProfanityCheck.WebAPI/*.csproj ProfanityCheck.WebAPI/
RUN dotnet restore
COPY . .

# testing
FROM build AS testing
WORKDIR /src/ProfanityCheckLib
RUN dotnet build
WORKDIR /src/ProfanityCheckLib.Tests
RUN dotnet test

# publish
FROM build AS publish
WORKDIR /src/ProfanityCheck.WebAPI
RUN dotnet publish -c Release -o /src/publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app
COPY --from=publish /src/publish .

# ENTRYPOINT ["dotnet", "ProfanityCheck.WebAPI.dll"]

# Heroku Commands
CMD ASPNETCORE_URLS=http://*:$PORT dotnet ProfanityCheck.WebAPI.dll