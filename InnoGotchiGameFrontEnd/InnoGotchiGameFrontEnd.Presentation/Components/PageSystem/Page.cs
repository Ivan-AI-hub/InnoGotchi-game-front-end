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
            PageStatus pageStatus;
            if (Number <= 1)
            {
                pageStatus = PageStatus.FirstPage;
            }
            else if (itemsCount < PageSize)
            {
                pageStatus = PageStatus.LastPage;
            }
            else
            {
                pageStatus = PageStatus.MiddlePage;
            }
            return pageStatus;
        }
    }
}
