
using System;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;
namespace CottonTradingSystem;

public class ViewExpensesForm : Form
{
    private readonly DataGridView expensesGrid = new DataGridView();

    public ViewExpensesForm()
    {
        this.Text = "Saved Expense Records";
        this.StartPosition = FormStartPosition.CenterScreen;
        this.Width = 900;
        this.Height = 500;

        expensesGrid.Dock = DockStyle.Fill;
        expensesGrid.ReadOnly = true;
        expensesGrid.AllowUserToAddRows = false;
        expensesGrid.AutoSizeColumnsMode =
            DataGridViewAutoSizeColumnsMode.Fill;
        expensesGrid.SelectionMode =
            DataGridViewSelectionMode.FullRowSelect;

        this.Controls.Add(expensesGrid);

        LoadExpenses();
    }

    private void LoadExpenses()
    {
        string databasePath = System.IO.Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "cotton.db");

        using var connection =
            new SqliteConnection($"Data Source={databasePath}");

        connection.Open();

        string query = @"
            SELECT Id, ExpenseName, ExpenseDate, Amount, PaymentStatus
            FROM Expenses
            ORDER BY Id DESC;";

        using var command = new SqliteCommand(query, connection);
        using var reader = command.ExecuteReader();

        var table = new System.Data.DataTable();
        table.Load(reader);

        expensesGrid.DataSource = table;
    }
}
