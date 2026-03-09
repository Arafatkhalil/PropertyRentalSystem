# Property Rental Management System

A full-stack web application for managing properties, tenants, and lease contracts with business rule enforcement.

## Tech Stack

| Layer | Technology |
|-------|-----------|
| Backend | ASP.NET Core Web API (.NET 10) |
| Frontend | Angular 21 |
| Database | SQL Server (LocalDB) |
| ORM | Entity Framework Core (Code First) |
| Architecture | Clean Architecture |
| Mapping | AutoMapper |

## Project Structure

```
PropertyRental.Domain/          → Entities (Property, Tenant, Lease)
PropertyRental.Application/     → DTOs, Interfaces, Services, Mappings
PropertyRental.Infrastructure/  → DbContext, DI Registration, Migrations
PropertyRental.API/             → Controllers, Program.cs
PropertyRentalFrontend/         → Angular SPA (Components, Services, Models)
```

## Business Rules

1. A property cannot be leased if it is not available
2. A property cannot have overlapping lease periods
3. When a lease is created, the property automatically becomes unavailable

## API Endpoints

### Properties
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/properties` | Get all properties |
| GET | `/api/properties/{id}` | Get property by ID |
| POST | `/api/properties` | Create a new property |
| PUT | `/api/properties/{id}` | Update a property |
| DELETE | `/api/properties/{id}` | Delete a property |

### Tenants
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/tenants` | Get all tenants |
| GET | `/api/tenants/{id}` | Get tenant by ID |
| POST | `/api/tenants` | Create a new tenant |
| PUT | `/api/tenants/{id}` | Update a tenant |
| DELETE | `/api/tenants/{id}` | Delete a tenant |

### Leases
| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/leases` | Create a new lease |
| GET | `/api/leases/property/{propertyId}` | Get leases by property |
| PUT | `/api/leases/end/{id}` | End an active lease |

## How to Run

### Prerequisites
- .NET 10 SDK
- Node.js (v18+)
- Angular CLI (`npm install -g @angular/cli`)
- SQL Server LocalDB

### Backend

```bash
cd PropertyRental.API
dotnet run
```

The API will start on `http://localhost:5030`. Swagger UI is available at `/swagger`.

The database is created automatically on first run using EF Core migrations.

### Frontend

```bash
cd PropertyRentalFrontend
npm install
ng serve
```

Open `http://localhost:4200` in your browser.

## Architecture Overview

The project follows **Clean Architecture** with clear separation of concerns:

- **Domain Layer**: Contains the core entities (`Property`, `Tenant`, `Lease`) with no external dependencies.
- **Application Layer**: Contains business logic (services), DTOs, interfaces, and AutoMapper profiles. Depends only on the Domain layer.
- **Infrastructure Layer**: Implements data access using EF Core, registers dependencies, and contains database migrations.
- **API Layer**: Handles HTTP requests/responses through controllers. Configures the application pipeline (CORS, Swagger, etc.).
