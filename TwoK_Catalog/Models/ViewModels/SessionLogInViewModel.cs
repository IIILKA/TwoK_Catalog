using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;
using TwoK_Catalog.Infrastructure;

namespace TwoK_Catalog.Models.ViewModels
{
    public class SessionLogInViewModel : LogInViewModel
    {
        [JsonIgnore]
        public ISession Session { get; set; }
        public DateTime LastActivity { get; set; }
        public static LogInViewModel GetLogInViewModel(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            SessionLogInViewModel logInViewModel = session.GetJson<SessionLogInViewModel>("LogInViewModel") ?? new SessionLogInViewModel();
            logInViewModel.Session = session;
            return logInViewModel;
        }

        public void SaveFailedLogInViewModel()
        {
            Session.SetJson("LogInViewModel", this);
        }

        public void RemoveSucsessLogInViewModel()
        {
            Session.Remove("LogInViewModel");
        }
    }
}
