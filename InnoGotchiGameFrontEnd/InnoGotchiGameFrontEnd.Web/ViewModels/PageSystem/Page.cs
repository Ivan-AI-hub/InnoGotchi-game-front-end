namespace InnoGotchiGameFrontEnd.Web.ViewModels.PageSystem
{
    public class Page
    {
        public Page(int number, PageStatus pageStatus)
        {
            Number = number;
            PageStatus = pageStatus;
        }

        public int Number { get; set; }
        public PageStatus PageStatus { get; set; }
    }
}
