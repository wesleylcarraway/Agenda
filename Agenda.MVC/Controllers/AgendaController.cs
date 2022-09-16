using Agenda.MVC.API;
using Agenda.MVC.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Agenda.MVC.Controllers
{
    [Authorize]
    public class AgendaController : Controller
    {
        private readonly HttpService _httpService;
        private readonly IMapper _mapper;

        public AgendaController(HttpService httpService, IMapper mapper)
        {
            _httpService = httpService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> Index(AgendaViewModel vm, int page = 1)
        {
            var skip = vm.Take * (page - 1);
            var query = new BaseParams { Prop = vm.Search.Prop, Value = vm.Search.Value, Skip = skip, Take = vm.Take };

            var result = await _httpService.GetContactsAsync(query);

            vm.Contacts = result;
            vm.TotalPages = (result.Total / query.Take) + 1;
            vm.CurrentPage = page;

            return View(vm);
        }

        [HttpGet]
        public async Task<ActionResult> Form(int? id = null)
        {
            await SetPhoneTypesSelectList();

            if (!id.HasValue)
                return View(new AdminContactFormViewModel());

            var contact = await _httpService.GetContactByIdAsync((int)id);
            if (contact == null)
                return NotFound();

            var model = _mapper.Map<ContactFormViewModel>(contact);

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Form(
            ContactFormViewModel form,
            string option = "Save"
            )
        {
            if (option.Contains("AddPhone"))
            {
                ModelState.Clear();
                form.Phones.Add(new PhoneFormViewModel());
            }

            if (option.Contains("RemovePhone"))
            {
                var split = option.Split("|");
                ModelState.Clear();
                form.Phones.Remove(form.Phones[int.Parse(split[1])]);
            }

            if (option.Contains("Save"))
            {
                var result = form.Id.HasValue ?
                    await _httpService.UpdateContactAsync(form) :
                    await _httpService.AddContactAsync(form);

                if (result.HasError)
                {
                    result.AddErrorsToModelState(ModelState);
                }
                else
                {
                    TempData.Add("toast", "Contact created with success!");
                    return RedirectToAction(nameof(Index));
                }
            }
            await SetPhoneTypesSelectList();
            return View(form);
        }

        [HttpGet]
        public async Task<ActionResult> Remove(int id)
        {
            await _httpService.RemoveContactAsync(id);
            TempData.Add("toast", "Contact removed with success!");
            return RedirectToAction(nameof(Index));
        }

        private async Task SetPhoneTypesSelectList()
        {
            var types = await _httpService.GetPhoneTypesAsync();
            var list = new SelectList(types, "Id", "Name");
            ViewBag.PhoneTypes = list;
        }
    }
}
