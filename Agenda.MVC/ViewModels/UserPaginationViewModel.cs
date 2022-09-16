using Agenda.MVC.API;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Agenda.MVC.ViewModels
{
    public class UserPaginationViewModel
    {
        public SearchViewModel Search { get; set; } = new SearchViewModel();
        public PaginationResponse<UserViewModel> Users { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int Take { get; set; } = 6;

        public IEnumerable<SelectListItem> GetSearchProps()
        {
            return new List<SelectListItem>()
            {
                new SelectListItem("Name", "Name"),
                new SelectListItem("Username", "Username"),
                new SelectListItem("Email", "Email")
            };
        }
    }
}
