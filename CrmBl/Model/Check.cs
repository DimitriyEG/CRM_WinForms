using System;
using System.Collections.Generic;

namespace CrmBl.Model
{
    /// <summary>
    /// Класс чека покупки
    /// </summary>
    public class Check
    {
        /// <summary>
        /// ID чека в БД
        /// </summary>
        public int CheckId { get; set; }
        /// <summary>
        /// ID покупателя
        /// </summary>
        public int CustomerId { get; set; }
        /// <summary>
        /// Экземпляр покупателя
        /// </summary>
        public virtual Customer Customer { get; set; }
        /// <summary>
        /// ID продавца
        /// </summary>
        public int SellerId { get; set; }
        /// <summary>
        /// Экземпляр продавца
        /// </summary>
        public virtual Seller Seller { get; set; }
        /// <summary>
        /// Дата печати чека
        /// </summary>
        public DateTime Created { get; set; }
        /// <summary>
        /// Связываем с продажами
        /// </summary>
        public virtual ICollection<Sell> Sells { get; set; }
        /// <summary>
        /// Итоговая стоимость покупки
        /// </summary>
        public decimal Price { get; set; }

        public override string ToString()
        {
            return $"№{CheckId} от {Created.ToString("dd.MM.yy hh:mm:ss")}";
        }
    }
}
