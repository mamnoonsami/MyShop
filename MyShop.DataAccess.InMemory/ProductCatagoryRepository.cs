using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    public class ProductCatagoryRepository
    {
        ObjectCache cache = MemoryCache.Default;

        List<ProductCatagory> productCatagories;

        public ProductCatagoryRepository()
        {
            productCatagories = cache["productCatagories"] as List<ProductCatagory>;
            if (productCatagories == null)
            {
                productCatagories = new List<ProductCatagory>();
            }
        }

        public void Commit()
        {
            cache["productCatagories"] = productCatagories;
        }

        public void Insert(ProductCatagory p)
        {
            productCatagories.Add(p);
        }

        public void Update(ProductCatagory p)
        {
            ProductCatagory productToUpdate = productCatagories.Find(pr => pr.Id == p.Id);

            if (productToUpdate != null)
            {

                productToUpdate = p;
            }
            else
            {
                throw new Exception("Product Category not found");
            }
        }

        public ProductCatagory Find(string Id)
        {
            ProductCatagory product = productCatagories.Find(pr => pr.Id == Id);

            if (product != null)
            {

                return product;
            }
            else
            {
                throw new Exception("Product Not Found");
            }
        }

        public IQueryable<ProductCatagory> Collection()
        {
            return productCatagories.AsQueryable();
        }

        public void Delete(string Id)
        {
            ProductCatagory productToDelete = productCatagories.Find(pr => pr.Id == Id);

            if (productToDelete != null)
            {

                productCatagories.Remove(productToDelete);
            }
            else
            {
                throw new Exception("Product Not Found");
            }
        }
    }
}
