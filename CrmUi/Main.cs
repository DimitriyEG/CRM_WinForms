using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrmBl.Model;

namespace CrmUi
{
    public partial class Main : Form
    {
        CrmContext db;
        Cart cart;
        Customer customer;
        CashDesk cashDesk;

        public Main()
        {
            InitializeComponent();
            db = new CrmContext();

            cart = new Cart(customer);
            cashDesk = new CashDesk(1, db.Sellers.FirstOrDefault(), db)
            {
                IsModel = false
            };
            dataGridView1.DataSource = db.Products.ToList();
            dataGridView1.Columns["ProductId"].Visible = false;
            dataGridView1.Columns["Sells"].Visible = false;
        }



        private void Main_Load(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
               listBox1.Invoke((Action)delegate
               {
                   listBox1.Items.AddRange(db.Products.ToArray());
                   UpdateLists();
               });
               
            });
        }
        private void DataGridView1_CellDoubleClick(Object sender, DataGridViewCellEventArgs e)
        {
            if(dataGridView1.SelectedRows[0].DataBoundItem is Product product)
            {
                cart.Add(product);
                listBox2.Items.Add(product);
                UpdateLists();
            }
        }
        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if(listBox1.SelectedItem is Product product)
            {
                var prodCount = product.Count;
                if (prodCount > 0)
                {
                    cart.Add(product);
                    listBox2.Items.Add(product);
                    UpdateLists();
                }
                else
                {
                    MessageBox.Show("Товар закончился!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
        }

        private void UpdateLists()
        {
            listBox2.Items.Clear();
            listBox2.Items.AddRange(cart.GetAll().ToArray());
            label1.Text = "Итого: " + cart.Price;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var form = new Login();
            form.ShowDialog();
            if(form.DialogResult == DialogResult.OK)
            {
                var tempCustomer = db.Customers.FirstOrDefault(c => c.Name.Equals(form.Customer.Name));
                if (tempCustomer != null)
                {
                    customer = tempCustomer;
                }
                else
                {
                    db.Customers.Add(form.Customer);
                    db.SaveChanges();
                    customer = form.Customer;
                }

                cart.Customer = customer;
            }

            linkLabel1.Text = $"Здравствуй, {customer.Name}";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(customer != null && cart.Price != 0)
            {
                cashDesk.Enqueue(cart);
                var price = cashDesk.Dequeue();
                listBox2.Items.Clear();
                cart = new Cart(customer);

                MessageBox.Show("Покупка выполнена успешно. Сумма: " + price, "Покупка выполнена", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if(cart.Price == 0)
            {
                MessageBox.Show("Нет выбранных товаров!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("Авторизуйтесь, пожалуйста!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            //var product =  (Product)dataGridView1.SelectedRows[0].Cells[0].Value;
            //if (product != null)
            //{
            //    cart.Add(product);
            //    listBox2.Items.Add(product);
            //    UpdateLists();
            //}
        }
        private void CustomerAddToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            var form = new CustomerForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                db.Customers.Add(form.Customer);
                db.SaveChanges();
            }
        }

        private void sellerAddToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var form = new SellerForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                db.Sellers.Add(form.Seller);
                db.SaveChanges();
            }
        }

        private void productAddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new ProductForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                db.Products.Add(form.Product);
                db.SaveChanges();
            }
        }

        private void modelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new ModelForm();
            form.Show();
        }
        private void CheckCatalogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var checkCustomer = new Catalog<Check>(db.Checks, db);
            checkCustomer.Show();
        }

        private void SellerCatalogToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var catalogSeller = new Catalog<Seller>(db.Sellers, db);
            catalogSeller.Show();
        }

        private void ProductCatalogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var catalogProduct = new Catalog<Product>(db.Products, db);
            catalogProduct.Show();
        }

        private void CustomerCatalogToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            var catalogCustomer = new Catalog<Customer>(db.Customers, db);
            catalogCustomer.Show();
        }
        private void ProductToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void SellerToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void CustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void CheckToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


    }
}
