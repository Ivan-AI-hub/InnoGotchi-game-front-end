using InnoGotchiGameFrontEnd.Domain.AggregatesModel.FarmAggregate.Filtrators;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.FarmAggregate.Sorters;
using System.Text;

namespace InnoGotchiGameFrontEnd.DAL.UriConstructors
{
    internal static class PetFarmUriConstructor
    {
        public static string GenerateUriQuery(FarmSorter? sorter = null, FarmFiltrator? filtrator = null)
        {
            var requestUrl = new StringBuilder("?");
            if (sorter != null)
                requestUrl.Append($"sortField={sorter.SortRule}&isDescendingSort={sorter.IsDescendingSort}");

            if (filtrator != null && !string.IsNullOrEmpty(filtrator.Name))
                requestUrl.Append($"&Name={filtrator.Name}");

            return requestUrl.ToString();
        }
    }
}
