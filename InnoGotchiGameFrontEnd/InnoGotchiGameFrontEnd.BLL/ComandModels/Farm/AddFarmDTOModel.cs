using System.ComponentModel.DataAnnotations;

namespace InnoGotchiGameFrontEnd.BLL.ComandModels.Farm
{
	public class AddFarmDTOModel
	{
		[Required]
		public int OwnerId { get; set; }
		[Required]
		public string Name { get; set; }
	}
}
