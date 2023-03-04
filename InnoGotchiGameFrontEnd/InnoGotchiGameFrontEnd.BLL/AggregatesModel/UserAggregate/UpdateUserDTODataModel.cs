using InnoGotchiGameFrontEnd.BLL.AggregatesModel.PictureAggregate;
using System.ComponentModel.DataAnnotations;

namespace InnoGotchiGameFrontEnd.BLL.AggregatesModel.UserAggregate
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
