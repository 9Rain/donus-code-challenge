
using System;

namespace DataAccess.Models
{
    public class BaseModel
    {
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool IsActive { get; set; }
    }
}