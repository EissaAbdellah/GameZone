namespace GameZone.ViewModels
{
    public class GameFormViewModel
    {

        [Required(ErrorMessage = "Name Required")]
        [MaxLength(250, ErrorMessage = "max lenght 500 ")]
        public string Name { get; set; } = string.Empty;


        [Required(ErrorMessage = "Category Required")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; } = Enumerable.Empty<SelectListItem>();

        [Required(ErrorMessage = "Availabe Devices Required")]
        [Display(Name = "Availabe Devices")]
        public List<int> SelectedDevices { get; set; } = new List<int>();
        public IEnumerable<SelectListItem> Devices { get; set; } = default!;


        [Required(ErrorMessage = "Descreption Required")]
        [MaxLength(2500)]
        public string Descreption { get; set; } = string.Empty;



    }
}
