using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Colegios.Web.Utils
{
    public class PageList<T>
    {
        public const int Size = 5;


        public PageList(IEnumerable<T> items, int pageNo, int totalCount, int highLighted, int pageSize = Size)
        {
            if ((items == null) || (items.Count() == 0))
                Items = new List<T>();
            else
                Items = items;

            PageNo = pageNo;
            TotalCount = totalCount;
            PageSize = pageSize;
            _highLighted = highLighted;
        }

        public IEnumerable<T> Items { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        private int _highLighted;

        public int LastPage
        {
            get
            {
                if (TotalCount - _highLighted == 0)
                    return 1;

                return (((TotalCount - _highLighted)  % PageSize == 0) ? (int)((TotalCount - _highLighted) / PageSize) :  ((int)((TotalCount - _highLighted) / PageSize) + 1));
            }
        }
    }
}