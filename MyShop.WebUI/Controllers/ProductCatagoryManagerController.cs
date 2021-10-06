using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class ProductCatagoryManagerController : Controller
    {
        ProductCatagoryRepository context;

        public ProductCatagoryManagerController()
        {
            context = new ProductCatagoryRepository();
        }

        // GET: ProductManager
        public ActionResult Index()
        {
            List<ProductCatagory> productCatagories = context.Collection().ToList();

            return View(productCatagories);
        }

        public ActionResult Create()
        {
            ProductCatagory product = new ProductCatagory();
            return View(product);
        }
        [HttpPost]
        public ActionResult Create(ProductCatagory product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                context.Insert(product);
                context.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            ProductCatagory product = context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(product);
            }
        }
        [HttpPost]
        public ActionResult Edit(ProductCatagory product, string Id)
        {
            ProductCatagory productToEdit = context.Find(Id);
            if (productToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }
                productToEdit.Catagory = product.Catagory;
                
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string Id)
        {
            ProductCatagory productToDelete = context.Find(Id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            ProductCatagory productToDelete = context.Find(Id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}