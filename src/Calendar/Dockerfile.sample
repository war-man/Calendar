FROM microsoft/aspnetcore-build:2.0.0 AS build-env
WORKDIR /app

ENV http_proxy http://user:passwd@192.168.x.x:8080
ENV https_proxy http://user:passwd@192.168.x.x:8080

 
# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM microsoft/aspnetcore:2.0.0
WORKDIR /app
COPY --from=build-env /app/out .

# Copy trusted ca certs
ADD cacerts/ha_trial.ca.tar /usr/local/share/ca-certificates/
RUN update-ca-certificates

ENTRYPOINT ["dotnet", "Calendar.dll"]

