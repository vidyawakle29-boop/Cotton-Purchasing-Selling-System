
namespace CottonTradingSystem;

public class PurchaseForm : Form
{
    public PurchaseForm()
    {
        this.Text = "Cotton Purchase";
        this.StartPosition = FormStartPosition.CenterScreen;
        this.Width = 650;
        this.Height = 600;

        Label heading = new Label();
        heading.Text = "Cotton Purchase Entry";
        heading.Font = new Font("Arial", 18, FontStyle.Bold);
        heading.AutoSize = true;
        heading.Location = new Point(170, 20);
        this.Controls.Add(heading);

        AddLabel("Farmer Name:", 40, 90);
        TextBox farmerName = new TextBox();
        farmerName.Location = new Point(220, 90);
        farmerName.Width = 300;
        this.Controls.Add(farmerName);

        AddLabel("Purchase Date:", 40, 140);
        DateTimePicker purchaseDate = new DateTimePicker();
        purchaseDate.Location = new Point(220, 140);
        purchaseDate.Width = 300;
        this.Controls.Add(purchaseDate);

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

        AddLabel("Quality:", 40, 290);
        ComboBox quality = new ComboBox();
        quality.Location = new Point(220, 290);
        quality.Width = 300;
        quality.DropDownStyle = ComboBoxStyle.DropDownList;
        quality.Items.AddRange(new object[]
        {
            "Premium",
            "Standard",
            "Low"
        });
        quality.SelectedIndex = 0;
        this.Controls.Add(quality);

        AddLabel("Receipt Number:", 40, 340);
        TextBox receiptNumber = new TextBox();
        receiptNumber.Location = new Point(220, 340);
        receiptNumber.Width = 300;
        this.Controls.Add(receiptNumber);

        AddLabel("Payment Status:", 40, 390);
        ComboBox paymentStatus = new ComboBox();
        paymentStatus.Location = new Point(220, 390);
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
        saveButton.Text = "Save Purchase";
        saveButton.Width = 160;
        saveButton.Height = 45;
        saveButton.Location = new Point(220, 460);
        saveButton.Font = new Font("Arial", 11, FontStyle.Bold);

       
        saveButton.Click += (sender, e) =>
        {
            if (string.IsNullOrWhiteSpace(farmerName.Text))
            {
                MessageBox.Show("Please enter Farmer Name.");
                farmerName.Focus();
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

try
{
    using var connection = new Microsoft.Data.Sqlite.SqliteConnection(
        "Data Source=" + System.IO.Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory, "cotton.db"));

    connection.Open();

    string query = @"
        INSERT INTO Purchases
        (FarmerName, PurchaseDate, Quantity, Rate, Quality,
         ReceiptNumber, PaymentStatus, TotalAmount)
        VALUES
        (@FarmerName, @PurchaseDate, @Quantity, @Rate, @Quality,
         @ReceiptNumber, @PaymentStatus, @TotalAmount);";

    using var command = new Microsoft.Data.Sqlite.SqliteCommand(query, connection);

    command.Parameters.AddWithValue("@FarmerName", farmerName.Text.Trim());
    command.Parameters.AddWithValue("@PurchaseDate", purchaseDate.Value.ToString("yyyy-MM-dd"));
    command.Parameters.AddWithValue("@Quantity", qty);
    command.Parameters.AddWithValue("@Rate", price);
    command.Parameters.AddWithValue("@Quality", quality.Text);
    command.Parameters.AddWithValue("@ReceiptNumber", receiptNumber.Text.Trim());
    command.Parameters.AddWithValue("@PaymentStatus", paymentStatus.Text);
    command.Parameters.AddWithValue("@TotalAmount", totalAmount);

    command.ExecuteNonQuery();

    MessageBox.Show(
        $"Purchase saved successfully!\n\n" +
        $"Farmer: {farmerName.Text}\n" +
        $"Quantity: {qty:N2} kg\n" +
        $"Rate: ₹{price:N2} per kg\n" +
        $"Total Amount: ₹{totalAmount:N2}",
        "Purchase Saved"
    );
}
catch (Exception ex)
{
    MessageBox.Show(
        "Purchase save failed.\n" + ex.Message,
        "Database Error"
    );
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
