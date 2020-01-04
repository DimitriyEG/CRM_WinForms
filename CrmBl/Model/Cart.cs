using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CrmBl.Model
{
    /// <summary>
    /// Класс покупательской корзины
    /// </summary>
    public class Cart : IEnumerable
    {
        /// <summary>
        /// Покупатель
        /// </summary>
        public Customer Customer { get; set; }
        /// <summary>
        /// Список товар Название - количество
        /// </summary>
        public Dictionary<Product, int> Products { get; set; }
        public decimal Price => GetAll().Sum(p => p.Price);

        public Cart(Customer customer)
        {
            Customer = customer;
            Products = new Dictionary<Product, int>();
        }

        /// <summary>
        /// ДОбавление товара в корзину
        /// </summary>
        /// <param name="product">Получаем конкретный товар</param>
        public void Add(Product product)
        {
            if(Products.TryGetValue(product, out int count)) //Если товар уже есть в корзине, увеличиваем его количество
            {
                Products[product] = ++count;
            }
            else //Иначе добавляем новый товар в словарь в количестве 1 единицы
            {
                Products.Add(product, 1);
            }
        }

        public IEnumerator GetEnumerator()
        {
            foreach(var product in Products.Keys)
            {
                for(int i = 0; i < Products[product]; i++)
                {
                    yield return product;
                }
            }
        }

        /// <summary>
        /// Получить список товаров в корзине
        /// </summary>
        /// <returns>Возвращает список товаров в формате List(Product)</returns>
        public List<Product> GetAll()
        {
            var result = new List<Product>();
            foreach(Product i in this)
            {
                result.Add(i);
            }
            return result;
        }
    }
}
