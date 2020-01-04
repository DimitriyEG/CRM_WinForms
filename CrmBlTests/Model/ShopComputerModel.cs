using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CrmBl.Model
{
    public class ShopComputerModel
    {
        public int CashDescCount { get; set; }
        Generator Generator = new Generator();
        Random rnd = new Random();
        List<Task> tasks = new List<Task>();
        CancellationTokenSource cancelTokenSource;
        CancellationToken token;

        /// <summary>
        /// Коллекция касс
        /// </summary>
        public List<CashDesk> CashDesks { get; set; } = new List<CashDesk>();
        /// <summary>
        /// Коллекция корзин покупателей
        /// </summary>
        public List<Cart> Carts { get; set; } = new List<Cart>();
        /// <summary>
        /// Коллекция чеков
        /// </summary>
        public List<Check> Checks { get; set; } = new List<Check>();
        /// <summary>
        /// Коллекция продаж
        /// </summary>
        public List<Sell> Sell { get; set; } = new List<Sell>();
        /// <summary>
        /// Очередь продавцов
        /// </summary>
        public Queue<Seller> Sellers { get; set; } = new Queue<Seller>();

        /// <summary>
        /// Время обработки покупателя
        /// </summary>
        public int CustomerSpeed { get; set; } = 100;
        /// <summary>
        /// Скорость работы кассы
        /// </summary>
        public int CashDeskSpeed { get; set; } = 100;
        /// <summary>
        /// Количество касс
        /// </summary>
        

        public ShopComputerModel()
        {
            var sellers = Generator.GetNewSellers(20);
            Generator.GetNewProducts(1000);
            Generator.GetNewCustomers(100);

            cancelTokenSource = new CancellationTokenSource();
            token = cancelTokenSource.Token;

            foreach (var seller in sellers)
            {
                Sellers.Enqueue(seller);
            }

            for(int i = 0; i < CashDescCount; i++)
            {
                CashDesks.Add(new CashDesk(i, Sellers.Dequeue(), null));
            }
        }

        public void Start()
        {
            tasks.Add(new Task(() => CreateCarts(10, token)));
            tasks.AddRange(CashDesks.Select(c => new Task(() => CashDeskWork(c, token))));
            foreach(var task in tasks)
            {
                task.Start();
            }
        }

        public void Stop()
        {
            cancelTokenSource.Cancel();
            Thread.Sleep(1000);
        }

        private void CashDeskWork(CashDesk cashDesk, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                if (cashDesk.Count > 0)
                {
                    cashDesk.Dequeue();
                    Thread.Sleep(CashDeskSpeed);
                }
            }
        }

        private void CreateCarts(int customerCounts, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                var customers = Generator.GetNewCustomers(customerCounts);

                foreach (var customer in customers)
                {
                    var cart = new Cart(customer);

                    foreach(var product in Generator.GetRandomProducts(10, 30))
                    {
                        cart.Add(product);
                    }

                    var cash = CashDesks[rnd.Next(CashDesks.Count)];
                    cash.Enqueue(cart);
                }

                Thread.Sleep(CustomerSpeed);
            }
        }
    }
}
