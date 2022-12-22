using InnoGotchiGameFrontEnd.BLL.Model;

namespace InnoGotchiGameFrontEnd.Web.ViewModels.Pets
{
    public class CreatePetViewModel
    {
        public int FarmId { get; set; }
        public int CurrentBody { get; set; }
        public List<PictureDTO> BodiesPictures { get; set; }
        public List<PictureDTO> EyesPictures { get; set; }
        public List<PictureDTO> NosesPictures { get; set; }
        public List<PictureDTO> MouthsPictures { get; set; }
    }
}
