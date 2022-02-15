namespace AutoShopApp
{
    using AutoShopApp.Data;
    public partial class MainForm : Form
    {
        public MainForm() 
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            using var store = new Store();
            store.LoadStore(orderBindingSource, custPartOrdBindingSource);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Order order = (Order)orderBindingSource.Current;
            using var store = new Store();
            store.UpdateStore(order);
            custPartOrdBindingSource.Clear();
            orderBindingSource.Clear();
            store.LoadStore(orderBindingSource, custPartOrdBindingSource);
        }
    }
}