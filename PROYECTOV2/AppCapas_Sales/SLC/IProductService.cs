using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;
using System.Threading.Tasks;

namespace SLC
{
    public interface IProductService
    {
        Products Create(Products products);

        bool Delete(int id);
    }
}
