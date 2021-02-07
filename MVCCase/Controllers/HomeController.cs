using MVCCase.Models;
using System.Linq;
using System.Web.Mvc;

namespace MVCCase.Controllers
{
    public class HomeController : Controller
    {
        ICaseEntities db = new ICaseEntities();
        public ActionResult Index()
        {
            var product = db.Products.ToList();
            return View(product);
        }
        [Authorize]
        public ActionResult About()
        {
            var product = db.Products.ToList();
            return View(product);
        }
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();
            return View();
        }

        public ActionResult Delete(string id)
        {
            var product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("About");
        }

        public ActionResult Update(int id)
        {
            var product = db.Products.Find(id);
            return View("Update", product);
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(Product product)
        {
            if (product.ProductName == null || product.barCode == null || product.Price == null || product.Piece == null)
            {
                return View("Contact");
            }
            db.Products.Add(product);
            db.SaveChanges();
            return RedirectToAction("About");
        }
        public ActionResult Updates(Product product)
        {
            var products = db.Products.Find(product.ProductId);
            products.ProductName = product.ProductName;
            products.barCode = product.barCode;
            products.Price = product.Price;
            products.Image = product.Image;
            products.explanation = product.explanation;
            products.Piece = product.Piece;
            db.SaveChanges();
            return RedirectToAction("About");
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            var admin = db.Users.FirstOrDefault(u => u.UserName == user.UserName && u.UserPassword == user.UserPassword);
            if (admin != null)
            {
                return RedirectToAction("Home", "About");
            }
            return View();
        }
    }
}