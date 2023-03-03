namespace InnoGotchiGameFrontEnd.Domain.AggregatesModel.FarmAggregate.Sorters
{
    public class FarmSorter
    {
        public FarmSortRule SortRule { get; set; }
        public bool IsDescendingSort { get; set; }

        public FarmSorter(FarmSortRule sortRule, bool isDescendingSort)
        {
            SortRule = sortRule;
            IsDescendingSort = isDescendingSort;
        }
    }
}
