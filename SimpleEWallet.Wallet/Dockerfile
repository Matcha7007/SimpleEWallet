# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Salin file csproj dan restore dependencies
COPY ["SimpleEWallet.Wallet/SimpleEWallet.Wallet.csproj", "SimpleEWallet.Wallet/"]
COPY ["SimpleEWallet.Comon/SimpleEWallet.Comon.csproj", "SimpleEWallet.Comon/"]
RUN dotnet restore "SimpleEWallet.Wallet/SimpleEWallet.Wallet.csproj"

# Salin seluruh isi solution
COPY . .

COPY SimpleEWallet.Wallet/appsettings.json ./SimpleEWallet.Wallet/appsettings.json

# Build project
WORKDIR /app/SimpleEWallet.Wallet
RUN dotnet publish -c Release -o /out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /out .

# Jalankan aplikasi
ENTRYPOINT ["dotnet", "SimpleEWallet.Wallet.dll"]
