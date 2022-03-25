using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StoreFront.DATA.EF;
using StoreFront.UI.MVC.Utilities;
using StoreFront.UI.MVC.Models;

namespace StoreFront.UI.MVC.Controllers
{
    public class ProductsController : Controller
    {
        private BurntheMeterEntities db = new BurntheMeterEntities();

        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.CableType).Include(p => p.Category).Include(p => p.Platform).Include(p => p.StockStatu);
            return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.CableTypeID = new SelectList(db.CableTypes, "CableTypeID", "CableTypeName");
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
            ViewBag.PlatformID = new SelectList(db.Platforms, "PlatformID", "PlatformName");
            ViewBag.StockStatusID = new SelectList(db.StockStatus, "StockStatusID", "StockStatus");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,ProductName,CategoryID,Price,StockStatusID,PlatformID,CableTypeID,Description,ProductImage")] Product product, HttpPostedFileBase ProductImage)
        {
            if (ModelState.IsValid)
            {

                #region File Upload

                string file = "NoImage.jpg";

                if (ProductImage != null)
                {
                    file = ProductImage.FileName;
                    string ext = file.Substring(file.LastIndexOf('.'));
                    string[] goodExts = { ".jpeg", ".jpg", ".png", ".gif" };
                    if (goodExts.Contains(ext.ToLower()) && ProductImage.ContentLength <= 4194304)
                    {
                        file = Guid.NewGuid() + ext;

                        #region Resize Image

                        string savePath = Server.MapPath("~/Content/imgstore/products/");
                        Image convertedImage = Image.FromStream(ProductImage.InputStream);
                        int maxImageSize = 500;
                        int maxThumbSize = 300;
                        ImageUtility.ResizeImage(savePath, file, convertedImage, maxImageSize, maxThumbSize);

                        #endregion
                    }
                    product.ProductImage = file;
                }

                #endregion

                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CableTypeID = new SelectList(db.CableTypes, "CableTypeID", "CableTypeName", product.CableTypeID);
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            ViewBag.PlatformID = new SelectList(db.Platforms, "PlatformID", "PlatformName", product.PlatformID);
            ViewBag.StockStatusID = new SelectList(db.StockStatus, "StockStatusID", "StockStatus", product.StockStatusID);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CableTypeID = new SelectList(db.CableTypes, "CableTypeID", "CableTypeName", product.CableTypeID);
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            ViewBag.PlatformID = new SelectList(db.Platforms, "PlatformID", "PlatformName", product.PlatformID);
            ViewBag.StockStatusID = new SelectList(db.StockStatus, "StockStatusID", "StockStatus", product.StockStatusID);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,ProductName,CategoryID,Price,StockStatusID,PlatformID,CableTypeID,Description,ProductImage")] Product product, HttpPostedFileBase ProductImage)
        {
            if (ModelState.IsValid)
            {
                #region File Upload

                string file = product.ProductImage;
                if (ProductImage != null)
                {
                    file = ProductImage.FileName;
                    string ext = file.Substring(file.LastIndexOf('.'));
                    string[] goodExts = { ".jpeg", ".jpg", ".png", ".gif" };
                    if (goodExts.Contains(ext.ToLower()) && ProductImage.ContentLength <= 4194304)
                    {
                        file = Guid.NewGuid() + ext;
                        #region Resize Image

                        string savePath = Server.MapPath("~/Content/imgstore/products/");
                        Image convertedImage = Image.FromStream(ProductImage.InputStream);
                        int maxImageSize = 500;
                        int maxThumbSize = 300;
                        ImageUtility.ResizeImage(savePath, file, convertedImage, maxImageSize, maxThumbSize);

                        #endregion
                        if (product.ProductImage != null && product.ProductImage != "NoImage.png")
                        {
                            string path = Server.MapPath("~/Content/imgstore/products/");
                            ImageUtility.Delete(path, product.ProductImage);
                        }
                        product.ProductImage = file;
                    }
                }

                #endregion

                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CableTypeID = new SelectList(db.CableTypes, "CableTypeID", "CableTypeName", product.CableTypeID);
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            ViewBag.PlatformID = new SelectList(db.Platforms, "PlatformID", "PlatformName", product.PlatformID);
            ViewBag.StockStatusID = new SelectList(db.StockStatus, "StockStatusID", "StockStatus", product.StockStatusID);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);

            string path = Server.MapPath("~/Content/imgstore/products/");
            ImageUtility.Delete(path, product.ProductImage);

            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Products()
        {
            var products = db.Products.Include(p => p.CableType).Include(p => p.Category).Include(p => p.Platform).Include(p => p.StockStatu);
            return View(products.ToList());
        }

        #region Custom Add-to-Cart Functionality (Called from Details View)

        public ActionResult AddToCart(int qty, int productID)
        {
            Dictionary<int, CartItemViewModel> shoppingCart = null;

            if (Session["cart"] != null)
            {
                shoppingCart = (Dictionary<int, CartItemViewModel>)Session["cart"];
            }
            else
            {
                shoppingCart = new Dictionary<int, CartItemViewModel>();
            }


            Product product = db.Products.Where(b => b.ProductID == productID).FirstOrDefault();

            if (product == null)
            {
                return RedirectToAction("Products");
            }
            else
            {
                CartItemViewModel item = new CartItemViewModel(qty, product);
                if (shoppingCart.ContainsKey(product.ProductID))
                {
                    shoppingCart[product.ProductID].Qty += qty;
                }
                else
                {
                    shoppingCart.Add(product.ProductID, item);
                }
                Session["cart"] = shoppingCart; 
            }
            return RedirectToAction("Products", "ShoppingCart");
        }

        #endregion
    }

}
