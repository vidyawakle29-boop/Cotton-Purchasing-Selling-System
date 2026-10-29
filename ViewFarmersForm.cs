
using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace CottonTradingSystem;

public class ViewFarmersForm : Form
{
    private DataGridView farmersGrid = new DataGridView();

    public ViewFarmersForm()
    {
        this.Text = "View Farmers";
        this.StartPosition = FormStartPosition.CenterScreen;
        this.Width = 850;
        this.Height = 550;

        Label heading = new Label();
        heading.Text = "Registered Farmers";
        heading.Font = new Font("Arial", 18, FontStyle.Bold);
        heading.AutoSize = true;
        heading.Location = new Point(20, 20);
        this.Controls.Add(heading);

        farmersGrid.Location = new Point(20, 70);
        farmersGrid.Size = new Size(790, 250);
        farmersGrid.ReadOnly = true;
        farmersGrid.AllowUserToAddRows = false;
        farmersGrid.AutoSizeColumnsMode =
            DataGridViewAutoSizeColumnsMode.Fill;
        farmersGrid.SelectionMode =
            DataGridViewSelectionMode.FullRowSelect;

        this.Controls.Add(farmersGrid);
        Button editButton = new Button();
editButton.Text = "Edit Selected Farmer";
editButton.Location = new Point(20, 350);
editButton.Size = new Size(200, 40);
editButton.Font = new Font("Arial", 10, FontStyle.Bold);

editButton.Click += (sender, e) =>
{
    if (farmersGrid.SelectedRows.Count == 0)
    {
        MessageBox.Show("Please select a farmer first.");
        return;
    }

    int farmerId = Convert.ToInt32(
        farmersGrid.SelectedRows[0].Cells["Id"].Value);

    using (EditFarmerForm editForm = new EditFarmerForm(farmerId))
{
    if (editForm.ShowDialog() == DialogResult.OK)
    {
        LoadFarmers();
    }
}
};

this.Controls.Add(editButton);

Button deleteButton = new Button();
deleteButton.Text = "Delete Selected Farmer";
deleteButton.Location = new Point(240, 350);
deleteButton.Size = new Size(220, 40);
deleteButton.Font = new Font("Arial", 10, FontStyle.Bold);

deleteButton.Click += (sender, e) =>
{
    if (farmersGrid.SelectedRows.Count == 0)
    {
        MessageBox.Show("Please select a farmer first.");
        return;
    }

    int farmerId = Convert.ToInt32(
        farmersGrid.SelectedRows[0].Cells["Id"].Value);

    string farmerName = Convert.ToString(
        farmersGrid.SelectedRows[0].Cells["FarmerName"].Value) ?? "";

    DialogResult answer = MessageBox.Show(
        $"Are you sure you want to delete farmer: {farmerName}?",
        "Confirm Delete",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Warning);

    if (answer != DialogResult.Yes)
    {
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

        string query = "DELETE FROM Farmers WHERE Id = @Id;";

        using var command = new SqliteCommand(query, connection);
        command.Parameters.AddWithValue("@Id", farmerId);
        command.ExecuteNonQuery();

        LoadFarmers();
        MessageBox.Show("Farmer deleted successfully.");
    }
    catch (Exception ex)
    {
        MessageBox.Show("Error deleting farmer:\n" + ex.Message);
    }
};

this.Controls.Add(deleteButton);


        LoadFarmers();
    }

    private void LoadFarmers()
    {
        string databasePath = System.IO.Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "cotton.db");

        using var connection =
            new SqliteConnection($"Data Source={databasePath}");

        connection.Open();

        string query = @"
            SELECT Id, FarmerName, MobileNumber, Village,
                   Address, RegistrationDate
            FROM Farmers
            ORDER BY Id DESC;";

        using var command = new SqliteCommand(query, connection);
        using var reader = command.ExecuteReader();

        var table = new System.Data.DataTable();
        table.Load(reader);

        farmersGrid.DataSource = table;
    }
}
