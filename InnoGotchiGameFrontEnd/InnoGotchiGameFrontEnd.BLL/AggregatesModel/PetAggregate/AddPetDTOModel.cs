using InnoGotchiGameFrontEnd.BLL.AggregatesModel.PetAggregate.BaseModels;
using System.ComponentModel.DataAnnotations;

namespace InnoGotchiGameFrontEnd.BLL.AggregatesModel.PetAggregate
{
    public class AddPetDTOModel
    {
        [Required]
        public int FarmId { get; set; }
        [Required]
        public string Name { get; set; }
        public PetViewDTO View { get; set; }

        public AddPetDTOModel()
        {
            View = new PetViewDTO();
        }
    }
}
