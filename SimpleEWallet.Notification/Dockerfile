# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Salin file csproj dan restore dependencies
COPY ["SimpleEWallet.Notification/SimpleEWallet.Notification.csproj", "SimpleEWallet.Notification/"]
COPY ["SimpleEWallet.Comon/SimpleEWallet.Comon.csproj", "SimpleEWallet.Comon/"]
RUN dotnet restore "SimpleEWallet.Notification/SimpleEWallet.Notification.csproj"

# Salin seluruh isi solution
COPY . .

# Build project
WORKDIR /app/SimpleEWallet.Notification
RUN dotnet publish -c Release -o /out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /out .

# Jalankan aplikasi
ENTRYPOINT ["dotnet", "SimpleEWallet.Notification.dll"]
