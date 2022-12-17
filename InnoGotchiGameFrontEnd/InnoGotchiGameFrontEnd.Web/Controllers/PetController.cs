using InnoGotchiGameFrontEnd.BLL;
using InnoGotchiGameFrontEnd.BLL.ComandModels.Pet;
using InnoGotchiGameFrontEnd.Web.ViewModels.Pets;
using Microsoft.AspNetCore.Mvc;

namespace InnoGotchiGameFrontEnd.Web.Controllers
{
    [Route("/Pets")]
    public class PetController : BaseController
    {
        private PetManager _petManager;
        public PetController(PetManager petManager)
        {
            _petManager = petManager;
        }

        [HttpGet("create")]
        public async Task<IActionResult> CreatePet(int farmId)
        {
            var bodiesLinks = new List<string>();
            var eyesLinks = new List<string>();
            var mouthsLinks = new List<string>();
            var nosesLinks = new List<string>();
            for(int i = 0; i < 5; i++)
            {
                bodiesLinks.Add("E:\\Projects\\Resourses\\Bodies\\body" + i + ".svg");
                eyesLinks.Add("E:\\Projects\\Resourses\\Eyes\\eyes" + i + ".svg");
                mouthsLinks.Add("E:\\Projects\\Resourses\\Mouths\\mouth" + i + ".svg");
                nosesLinks.Add("E:\\Projects\\Resourses\\Noses\\nose" + i + ".svg");
            }
            var model = new CreatePetViewModel()
            {
                FarmId = farmId,
                BodiesLinks = bodiesLinks,
                EyesLinks = eyesLinks,
                MouthsLinks = mouthsLinks,
                NosesLinks = nosesLinks
            };
            return View(model);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreatePet(AddPetDTOModel addModel)
        {
            var rez = await _petManager.Create(addModel);
            return Redirect("/");
        }

    }
}
