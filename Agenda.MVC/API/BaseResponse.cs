using Agenda.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Agenda.MVC.API
{
    public class BaseResponse<T>
    {
        public T Result { get; set; }
        public IEnumerable<ErrorViewModel> Errors { get; set; } = new List<ErrorViewModel>();
        public bool HasError { get => Errors.Any(); }
        public void AddErrorsToModelState(ModelStateDictionary modelState)
        {
            foreach (var error in Errors)
                modelState.AddModelError(string.Empty, error.ErrorMessage);
        }
    }
}
