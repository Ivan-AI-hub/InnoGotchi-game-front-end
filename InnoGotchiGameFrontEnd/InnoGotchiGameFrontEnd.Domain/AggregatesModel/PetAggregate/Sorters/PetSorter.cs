namespace InnoGotchiGameFrontEnd.Domain.AggregatesModel.PetAggregate.Sorters
{
    public class PetSorter
    {
        public PetSortRule SortRule { get; set; }
        public bool IsDescendingSort { get; set; }
        public PetSorter(PetSortRule sortRule, bool isDescendingSort)
        {
            SortRule = sortRule;
            IsDescendingSort = isDescendingSort;
        }
    }
}
