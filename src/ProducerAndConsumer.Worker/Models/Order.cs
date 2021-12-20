namespace ProducerAndConsumer.Worker.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

        public override string ToString()
        {
            return $"OrderId: {OrderId}|OrderId: {Title}|OrderId: {Description}|OrderId: {Price}";
        }
    }
}
