# Cotton Purchasing & Selling System

## Project Overview
The Cotton Purchasing & Selling System is an offline desktop application developed to simplify and manage cotton trading activities.

The application helps users maintain purchase records, sales records, farmer details, expenses, and cotton stock in one place.

## Technologies Used
- C#
- Windows Forms
- .NET 10
- SQLite
- Visual Studio

## Key Features
- **Dashboard:** Access all application modules.
- **Farmer Management:** Register, view, edit, and delete farmer details.
- **Cotton Purchase:** Record farmer name, purchase date, quantity, rate, quality, receipt number, and payment status.
- **Cotton Sales:** Maintain buyer details, sales quantity, rate, receipt number, and payment status.
- **Stock Management:** Calculate available cotton stock.
- **Expense Management:** Record additional expenses such as transportation.
- **Reports:** View purchase totals, sales totals, expenses, and an estimated result.
- **Offline Functionality:** Manage records locally using SQLite.

## Project Structure
- `Program.cs` – Application entry point.
- `DatabaseHelper.cs` – Database initialization and operations.
- `Form1.cs` – Main dashboard.
- `PurchaseForm.cs` – Cotton purchase module.
- `SaleForm.cs` – Cotton sales module.
- `FarmerForm.cs` – Farmer registration.
- `ExpenseForm.cs` – Expense management.
- `StockForm.cs` – Stock management.
- `ReportsForm.cs` – Reports.

## How to Run
1. Download or clone this repository.
2. Install the .NET 10 SDK and Windows Desktop development tools.
3. Open the project folder in Visual Studio.
4. Open `CottonTradingSystem.csproj`.
5. Build and run the application.

## Project Objective
The objective is to reduce manual record keeping, improve data organization, calculate available cotton stock, and make cotton purchasing and selling activities easier to manage.

## Future Improvements
- Advanced financial reports.
- Improved user authentication.
- Data backup and restore.
- Export reports to Excel and PDF.
- Enhanced user interface.

## Developer
Developed as a practical live project using C# Windows Forms and SQLite.
