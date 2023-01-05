using InnoGotchiGameFrontEnd.BLL.Filtrators;
using InnoGotchiGameFrontEnd.BLL.Model.Pet;
using InnoGotchiGameFrontEnd.BLL.Sorters;
using InnoGotchiGameFrontEnd.Presentation.Components.PageSystem;

namespace InnoGotchiGameFrontEnd.Presentation.Pages.Pets.Models
{
    public class PetsData
    {
        public IEnumerable<PetDTO>? Pets { get; set; }
        public PetDTOFiltrator Filtrator { get; set; }
        public PetDTOSorter Sorter { get; set; }
        public Page Page { get; set; }
        public PageStatus PageStatus => Page.GetPageStatus(Pets == null? 0 : Pets.Count());
        public PetsData()
        {
            Pets = null;
            Filtrator = new PetDTOFiltrator();
            Sorter = new PetDTOSorter() 
            { 
                SortRule = PetDTOSortRule.happinessDays
            };
            Page = new Page(1, 15);
        }
    }
}
