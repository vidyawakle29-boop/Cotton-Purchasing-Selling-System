
using Microsoft.Data.Sqlite;

namespace CottonTradingSystem;

public class ViewPurchasesForm : Form
{
    private readonly DataGridView purchasesGrid = new DataGridView();

    public ViewPurchasesForm()
    {
        this.Text = "Saved Purchase Records";
        this.StartPosition = FormStartPosition.CenterScreen;
        this.Width = 1100;
        this.Height = 500;

        purchasesGrid.Dock = DockStyle.Fill;
        purchasesGrid.ReadOnly = true;
        purchasesGrid.AllowUserToAddRows = false;
        purchasesGrid.AutoSizeColumnsMode =
            DataGridViewAutoSizeColumnsMode.Fill;
        purchasesGrid.SelectionMode =
            DataGridViewSelectionMode.FullRowSelect;

        this.Controls.Add(purchasesGrid);

        LoadPurchases();
    }

    private void LoadPurchases()
    {
        string databasePath = System.IO.Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "cotton.db");

        using var connection = new SqliteConnection(
            $"Data Source={databasePath}");

        connection.Open();

        string query = @"
            SELECT Id, FarmerName, PurchaseDate, Quantity, Rate,
                   Quality, ReceiptNumber, PaymentStatus, TotalAmount
            FROM Purchases
            ORDER BY Id DESC;";

        using var command = new SqliteCommand(query, connection);
        using var reader = command.ExecuteReader();

        var table = new System.Data.DataTable();
        table.Load(reader);

        purchasesGrid.DataSource = table;
    }
}
