using InnoGotchiGameFrontEnd.BLL.Model;
using System.ComponentModel.DataAnnotations;

namespace InnoGotchiGameFrontEnd.BLL.ComandModels.User
{
    public class UpdateUserDTODataModel
    {
        [Required]
        public int UpdatedId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public PictureDTO? Picture { get; set; }
    }
}
