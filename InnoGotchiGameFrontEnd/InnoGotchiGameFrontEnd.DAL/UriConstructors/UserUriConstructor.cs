using InnoGotchiGameFrontEnd.Domain.AggregatesModel.PetAggregate.Filtrators;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.PetAggregate.Sorters;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.UserAggregate.Filtrators;
using InnoGotchiGameFrontEnd.Domain.AggregatesModel.UserAggregate.Sorters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoGotchiGameFrontEnd.DAL.UriConstructors
{
    internal static class UserUriConstructor
    {
        public static string GenerateUriQuery(UserSorter? sorter = null, UserFiltrator? filtrator = null)
        {
            var requestUrl = new StringBuilder("?");
            if (sorter != null)
                requestUrl.Append($"sortField={sorter.SortRule}&isDescendingSort={sorter.IsDescendingSort}");

            if (filtrator == null)
                return requestUrl.ToString();

            if (!String.IsNullOrEmpty(filtrator.FirstName))
            {
                requestUrl.Append($"&firstName={filtrator.FirstName}");
            }
            if (!String.IsNullOrEmpty(filtrator.LastName))
            {
                requestUrl.Append($"&lastName={filtrator.LastName}");
            }
            if (!String.IsNullOrEmpty(filtrator.Email))
            {
                requestUrl.Append($"&email={filtrator.Email}");
            }
            if (filtrator.PetFarmId != -1)
            {
                requestUrl.Append($"&petFarnId={filtrator.PetFarmId}");
            }
            return requestUrl.ToString();
        }
    }
}
