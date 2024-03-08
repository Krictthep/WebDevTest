using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace WebDevTest.Models
{
    [Keyless]
    public class BookResults
    {
        
            public string? Title { get; set; }

            public string? AuthorName { get; set; }

            public int AuthorCount { get; set; }
        
    }







}
