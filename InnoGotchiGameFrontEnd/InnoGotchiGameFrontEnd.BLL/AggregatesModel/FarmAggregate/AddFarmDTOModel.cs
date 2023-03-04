using System.ComponentModel.DataAnnotations;

namespace InnoGotchiGameFrontEnd.BLL.AggregatesModel.FarmAggregate
{
    public class AddFarmDTOModel
    {
        [Required]
        public string Name { get; set; }
    }
}
