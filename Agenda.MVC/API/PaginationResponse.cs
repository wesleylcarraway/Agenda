namespace Agenda.MVC.API
{
    public class PaginationResponse<T>
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public int Total { get; set; }
        public IEnumerable<T> Data { get; set; }

        public PaginationResponse(int take, int skip, int total, IEnumerable<T> data)
        {
            Skip = skip;
            Take = take;
            Total = total;
            Data = data;
        }
    }
}
