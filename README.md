# 🚀 API + Front - Sistema de Reservas

## 📌 Sobre o Projeto

Este projeto é uma aplicação completa para gerenciamento de reservas de suítes em motéis. Ele inclui:

- **Backend** em **.NET Core** com **Entity Framework** e **MySQL**.
- **Frontend** em **Next.js (React Framework)**.
- **Autenticação com JWT** para segurança de usuários.
- **Cache e otimização** para melhor performance.
- **Swagger** para documentação da API.

---

## 🛠️ Tecnologias Utilizadas

### Backend
- **.NET Core**
- **Entity Framework Core**
- **MySQL**
- **JWT para autenticação**
- **Docker (para banco de dados)**
- **Swagger (documentação da API)**

### Frontend
- **Next.js (React Framework)**
- **TailwindCSS (Estilização)**
- **Axios (Requisições HTTP)**
- **React Hooks e Context API**

---

## 📂 Estrutura do Projeto

### 📌 Backend (`API-Reservas/backend`)

```
📦 backend
 ┣ 📂 config
 ┣ 📂 Controllers
 ┣ 📂 Data
 ┣ 📂 DTOs
 ┣ 📂 Migrations
 ┣ 📂 Models
 ┣ 📂 Repositories
 ┣ 📂 Services
 ┣ 📂 Utils
 ┣ 📜 appsettings.json
 ┣ 📜 appsettings.Development.json
 ┣ 📜 backend.http
 ┣ 📜 Program.cs
 ┗ 📂 obj / bin (.NET compilação)
```

### 📌 Frontend (`API-Reservas/frontend`)

```
📦 frontend
 ┣ 📂 .next (build do Next.js)
 ┣ 📂 node_modules
 ┣ 📂 public
 ┣ 📂 src
 ┃ ┣ 📂 app
 ┃ ┃ ┣ 📂 booking
 ┃ ┃ ┣ 📂 components
 ┃ ┃ ┣ 📂 hoc
 ┃ ┃ ┣ 📂 login
 ┃ ┃ ┣ 📂 motels
 ┃ ┃ ┣ 📂 register
 ┃ ┃ ┣ 📂 revenue
 ┃ ┃ ┣ 📂 services
 ┃ ┃ ┗ 📂 user
 ┃ ┣ 📜 globals.css
 ┃ ┗ 📜 layout.js
 ┣ 📜 .gitignore
 ┣ 📜 jsconfig.json
 ┣ 📜 next.config.mjs
 ┣ 📜 package.json
 ┣ 📜 package-lock.json
 ┣ 📜 postcss.config.mjs
 ┣ 📜 README.md
 ┗ 📜 tailwind.config.mjs
```

---

## 🚀 Como Rodar o Projeto

### 🛠️ Pré-requisitos

Antes de iniciar, você precisa ter instalado:

- **.NET 7+**
- **Docker** (para rodar o banco de dados)
- **Node.js + npm** (para rodar o frontend)
- **Jetbrains Rider**, ou outro similar
- **Postman** (opcional, para testar a API)

---

## 🏗️ Configuração e Execução do **Backend**

### 1️⃣ **Criando o Container MySQL com Docker**
##### **Windows**:
```
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=123456" -p 1433:1433 --name sqlserver -d mcr.microsoft.com/mssql/server:latest
```
##### **Linux ou Mac**:
```
docker run --platform linux/amd64 -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=123456" -p 1433:1433 --name sqlserver -d mcr.microsoft.com/mssql/server:latest
```
> **⚠️** A porta **1433** foi usada pois a porta padrão do SQL Server estava ocupada. Trocar por 3306, opcional.

### 2️⃣ **Configurando a String de Conexão**
No arquivo `appsettings.json`:

```
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Port=1433;Database=BookingDB;User Id=sa;Password=123456;"
}
```

### 3️⃣ **Executando as Migrations**

no processo de build as migrations são executadas, mas opcionalmente, pode ser utilizado:

```
dotnet ef database update
```

### 4️⃣ **Rodando a API**
```
dotnet run
```
ou, para hot reload:
```
dotnet watch run
```

A API estará disponível em:
```
https://localhost:7009
```
### 📌 Acessando o **Swagger**
A documentação da API pode ser acessada em:
```
https://localhost:7009/swagger/index.html
```

---

## 🏗️ Configuração e Execução do **Frontend**

### 1️⃣ **Instalar dependências**
```
cd frontend
npm install
```

### 2️⃣ **Rodar o projeto**
```
npm run dev
```
A aplicação será executada em:
```
http://localhost:3000
```

---
## 📌 Endpoints da API

### 🔑 **Autenticação**
- **POST** `/api/auth/register` → Cadastra um novo usuário.
- **POST** `/api/auth/login` → Realiza login e retorna um token JWT.
- **GET** `/api/auth/user` → Obtém os dados do usuário autenticado.

### 📅 **Reservas**
- **GET** `/api/bookings` → Lista todas as reservas.
- **POST** `/api/bookings` → Cria uma nova reserva.
- **PUT** `/api/bookings/{id}` → Atualiza uma reserva existente.
- **DELETE** `/api/bookings/{id}` → Remove uma reserva.

### 🏨 **Motéis**
- **GET** `/api/motels` → Lista todos os motéis.
- **POST** `/api/motels` → Cria um novo motel.
- **GET** `/api/motels/{id}` → Obtém detalhes de um motel específico.
- **PUT** `/api/motels/{id}` → Atualiza um motel existente.
- **DELETE** `/api/motels/{id}` → Remove um motel.

### 📊 **Relatórios**
- **GET** `/api/revenue` → Obtém o faturamento mensal.

### 🏢 **Suítes**
- **GET** `/api/suites` → Lista todas as suítes.
- **POST** `/api/suites` → Cria uma nova suíte.
- **GET** `/api/suites/{id}` → Obtém detalhes de uma suíte específica.
- **PUT** `/api/suites/{id}` → Atualiza uma suíte existente.
- **DELETE** `/api/suites/{id}` → Remove uma suíte.


## 🏗️ Estrutura do Banco de Dados (DDL)

```sql
CREATE TABLE BookingDB.Motels (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Address VARCHAR(200) NULL
);

CREATE TABLE BookingDB.Suites (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    MotelId INT NOT NULL,
    SuiteName VARCHAR(100) NOT NULL,
    Description VARCHAR(255) NULL,
    BasePrice DECIMAL(65,30) NOT NULL,
    CONSTRAINT FK_Suites_Motels FOREIGN KEY (MotelId) REFERENCES BookingDB.Motels(Id) ON DELETE CASCADE
);

CREATE TABLE BookingDB.Users (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Email VARCHAR(255) NOT NULL UNIQUE,
    PasswordHash TEXT NOT NULL,
    CreatedAt DATETIME NOT NULL
);

CREATE TABLE BookingDB.Bookings (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    UserId INT NOT NULL,
    SuiteId INT NOT NULL,
    CheckInDate DATETIME NOT NULL,
    CheckOutDate DATETIME NOT NULL,
    Price DECIMAL(65,30) NOT NULL,
    CreatedAt DATETIME NOT NULL,
    CONSTRAINT FK_Bookings_Users FOREIGN KEY (UserId) REFERENCES BookingDB.Users(Id) ON DELETE CASCADE,
    CONSTRAINT FK_Bookings_Suites FOREIGN KEY (SuiteId) REFERENCES BookingDB.Suites(Id) ON DELETE CASCADE
);
```

---

## 📝 Diagrama de entidade-relacionamento
    
![DER](https://github.com/user-attachments/assets/8382e4b9-ccf4-4416-889b-31a4416fbabb)


---

## 📄 Licença

Este projeto está licenciado sob a **MIT License**.
