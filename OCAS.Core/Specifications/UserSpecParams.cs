using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCAS.Core.Specifications
{
    public class UserSpecParams
    {
        private const int MaxPageSize = 50;

        public int PageIndex { get; set; } = 1;

        private int _pageSize = 10;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }


        public int? ActivityId { get; set; }

        public string SortName { get; set; }


        private string _searchWords;
        public string SearchWords
        {
            get => _searchWords;
            set => _searchWords = value.ToLower();
        }

    }
}
