using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrmBl.Model
{
    /// <summary>
    /// Класс кассы
    /// </summary>
    public class CashDesk
    {
        CrmContext db;
        /// <summary>
        /// Номер кассы
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// Продавец на кассе
        /// </summary>
        public Seller Seller { get; set; }
        /// <summary>
        /// Очередь корзин покупателей на кассе
        /// </summary>
        public Queue<Cart> Queue { get; set; }
        /// <summary>
        /// Максимальная длина очереди
        /// </summary>
        public int MaxQueueLenght { get; set; }
        /// <summary>
        /// Количество необслуженных покупателей
        /// </summary>
        public int ExitCustomer { get; set; }
        /// <summary>
        /// Это модель?
        /// </summary>
        public bool IsModel { get; set; }
        public int TotalCustomer { get; private set; }
        public int ServedCustomers { get; private set; }
        public int Count => Queue.Count;

        public event EventHandler<Check> CheckClosed;

        /// <summary>
        /// Конструктор кассы
        /// </summary>
        /// <param name="number">Номер кассы</param>
        /// <param name="seller">Объект продавца</param>
        /// <param name="db">контекст БД</param>
        public CashDesk(int number, Seller seller, CrmContext db)
        {
            Number = number;
            Seller = seller;
            Queue = new Queue<Cart>(); //инициализируем очередь покупательских корзин
            IsModel = true;
            MaxQueueLenght = 10;
            this.db = db ?? new CrmContext();
        }
        /// <summary>
        /// Добавление покупательской корзины в очередь
        /// </summary>
        /// <param name="cart">Экземпляр корзины</param>
        public void Enqueue(Cart cart)
        {
            TotalCustomer++;
            if(Queue.Count < MaxQueueLenght) //Если в очереди корзин меньше, чем лимит кассы на очередь, то добавляем
            {
                Queue.Enqueue(cart);
            }
            else
            {
                ExitCustomer++; // иначе наш покупатель кидает корзину в менеджера и уходит
            }
        }
        /// <summary>
        /// Удаление обслуженной корзины из очереди
        /// </summary>
        /// <returns>Возвращает стоимость покупок</returns>
        public decimal Dequeue()
        {
            decimal sum = 0; //создаём переменную для хранения итоговой стимости
            if(Queue.Count == 0) //если очередь пустая возвращаем ноль
            {
                return 0;
            }

            var card = Queue.Dequeue(); // создаём переменную в которую получаем корзину из начала очереди

            if(card != null)
            {
                var check = new Check() // создаём чек по покупке
                {
                    SellerId = Seller.SellerId, //вписываем ИД продавца
                    Seller = Seller, // прикрепляем экземпляр продавца
                    CustomerId = card.Customer.CustomerId, //вписываем ИД покупателя
                    Customer = card.Customer, // прикоепляем экземпляр покупателя
                    Created = DateTime.Now // текущее время
                };

                if(!IsModel) //если это не модель, то чек вписываем в базу
                {
                    db.Checks.Add(check);
                    db.SaveChanges();
                }
                else //иначе держим виртуально
                {
                    check.CheckId = 0;
                }

                var sells = new List<Sell>(); // инициализируем список продаж

                foreach(Product product in card) // перебираем продукты в корзине
                {
                    if (product.Count > 0)//если продукта на складе больше нуля
                    {
                        var sell = new Sell() //то создаём экземпляр продажи конкретной единицы товара
                        {
                            CheckId = check.CheckId,
                            Check = check,
                            ProductId = product.ProductId,
                            Product = product
                        };

                        sells.Add(sell); //добавляем экземпляр к списку

                        if (!IsModel) //если не модель. то добавляем продажу в БД
                        {
                            db.Sells.Add(sell);
                        }

                        product.Count--; //убираем единицу товара со склада, добавляем стоимость к чеку
                        sum += product.Price;
                    }
                }

                check.Price = sum; //записываем в чек сумму

                if(!IsModel) //если не модель, записываем чек в бд
                {
                    db.SaveChanges();
                }

                CheckClosed?.Invoke(this, check); //если продажа закрыта, вызываем событие
                ServedCustomers++;
            }

            return sum; //возвращаем сумму в вызов
        }

        public override string ToString()
        {
            return $"Касса №{Number}";
        }
    }
}
