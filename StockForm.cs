
using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace CottonTradingSystem;

public class StockForm : Form
{
    private Label purchasesLabel = new Label();
    private Label salesLabel = new Label();
    private Label stockLabel = new Label();

    public StockForm()
    {
        this.Text = "Cotton Stock Management";
        this.StartPosition = FormStartPosition.CenterScreen;
        this.Width = 550;
        this.Height = 350;

        Label heading = new Label();
        heading.Text = "Cotton Stock Management";
        heading.Font = new Font("Arial", 18, FontStyle.Bold);
        heading.AutoSize = true;
        heading.Location = new Point(60, 25);
        this.Controls.Add(heading);

        purchasesLabel.Location = new Point(50, 100);
        purchasesLabel.AutoSize = true;
        purchasesLabel.Font = new Font("Arial", 12, FontStyle.Bold);
        this.Controls.Add(purchasesLabel);

        salesLabel.Location = new Point(50, 150);
        salesLabel.AutoSize = true;
        salesLabel.Font = new Font("Arial", 12, FontStyle.Bold);
        this.Controls.Add(salesLabel);

        stockLabel.Location = new Point(50, 220);
        stockLabel.AutoSize = true;
        stockLabel.Font = new Font("Arial", 14, FontStyle.Bold);
        this.Controls.Add(stockLabel);

        LoadStock();
    }

    private void LoadStock()
    {
        string databasePath = System.IO.Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "cotton.db");

        using var connection =
            new SqliteConnection($"Data Source={databasePath}");

        connection.Open();

        string purchaseQuery =
            "SELECT COALESCE(SUM(Quantity), 0) FROM Purchases;";

        using var purchaseCommand =
            new SqliteCommand(purchaseQuery, connection);

        decimal totalPurchased =
            Convert.ToDecimal(purchaseCommand.ExecuteScalar());

        string salesQuery =
            "SELECT COALESCE(SUM(Quantity), 0) FROM Sales;";

        using var salesCommand =
            new SqliteCommand(salesQuery, connection);

        decimal totalSold =
            Convert.ToDecimal(salesCommand.ExecuteScalar());

        decimal remainingStock = totalPurchased - totalSold;

        purchasesLabel.Text =
            $"Total Cotton Purchased: {totalPurchased:N2} kg";

        salesLabel.Text =
            $"Total Cotton Sold: {totalSold:N2} kg";

        stockLabel.Text =
            $"Remaining Cotton Stock: {remainingStock:N2} kg";
    }
}
