using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    public class EspecialidadViewModel
    {

        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string Nombre { get; set; }

    }
}
