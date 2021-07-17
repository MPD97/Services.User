FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /publish
COPY . . 
RUN dotnet publish ./Services.User.sln -c release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build /publish/out .
ENV ASPNETCORE_ENVIRONMENT local_docker
ENV ASPNETCORE_URLS http://*:5012
ENTRYPOINT dotnet Services.User.Api.dll