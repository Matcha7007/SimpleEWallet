# Mini Project Simple e-Wallet API

Simple e-Wallet API adalah sebuah mini project microservice REST API yang dibangun menggunakan .NET 8. Project ini menggunakan PostgreSQL sebagai database, MediatR untuk komunikasi langsung antar objek tanpa perlu dependency injection manual, serta RabbitMQ sebagai message broker antar layanan.

API ini menyediakan fitur-fitur dasar (CRUD) seperti Create, Read, Update, dan Delete. Melalui project ini, pengguna dapat:

- Mendaftar untuk membuat akun e-wallet,
- Login ke sistem,
- Melakukan top-up saldo,
- Transfer saldo ke pengguna lain,
- Melihat mutasi transaksi,
- Memperbarui data diri,
- Menghapus akun e-wallet,
- Melihat daftar penerima transfer.

## API Reference
Untuk lengkapnya serta cara penggunaan, port ada di folder postman dan import ke postman kamu!

### 🛡️ Auth/User Service
Auth Service berjalan pada port 7095
#### 🔐 Login
```http
POST /Auth/login
```
Login menggunakan nomor telepon dan password.

| Body Parameter | Type     | Description                     |
|----------------|----------|---------------------------------|
| `phone`        | `string` | **Required**. Nomor telepon     |
| `password`     | `string` | **Required**. Password          |

#### 🔐 Claim Token
```http
POST /Auth/claim-token
```
Validasi dan klaim token JWT.

| Header         | Type     | Description               |
|----------------|----------|---------------------------|
| `Authorization`| `string` | **Required**. Bearer Token|

#### 🧪 Cek Auth Service Online
```http
GET /Auth/is-online
```

#### 👤 Register User
```http
POST /User/create
```

| Body Parameter | Type     | Description                  |
|----------------|----------|------------------------------|
| `username`     | `string` | **Required**. Username unik  |
| `fullName`     | `string` | **Required**. Nama lengkap   |
| `email`        | `string` | **Required**. Email pengguna |
| `phone`        | `string` | **Required**. Nomor telepon  |
| `password`     | `string` | **Required**. Password       |
| `pin`          | `string` | **Required**. 6 digit PIN    |

#### 👤 Update User
```http
POST /User/update
```

| Header         | Type     | Description               |
|----------------|----------|---------------------------|
| `Authorization`| `string` | **Required**. Bearer Token|

| Body Parameter | Type     | Description              |
|----------------|----------|--------------------------|
| `userId`       | `string` | **Required**. ID user     |

#### 👤 Get Self Data
```http
POST /User/get-self-data
```

| Header         | Type     | Description               |
|----------------|----------|---------------------------|
| `Authorization`| `string` | **Required**. Bearer Token|

#### 👤 Get User By ID
```http
POST /User/get-by-id
```

| Header         | Type     | Description               |
|----------------|----------|---------------------------|
| `Authorization`| `string` | **Required**. Bearer Token|

| Body Parameter | Type     | Description        |
|----------------|----------|--------------------|
| `userId`       | `string` | **Required**. ID user |

#### ❌ Delete User
```http
POST /User/delete
```

| Header         | Type     | Description               |
|----------------|----------|---------------------------|
| `Authorization`| `string` | **Required**. Bearer Token|

| Body Parameter | Type     | Description        |
|----------------|----------|--------------------|
| `userId`       | `string` | **Required**. ID user |

#### 📇 Search Receiver
```http
POST /User/search-receiver
```

| Header         | Type     | Description               |
|----------------|----------|---------------------------|
| `Authorization`| `string` | **Required**. Bearer Token|

| Body Parameter     | Type        | Description                     |
|--------------------|-------------|---------------------------------|
| `pageNumber`       | `int`       | Nomor halaman                   |
| `pageSize`         | `int`       | Jumlah data per halaman         |
| `orderBy`          | `string`    | Kolom untuk sorting             |
| `isAscending`      | `boolean`   | Urutan ascending atau tidak     |
| `filter.keyword`   | `string`    | Keyword pencarian (opsional)    |

---

### 💰 Wallet Service
Wallet Service berjalan pada port 7061
#### 👜 Get Wallet by User ID
```http
POST /Wallet/get-by-user-id
```

| Header         | Type     | Description               |
|----------------|----------|---------------------------|
| `Authorization`| `string` | **Required**. Bearer Token|

| Body Parameter | Type     | Description         |
|----------------|----------|---------------------|
| `userId`       | `string` | **Required**. ID user |

#### 👜 Get Self Wallet
```http
POST /Wallet/get-self-wallet
```

| Header         | Type     | Description               |
|----------------|----------|---------------------------|
| `Authorization`| `string` | **Required**. Bearer Token|

#### ➕ Topup Wallet
```http
POST /Wallet/topup-request
```

| Header         | Type     | Description               |
|----------------|----------|---------------------------|
| `Authorization`| `string` | **Required**. Bearer Token|

| Body Parameter | Type     | Description                |
|----------------|----------|----------------------------|
| `walletNumber` | `string` | **Required**. Nomor wallet |
| `amount`       | `int`    | **Required**. Jumlah topup |

#### 🔁 Transfer Wallet
```http
POST /Wallet/transfer-request
```

| Header         | Type     | Description               |
|----------------|----------|---------------------------|
| `Authorization`| `string` | **Required**. Bearer Token|

| Body Parameter         | Type     | Description                     |
|------------------------|----------|---------------------------------|
| `walletNumberReceiver`| `string` | **Required**. Wallet tujuan     |
| `amount`              | `int`    | **Required**. Jumlah transfer   |
| `pin`                 | `string` | **Required**. PIN pengguna      |
| `description`         | `string` | **Optional**. Keterangan transfer |

---

### 📄 Transaction Service
Transaction Service berjalan pada port 7294
#### 📜 Get Transaction History
```http
POST /Transaction/search-transaction-history
```

| Header         | Type     | Description               |
|----------------|----------|---------------------------|
| `Authorization`| `string` | **Required**. Bearer Token|

| Body Parameter            | Type      | Description                  |
|---------------------------|-----------|------------------------------|
| `pageNumber`              | `int`     | Nomor halaman                |
| `pageSize`                | `int`     | Jumlah data per halaman      |
| `orderBy`                 | `string`  | Kolom sorting                |
| `isAscending`             | `boolean` | Ascending/descending         |
| `filter.walletId`         | `string`  | **Required**. ID wallet      |
| `filter.filterPeriodFrom`| `string`  | Tanggal mulai (YYYY-MM-DD)   |
| `filter.filterPeriodTo`  | `string`  | Tanggal akhir (YYYY-MM-DD)   |

#### 🧪 Cek Transaction Service Online
```http
GET /Transaction/is-online
```
## 🚀 Run Locally

Clone project dan pastikan berada di branch `version_1`:

```bash
git clone https://github.com/Matcha7007/SimpleEWallet.git
```

Masuk ke direktori project:

```bash
cd SimpleEWallet
```

Jika belum menginstal Docker, silakan install terlebih dahulu melalui:  
👉 [https://docs.docker.com/get-started/get-docker/](https://docs.docker.com/get-started/get-docker/)

Setelah Docker siap, jalankan perintah berikut:

```bash
docker compose up --build
```

Semua dependency termasuk RabbitMQ dan database untuk setiap service akan otomatis dibangun dan dijalankan melalui Docker. Project siap digunakan!

## 🧰 Tech Stack

- **Framework**: .NET 8 (ASP.NET Core Web API)
- **Architecture**: Microservices
- **Mediator Pattern**: MediatR
- **Database**: PostgreSQL (per service)
- **Message Broker**: RabbitMQ
- **Testing & API Client**: Postman
- **Containerization**: Docker + Docker Compose


## Authors
Ilham [@Matcha7007](https://www.github.com/matcha7007)

