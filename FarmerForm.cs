
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using Microsoft.Data.Sqlite;

namespace CottonTradingSystem;
public class FarmerForm : Form
{
    private TextBox farmerNameTextBox = new TextBox();
    private TextBox mobileTextBox = new TextBox();
    private TextBox villageTextBox = new TextBox();
    private TextBox addressTextBox = new TextBox();
    private DateTimePicker registrationDatePicker = new DateTimePicker();
    private Button saveButton = new Button();

    public FarmerForm()
    {
        this.Text = "Farmer Registration";
        this.StartPosition = FormStartPosition.CenterScreen;
        this.Width = 550;
        this.Height = 450;

        Label heading = new Label();
        heading.Text = "Farmer Registration";
        heading.Location = new Point(130, 20);
        heading.AutoSize = true;
        heading.Font = new Font("Arial", 18, FontStyle.Bold);
        this.Controls.Add(heading);

        AddLabel("Farmer Name:", 40, 85);
        farmerNameTextBox.Location = new Point(200, 85);
        farmerNameTextBox.Width = 250;
        this.Controls.Add(farmerNameTextBox);

        AddLabel("Mobile Number:", 40, 130);
        mobileTextBox.Location = new Point(200, 130);
        mobileTextBox.Width = 250;
        this.Controls.Add(mobileTextBox);

        AddLabel("Village:", 40, 175);
        villageTextBox.Location = new Point(200, 175);
        villageTextBox.Width = 250;
        this.Controls.Add(villageTextBox);

        AddLabel("Address:", 40, 220);
        addressTextBox.Location = new Point(200, 220);
        addressTextBox.Width = 250;
        this.Controls.Add(addressTextBox);

        AddLabel("Registration Date:", 40, 265);
        registrationDatePicker.Location = new Point(200, 265);
        registrationDatePicker.Width = 250;
        this.Controls.Add(registrationDatePicker);

        saveButton.Text = "Save Farmer";
        saveButton.Location = new Point(180, 325);
        saveButton.Width = 150;
        saveButton.Height = 40;
        saveButton.Click += SaveButton_Click;
        this.Controls.Add(saveButton);
    }

    private void AddLabel(string text, int x, int y)
    {
        Label label = new Label();
        label.Text = text;
        label.Location = new Point(x, y);
        label.AutoSize = true;
        label.Font = new Font("Arial", 10);
        this.Controls.Add(label);
    }

    
private void SaveButton_Click(object? sender, EventArgs e)
{
    string farmerName = farmerNameTextBox.Text.Trim();
    string mobile = mobileTextBox.Text.Trim();
    string village = villageTextBox.Text.Trim();
    string address = addressTextBox.Text.Trim();

    if (string.IsNullOrWhiteSpace(farmerName))
    {
        MessageBox.Show("Please enter farmer name.");
        return;
    }

    if (!string.IsNullOrWhiteSpace(mobile) &&
        (mobile.Length != 10 || !mobile.All(char.IsDigit)))
    {
        MessageBox.Show("Enter a valid 10-digit mobile number.");
        return;
    }

    string databasePath = System.IO.Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "cotton.db");

    try
    {
        using var connection =
            new SqliteConnection($"Data Source={databasePath}");

        connection.Open();

        string query = @"
            INSERT INTO Farmers
            (FarmerName, MobileNumber, Village, Address, RegistrationDate)
            VALUES
            (@FarmerName, @MobileNumber, @Village, @Address, @RegistrationDate);";

        using var command = new SqliteCommand(query, connection);

        command.Parameters.AddWithValue("@FarmerName", farmerName);
        command.Parameters.AddWithValue("@MobileNumber", mobile);
        command.Parameters.AddWithValue("@Village", village);
        command.Parameters.AddWithValue("@Address", address);
        command.Parameters.AddWithValue(
            "@RegistrationDate",
            registrationDatePicker.Value.ToString("yyyy-MM-dd"));

        command.ExecuteNonQuery();

        MessageBox.Show(
            $"Farmer registered successfully!\n\nName: {farmerName}");

        farmerNameTextBox.Clear();
        mobileTextBox.Clear();
        villageTextBox.Clear();
        addressTextBox.Clear();
    }
    catch (Exception ex)
    {
        MessageBox.Show("Error saving farmer:\n" + ex.Message);
    }
}
}
