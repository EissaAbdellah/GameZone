namespace GameZone.ViewModels
{
    public class EditGameFromViewModel : GameFormViewModel
    {

        public int Id { get; set; }


        public string currentCover { get; set; }

        public IFormFile? Cover { get; set; } = default!;


    }
}
