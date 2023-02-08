using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Entities
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
