namespace InnoGotchiGameFrontEnd.Presentation.Components.PageSystem
{
    public class Page
    {
        public Page(int number, int pageSize)
        {
            Number = number;
            PageSize = pageSize;
        }

        public int Number { get; set; }
        public int PageSize { get; set; }

        public PageStatus GetPageStatus(int itemsCount)
        {
            if (itemsCount < PageSize && Number <= 1)
            {
                return PageStatus.OnlyPage;
            }
            if (Number <= 1)
            {
                return PageStatus.FirstPage;
            }
            if (itemsCount < PageSize)
            {
                return PageStatus.LastPage;
            }
            return PageStatus.MiddlePage;
        }
    }
}
