# Campany.Joe(ASP.NET MVC)

A production-style **company management system** for managing **employees and departments** using **ASP.NET MVC**.  
The project focuses on clean architecture principles, separation of concerns, and maintainable code structure suitable for real-world enterprise applications.

---

## 🚀 Highlights

- **ASP.NET MVC** application with layered architecture.
- **Authentication & Authorization**
  - User login and role-based access (Admin / HR / User).
  - Secure access to employee and department management features.
- **Employees Management**
  - Create, update, delete, and view employees.
  - Assign employees to departments.
  - Search, filter, and list employees.
- **Departments Management**
  - Create, update, delete, and view departments.
  - View all employees inside a department.
- **Data Access**
  - **Entity Framework Core (SQL Server)** with migrations.
  - Repository pattern for clean data access.
- **Reliability & Maintainability**
  - Centralized error handling.
  - Validation using Data Annotations / Fluent Validation (if used).
  - Clean code and separation of concerns.

---

## 🧱 Solution Structure

```text
Company.Joe/
 ├─ Company.Joe.PL/   # MVC layer (Controllers, Views, ViewModels)
 │  ├─ Controllers/
 │  │   ├─ AccountController.cs      # Login, Logout, Register (if exists)
 │  │   ├─ EmployeesController.cs    # CRUD for employees
 |  |   ├─ DepartmentsController.cs    # CRUD for departments
 |  |   ├─ UserController.cs    # Index, Details, Edit, Delete
 |  |   ├─ RoleController.cs    # Index, Create, Details, Edit, Delete
 │  │   └─ HomeController.cs  # CRUD for departments
 │  ├─ Views/
 │  │   ├─ Employees/
 │  │   ├─ Departments/
 │  │   └─ Account/
 │  ├─ Helpers/
 |  ├─ Dtos/
 |  ├─ Mapping/
 │  └─ wwwroot/                      # Static files (css, js, images)
 │
 ├─ Company.Joe.BLL/    # Business Logic Layer
 │  ├─ Entities/
 │  │   ├─ Employee.cs
 │  │   └─ Department.cs
 │  ├─ Interfaces/                   # Repositories / Services contracts
 │  └─ DTOs / Models (if any)
 │
 ├─ Company.Joe.DAL/  # Data access layer
 │  ├─ Data/
 │  │   └─ AppDbContext.cs
 │  ├─ Migrations/
 │  ├─ Repositories/
 │  │   ├─ GenericRepository.cs
 │  │   ├─ EmployeeRepository.cs
 │  │   └─ DepartmentRepository.cs
 │  └─ UnitOfWork.cs (if used)
 └─
```
---

## 🗝️  Key Features (Examples)

Authentication

Login / Logout

Role-based authorization for admin features

Employees

GET /Employees — list all employees

GET /Employees/Details/{id} — view employee details

GET /Employees/Create — create new employee

POST /Employees/Create

GET /Employees/Edit/{id}

POST /Employees/Edit/{id}

POST /Employees/Delete/{id}

Departments

GET /Departments — list all departments

GET /Departments/Details/{id} — view department details + employees

GET /Departments/Create

POST /Departments/Create

GET /Departments/Edit/{id}

POST /Departments/Edit/{id}

POST /Departments/Delete/{id}


## 🧪 Tech Stack

ASP.NET MVC

Entity Framework Core

SQL Server

Razor Views

Bootstrap / CSS (for UI, if used)

AutoMapper (if used)

Authentication & Authorization (ASP.NET Identity or custom)
    ├─ EmployeeService.cs
    └─ DepartmentService.cs


## 👨‍💻 Author

**Youssef Taha**  
- 📧 Email: yousif.t.abdulwahab@gmail.com 
- 🔗 [LinkedIn](https://www.linkedin.com/in/yousif-taha-89454922b/)  
- 🔗 [GitHub](https://github.com/yosif-taha)  
---
