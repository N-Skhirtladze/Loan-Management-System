# Loan Management System

A REST API for managing customers, loans, and payments — built with ASP.NET Core and Entity Framework Core.

## Features

- Customer management (create, retrieve)
- Loan applications with credit score validation
- Automatic loan schedule generation
- Payment processing with credit score adjustments
- Swagger/OpenAPI documentation

## Tech Stack

- **.NET 8**
- **Entity Framework Core** (Pomelo MySQL provider)
- **MySQL**
- **Swagger / Swashbuckle** for API documentation

## Prerequisites

Before running this project, make sure you have installed:

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [MySQL Server](https://dev.mysql.com/downloads/mysql/) (running locally or remotely)
- (Optional) [Entity Framework Core CLI tools](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)

```bash
dotnet tool install --global dotnet-ef
```

## Setup Instructions

### 1. Clone the repository

```bash
git clone <your-repo-url>
cd loan_management_system
```

### 2. Configure the database connection

Open `appsettings.json` (or `appsettings.Development.json`) and update the connection string with your MySQL credentials:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=loan_management_system;User=root;Password=yourpassword;"
  }
}
```

### 3. Apply database migrations

This will create the database and tables based on the project's models:

```bash
dotnet ef database update
```

> If you don't have migrations yet, generate one first:
> ```bash
> dotnet ef migrations add InitialCreate
> dotnet ef database update
> ```

### 4. Restore dependencies

```bash
dotnet restore
```

### 5. Run the project

```bash
dotnet run
```

The console will show the URL the app is running on, e.g.:

Now listening on: https://localhost:7073
Now listening on: http://localhost:5038


### 6. Open Swagger UI

Navigate to:
https://localhost:7073/swagger

(replace the port with the one shown in your console)

From here you can explore and test all available API endpoints.

## API Overview

| Endpoint                                     | Method | Description                     |
|----------------------------------------------|--------|---------------------------------|
| `/api/Customers`                             | POST   | Create a new customer           |
| `/api/Customers/{id}`                        | GET    | Get customer details with loans |
| `/api/Customers/loans`                       | GET    | Get all loans for a customer    |
| `/api/Customers/AllCustomers`                | GET    | Get all customer's details      |
| `/api/Customers/DeleteCustomer/{customerId}` | DELETE | Delete custumer                 |
| `/api/Loans/CreateApplication`               | POST   | Apply for a new loan            |
| `/api/Loans/GetLoan/{id}`                    | GET    | Get loan details                |
| `/api/Loans/{id}`                            | GET    | Get loan status                 |
| `/api/Loans/GetAllLoans`                     | GET    | Get all loan details            |
| `/api/Loans/DeleteLoan/{id}`                 | DELETE | Delete loan                     | 
| `/api/Payments/{loanId}`                     | POST   | Make a payment on a loan        |

> Endpoints may vary — check Swagger UI for the full, up-to-date list.