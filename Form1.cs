
namespace CottonTradingSystem;

public partial class Form1 : Form
{
    private Label totalPurchaseValue = null!;
    private Label totalSalesValue = null!;
    private Label availableStockValue = null!;
    private Label totalExpensesValue = null!;

    public Form1()
    {
        InitializeComponent();

        this.Text = "Cotton Trading Management System";
        this.StartPosition = FormStartPosition.CenterScreen;
        this.Size = new Size(1150, 780);
        this.MinimumSize = new Size(1000, 700);
        this.BackColor = Color.FromArgb(243, 246, 250);
        this.Font = new Font("Segoe UI", 10);

        // LEFT SIDEBAR
        Panel sidebar = new Panel();
        sidebar.Dock = DockStyle.Left;
        sidebar.Width = 225;
        sidebar.BackColor = Color.FromArgb(22, 39, 62);

        Label logo = new Label();
        logo.Text = "COTTON";
        logo.Font = new Font("Segoe UI", 23, FontStyle.Bold);
        logo.ForeColor = Color.White;
        logo.Location = new Point(25, 35);
        logo.AutoSize = true;
        sidebar.Controls.Add(logo);

        Label subLogo = new Label();
        subLogo.Text = "TRADING MANAGEMENT";
        subLogo.Font = new Font("Segoe UI", 9, FontStyle.Bold);
        subLogo.ForeColor = Color.FromArgb(65, 195, 181);
        subLogo.Location = new Point(27, 78);
        subLogo.AutoSize = true;
        sidebar.Controls.Add(subLogo);

        Panel line = new Panel();
        line.Location = new Point(25, 115);
        line.Size = new Size(175, 2);
        line.BackColor = Color.FromArgb(65, 195, 181);
        sidebar.Controls.Add(line);

        Label menuTitle = new Label();
        menuTitle.Text = "MAIN MENU";
        menuTitle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
        menuTitle.ForeColor = Color.FromArgb(160, 180, 200);
        menuTitle.Location = new Point(28, 145);
        menuTitle.AutoSize = true;
        sidebar.Controls.Add(menuTitle);

        Label menuInfo = new Label();
        menuInfo.Text =
            "Purchase\n\n" +
            "Sales\n\n" +
            "Farmer Management\n\n" +
            "Expenses\n\n" +
            "Stock Management\n\n" +
            "Reports";
        menuInfo.Font = new Font("Segoe UI", 11);
        menuInfo.ForeColor = Color.White;
        menuInfo.Location = new Point(30, 185);
        menuInfo.AutoSize = true;
        sidebar.Controls.Add(menuInfo);

        Label footer = new Label();
        footer.Text = "OFFLINE DESKTOP SOFTWARE\nPowered by C# and SQLite";
        footer.Font = new Font("Segoe UI", 8);
        footer.ForeColor = Color.FromArgb(170, 190, 210);
        footer.Location = new Point(20, 660);
        footer.AutoSize = true;
        sidebar.Controls.Add(footer);

        // MAIN CONTENT
        Panel mainPanel = new Panel();
        mainPanel.Dock = DockStyle.Fill;
        mainPanel.BackColor = Color.FromArgb(243, 246, 250);

        // HEADER
        Panel header = new Panel();
        header.Dock = DockStyle.Top;
        header.Height = 115;
        header.BackColor = Color.White;

        Label heading = new Label();
        heading.Text = "Cotton Trading Dashboard";
        heading.Font = new Font("Segoe UI", 23, FontStyle.Bold);
        heading.ForeColor = Color.FromArgb(22, 39, 62);
        heading.Location = new Point(30, 20);
        heading.AutoSize = true;
        header.Controls.Add(heading);

        Label subtitle = new Label();
        subtitle.Text =
            "Manage purchases, sales, farmers, expenses and stock in one place.";
        subtitle.Font = new Font("Segoe UI", 10);
        subtitle.ForeColor = Color.FromArgb(110, 125, 145);
        subtitle.Location = new Point(33, 65);
        subtitle.AutoSize = true;
        header.Controls.Add(subtitle);

        // LIVE SUMMARY PANEL
        FlowLayoutPanel summaryPanel = new FlowLayoutPanel();
        summaryPanel.Dock = DockStyle.Top;
        summaryPanel.Height = 125;
        summaryPanel.Padding = new Padding(15, 12, 5, 5);
        summaryPanel.BackColor = Color.FromArgb(243, 246, 250);
        summaryPanel.WrapContents = false;
        summaryPanel.AutoScroll = true;

        totalPurchaseValue = AddSummaryCard(
            summaryPanel, "TOTAL PURCHASE", "Loading...",
            Color.FromArgb(31, 91, 135));

        totalSalesValue = AddSummaryCard(
            summaryPanel, "TOTAL SALES", "Loading...",
            Color.FromArgb(8, 126, 131));

        availableStockValue = AddSummaryCard(
            summaryPanel, "AVAILABLE STOCK", "Loading...",
            Color.FromArgb(39, 120, 100));

        totalExpensesValue = AddSummaryCard(
            summaryPanel, "TOTAL EXPENSES", "Loading...",
            Color.FromArgb(174, 119, 42));

        // MODULE CARDS
        FlowLayoutPanel cards = new FlowLayoutPanel();
        cards.Dock = DockStyle.Fill;
        cards.AutoScroll = true;
        cards.Padding = new Padding(25, 25, 15, 20);
        cards.BackColor = Color.FromArgb(243, 246, 250);
        cards.WrapContents = true;

        AddModuleCard(cards, "Cotton Purchase",
            "Record new cotton purchases",
            "OPEN PURCHASE", Color.FromArgb(31, 91, 135),
            () =>
            {
                PurchaseForm form = new PurchaseForm();
                form.ShowDialog();
            });

        AddModuleCard(cards, "Cotton Sale",
            "Manage cotton sales records",
            "OPEN SALES", Color.FromArgb(8, 126, 131),
            () =>
            {
                SaleForm form = new SaleForm();
                form.ShowDialog();
            });

        AddModuleCard(cards, "Farmer Details",
            "Manage farmer information",
            "OPEN FARMERS", Color.FromArgb(133, 91, 160),
            () =>
            {
                FarmerForm form = new FarmerForm();
                form.ShowDialog();
            });

        AddModuleCard(cards, "Expenses",
            "Record additional expenses",
            "OPEN EXPENSES", Color.FromArgb(174, 119, 42),
            () =>
            {
                ExpenseForm form = new ExpenseForm();
                form.ShowDialog();
            });

        AddModuleCard(cards, "Reports",
            "View business summary and reports",
            "OPEN REPORTS", Color.FromArgb(39, 105, 145),
            () =>
            {
                ReportsForm form = new ReportsForm();
                form.ShowDialog();
            });

        AddModuleCard(cards, "View Purchases",
            "Check saved purchase records",
            "VIEW PURCHASES", Color.FromArgb(31, 91, 135),
            () =>
            {
                ViewPurchasesForm form = new ViewPurchasesForm();
                form.ShowDialog();
            });

        AddModuleCard(cards, "View Sales",
            "Check saved sales records",
            "VIEW SALES", Color.FromArgb(8, 126, 131),
            () =>
            {
                ViewSalesForm form = new ViewSalesForm();
                form.ShowDialog();
            });

        AddModuleCard(cards, "View Expenses",
            "Check saved expense records",
            "VIEW EXPENSES", Color.FromArgb(174, 119, 42),
            () =>
            {
                ViewExpensesForm form = new ViewExpensesForm();
                form.ShowDialog();
            });

        AddModuleCard(cards, "View Farmers",
            "Check registered farmer records",
            "VIEW FARMERS", Color.FromArgb(133, 91, 160),
            () =>
            {
                ViewFarmersForm form = new ViewFarmersForm();
                form.ShowDialog();
            });

        AddModuleCard(cards, "Stock Management",
            "Check available cotton stock",
            "OPEN STOCK", Color.FromArgb(39, 120, 100),
            () =>
            {
                StockForm form = new StockForm();
                form.ShowDialog();
            });

        // ADD PANELS
        mainPanel.Controls.Add(cards);
        mainPanel.Controls.Add(summaryPanel);
        mainPanel.Controls.Add(header);

        this.Controls.Add(mainPanel);
        this.Controls.Add(sidebar);

        // Refresh summary whenever dashboard becomes active
        this.Activated += (sender, e) => UpdateDashboardSummary();

        UpdateDashboardSummary();
    }

    // SUMMARY CARD
    private Label AddSummaryCard(
        FlowLayoutPanel container,
        string title,
        string initialValue,
        Color accentColor)
    {
        Panel card = new Panel();
        card.Size = new Size(205, 88);
        card.Margin = new Padding(8, 5, 8, 5);
        card.BackColor = Color.White;
        card.BorderStyle = BorderStyle.FixedSingle;

        Panel accent = new Panel();
        accent.Dock = DockStyle.Left;
        accent.Width = 5;
        accent.BackColor = accentColor;
        card.Controls.Add(accent);

        Label titleLabel = new Label();
        titleLabel.Text = title;
        titleLabel.Font = new Font("Segoe UI", 8, FontStyle.Bold);
        titleLabel.ForeColor = Color.FromArgb(110, 125, 145);
        titleLabel.Location = new Point(16, 13);
        titleLabel.AutoSize = true;
        card.Controls.Add(titleLabel);

        Label valueLabel = new Label();
        valueLabel.Text = initialValue;
        valueLabel.Font = new Font("Segoe UI", 15, FontStyle.Bold);
        valueLabel.ForeColor = Color.FromArgb(22, 39, 62);
        valueLabel.Location = new Point(16, 39);
        valueLabel.AutoSize = true;
        card.Controls.Add(valueLabel);

        container.Controls.Add(card);

        return valueLabel;
    }

    // UPDATE LIVE SUMMARY FROM DATABASE
    private void UpdateDashboardSummary()
    {
        try
        {
            totalPurchaseValue.Text =
                "₹ " + DatabaseHelper.GetTotalPurchases().ToString("N2");

            totalSalesValue.Text =
                "₹ " + DatabaseHelper.GetTotalSales().ToString("N2");

            availableStockValue.Text =
                DatabaseHelper.GetAvailableStock().ToString("N2") + " kg";

            totalExpensesValue.Text =
                "₹ " + DatabaseHelper.GetTotalExpenses().ToString("N2");
        }
        catch (Exception ex)
        {
            totalPurchaseValue.Text = "Unavailable";
            totalSalesValue.Text = "Unavailable";
            availableStockValue.Text = "Unavailable";
            totalExpensesValue.Text = "Unavailable";

            MessageBox.Show(
                "Unable to load dashboard summary.\n" + ex.Message,
                "Dashboard Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
    }

    // REUSABLE MODULE CARD
    private void AddModuleCard(
        FlowLayoutPanel container,
        string title,
        string description,
        string buttonText,
        Color accentColor,
        Action openForm)
    {
        Panel card = new Panel();
        card.Size = new Size(265, 145);
        card.Margin = new Padding(10);
        card.BackColor = Color.White;
        card.BorderStyle = BorderStyle.FixedSingle;

        Panel accent = new Panel();
        accent.Dock = DockStyle.Top;
        accent.Height = 6;
        accent.BackColor = accentColor;
        card.Controls.Add(accent);

        Label titleLabel = new Label();
        titleLabel.Text = title;
        titleLabel.Font = new Font("Segoe UI", 13, FontStyle.Bold);
        titleLabel.ForeColor = Color.FromArgb(22, 39, 62);
        titleLabel.Location = new Point(15, 20);
        titleLabel.AutoSize = true;
        card.Controls.Add(titleLabel);

        Label descriptionLabel = new Label();
        descriptionLabel.Text = description;
        descriptionLabel.Font = new Font("Segoe UI", 9);
        descriptionLabel.ForeColor = Color.FromArgb(110, 125, 145);
        descriptionLabel.Location = new Point(16, 50);
        descriptionLabel.AutoSize = true;
        card.Controls.Add(descriptionLabel);

        Button openButton = new Button();
        openButton.Text = buttonText;
        openButton.Location = new Point(15, 85);
        openButton.Size = new Size(225, 38);
        openButton.BackColor = accentColor;
        openButton.ForeColor = Color.White;
        openButton.Font = new Font("Segoe UI", 9, FontStyle.Bold);
        openButton.FlatStyle = FlatStyle.Flat;
        openButton.FlatAppearance.BorderSize = 0;
        openButton.Cursor = Cursors.Hand;

        openButton.Click += (sender, e) => openForm();

        card.Controls.Add(openButton);
        container.Controls.Add(card);
    }
}
