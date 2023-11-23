namespace GameZone.ViewModels
{
    public class CreateGameFromViewModel : GameFormViewModel
    {


        // [AllowedExtention(FileSettings.AllowedExtentions)]
        //  [MaxFileSize(FileSettings.MaxFileSizeInBytes)]
        [Required(ErrorMessage = "Cover Required")]
        public IFormFile Cover { get; set; } = default!;


    }
}
