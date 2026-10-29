
namespace CottonTradingSystem;

public class SaleForm : Form
{
    public SaleForm()
    {
        this.Text = "Cotton Sale";
        this.StartPosition = FormStartPosition.CenterScreen;
        this.Width = 650;
        this.Height = 600;

        Label heading = new Label();
        heading.Text = "Cotton Sale Entry";
        heading.Font = new Font("Arial", 18, FontStyle.Bold);
        heading.AutoSize = true;
        heading.Location = new Point(190, 20);
        this.Controls.Add(heading);

        AddLabel("Buyer Name:", 40, 90);
        TextBox buyerName = new TextBox();
        buyerName.Location = new Point(220, 90);
        buyerName.Width = 300;
        this.Controls.Add(buyerName);

        AddLabel("Sale Date:", 40, 140);
        DateTimePicker saleDate = new DateTimePicker();
        saleDate.Location = new Point(220, 140);
        saleDate.Width = 300;
        this.Controls.Add(saleDate);

        AddLabel("Quantity (kg):", 40, 190);
        TextBox quantity = new TextBox();
        quantity.Location = new Point(220, 190);
        quantity.Width = 300;
        this.Controls.Add(quantity);

        AddLabel("Rate per kg (₹):", 40, 240);
        TextBox rate = new TextBox();
        rate.Location = new Point(220, 240);
        rate.Width = 300;
        this.Controls.Add(rate);

        AddLabel("Receipt Number:", 40, 290);
        TextBox receiptNumber = new TextBox();
        receiptNumber.Location = new Point(220, 290);
        receiptNumber.Width = 300;
        this.Controls.Add(receiptNumber);

        AddLabel("Payment Status:", 40, 340);
        ComboBox paymentStatus = new ComboBox();
        paymentStatus.Location = new Point(220, 340);
        paymentStatus.Width = 300;
        paymentStatus.DropDownStyle = ComboBoxStyle.DropDownList;
        paymentStatus.Items.AddRange(new object[]
        {
            "Paid",
            "Partial",
            "Pending"
        });
        paymentStatus.SelectedIndex = 0;
        this.Controls.Add(paymentStatus);

        Button saveButton = new Button();
        saveButton.Text = "Save Sale";
        saveButton.Width = 160;
        saveButton.Height = 45;
        saveButton.Location = new Point(220, 420);
        saveButton.Font = new Font("Arial", 11, FontStyle.Bold);

        
saveButton.Click += (sender, e) =>
{
    if (string.IsNullOrWhiteSpace(buyerName.Text))
    {
        MessageBox.Show("Please enter Buyer Name.");
        buyerName.Focus();
        return;
    }

    if (!decimal.TryParse(quantity.Text, out decimal qty) || qty <= 0)
    {
        MessageBox.Show("Please enter a valid Quantity.");
        quantity.Focus();
        return;
    }

    if (!decimal.TryParse(rate.Text, out decimal price) || price <= 0)
    {
        MessageBox.Show("Please enter a valid Rate.");
        rate.Focus();
        return;
    }

    decimal totalAmount = qty * price;
    string databasePath = System.IO.Path.Combine(
    AppDomain.CurrentDomain.BaseDirectory,
    "cotton.db");

using (var stockConnection =
    new Microsoft.Data.Sqlite.SqliteConnection(
        "Data Source=" + databasePath))
{
    stockConnection.Open();

    string stockQuery = @"
        SELECT
            (SELECT COALESCE(SUM(Quantity), 0) FROM Purchases)
            -
            (SELECT COALESCE(SUM(Quantity), 0) FROM Sales);";

    using var stockCommand =
        new Microsoft.Data.Sqlite.SqliteCommand(
            stockQuery, stockConnection);

    decimal availableStock =
        Convert.ToDecimal(stockCommand.ExecuteScalar());

    if (qty > availableStock)
    {
        MessageBox.Show(
            $"Insufficient cotton stock!\n\n" +
            $"Available Stock: {availableStock:N2} kg\n" +
            $"Requested Sale: {qty:N2} kg",
            "Stock Warning");

        return;
    }
}

    try
    {
        string stockDatabasePath = System.IO.Path.Combine(
    AppDomain.CurrentDomain.BaseDirectory,
    "cotton.db");

        using var connection =
            new Microsoft.Data.Sqlite.SqliteConnection(
               "Data Source=" + stockDatabasePath);

        connection.Open();

        string query = @"
            INSERT INTO Sales
            (BuyerName, SaleDate, Quantity, Rate, ReceiptNumber,
             PaymentStatus, TotalAmount)
            VALUES
            (@BuyerName, @SaleDate, @Quantity, @Rate, @ReceiptNumber,
             @PaymentStatus, @TotalAmount);";

        using var command =
            new Microsoft.Data.Sqlite.SqliteCommand(query, connection);

        command.Parameters.AddWithValue("@BuyerName", buyerName.Text.Trim());
        command.Parameters.AddWithValue(
            "@SaleDate", saleDate.Value.ToString("yyyy-MM-dd"));
        command.Parameters.AddWithValue("@Quantity", qty);
        command.Parameters.AddWithValue("@Rate", price);
        command.Parameters.AddWithValue(
            "@ReceiptNumber", receiptNumber.Text.Trim());
        command.Parameters.AddWithValue("@PaymentStatus", paymentStatus.Text);
        command.Parameters.AddWithValue("@TotalAmount", totalAmount);

        command.ExecuteNonQuery();

        MessageBox.Show(
            $"Sale saved successfully!\n\n" +
            $"Buyer: {buyerName.Text}\n" +
            $"Quantity: {qty:N2} kg\n" +
            $"Rate: ₹{price:N2} per kg\n" +
            $"Total Amount: ₹{totalAmount:N2}",
            "Sale Saved");
    }
    catch (Exception ex)
    {
        MessageBox.Show(
            "Sale save failed.\n" + ex.Message,
            "Database Error");
    }
};


        this.Controls.Add(saveButton);
    }

    private void AddLabel(string text, int x, int y)
    {
        Label label = new Label();
        label.Text = text;
        label.Location = new Point(x, y);
        label.Width = 170;
        label.Height = 25;
        label.Font = new Font("Arial", 10);
        this.Controls.Add(label);
    }
}
