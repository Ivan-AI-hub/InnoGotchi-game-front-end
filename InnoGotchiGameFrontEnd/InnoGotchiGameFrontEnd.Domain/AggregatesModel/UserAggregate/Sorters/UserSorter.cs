namespace InnoGotchiGameFrontEnd.Domain.AggregatesModel.UserAggregate.Sorters
{
    public class UserSorter
    {
        public UserSortRule SortRule { get; set; }
        public bool IsDescendingSort { get; set; }
        public UserSorter(UserSortRule sortRule, bool isDescendingSort)
        {
            SortRule = sortRule;
            IsDescendingSort = isDescendingSort;
        }
    }
}
