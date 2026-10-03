[unibooking_readme.md](https://github.com/user-attachments/files/32985804/unibooking_readme.md)
# 📅 UniBooking - Shared Resource & Reservation Management System

> A robust, full-stack enterprise web application designed to manage and schedule shared organizational resources (rooms, labs, devices, facilities) seamlessly, preventing scheduling conflicts and automating reservation workflows.

## 🌟 Overview & Architecture

UniBooking is built following **Clean Architecture** principles to enforce separation of concerns, high testability, and maintainability. The core backend relies on domain-driven design concepts, leveraging custom middleware for global exception handling and JWT authentication for security.

```text
                  ┌────────────────────────┐
                  │     Client / React     │
                  └───────────┬────────────┘
                              │ HTTP / REST API
                              ▼
        ┌──────────────────────────────────────────────┐
        │                 Web API Layer                │
        │    (Controllers, Middlewares, Filters)       │
        └─────────────────────┬────────────────────────┘
                              │
                              ▼
        ┌──────────────────────────────────────────────┐
        │               Application Layer              │
        │      (Services, DTOs, Mappers, Logic)        │
        └─────────────────────┬────────────────────────┘
                              │
                              ▼
        ┌──────────────────────────────────────────────┐
        │                 Domain Layer                 │
        │         (Entities, Core Contracts)           │
        └─────────────────────▲────────────────────────┘
                              │
        ┌─────────────────────┴────────────────────────┐
        │             Infrastructure Layer             │
        │     (EF Core, SQL Server, Auth, Services)    │
        └──────────────────────────────────────────────┘
```

## 🛠️ Tech Stack & Tools

* **Backend Framework:** .NET Core / ASP.NET Core Web API
* **Architecture:** Clean Architecture, Repository Pattern, Dependency Injection
* **Persistence & Database:** Entity Framework Core, SQL Server, Code-First Migrations
* **Security & Auth:** JWT (JSON Web Tokens), Role-Based Access Control (RBAC), Password Hashing
* **Error Handling:** Centralized Global Exception Middleware
* **Frontend:** React.js, Axios, React Router, Modern CSS / Tailwind
* **API Documentation:** Swagger / OpenAPI

## ✨ Key Features

- [x] **Resource Management:** Create, update, and manage shared assets (equipment, conference rooms, study halls) with availability schedules.
- [x] **Conflict-Free Reservation System:** Advanced validation logic to prevent double-booking or overlapping time slots.
- [x] **Authentication & Authorization:** Secure JWT-based authentication supporting multiple roles (User, Admin, Manager).
- [x] **Consistent API Response:** Standardized response wrappers and global exception handling for clean error responses.
- [x] **Responsive UI:** Fast, user-friendly dashboard built with React for viewing availability calendars and booking slots.

## 🚀 Getting Started

Follow these steps to set up and run the project locally.

### Prerequisites

* [.NET 8.0 SDK](https://dotnet.microsoft.com/download) or later
* [Node.js](https://nodejs.org/) (v18+)
* [SQL Server](https://www.microsoft.com/en-us/sql-server/) (Express or LocalDB)

### 1. Backend Setup

```bash
# Clone the repository
git clone https://github.com/yousefjrad/UniBooking.git
cd UniBooking/Backend

# Update Database Connection String in appsettings.json
# Default setting connects to LocalDB / SQL Server Express

# Apply Entity Framework Migrations
dotnet ef database update

# Run the API server
dotnet run
```

The API will start at `https://localhost:7123` (or configured port). Access Swagger documentation at `https://localhost:7123/swagger`.

### 2. Frontend Setup

```bash
# Navigate to the frontend directory
cd ../Frontend

# Install dependencies
npm install

# Start the React development server
npm start
```

## 📸 API Documentation & Testing

The backend exposes a comprehensive RESTful API fully documented via Swagger UI.

| Endpoint Group | Description |
| :--- | :--- |
| `/api/auth` | User registration, login, and JWT token issuance |
| `/api/resources` | CRUD operations for managing shared resources |
| `/api/reservations` | Booking requests, approvals, slot checking, and cancellations |
| `/api/users` | User management and profile configuration |

## 👤 Author

**Yousef Jrad**
* **Role:** Full-Stack Developer (.NET Core & React)
* **GitHub:** [@yousefjrad](https://github.com/yousefjrad)
* **LinkedIn:** [Yousef Jrad](https://linkedin.com/in/yousefjrad)

## 📄 License

This project is open-source and available under the [MIT License](LICENSE).
