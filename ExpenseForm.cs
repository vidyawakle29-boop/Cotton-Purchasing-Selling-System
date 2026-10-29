
using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;
using System.Globalization;
namespace CottonTradingSystem;

public class ExpenseForm : Form
{
    private TextBox expenseNameTextBox = new TextBox();
    private TextBox amountTextBox = new TextBox();
    private DateTimePicker expenseDatePicker = new DateTimePicker();
    private ComboBox paymentStatusComboBox = new ComboBox();
    private Button saveButton = new Button();

    public ExpenseForm()
    {
        this.Text = "Cotton Business - Expenses";
        this.StartPosition = FormStartPosition.CenterScreen;
        this.Width = 500;
        this.Height = 400;

        Label heading = new Label();
        heading.Text = "Add Business Expense";
        heading.Location = new Point(100, 20);
        heading.AutoSize = true;
        heading.Font = new Font("Arial", 16, FontStyle.Bold);
        this.Controls.Add(heading);

        Label nameLabel = new Label();
        nameLabel.Text = "Expense Name:";
        nameLabel.Location = new Point(40, 80);
        nameLabel.AutoSize = true;
        this.Controls.Add(nameLabel);

        expenseNameTextBox.Location = new Point(180, 80);
        expenseNameTextBox.Width = 220;
        this.Controls.Add(expenseNameTextBox);

        Label dateLabel = new Label();
        dateLabel.Text = "Expense Date:";
        dateLabel.Location = new Point(40, 125);
        dateLabel.AutoSize = true;
        this.Controls.Add(dateLabel);

        expenseDatePicker.Location = new Point(180, 125);
        expenseDatePicker.Width = 220;
        this.Controls.Add(expenseDatePicker);

        Label amountLabel = new Label();
        amountLabel.Text = "Amount (₹):";
        amountLabel.Location = new Point(40, 170);
        amountLabel.AutoSize = true;
        this.Controls.Add(amountLabel);

        amountTextBox.Location = new Point(180, 170);
        amountTextBox.Width = 220;
        this.Controls.Add(amountTextBox);

        Label paymentLabel = new Label();
        paymentLabel.Text = "Payment Status:";
        paymentLabel.Location = new Point(40, 215);
        paymentLabel.AutoSize = true;
        this.Controls.Add(paymentLabel);

        paymentStatusComboBox.Location = new Point(180, 215);
        paymentStatusComboBox.Width = 220;
        paymentStatusComboBox.DropDownStyle =
            ComboBoxStyle.DropDownList;
        paymentStatusComboBox.Items.AddRange(
            new object[] { "Paid", "Pending", "Partial" });
        paymentStatusComboBox.SelectedIndex = 0;
        this.Controls.Add(paymentStatusComboBox);

        saveButton.Text = "Save Expense";
        saveButton.Location = new Point(150, 275);
        saveButton.Width = 150;
        saveButton.Height = 40;
        saveButton.Click += SaveButton_Click;
        this.Controls.Add(saveButton);
    }

    
private void SaveButton_Click(object? sender, EventArgs e)
{
    string expenseName = expenseNameTextBox.Text.Trim();

    if (string.IsNullOrWhiteSpace(expenseName))
    {
        MessageBox.Show("Please enter expense name.");
        return;
    }

    if (!decimal.TryParse(
        amountTextBox.Text.Trim(),
        NumberStyles.Number,
        CultureInfo.InvariantCulture,
        out decimal amount) || amount <= 0)
    {
        MessageBox.Show("Please enter a valid amount greater than zero.");
        return;
    }

    string databasePath = System.IO.Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "cotton.db");

    using var connection =
        new SqliteConnection($"Data Source={databasePath}");

    try
    {
        connection.Open();

        string query = @"
            INSERT INTO Expenses
            (ExpenseName, ExpenseDate, Amount, PaymentStatus)
            VALUES
            (@ExpenseName, @ExpenseDate, @Amount, @PaymentStatus);";

        using var command = new SqliteCommand(query, connection);

        command.Parameters.AddWithValue("@ExpenseName", expenseName);
        command.Parameters.AddWithValue(
            "@ExpenseDate",
            expenseDatePicker.Value.ToString("yyyy-MM-dd"));
        command.Parameters.AddWithValue("@Amount", amount);
        command.Parameters.AddWithValue(
            "@PaymentStatus",
            paymentStatusComboBox.SelectedItem?.ToString() ?? "Paid");

        command.ExecuteNonQuery();

        MessageBox.Show(
            $"Expense saved successfully!\n\n" +
            $"Name: {expenseName}\n" +
            $"Amount: ₹{amount:N2}");

        expenseNameTextBox.Clear();
        amountTextBox.Clear();
        paymentStatusComboBox.SelectedIndex = 0;
    }
    catch (Exception ex)
    {
        MessageBox.Show(
            "Error saving expense:\n" + ex.Message);
    }
}
}