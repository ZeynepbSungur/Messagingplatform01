﻿namespace AnketPortali01.Models
{
    public class messaging
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public bool IsActive { get; set; }

        public int CategoryId { get; set; }
        public Group Category { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public int GroupId { get; internal set; }
    }
}
