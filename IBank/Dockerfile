FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src
COPY ["IBank/IBank.csproj", "IBank/"]
RUN dotnet restore "IBank/IBank.csproj"
COPY . .
WORKDIR "/src/IBank"
RUN dotnet publish "IBank.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "IBank.dll"]
