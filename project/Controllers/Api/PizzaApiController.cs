using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project.Models;
using System.Data;

namespace project.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]

    public class PizzaApiController : ControllerBase
    {
        private PizzeriaContext _database;
        public PizzaApiController(PizzeriaContext database)
        {
            _database = database;
        }

        [HttpGet]
        public IActionResult Index(string? filter)
        {
            List<Pizza> pizzas = _database.pizze.Include(pizzas => pizzas.Ingredienti).ToList<Pizza>();
            if(filter != null)
            {
                pizzas = pizzas.FindAll(x => x.Nome.ToLower().Contains(filter.ToLower()));
            }
            return Ok(pizzas);
        }

        [HttpPost]
        public IActionResult CreatePizza([FromBody] Pizza data)
        {
            Pizza pizza = new Pizza();
            pizza.Nome = data.Nome;
            pizza.Descrizione = data.Descrizione;
            pizza.Prezzo = data.Prezzo;
            pizza.CategoryId = data.CategoryId;
            pizza.Img = data.Img;
            if(data.Ingredienti != null)
            {
                pizza.Ingredienti = new List<Ingrediente>();
                foreach (var i in data.Ingredienti)
                    pizza.Ingredienti.Add(_database.ingredienti.FirstOrDefault(x => x.Id == i.Id));
            }
            _database.pizze.Add(pizza);
            _database.SaveChanges();
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult PizzaDetail(int id)
        {
            Pizza pizza = _database.pizze.Include(pizza => pizza.Category).Include(pizza => pizza.Ingredienti).FirstOrDefault(x => x.Id == id);
            if (pizza == null)
            {
                return NotFound($"Pizza {id} non trovata");
            }
            return Ok(pizza);
        }

        [HttpPut("{id}")]
        public ActionResult UpdatePizza(int id, Pizza data)
        {
            Pizza pizza = _database.pizze.Include(pizza => pizza.Ingredienti).FirstOrDefault(x => x.Id == id);
            if (pizza == null)
            {
                return NotFound($"Pizza {id} non trovata");
            }
            pizza.Nome = data.Nome;
            pizza.Descrizione = data.Descrizione;
            pizza.Prezzo = data.Prezzo;
            pizza.CategoryId = data.CategoryId;
            pizza.Img = data.Img;
            if (pizza.Ingredienti == null)
            {
                pizza.Ingredienti = new List<Ingrediente>();
            }
            else
            {
                pizza.Ingredienti.Clear();
            }
            if(data.Ingredienti != null)
            {
                foreach (var i in data.Ingredienti)
                    pizza.Ingredienti.Add(_database.ingredienti.FirstOrDefault(x => x.Id == i.Id));
            }
            _database.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePizza(int id)
        {
            Pizza pizza = _database.pizze.Where(x => x.Id == id).FirstOrDefault();
            if (pizza == null)
            {
                return NotFound($"Pizza {id} non trovata");
            }
            _database.pizze.Remove(pizza);
            _database.SaveChanges();
            return Ok();
        }
    }
}
