using System.Collections.Generic;

namespace CrmBl.Model
{
    /// <summary>
    /// Класс товара
    /// </summary>
    public class Product
    {
        /// <summary>
        /// ID товара в базе
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// Название товара
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Стоимость товара
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Доступное количество
        /// </summary>
        public int Count { get; set; }

        public virtual ICollection<Sell> Sells { get; set; }

        public override string ToString()
        {
            return $"{Name} - {Price}";
        }
        public string ToString(int i)
        {
            return $"{Name} - {Price}, Осталось {Count}";
        }

        public override int GetHashCode()
        {
            return ProductId;
        }

        public override bool Equals(object obj)
        {
            if (obj is Product product)
            {
                return ProductId.Equals(product.ProductId);
            }

            return false;
        }
    }
}
