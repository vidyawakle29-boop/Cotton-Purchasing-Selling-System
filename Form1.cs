
namespace CottonTradingSystem;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();

        this.Text = "Cotton Purchasing & Selling System";
        this.StartPosition = FormStartPosition.CenterScreen;
        this.Width = 1000;
        this.Height = 650;
                Label heading = new Label();

        heading.Text = "Cotton Purchasing & Selling System";
        heading.Font = new Font("Arial", 22, FontStyle.Bold);
        heading.AutoSize = true;
        heading.Location = new Point(180, 40);

        this.Controls.Add(heading);
        
        Button purchaseButton = new Button();

        purchaseButton.Text = "Cotton Purchase";
        purchaseButton.Width = 200;
        purchaseButton.Height = 55;
        purchaseButton.Location = new Point(100, 150);
        purchaseButton.Font = new Font("Arial", 12, FontStyle.Bold);

        purchaseButton.Click += (sender, e) =>
        {
            MessageBox.Show("Cotton Purchase section");
        };

        this.Controls.Add(purchaseButton);
        
        Button saleButton = new Button();

        saleButton.Text = "Cotton Sale";
        saleButton.Width = 200;
        saleButton.Height = 55;
        saleButton.Location = new Point(330, 150);
        saleButton.Font = new Font("Arial", 12, FontStyle.Bold);

        saleButton.Click += (sender, e) =>
{
    SaleForm saleForm = new SaleForm();
    saleForm.ShowDialog();
};

        this.Controls.Add(saleButton);
        
        Button farmerButton = new Button();

        farmerButton.Text = "Farmer Details";
        farmerButton.Width = 200;
        farmerButton.Height = 55;
        farmerButton.Location = new Point(560, 150);
        farmerButton.Font = new Font("Arial", 12, FontStyle.Bold);

        farmerButton.Click += (sender, e) =>
{
    FarmerForm farmerForm = new FarmerForm();
    farmerForm.ShowDialog();
};

        this.Controls.Add(farmerButton);
        
        Button expensesButton = new Button();

        expensesButton.Text = "Expenses";
        expensesButton.Width = 200;
        expensesButton.Height = 55;
        expensesButton.Location = new Point(100, 250);
        expensesButton.Font = new Font("Arial", 12, FontStyle.Bold);

       expensesButton.Click += (sender, e) =>
{
    ExpenseForm expenseForm = new ExpenseForm();
    expenseForm.ShowDialog();
};
        this.Controls.Add(expensesButton);
        
        Button reportsButton = new Button();

        reportsButton.Text = "Reports";
        reportsButton.Width = 200;
        reportsButton.Height = 55;
        reportsButton.Location = new Point(330, 250);
        reportsButton.Font = new Font("Arial", 12, FontStyle.Bold);

reportsButton.Font = new Font("Arial", 12, FontStyle.Bold);

reportsButton.Click += (sender, e) =>
{
    ReportsForm reportsForm = new ReportsForm();
    reportsForm.ShowDialog();
};

this.Controls.Add(reportsButton);


        purchaseButton.Click += (sender, e) =>
        {
            PurchaseForm purchaseForm = new PurchaseForm();
            purchaseForm.ShowDialog();
        };


        this.Controls.Add(reportsButton);
        Button viewPurchasesButton = new Button();
viewPurchasesButton.Text = "View Purchases";
viewPurchasesButton.Width = 200;
viewPurchasesButton.Height = 55;
viewPurchasesButton.Location = new Point(560, 250);
viewPurchasesButton.Font = new Font("Arial", 12, FontStyle.Bold);

viewPurchasesButton.Click += (sender, e) =>
{
    ViewPurchasesForm viewForm = new ViewPurchasesForm();
    viewForm.ShowDialog();
};
this.Controls.Add(viewPurchasesButton);

Button viewSalesButton = new Button();
viewSalesButton.Text = "View Sales";
viewSalesButton.Width = 200;
viewSalesButton.Height = 55;
viewSalesButton.Location = new Point(100, 350);
viewSalesButton.Font = new Font("Arial", 12, FontStyle.Bold);

viewSalesButton.Click += (sender, e) =>
{
    ViewSalesForm viewForm = new ViewSalesForm();
    viewForm.ShowDialog();
};

this.Controls.Add(viewSalesButton);

Button viewExpensesButton = new Button();
viewExpensesButton.Text = "View Expenses";
viewExpensesButton.Width = 200;
viewExpensesButton.Height = 55;
viewExpensesButton.Location = new Point(320, 350);
viewExpensesButton.Font = new Font("Arial", 12, FontStyle.Bold);

viewExpensesButton.Click += (sender, e) =>
{
    ViewExpensesForm viewForm = new ViewExpensesForm();
    viewForm.ShowDialog();
};

this.Controls.Add(viewExpensesButton);
Button viewFarmersButton = new Button();

viewFarmersButton.Text = "View Farmers";
viewFarmersButton.Width = 200;
viewFarmersButton.Height = 55;
viewFarmersButton.Location = new Point(560, 350);
viewFarmersButton.Font = new Font("Arial", 12, FontStyle.Bold);

viewFarmersButton.Click += (sender, e) =>
{
    ViewFarmersForm viewForm = new ViewFarmersForm();
    viewForm.ShowDialog();
};

this.Controls.Add(viewFarmersButton);

Button stockButton = new Button();

stockButton.Text = "Stock Management";
stockButton.Width = 200;
stockButton.Height = 55;
stockButton.Location = new Point(560, 430);
stockButton.Font = new Font("Arial", 12, FontStyle.Bold);

stockButton.Click += (sender, e) =>
{
    StockForm stockForm = new StockForm();
    stockForm.ShowDialog();
};

this.Controls.Add(stockButton);


    }
}


