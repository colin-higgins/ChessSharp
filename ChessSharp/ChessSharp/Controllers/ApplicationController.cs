using System.Collections.Generic;
using System.Web.Mvc;

namespace ChessSharp.Controllers
{
    public class UserAlert
    {
        public string Message { get; set; }
        public bool? IsError { get; set; }
    }

    public class ApplicationController : Controller
    {
        private const string AlertsKey = "_UserAlerts";

        protected bool ErrorsExist { get; private set; }

        protected void AddUserError(string message)
        {
            AddUserAlert(new UserAlert() { IsError = true, Message = message });
            ErrorsExist = true;
        }

        protected void AddUserInfo(string message)
        {
            AddUserAlert(new UserAlert() { Message = message });
        }

        protected void AddUserSuccess(string message)
        {
            AddUserAlert(new UserAlert() { IsError = false, Message = message });
        }

        private void AddUserAlert(UserAlert message)
        {
            var temp = UserAlerts ?? new List<UserAlert>();

            temp.Add(message);

            UserAlerts = temp;
        }

        public List<UserAlert> UserAlerts
        {
            get
            {
                return (List<UserAlert>)TempData[AlertsKey];
            }
            set
            {
                TempData[AlertsKey] = value;
            }
        }

    }
}
