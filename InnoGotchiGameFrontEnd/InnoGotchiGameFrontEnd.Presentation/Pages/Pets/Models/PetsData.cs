using InnoGotchiGameFrontEnd.BLL.AggregatesModel.PetAggregate;
using InnoGotchiGameFrontEnd.BLL.AggregatesModel.PetAggregate.BaseModels;
using InnoGotchiGameFrontEnd.Presentation.Components.PageSystem;

namespace InnoGotchiGameFrontEnd.Presentation.Pages.Pets.Models
{
    public class PetsData
    {
        private string _feedSelectValue;
        private string _thirstySelectValue;
        public string FeedSelectValue
        {
            get => _feedSelectValue;
            set
            {
                _feedSelectValue = value;
                if (_feedSelectValue == "null")
                {
                    Filtrator.HungerLevel = null;
                }
                else
                {
                    Filtrator.HungerLevel = Enum.Parse<HungerLevel>(_feedSelectValue);
                }
            }
        }
        public string ThirstySelectValue
        {
            get => _thirstySelectValue;
            set
            {
                _thirstySelectValue = value;
                if (_thirstySelectValue == "null")
                {
                    Filtrator.ThirstyLevel = null;
                }
                else
                {
                    Filtrator.ThirstyLevel = Enum.Parse<ThirstyLevel>(_thirstySelectValue);
                }
            }
        }
        public IEnumerable<PetDTO>? Pets { get; set; }
        public PetDTOFiltrator Filtrator { get; set; }
        public PetDTOSorter Sorter { get; set; }
        public Page Page { get; set; }
        public PageStatus PageStatus => Page.GetPageStatus(Pets == null ? 0 : Pets.Count());
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
