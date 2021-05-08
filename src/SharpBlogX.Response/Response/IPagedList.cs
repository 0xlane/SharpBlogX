namespace SharpBlogX.Response
{
    public interface IPagedList<T> : IListResult<T>, IHasTotalCount
    {
    }
}