using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace xcaret.Models
{
    
    public class Principal
    {        
        public int count { get; set; }
        public List<Entry> entries { get; set; }
    }
}
