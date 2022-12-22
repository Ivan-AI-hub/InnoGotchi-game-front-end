using InnoGotchiGameFrontEnd.BLL;
using InnoGotchiGameFrontEnd.BLL.ComandModels.Pet;
using InnoGotchiGameFrontEnd.Web.ViewModels.Pets;
using Microsoft.AspNetCore.Mvc;

namespace InnoGotchiGameFrontEnd.Web.Controllers
{
    [Route("/Pets/create")]
    public class PetController : BaseController
    {
        private PetManager _petManager;
        private PictureManager _pictureManager;
        public PetController(PetManager petManager, PictureManager pictureManager)
        {
            _petManager = petManager;
            _pictureManager = pictureManager;
        }

        public async Task<IActionResult> CreatePet(int farmId)
        {
            var bodiesPictures = await _pictureManager.GetAllPictures("body");
            var eyesPictures = await _pictureManager.GetAllPictures("eye");
            var mouthsPictures = await _pictureManager.GetAllPictures("mouth");
            var nosesPictures = await _pictureManager.GetAllPictures("nose");

            var model = new CreatePetViewModel()
            {
                FarmId = farmId,
                BodiesPictures = bodiesPictures.ToList(),
                EyesPictures = eyesPictures.ToList(),
                MouthsPictures = mouthsPictures.ToList(),
                NosesPictures = nosesPictures.ToList()
            };
            return View(model);
        }
        //[HttpPost("create")]
        //public async Task<IActionResult> CreatePet(AddPetDTOModel addModel)
        //{
        //    var rez = await _petManager.Create(addModel);
        //    return Redirect("/");
        //}
    }
}
