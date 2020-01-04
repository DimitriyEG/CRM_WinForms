using System;
using System.Collections.Generic;

namespace CrmBl.Model
{
    /// <summary>
    /// Генератор сущностей
    /// </summary>
    public class Generator
    {
        Random rnd = new Random();
        /// <summary>
        /// Список покупателей
        /// </summary>
        public List<Customer> Customers { get; set; } = new List<Customer>();
        /// <summary>
        /// Список продуктов
        /// </summary>
        public List<Product> Products { get; set; } = new List<Product>();
        /// <summary>
        /// Список продавцов
        /// </summary>
        public List<Seller> Sellers { get; set; } = new List<Seller>();
        /// <summary>
        /// вызов генератора покупателей (количество)
        /// </summary>
        /// <param name="count">Количество покупателей</param>
        /// <returns>Коллекция покупателей</returns>
        public List<Customer> GetNewCustomers(int count)
        {
            var result = new List<Customer>();

            for(int i = 0; i < count; i++)
            {
                var customer = new Customer()
                {
                    CustomerId = Customers.Count,
                    Name = GetRandomText()
                };
                Customers.Add(customer);
                result.Add(customer);
            }

            return result;
        }

        public List<Seller> GetNewSellers(int count)
        {
            var result = new List<Seller>();

            for (int i = 0; i < count; i++)
            {
                var seller = new Seller()
                {
                    SellerId = Sellers.Count,
                    Name = GetRandomText()
                };
                Sellers.Add(seller);
                result.Add(seller);
            }

            return result;
        }

        /// <summary>
        /// Создать случайный товар
        /// </summary>
        /// <param name="count">Количество товаров</param>
        /// <returns>Коллекция случайных товаров</returns>
        public List<Product> GetNewProducts(int count)
        {
            var result = new List<Product>();

            for (int i = 0; i < count; i++)
            {
                var product = new Product()
                {
                    ProductId = Products.Count,
                    Name = GetRandomText(),
                    Count = rnd.Next(10, 1000),
                    Price = Convert.ToDecimal(rnd.Next(5, 100000) +  rnd.NextDouble())
                };
                Products.Add(product);
                result.Add(product);
            }

            return result;
        }
        /// <summary>
        /// Получить рандомное количество товара
        /// </summary>
        /// <param name="min">От</param>
        /// <param name="max">До</param>
        /// <returns>Коллекция случайного количества товара</returns>
        public List<Product> GetRandomProducts(int min, int max)
        {
            var result = new List<Product>();

            var count = rnd.Next(min, max);
            for(int i = 0; i < count; i++)
            {
                result.Add(Products[rnd.Next(Products.Count - 1)]);
            }
            return result;
        }

        private static string GetRandomText()
        {
            return Guid.NewGuid().ToString().Substring(0, 5);
        }
    }
}
