namespace Lesson_3_App.Models
{
    public class Storage 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ProductId { get; set; }
        public virtual Product? Product { get; set; }
        public int Amount { get; set; }
    }
}
