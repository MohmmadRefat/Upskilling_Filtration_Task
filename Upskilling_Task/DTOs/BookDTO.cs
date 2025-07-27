namespace Upskilling_Task.DTOs
{
    public class BookDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
        public int  Stock { get; set; }
        public int  CategoryId { get; set; }
    }
}
