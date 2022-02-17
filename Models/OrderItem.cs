namespace book_store_for_developers.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public decimal PurchasePrice{ get; set; }
        public virtual Book book { get; set; }
        public virtual  Order order { get; set; }
    }
}