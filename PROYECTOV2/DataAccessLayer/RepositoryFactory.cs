using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class RepositoryFactory 
    {
        public static Repository CreateRepository()
        {
            return new EFRepository(new Entities.Sales_DBEntities());
        }
    }
}
