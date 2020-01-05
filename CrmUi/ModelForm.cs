using CrmBl.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrmUi
{
    public partial class ModelForm : Form
    {
        ShopComputerModel model = new ShopComputerModel();

        public ModelForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            var cashBoxes = new List<CashBoxView>();

            for(int i = 0; i < model.CashDesks.Count; i++)
            {
                var box = new CashBoxView(model.CashDesks[i], i, 30, 26 * i);
                cashBoxes.Add(box);
                Controls.Add(box.CashDeskName);
                Controls.Add(box.Price);
                Controls.Add(box.QueueLenght);
                Controls.Add(box.LeaveCustomersCount);
                Controls.Add(box.ServedCustomer);
                Controls.Add(box.TotalCustomer);
            }
            label4.Text = cashBoxes.Count.ToString();
            model.Start();
            button1.Enabled = false;


        }

        private void ModelForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            model.Stop();
        }

        private void ModelForm_Load(object sender, EventArgs e)
        {
            numericUpDown1.Value = model.CustomerSpeed;
            numericUpDown2.Value = model.CashDeskSpeed;
            
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            model.CustomerSpeed = (int)numericUpDown1.Value;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            model.CashDeskSpeed = (int)numericUpDown2.Value;
        }

        private void ModelForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            model.Stop();
            //Close();
        }



        private void button2_Click(object sender, EventArgs e)
        {
            
            model.Stop();
            

            
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
