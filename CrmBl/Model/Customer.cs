using System.Collections.Generic;

namespace CrmBl.Model
{
    /// <summary>
    /// Класс покупателя
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// ID покупателя
        /// </summary>
        public int CustomerId { get; set; }
        /// <summary>
        /// Имя покупателя
        /// </summary>
        public string Name { get; set; }

        public virtual ICollection<Check> Checks { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
