using System.ComponentModel.DataAnnotations;

namespace InnoGotchiGameFrontEnd.BLL.ComandModels.Farm
{
    public class AddFarmDTOModel
    {
        [Required]
        public string Name { get; set; }
    }
}
