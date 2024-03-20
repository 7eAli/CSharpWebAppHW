namespace App.Models
{
    public class Storage : BaseModel
    {
        public int ProductId { get; set; }
        public virtual Product? Product { get; set; }
        public int Amount { get; set; }
    }
}
