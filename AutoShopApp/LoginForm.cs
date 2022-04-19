namespace AutoShopApp
{
    using AutoShopApp.Data;
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            using var store = new Store();
            store.LoadAdmin(adminBindingSource);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var admin = (Admin)adminBindingSource.Current;

            using var store = new Store();
            bool authenticatication = store.Authenticate(admin.AdminName, admin.AdminPassword);

            //bool authenticatication =  admin.Authenticate(admin);

            if (authenticatication)
            {
                switch (admin.Operation)
                {
                    case "Orders":
                        var mf = new MainForm();
                        Hide();
                        mf.ShowDialog();
                        adminBindingSource.Clear();
                        Close();
                        break;

                    case "Parts":
                        var partForm = new PartForm();
                        Hide();
                        partForm.ShowDialog();
                        adminBindingSource.Clear();
                        Close();
                        break;
                    default:
                        if (MessageBox.Show("Please,\n select Orders or Parts.", "AutoShoppApp", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) == DialogResult.OK)
                            return;
                        break;

                }
                
            }
            else
            {
                if (MessageBox.Show("Invalid UserId or Password", "AutoShoppApp", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) == DialogResult.OK)
                    return;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            var admin = (Admin)adminBindingSource.Current;
            admin.Operation = "Orders";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            var admin = (Admin)adminBindingSource.Current;
            admin.Operation = "Parts";
        }

       
    }
}
