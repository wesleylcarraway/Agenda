using Agenda.MVC.ViewModels;
using Flurl.Http;

namespace Agenda.MVC.API
{
    public class HttpService
    {
        private readonly string _apiUrl;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpService(IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _apiUrl = config.GetValue<string>("ApiUrl");
            _httpContextAccessor = httpContextAccessor;
        }

        #region LOGIN
        public async Task<string> LoginAsync(LoginViewModel loginViewModel)
        {
            var response = await _apiUrl
                .AllowAnyHttpStatus()
                .AppendPathSegment("auth")
                .PostJsonAsync(loginViewModel);

            if (response.StatusCode == 400)
                return null;

            var result = await response.GetJsonAsync();
            var token = result.token as string;

            return token;
        }
        #endregion

        #region Agenda
        public Task<PaginationResponse<ContactViewModel>> GetContactsAsync(BaseParams queryParams)
        {
            var req = GetAuthApiUrl()
                .AppendPathSegment("agenda")
                .SetQueryParam("Take", queryParams.Take)
                .SetQueryParam("Skip", queryParams.Skip);

            if (!string.IsNullOrEmpty(queryParams.Prop) && !string.IsNullOrEmpty(queryParams.Value))
                req.SetQueryParam(queryParams.Prop, queryParams.Value);

            return req.GetJsonAsync<PaginationResponse<ContactViewModel>>();
        }

        public Task<ContactViewModel> GetContactByIdAsync(int id)
        {
            return GetAuthApiUrl()
                .AppendPathSegment("agenda")
                .AppendPathSegment(id)
                .GetJsonAsync<ContactViewModel>();

        }

        public Task<BaseResponse<ContactViewModel>> AddContactAsync(ContactFormViewModel contact)
        {
            return GetAuthApiUrl()
                .AppendPathSegment("agenda")
                .AllowHttpStatus("400")
                .PostJsonAsync(contact)
                .ReceiveResponse<ContactViewModel>();
        }

        public Task<BaseResponse<ContactViewModel>> UpdateContactAsync(ContactFormViewModel contact)
        {
            return GetAuthApiUrl()
                .AppendPathSegment("agenda")
                .AppendPathSegment(contact.Id)
                .AllowHttpStatus("400")
                .PutJsonAsync(contact)
                .ReceiveResponse<ContactViewModel>();
        }

        public Task RemoveContactAsync(int id)
        {
            return GetAuthApiUrl()
                .AppendPathSegment("agenda")
                .AppendPathSegment(id)
                .DeleteAsync();
        }
        public Task<IEnumerable<EnumerationViewModel>> GetPhoneTypesAsync()
        {
            return GetAuthApiUrl()
                .AppendPathSegment("agenda/phone-types")
                .GetJsonAsync<IEnumerable<EnumerationViewModel>>();
        }
        #endregion

        #region AGENDA_ADMIN
        public Task<PaginationResponse<AdminContactViewModel>> GetAdminContactsAsync(BaseParams queryParams)
        {
            var req = GetAuthApiUrl()
                .AppendPathSegment("admin/agenda")
                .SetQueryParam("Take", queryParams.Take)
                .SetQueryParam("Skip", queryParams.Skip);

            if (!string.IsNullOrEmpty(queryParams.Prop) && !string.IsNullOrEmpty(queryParams.Value))
                req.SetQueryParam(queryParams.Prop, queryParams.Value);

            return req.GetJsonAsync<PaginationResponse<AdminContactViewModel>>();
        }

        public Task<AdminContactViewModel> GetAdminContactByIdAsync(int id)
        {
            return GetAuthApiUrl()
                .AppendPathSegment("admin/agenda")
                .AppendPathSegment(id)
                .GetJsonAsync<AdminContactViewModel>();
        }

        public Task<BaseResponse<AdminContactViewModel>> AddAdminContactAsync(AdminContactFormViewModel contact)
        {
            return GetAuthApiUrl()
                .AppendPathSegment("admin/agenda")
                .AllowHttpStatus("400")
                .PostJsonAsync(contact)
                .ReceiveResponse<AdminContactViewModel>();
        }

        public Task<BaseResponse<AdminContactViewModel>> UpdateAdminContactAsync(AdminContactFormViewModel contact)
        {
            return GetAuthApiUrl()
                .AppendPathSegment("admin/agenda")
                .AppendPathSegment(contact.Id)
                .AllowHttpStatus("400")
                .PutJsonAsync(contact)
                .ReceiveResponse<AdminContactViewModel>();
        }

        public Task RemoveAdminContactAsync(int id)
        {
            return GetAuthApiUrl()
                .AppendPathSegment("admin/agenda")
                .AppendPathSegment(id)
                .DeleteAsync();
        }
        #endregion

        #region USER
        public Task<PaginationResponse<UserViewModel>> GetUsersAsync(BaseParams queryParams)
        {
            var req = GetAuthApiUrl()
                .AppendPathSegment("admin/users")
                .SetQueryParam("Take", queryParams.Take)
                .SetQueryParam("Skip", queryParams.Skip);

            if (!string.IsNullOrEmpty(queryParams.Prop) && !string.IsNullOrEmpty(queryParams.Value))
                req.SetQueryParam(queryParams.Prop, queryParams.Value);

            return req.GetJsonAsync<PaginationResponse<UserViewModel>>();
        }

        public Task<UserViewModel> GetUserByIdAsync(int id)
        {
            return GetAuthApiUrl()
                .AppendPathSegment("admin/users")
                .AppendPathSegment(id)
                .GetJsonAsync<UserViewModel>();
        }
        public Task<IEnumerable<UserViewModel>> GetAllUsersAsync()
        {
            return GetAuthApiUrl()
                .AppendPathSegment("admin/users/all")
                .GetJsonAsync<IEnumerable<UserViewModel>>();
        }

        public Task<BaseResponse<UserViewModel>> AddUserAsync(UserFormViewModel user)
        {
            return GetAuthApiUrl()
                .AppendPathSegment("admin/users")
                .AllowHttpStatus("400")
                .PostJsonAsync(user)
                .ReceiveResponse<UserViewModel>();
        }

        public Task<BaseResponse<UserViewModel>> AddCommonUserAsync(UserFormViewModel user)
        {
            return _apiUrl.AllowHttpStatus("400")
            .AppendPathSegment("common-users")
            .PostJsonAsync(user)
            .ReceiveResponse<UserViewModel>();
        }

        public Task<BaseResponse<UserViewModel>> UpdateUserAsync(UserFormViewModel user)
        {
            return GetAuthApiUrl()
                .AppendPathSegment("admin/users")
                .AppendPathSegment(user.Id)
                .AllowHttpStatus("400")
                .PutJsonAsync(user)
                .ReceiveResponse<UserViewModel>();
        }

        public Task RemoveUserAsync(int id)
        {
            return GetAuthApiUrl()
                .AppendPathSegment("admin/users")
                .AppendPathSegment(id)
                .DeleteAsync();
        }

        public Task<IEnumerable<EnumerationViewModel>> GetUserRolesAsync()
        {
            return GetAuthApiUrl()
                .AppendPathSegment("admin/users/user-roles")
                .GetJsonAsync<IEnumerable<EnumerationViewModel>>();
        }
        #endregion

        private IFlurlRequest GetAuthApiUrl()
        {
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var token = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "jwt-token")?.Value;

                if (token == null)
                    throw new InvalidOperationException("Add Claim jwt-token token value.");

                return _apiUrl.WithOAuthBearerToken(token);
            }

            throw new InvalidOperationException("Unauthenticated user to set token in the request.");
        }
    }

    public static class FlurlExtensions
    {
        public static async Task<BaseResponse<T>> ReceiveResponse<T>(this Task<IFlurlResponse> task)
        {
            var response = await task.ConfigureAwait(false);

            if (response.StatusCode == 400)
            {
                var result = await response.GetJsonAsync<ErrorResponse>().ConfigureAwait(false);

                return new BaseResponse<T>
                {
                    Errors = result.Errors,
                    Result = default(T)
                };
            }

            return new BaseResponse<T>
            {
                Result = await response.GetJsonAsync<T>().ConfigureAwait(false)
            };
        }
    }
}
