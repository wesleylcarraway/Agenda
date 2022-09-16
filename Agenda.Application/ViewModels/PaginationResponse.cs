namespace Agenda.Application.ViewModels
{
    public class PaginationResponse<T>
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public int Total { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}
