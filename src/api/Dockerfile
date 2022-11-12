FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-stage
WORKDIR /app
EXPOSE 7069
EXPOSE 80
EXPOSE 443

ENV DOTNET_URLS=http://+:7069
WORKDIR /app
    
COPY *.csproj ./
RUN dotnet restore
    
COPY engine/examples ./
RUN dotnet publish -c Release -o out
    
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-stage /app/out .
ENTRYPOINT ["dotnet", "Wild-Boar-IOT.dll"]
