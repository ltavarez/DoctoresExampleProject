using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Database.Model
{
    public partial class Session
    {
        
        public int Id { get; set; }
        public string Token { get; set; }
        public string UserId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastActivity { get; set; }
        public bool IsExpired { get; set; }
        public bool Expirable { get; set; }
    }
}
