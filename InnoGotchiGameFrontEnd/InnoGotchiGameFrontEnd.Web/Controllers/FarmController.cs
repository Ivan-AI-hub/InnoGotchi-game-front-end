using InnoGotchiGameFrontEnd.BLL;
using InnoGotchiGameFrontEnd.BLL.ComandModels.Farm;
using Microsoft.AspNetCore.Mvc;

namespace InnoGotchiGameFrontEnd.Web.Controllers
{
	[Route("/Farms")]
	public class FarmController : BaseController
	{
		private FarmManager _farmManager;

		public FarmController(FarmManager farmManager)
		{
			_farmManager = farmManager;
		}

		[HttpGet("create")]
		public async Task<IActionResult> CreateFarm(int ownerID)
		{
			return View(ownerID);
		}

		[HttpPost("create")]
		public async Task<IActionResult> CreateFarm(AddFarmDTOModel addModel)
		{
			var rez = await _farmManager.Create(addModel);
			return Redirect("/");
		}

		[HttpGet("update")]
		public async Task<IActionResult> UpdateFarm()
		{
			return View();
		}

		[HttpPost("update")]
		public async Task<IActionResult> UpdateFarm(UpdateFarmDTOModel updateModel)
		{
			var rez = await _farmManager.UpdateFarm(updateModel);
			return Redirect("/");
		}

		[HttpGet()]
		public async Task<IActionResult> FarmPage(int id)
		{
			var farm = await _farmManager.GetFarmById(id);
			return View(farm);
		}
	}
}
