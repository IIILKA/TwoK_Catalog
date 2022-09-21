using System.Text.Json.Serialization;
using TwoK_Catalog.Infrastructure;

namespace TwoK_Catalog.Models.ViewModels
{
    public class SessionRegisterViewModel : RegisterViewModel
    {
        [JsonIgnore]
        public ISession Session { get; set; }
        public DateTime LastActivity { get; set; }
        public static RegisterViewModel GetRegisterViewModel(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            SessionRegisterViewModel registerViewModel = session.GetJson<SessionRegisterViewModel>("RegisterViewModel") ?? new SessionRegisterViewModel();
            registerViewModel.Session = session;
            return registerViewModel;
        }

        public void SaveFailedRegisterViewModel()
        {
            Session.SetJson("RegisterViewModel", this);
        }

        public void RemoveSucsessRegisterViewModel()
        {
            Session.Remove("RegisterViewModel");
        }
    }
}
