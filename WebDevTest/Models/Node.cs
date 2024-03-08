using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;


namespace WebDevTest.Models
{
    [Keyless]
    public class Node
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public bool IsEnabled { get; set; }

    }
}
