# Board Game Logger 🎲

## Project Overview
Board Game Logger is a specialized web application designed for board game enthusiasts to manage their personal collections and track game loans. Developed for the **ASP.NET Fundamentals Retake**, this project focuses on robust data integrity and clean MVC architecture.

## Key Features
* **Board Game Management**: Full CRUD operations for tracking titles, release years, and publishers.
* **Publisher Registry**: Comprehensive management with **referential integrity checks** (prevents deleting publishers with assigned games).
* **Loan Registry**: Track who has borrowed which game and when.
* **Three-Layer Validation**: Implements Client-side (jQuery), Server-side (ModelState), and Database-level constraints for maximum reliability.

## Technologies Used
* **Framework**: ASP.NET Core 8 (MVC)
* **Database**: SQL Server & Entity Framework Core
* **Design**: Bootstrap 5
* **Patterns**: Service Layer & Dependency Injection

## ⚙️ Setup and Installation
1.  **Clone the repository**:
    `git clone https://github.com/AsanMahmudov/BoardGameLogger`

2.  **Configure the Database**:
    Open `appsettings.json` and ensure the `DefaultConnection` matches your local instance:
    * *Default SQL Express*: `Server=.\\SQLEXPRESS;Database=BoardGameLoggerDatabase;Trusted_Connection=True;MultipleActiveResultSets=true;encrypt=false`

3.  **Apply Migrations**:
    Open the **Package Manager Console**, set the **Default Project** to `BoardGameLogger.Data`, and run:
    `Update-Database`
    *(Note: This creates the schema and seeds initial test data automatically.)*

4.  **Launch**:
    Press `F5` or use the `dotnet run` command.
