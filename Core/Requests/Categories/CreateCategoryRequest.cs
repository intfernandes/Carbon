
using System.ComponentModel.DataAnnotations;

namespace Core.Requests.Categories
{
    public class CreateCategoryRequest : Request
    {
        [Required(ErrorMessage = "Titulo Inválido")]
        [MaxLength(80, ErrorMessage = "Max. 80 caracteres")]
        public string Title { get; set; } = string.Empty;


        [Required(ErrorMessage = "Descrição Inválida")]
        public string Description { get; set; } = string.Empty;


    }
}