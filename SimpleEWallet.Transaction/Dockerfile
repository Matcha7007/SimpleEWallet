# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Salin file csproj dan restore dependencies
COPY ["SimpleEWallet.Transaction/SimpleEWallet.Transaction.csproj", "SimpleEWallet.Transaction/"]
COPY ["SimpleEWallet.Comon/SimpleEWallet.Comon.csproj", "SimpleEWallet.Comon/"]
RUN dotnet restore "SimpleEWallet.Transaction/SimpleEWallet.Transaction.csproj"

# Salin seluruh isi solution
COPY . .

COPY SimpleEWallet.Transaction/appsettings.json ./SimpleEWallet.Transaction/appsettings.json

# Build project
WORKDIR /app/SimpleEWallet.Transaction
RUN dotnet publish -c Release -o /out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /out .

# Jalankan aplikasi
ENTRYPOINT ["dotnet", "SimpleEWallet.Transaction.dll"]
