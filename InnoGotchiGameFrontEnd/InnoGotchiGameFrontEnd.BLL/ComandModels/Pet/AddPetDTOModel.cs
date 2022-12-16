using InnoGotchiGameFrontEnd.BLL.Model;

namespace InnoGotchiGameFrontEnd.BLL.ComandModels.Pet
{
	public class AddPetDTOModel
	{
		public int FarmId { get; set; }
		public string Name { get; set; }
		public PetViewDTO View { get; set; }
	}
}
