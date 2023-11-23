
namespace GameZone.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }

        [MaxLength(250, ErrorMessage = "max lenght 500 ")]
        public string Name { get; set; } = string.Empty;

    }
}
