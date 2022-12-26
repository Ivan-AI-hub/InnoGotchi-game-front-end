using InnoGotchiGameFrontEnd.BLL.Model;
using System.ComponentModel.DataAnnotations;

namespace InnoGotchiGameFrontEnd.BLL.ComandModels.Pet
{
	public class AddPetDTOModel
	{
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
