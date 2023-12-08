using Microsoft.AspNetCore.Http.Features;
using MyEFCourse.Entities;

namespace MyEFCourse.DataTransferObject
{
    public class PageResult<T>
    {
        public List<T> Items { get; set; }
        public int TotalPages { get; set; }
        public int ItemsFrom { get; set; }
        public int ItemsTo { get; set; }
        public int TotalMatchingItems { get; set; }

        public PageResult(List<T> items, int totalCount, int pageSize, int pageNumber)
        {
            Items = items;
            TotalMatchingItems = totalCount;
            ItemsFrom = pageSize * (pageNumber - 1) + 1;
            ItemsTo = ItemsFrom + pageSize - 1;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        }
    }
}
