namespace ProducerAndConsumer.WebApi.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
    }
}
