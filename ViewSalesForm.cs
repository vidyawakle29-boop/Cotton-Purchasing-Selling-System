

using Microsoft.Data.Sqlite;

namespace CottonTradingSystem;

public class ViewSalesForm : Form
{
    private readonly DataGridView salesGrid = new DataGridView();

    public ViewSalesForm()
    {
        this.Text = "Saved Sale Records";
        this.StartPosition = FormStartPosition.CenterScreen;
        this.Width = 1100;
        this.Height = 500;

        salesGrid.Dock = DockStyle.Fill;
        salesGrid.ReadOnly = true;
        salesGrid.AllowUserToAddRows = false;
        salesGrid.AutoSizeColumnsMode =
            DataGridViewAutoSizeColumnsMode.Fill;
        salesGrid.SelectionMode =
            DataGridViewSelectionMode.FullRowSelect;
        salesGrid.MultiSelect = false;

        Button deleteButton = new Button();
        deleteButton.Text = "Delete Selected Sale";
        deleteButton.Dock = DockStyle.Bottom;
        deleteButton.Height = 40;
        deleteButton.Click += DeleteSelectedSale;

        this.Controls.Add(salesGrid);
        this.Controls.Add(deleteButton);

        LoadSales();
    }

    private void LoadSales()
    {
        string databasePath = System.IO.Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "cotton.db");

        using var connection =
            new SqliteConnection($"Data Source={databasePath}");

        connection.Open();

        string query = @"
            SELECT Id, BuyerName, SaleDate, Quantity, Rate,
                   ReceiptNumber, PaymentStatus, TotalAmount
            FROM Sales
            ORDER BY Id DESC;";

        using var command = new SqliteCommand(query, connection);
        using var reader = command.ExecuteReader();

        var table = new System.Data.DataTable();
        table.Load(reader);

        salesGrid.DataSource = table;
    }

    private void DeleteSelectedSale(object? sender, EventArgs e)
    {
        if (salesGrid.SelectedRows.Count == 0)
        {
            MessageBox.Show("Please select a sale record first.");
            return;
        }

        int id = Convert.ToInt32(
            salesGrid.SelectedRows[0].Cells["Id"].Value);

        string receipt = Convert.ToString(
            salesGrid.SelectedRows[0].Cells["ReceiptNumber"].Value) ?? "";

        decimal quantity = Convert.ToDecimal(
            salesGrid.SelectedRows[0].Cells["Quantity"].Value);

        if (id != 2 || receipt != "TEST002" || quantity != 100)
        {
            MessageBox.Show(
                "For safety, this test step allows deleting only ID 2, TEST002, 100 kg.");
            return;
        }

        DialogResult answer = MessageBox.Show(
            "Delete duplicate test sale ID 2 (TEST002, 100 kg)?",
            "Confirm Delete",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning);

        if (answer != DialogResult.Yes)
            return;

        string databasePath = System.IO.Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "cotton.db");

        using var connection =
            new SqliteConnection($"Data Source={databasePath}");

        connection.Open();

        using var command = new SqliteCommand(
            "DELETE FROM Sales WHERE Id = @Id AND ReceiptNumber = @Receipt AND Quantity = @Qty",
            connection);

        command.Parameters.AddWithValue("@Id", id);
        command.Parameters.AddWithValue("@Receipt", receipt);
        command.Parameters.AddWithValue("@Qty", quantity);

        int deleted = command.ExecuteNonQuery();

        if (deleted == 1)
        {
            MessageBox.Show("Duplicate test sale deleted.");
            LoadSales();
        }
        else
        {
            MessageBox.Show("No record was deleted. Please check the sale ID.");
        }
    }
}

