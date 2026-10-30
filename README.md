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
- `Program.cs` - Application entry point.
- `DatabaseHelper.cs` - Database initialization and operations.
- `Form1.cs` - Main dashboard with live summary cards.
- `Form1.Designer.cs` - Dashboard UI components.
- `PurchaseForm.cs` - Cotton purchase module.
- `SaleForm.cs` - Cotton sales module.
- `FarmerForm.cs` - Farmer registration.
- `EditFarmerForm.cs` - Edit farmer information.
- `ViewFarmersForm.cs` - View registered farmers.
- `ViewPurchasesForm.cs` - View purchase records.
- `ViewSalesForm.cs` - View sales records.
- `ExpenseForm.cs` - Expense management.
- `ViewExpensesForm.cs` - View expense records.
- `StockForm.cs` - Stock management.
- `ReportsForm.cs` - Reports and summaries.

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

## Dashboard Summary
The dashboard displays live summary information:
- Total Purchase Amount
- Total Sales Amount
- Available Cotton Stock (kg)
- Total Expenses

The summary values refresh when users return from other modules.

## Additional Project Files
- `Form1.Designer.cs` - Dashboard UI components.
- `ViewPurchasesForm.cs` - View purchase records.
- `ViewSalesForm.cs` - View sales records.
- `ViewExpensesForm.cs` - View expense records.
- `ViewFarmersForm.cs` - View registered farmers.
- `EditFarmerForm.cs` - Edit farmer information.

## Standalone Windows Application
A self-contained Windows x64 version can be published using:

```powershell
dotnet publish -c Release -r win-x64 --self-contained true
