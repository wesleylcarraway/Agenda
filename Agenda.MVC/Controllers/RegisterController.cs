using Agenda.MVC.API;
using Agenda.MVC.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Agenda.MVC.Controllers
{
    public class RegisterController : Controller
    {
        private readonly HttpService _httpService;
        private readonly IMapper _mapper;

        public RegisterController(HttpService httpService, IMapper mapper)
        {
            _httpService = httpService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(
            UserFormViewModel form,
            string option = "Save"
            )
        {
            if (option.Contains("Save"))
            {
                var result = await _httpService.AddCommonUserAsync(form);

                if (result.HasError)
                {
                    result.AddErrorsToModelState(ModelState);
                }
                else
                {
                    TempData.Add("toast", "User created with success!");
                    return Redirect(Url.Action("Index", "Home"));
                }
            }
            return View(form);
        }
    }
}
