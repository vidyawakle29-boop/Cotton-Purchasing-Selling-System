
using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace CottonTradingSystem;

public class ReportsForm : Form
{
    private Label purchasesLabel = new Label();
    private Label salesLabel = new Label();
    private Label expensesLabel = new Label();
    private Label resultLabel = new Label();

    public ReportsForm()
    {
        this.Text = "Cotton Business Reports";
        this.StartPosition = FormStartPosition.CenterScreen;
        this.Width = 600;
        this.Height = 400;

        Label heading = new Label();
        heading.Text = "Business Reports Dashboard";
        heading.Location = new Point(100, 25);
        heading.AutoSize = true;
        heading.Font = new Font("Arial", 18, FontStyle.Bold);
        this.Controls.Add(heading);

        purchasesLabel.Location = new Point(50, 100);
        purchasesLabel.AutoSize = true;
        purchasesLabel.Font = new Font("Arial", 12, FontStyle.Bold);
        this.Controls.Add(purchasesLabel);

        salesLabel.Location = new Point(50, 145);
        salesLabel.AutoSize = true;
        salesLabel.Font = new Font("Arial", 12, FontStyle.Bold);
        this.Controls.Add(salesLabel);

        expensesLabel.Location = new Point(50, 190);
        expensesLabel.AutoSize = true;
        expensesLabel.Font = new Font("Arial", 12, FontStyle.Bold);
        this.Controls.Add(expensesLabel);

        resultLabel.Location = new Point(50, 245);
        resultLabel.AutoSize = true;
        resultLabel.Font = new Font("Arial", 14, FontStyle.Bold);
        this.Controls.Add(resultLabel);

        LoadReport();
    }

    private void LoadReport()
    {
        string databasePath = System.IO.Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "cotton.db");

        using var connection =
            new SqliteConnection($"Data Source={databasePath}");

        connection.Open();

        decimal totalPurchases = GetTotal(connection, "Purchases");
        decimal totalSales = GetTotal(connection, "Sales");
        decimal totalExpenses = GetTotal(connection, "Expenses");

        purchasesLabel.Text =
            $"Total Purchases: ₹{totalPurchases:N2}";

        salesLabel.Text =
            $"Total Sales: ₹{totalSales:N2}";

        expensesLabel.Text =
            $"Total Expenses: ₹{totalExpenses:N2}";

        decimal estimatedResult =
            totalSales - totalPurchases - totalExpenses;

        resultLabel.Text =
            $"Simple Estimated Result: ₹{estimatedResult:N2}";
    }

    private decimal GetTotal(SqliteConnection connection, string tableName)
    {
        string query = $"SELECT COALESCE(SUM(TotalAmount), 0) FROM {tableName};";

        if (tableName == "Expenses")
        {
            query = "SELECT COALESCE(SUM(Amount), 0) FROM Expenses;";
        }

        using var command = new SqliteCommand(query, connection);

        return Convert.ToDecimal(command.ExecuteScalar());
    }
}
