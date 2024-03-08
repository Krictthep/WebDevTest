using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace WebDevTest.Models
{

    [Keyless]
    public class AuthorResults
    {
        public string AuthorId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PenName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }


    }
}
