using System.Collections.Generic;

namespace SharpBlogX.Response
{
    public interface IListResult<T>
    {
        IReadOnlyList<T> Item { get; set; }
    }
}