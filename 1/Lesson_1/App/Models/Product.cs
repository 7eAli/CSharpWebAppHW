﻿namespace App.Models
{
    public class Product : BaseModel
    {
        public int Cost { get; set; }
        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }
        public virtual List<Storage> Storages { get; set; } = new List<Storage>();
    }
}