using SLC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SLC;
using BLL;
using Entities;
namespace ServiceREST.Controllers
{
    public class ProductController : ApiController, IProductService
    {
        [HttpPost]
        public Products Create(Products products)
        {
            var productLogic = new ProductLogic();
            var product = productLogic.Create(products);
            return product;
        }

        [HttpDelete]
        public bool Delete(int id)
        {
            var productLogic = new ProductLogic();
            var product = productLogic.Delete(id);
            return product;
        }
    }
}
