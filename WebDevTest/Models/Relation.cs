using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace WebDevTest.Models
{
    

    [Keyless]
    public class Relation
    {
        public int Id { get; set; }
        public int BeforeId { get; set; }
      


    }
}
