using Agenda.MVC.API;
using Agenda.MVC.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Agenda.MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly HttpService _httpService;
        private readonly IMapper _mapper;

        public UsersController(HttpService httpService, IMapper mapper)
        {
            _httpService = httpService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> Index(UserPaginationViewModel vm, int page = 1)
        {
            var skip = vm.Take * (page - 1);
            var query = new BaseParams { Prop = vm.Search.Prop, Value = vm.Search.Value, Skip = skip, Take = vm.Take };

            var result = await _httpService.GetUsersAsync(query);

            vm.Users = result;
            vm.TotalPages = (result.Total / query.Take) + 1;
            vm.CurrentPage = page;

            return View(vm);
        }

        [HttpGet]
        public async Task<ActionResult> Form(int? id = null)
        {
            await SetUserRolesSelectList();

            if (!id.HasValue)
                return View(new UserFormViewModel());

            var user = await _httpService.GetUserByIdAsync((int)id);
            if (user == null)
                return NotFound();

            var model = _mapper.Map<UserFormViewModel>(user);

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Form(
            UserFormViewModel form,
            string option = "Save"
            )
        {
            if (option.Contains("Save"))
            {
                var result = form.Id.HasValue ?
                    await _httpService.UpdateUserAsync(form) :
                    await _httpService.AddUserAsync(form);

                if (result.HasError)
                {
                    result.AddErrorsToModelState(ModelState);
                }
                else
                {
                    TempData.Add("toast", "User created with success!");
                    return RedirectToAction(nameof(Index));
                }
            }
            await SetUserRolesSelectList();
            return View(form);
        }

        [HttpGet]
        public async Task<ActionResult> Remove(int id)
        {
            await _httpService.RemoveUserAsync(id);
            TempData.Add("toast", "User removed with success!");
            return RedirectToAction(nameof(Index));
        }

        private async Task SetUserRolesSelectList()
        {
            var roles = await _httpService.GetUserRolesAsync();
            var list = new SelectList(roles, "Id", "Name");
            ViewBag.roles = list;
        }
    }
}
