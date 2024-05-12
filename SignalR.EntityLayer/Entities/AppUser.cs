using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.EntityLayer.Entities
{
	public class AppUser:IdentityUser<int>
	{
        //normalde identity id'yi string veriyo ben yukarıda 
        //<int> diyerek id yi int al dedim
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
