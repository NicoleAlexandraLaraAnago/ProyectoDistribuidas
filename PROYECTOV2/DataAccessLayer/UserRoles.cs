using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class UserRoles
    {
        public int UserID { get; set; }
        public int RoleID { get; set; }

        public virtual Users User { get; set; }
        public virtual Roles Role { get; set; }
    }
}
