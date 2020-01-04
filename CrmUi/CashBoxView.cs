using CrmBl.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrmUi
{
    /// <summary>
    /// Представление кассы
    /// </summary>
    class CashBoxView
    {
        /// <summary>
        /// Экземпляр кассы
        /// </summary>
        CashDesk cashDesk;
        /// <summary>
        /// Обозначение кассы
        /// </summary>
        public Label CashDeskName { get; set; }
        /// <summary>
        /// Сумма покупок на кассе
        /// </summary>
        public NumericUpDown Price { get; set; }
        /// <summary>
        /// Заполненность очереди
        /// </summary>
        public ProgressBar QueueLenght { get; set; }
        /// <summary>
        /// Счётчик ушедших посетителей
        /// </summary>
        public Label LeaveCustomersCount { get; set; }
        public Label ServedCustomer { get; set; }
        public Label TotalCustomer { get; set; }

        public CashBoxView(CashDesk cashDesk, int number, int x, int y)
        {
            this.cashDesk = cashDesk;

            CashDeskName = new Label();
            Price = new NumericUpDown();
            QueueLenght = new ProgressBar();
            LeaveCustomersCount = new Label();
            ServedCustomer = new Label();
            TotalCustomer = new Label();

            CashDeskName.AutoSize = true;
            CashDeskName.Location = new System.Drawing.Point(x, y + 40);
            CashDeskName.Name = "label" + number;
            CashDeskName.Size = new System.Drawing.Size(35, 13);
            CashDeskName.TabIndex = number;
            CashDeskName.Text = cashDesk.ToString();


            Price.Location = new System.Drawing.Point(x + 70, y + 40);
            Price.Name = "numericUpDown" + number;
            Price.Size = new System.Drawing.Size(120, 20);
            Price.TabIndex = number;
            Price.Maximum = 1000000000000000;

            QueueLenght.Location = new System.Drawing.Point(x + 240, y + 40);
            QueueLenght.Maximum = cashDesk.MaxQueueLenght;
            QueueLenght.Name = "progressBar" + number;
            QueueLenght.Size = new System.Drawing.Size(100, 23);
            QueueLenght.TabIndex = number;
            QueueLenght.Value = 0;

            LeaveCustomersCount.AutoSize = true;
            LeaveCustomersCount.Location = new System.Drawing.Point(x + 430, y + 40);
            LeaveCustomersCount.Name = "label2" + number;
            LeaveCustomersCount.Size = new System.Drawing.Size(35, 13);
            LeaveCustomersCount.TabIndex = number;
            LeaveCustomersCount.Text = "";

            ServedCustomer.AutoSize = true;
            ServedCustomer.Location = new System.Drawing.Point(x + 570, y + 40);
            ServedCustomer.Name = "ServedCustomer" + number;
            ServedCustomer.Size = new System.Drawing.Size(35, 13);
            ServedCustomer.TabIndex = number;
            ServedCustomer.Text = "";

            TotalCustomer.AutoSize = true;
            TotalCustomer.Location = new System.Drawing.Point(x + 700, y + 40);
            TotalCustomer.Name = "TotalCustomer" + number;
            TotalCustomer.Size = new System.Drawing.Size(35, 13);
            TotalCustomer.TabIndex = number;
            TotalCustomer.Text = "";

            cashDesk.CheckClosed += CashDesk_CheckClosed;
        }

        private void CashDesk_CheckClosed(object sender, Check e)
        {
            Price.Invoke((Action)delegate
            {
                Price.Value += e.Price;
                QueueLenght.Value = cashDesk.Count;
                LeaveCustomersCount.Text = cashDesk.ExitCustomer.ToString();
                TotalCustomer.Text = cashDesk.TotalCustomer.ToString();
                ServedCustomer.Text = cashDesk.ServedCustomers.ToString();
            });
        }
    }
}
