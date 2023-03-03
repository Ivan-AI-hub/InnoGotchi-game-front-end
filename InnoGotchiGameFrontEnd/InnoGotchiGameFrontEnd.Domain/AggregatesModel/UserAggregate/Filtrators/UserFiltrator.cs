namespace InnoGotchiGameFrontEnd.Domain.AggregatesModel.UserAggregate.Filtrators
{
    public class UserFiltrator
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public int PetFarmId { get; set; } = -1;
    }
}
