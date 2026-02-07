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
 │
 └─ CompanyManagementSystem.Services/ # Business logic layer
    ├─ EmployeeService.cs
    └─ DepartmentService.cs
