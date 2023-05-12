using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace project.Models
{
    public class PizzaFormModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo obbligatorio!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Il nome deve essere compreso tra 3 e 50 caratteri")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo obbligatorio!")]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "La descrizione deve essere compresa tra 10 e 100 caratteri")]
        [MoreThan2Words]
        public string Descrizione { get; set; }

        public IFormFile? Img { get; set; }

        [Required(ErrorMessage = "Campo obbligatorio!")]
        [Range(1, 9999.99, ErrorMessage = "Il prezzo deve essere compreso tra 1 e 9999.99")]
        public double Prezzo { get; set; }

        public int? CategoryID { get; set; }

        public List<Category>? categories { get; set; }

        public List<int>? IngredientiId { get; set;}

        public List<Ingrediente>? AllIngrdients { get; set; }

        public PizzaFormModel() 
        {
            IngredientiId = new List<int>();
            AllIngrdients = new List<Ingrediente>();
        }
    }
}
