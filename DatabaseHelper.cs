
using Microsoft.Data.Sqlite;

namespace CottonTradingSystem;

public static class DatabaseHelper
{
    private static readonly string DatabasePath =
        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cotton.db");

    private static readonly string ConnectionString =
        $"Data Source={DatabasePath}";

    public static void InitializeDatabase()
    {
        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();

        string createTableQuery = @"
            CREATE TABLE IF NOT EXISTS Purchases
            (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                FarmerName TEXT NOT NULL,
                PurchaseDate TEXT NOT NULL,
                Quantity REAL NOT NULL,
                Rate REAL NOT NULL,
                Quality TEXT NOT NULL,
                ReceiptNumber TEXT,
                PaymentStatus TEXT NOT NULL,
                TotalAmount REAL NOT NULL
            );";

        using var command = new SqliteCommand(createTableQuery, connection);
        command.ExecuteNonQuery();
        
string createSalesTableQuery = @"
    CREATE TABLE IF NOT EXISTS Sales
    (
        Id INTEGER PRIMARY KEY AUTOINCREMENT,
        BuyerName TEXT NOT NULL,
        SaleDate TEXT NOT NULL,
        Quantity REAL NOT NULL,
        Rate REAL NOT NULL,
        ReceiptNumber TEXT,
        PaymentStatus TEXT NOT NULL,
        TotalAmount REAL NOT NULL
    );";

using var salesCommand =
    new SqliteCommand(createSalesTableQuery, connection);

salesCommand.ExecuteNonQuery();

        string createExpensesTableQuery = @"
            CREATE TABLE IF NOT EXISTS Expenses
            (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                ExpenseName TEXT NOT NULL,
                ExpenseDate TEXT NOT NULL,
                Amount REAL NOT NULL,
                PaymentStatus TEXT NOT NULL
            );";

        using var expensesCommand =
            new SqliteCommand(createExpensesTableQuery, connection);

        expensesCommand.ExecuteNonQuery();
        
        string createFarmersTableQuery = @"
            CREATE TABLE IF NOT EXISTS Farmers
            (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                FarmerName TEXT NOT NULL,
                MobileNumber TEXT,
                Village TEXT,
                Address TEXT,
                RegistrationDate TEXT NOT NULL
            );";

        using var farmersCommand =
            new SqliteCommand(createFarmersTableQuery, connection);

               farmersCommand.ExecuteNonQuery();
    }

    // Get total purchase amount
        
    // Get total purchase amount
    public static double GetTotalPurchases()
    {
        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();

        using var command = new SqliteCommand(
            "SELECT COALESCE(SUM(TotalAmount), 0) FROM Purchases",
            connection);

        return Convert.ToDouble(command.ExecuteScalar());
    }

    // Get total sales amount
    public static double GetTotalSales()
    {
        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();

        using var command = new SqliteCommand(
            "SELECT COALESCE(SUM(TotalAmount), 0) FROM Sales",
            connection);

        return Convert.ToDouble(command.ExecuteScalar());
    }

    // Get total expenses
    public static double GetTotalExpenses()
    {
        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();

        using var command = new SqliteCommand(
            "SELECT COALESCE(SUM(Amount), 0) FROM Expenses",
            connection);

        return Convert.ToDouble(command.ExecuteScalar());
    }

    // Get available cotton stock in kg
    public static double GetAvailableStock()
    {
        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();

        string query = @"
            SELECT
                (SELECT COALESCE(SUM(Quantity), 0) FROM Purchases)
                -
                (SELECT COALESCE(SUM(Quantity), 0) FROM Sales);";

        using var command = new SqliteCommand(query, connection);

        return Convert.ToDouble(command.ExecuteScalar());
    

    }
}
