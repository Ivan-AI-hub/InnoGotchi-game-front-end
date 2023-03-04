using InnoGotchiGameFrontEnd.Domain.AggregatesModel.PetAggregate.Filtrators;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.PetAggregate.Sorters;
using System.Text;

namespace InnoGotchiGameFrontEnd.DAL.UriConstructors
{
    internal static class PetUriConstructor
    {
        public static string GenerateUriQuery(PetSorter? sorter = null, PetFiltrator? filtrator = null)
        {
            var quary = new StringBuilder("?");
            if (sorter != null)
                quary.Append($"sortField={sorter.SortRule}&isDescendingSort={sorter.IsDescendingSort}");

            if (filtrator == null)
                return quary.ToString();

            if (!String.IsNullOrEmpty(filtrator.Name))
            {
                quary.Append($"&Name={filtrator.Name}");
            }

            if (filtrator.DrinkingInterval != null)
            {
                quary.Append($"&MaxDaysFromLastDrinking={filtrator.DrinkingInterval.MaxDays}" +
                                  $"&MinDaysFromLastDrinking={filtrator.DrinkingInterval.MinDays}");
            }

            if (filtrator.FeedingInterval != null)
            {
                quary.Append($"&MaxDaysFromLastFeeding={filtrator.FeedingInterval.MaxDays}" +
                                  $"&MinDaysFromLastFeeding={filtrator.FeedingInterval.MinDays}");
            }

            quary.Append($"&DaysAlive={filtrator.DaysAlive}");
            return quary.ToString();
        }
    }
}
