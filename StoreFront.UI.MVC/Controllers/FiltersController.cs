using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreFront.DATA.EF;
using System.Data;
using System.Data.Entity;

namespace StoreFront.UI.MVC.Controllers
{
    public class FiltersController : Controller
    {
        private BurntheMeterEntities db = new BurntheMeterEntities();
        // GET: Filters
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductsQS(string searchFilter)
        {
            #region Optional Search Filter

            if (String.IsNullOrEmpty(searchFilter))
            {
                var products = db.Products;
                return View(products.ToList());
            }
            else
            {
                string searchUpCase = searchFilter.ToUpper();

                List<Product> searchResults =
                    (from m in db.Products
                     where m.ProductName.ToUpper().Contains(searchUpCase)
                     select m).ToList();

                return View(searchResults);
            }

            #endregion
        }

        public ActionResult PlatformsFilter(string platform)
        {
            List<Product> platformsFilter =
                (from m in db.Products
                 where m.Platform.PlatformName.Contains(platform)
                 select m).ToList();

            return View(platformsFilter);
        }
    }
}