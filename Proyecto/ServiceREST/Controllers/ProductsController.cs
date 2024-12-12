using SLC;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL;
using Entities;

namespace ServiceREST.Controllers
{
    public class ProductController : ApiController, IProductService
    {
        private readonly ProductLogic productLogic = new ProductLogic();

        [HttpPost]
        public Products Create(Products products)
        {
            var product = productLogic.Create(products);
            return product;
        }

        [HttpGet]
        public Products Retrieve(int id)
        {
            var product = productLogic.RetriveByID(id);
            if (product == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return product;
        }

        [HttpPut]
        public bool Update(Products productToUpdate)
        {
            var updated = productLogic.Update(productToUpdate);
            return updated;
        }

        [HttpDelete]
        public bool Delete(int id)
        {
            var deleted = productLogic.Delete(id);
            return deleted;
        }

        [HttpGet]
        public List<Products> GetAllProducts()
        {
            var products = productLogic.GetAllProducts();
            return products;
        }

        [HttpGet]

        public List<Products> FilterByCategoryID(int categoryID)
        {
            var products = productLogic.FilterByCategoryID(categoryID);
            if (products == null || products.Count == 0)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return products;
        }

    }
}
