using Microsoft.AspNetCore.Mvc;
using project.Models;
using project.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace project.Controllers
{
    public class PizzaController : Controller
    {
        private PizzeriaContext _database;
        private ICustomLog _logged;

        public PizzaController(PizzeriaContext database, ICustomLog logged)
        {
            _database = database;
            _logged = logged;
        }

        [HttpGet]
        public IActionResult Landing()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            List<Pizza> lista = _database.pizze.ToList<Pizza>();
            _logged.Log("Connessione riuscita!");
            return View(lista);
        }

        [HttpGet]
        [Authorize]
        public IActionResult PizzaDetail(int id)
        {
            Pizza pizza = _database.pizze.Include(pizza => pizza.Category).Include(pizza => pizza.Ingredienti).FirstOrDefault(x => x.Id == id);
            if (pizza == null)
            {
                return NotFound();
            }
            _logged.Log("Connessione riuscita!");
            return View(pizza);
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public IActionResult PizzaForm()
        {
            PizzaFormModel model = new PizzaFormModel();
            model.categories =_database.categories.ToList<Category>();
            model.AllIngrdients = _database.ingredienti.ToList<Ingrediente>();
            //foreach (Ingrediente i in _database.ingredienti.ToList<Ingrediente>())
            //{
            //    model.AllIngrdients.Add(
            //        new SelectListItem()
            //        {
            //            Text = i.Nome,
            //            Value = i.Id.ToString(),
            //        });
            //}
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN")]
        public IActionResult CreatePizza(PizzaFormModel data)
        {
            if (!ModelState.IsValid)
            {
                PizzaFormModel model = new PizzaFormModel();
                model.categories = _database.categories.ToList<Category>();
                //foreach (Ingrediente i in _database.ingredienti.ToList<Ingrediente>())
                //{
                //    model.AllIngrdients.Add(
                //        new SelectListItem()
                //        {
                //            Text = i.Nome,
                //            Value = i.Id.ToString(),
                //        });
                //}
                model.AllIngrdients = _database.ingredienti.ToList<Ingrediente>();
                return View("PizzaForm", model);
            }
            Pizza pizza = new Pizza();
            pizza.Nome = data.Nome;
            pizza.Descrizione = data.Descrizione;
            pizza.Prezzo = data.Prezzo;
            pizza.CategoryId = data.CategoryID;
            if(data.Img != null)
                pizza.Img=PhotoHelper.SavePhoto(data.Img);
            pizza.Ingredienti = new List<Ingrediente>();
            foreach (int id in data.IngredientiId)
                pizza.Ingredienti.Add(_database.ingredienti.FirstOrDefault(x => x.Id == id));
            _database.pizze.Add(pizza);
            _database.SaveChanges();
            _logged.Log("Pizza salvata!");
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public IActionResult EditPizza(int id)
        {
            Pizza pizza =_database.pizze.Include(pizza => pizza.Ingredienti).FirstOrDefault(x => x.Id == id);
            if (pizza == null)
            {
                return NotFound();
            }
            _logged.Log("Connessione riuscita!");
            PizzaFormModel model = new PizzaFormModel();
            model.Nome = pizza.Nome;
            model.Descrizione = pizza.Descrizione;
            model.Prezzo= pizza.Prezzo;
            model.Id = pizza.Id;
            model.CategoryID = pizza.CategoryId;
            model.categories = _database.categories.ToList<Category>();
            model.AllIngrdients = _database.ingredienti.ToList<Ingrediente>();
            //foreach (Ingrediente i in _database.ingredienti.ToList<Ingrediente>())
            //{
            //    model.AllIngrdients.Add(
            //        new SelectListItem()
            //        {
            //            Text = i.Nome,
            //            Value = i.Id.ToString(),
            //        });
            //}
            foreach (Ingrediente i in pizza.Ingredienti)
            {
                model.IngredientiId.Add(i.Id);
            }
            return View("PizzaForm", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN")]
        public ActionResult UpdatePizza(int id, PizzaFormModel data) 
        {
            Pizza pizza = _database.pizze.Include(pizza => pizza.Ingredienti).FirstOrDefault(x => x.Id == id);
            if (pizza == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return RedirectToAction("EditPizza", id);
            }
            pizza.Nome = data.Nome;
            pizza.Descrizione = data.Descrizione;
            pizza.Prezzo = data.Prezzo;
            pizza.CategoryId = data.CategoryID;
            if(pizza.Ingredienti == null)
            {
                pizza.Ingredienti = new List<Ingrediente>();
            }
            else
            {
                pizza.Ingredienti.Clear();
            }
            foreach (int i in data.IngredientiId)
                pizza.Ingredienti.Add(_database.ingredienti.FirstOrDefault(x => x.Id == i));
            if (data.Img != null)
            {
                PhotoHelper.DeletePhoto(pizza.Img);
                pizza.Img = PhotoHelper.SavePhoto(data.Img);
            }
            _database.SaveChanges();
            _logged.Log("Pizza salvata!");
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN")]
        public IActionResult DeletePizza(int id)
        {
            Pizza pizza = _database.pizze.Where(x => x.Id == id).FirstOrDefault();
            if (pizza == null)
            {
                return NotFound();
            }
            PhotoHelper.DeletePhoto(pizza.Img);
            _database.pizze.Remove(pizza);
            _database.SaveChanges();
            _logged.Log("Pizza cancellata!");
            return RedirectToAction("Index");
        }
    }
}
