using System.Diagnostics.CodeAnalysis;

namespace starter_serv.Model
{

    [ExcludeFromCodeCoverage]
    public class ListPagedResults<T>
    {
        public int Count { set; get; }
        public int CountTotal { set; get; }
        public List<T> Data { set; get; }
        public List<T> Error { set; get; }
    }
}
