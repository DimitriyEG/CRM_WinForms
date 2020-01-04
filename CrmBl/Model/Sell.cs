namespace CrmBl.Model
{
    /// <summary>
    /// Класс продаж
    /// </summary>
    public class Sell
    {
        /// <summary>
        ///ID продажи 
        /// </summary>
        public int SellId { get; set; }
        /// <summary>
        /// ID чека
        /// </summary>
        public int CheckId { get; set; }
        /// <summary>
        /// ID продукта
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// Привязкак экземпляру чека
        /// </summary>
        public virtual Check Check { get; set; }
        /// <summary>
        /// Привязка к экземпляру товара
        /// </summary>
        public virtual Product Product { get; set; }
    }
}
