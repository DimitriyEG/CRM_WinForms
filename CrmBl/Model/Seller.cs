using System.Collections.Generic;

namespace CrmBl.Model
{
    /// <summary>
    /// Класс продавца
    /// </summary>
    public class Seller
    {
        /// <summary>
        /// ID продавана в БД
        /// </summary>
        public int SellerId { get; set; }
        /// <summary>
        /// Имя продавана
        /// </summary>
        public string Name { get; set; }

        public virtual ICollection<Check> Checks { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Возвращаем имя</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
