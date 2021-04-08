using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TourBase_Stage_1_2.Models
{
    public class User : IdentityUser
    {
        public int Year { set; get; }
    }
}
