
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace CottonTradingSystem;

public class EditFarmerForm : Form
{
    private readonly int farmerId;

    private TextBox nameBox = new TextBox();
    private TextBox mobileBox = new TextBox();
    private TextBox villageBox = new TextBox();
    private TextBox addressBox = new TextBox();

    public EditFarmerForm(int id)
    {
        farmerId = id;

        this.Text = "Edit Farmer";
        this.StartPosition = FormStartPosition.CenterScreen;
        this.Width = 500;
        this.Height = 380;

        AddField("Farmer Name:", nameBox, 30);
        AddField("Mobile Number:", mobileBox, 80);
        AddField("Village:", villageBox, 130);
        AddField("Address:", addressBox, 180);

        Button saveButton = new Button();
        saveButton.Text = "Update Farmer";
        saveButton.Location = new Point(160, 250);
        saveButton.Size = new Size(150, 40);
        saveButton.Click += SaveButton_Click;
        this.Controls.Add(saveButton);

        LoadFarmer();
    }

    private void AddField(string labelText, TextBox textBox, int y)
    {
        Label label = new Label();
        label.Text = labelText;
        label.Location = new Point(30, y + 3);
        label.AutoSize = true;
        this.Controls.Add(label);

        textBox.Location = new Point(170, y);
        textBox.Width = 250;
        this.Controls.Add(textBox);
    }

    private string GetDatabasePath()
    {
        return System.IO.Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "cotton.db");
    }

    private void LoadFarmer()
    {
        using var connection =
            new SqliteConnection($"Data Source={GetDatabasePath()}");
        connection.Open();

        string query = @"
            SELECT FarmerName, MobileNumber, Village, Address
            FROM Farmers
            WHERE Id = @Id;";

        using var command = new SqliteCommand(query, connection);
        command.Parameters.AddWithValue("@Id", farmerId);

        using var reader = command.ExecuteReader();

        if (reader.Read())
        {
            nameBox.Text = reader["FarmerName"]?.ToString() ?? "";
            mobileBox.Text = reader["MobileNumber"]?.ToString() ?? "";
            villageBox.Text = reader["Village"]?.ToString() ?? "";
            addressBox.Text = reader["Address"]?.ToString() ?? "";
        }
        else
        {
            MessageBox.Show("Farmer record not found.");
            this.Close();
        }
    }

    private void SaveButton_Click(object? sender, EventArgs e)
    {
        string name = nameBox.Text.Trim();
        string mobile = mobileBox.Text.Trim();
        string village = villageBox.Text.Trim();
        string address = addressBox.Text.Trim();

        if (string.IsNullOrWhiteSpace(name))
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

        try
        {
            using var connection =
                new SqliteConnection($"Data Source={GetDatabasePath()}");
            connection.Open();

            string query = @"
                UPDATE Farmers
                SET FarmerName = @Name,
                    MobileNumber = @Mobile,
                    Village = @Village,
                    Address = @Address
                WHERE Id = @Id;";

            using var command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("@Name", name);
            command.Parameters.AddWithValue("@Mobile", mobile);
            command.Parameters.AddWithValue("@Village", village);
            command.Parameters.AddWithValue("@Address", address);
            command.Parameters.AddWithValue("@Id", farmerId);

            command.ExecuteNonQuery();

            MessageBox.Show("Farmer details updated successfully!");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error updating farmer:\n" + ex.Message);
        }
    }
}
