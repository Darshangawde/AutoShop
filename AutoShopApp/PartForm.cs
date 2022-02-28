namespace AutoShopApp
{
    using AutoShopApp.Data;
    public partial class PartForm : Form
    {
        public PartForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void PartForm_Load(object sender, EventArgs e)
        {
            Store store = new Store();
            store.LoadParts(PartBindingSource);
        }
    }
}
