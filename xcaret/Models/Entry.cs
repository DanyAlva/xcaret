using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace xcaret.Models
{
    
    public class Entry
    {
        
        public string API { get; set; }
        public string Description { get; set; }
        public string Auth { get; set; }
        public bool HTTPS { get; set; }
        public string Cors { get; set; }
        public string Link { get; set; }
        public string Category { get; set; }
    }
}
