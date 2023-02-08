using Microsoft.AspNetCore.Mvc;
using Portfolio.Common;
using Portfolio.Common.Enums;

namespace Portfolio.WebUI.Controllers
{
    public static class ControllerExtensions
    {
        public static IActionResult ResponseValidation<T>(this Controller controller, IResponse<T> response)
        {
            if (response.ResponseType == ResponseType.NotFound)
                return controller.NotFound();
            if(response.ResponseType == ResponseType.ValidationError)
            { 
                foreach (var error in response.ValidationErrors)
                {
                    controller.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                
            }
            return controller.View(response.Data);
        }
        public static string ViewAlert(this Controller controller,AlertType alert=AlertType.Info,string alertMessage="Info")
        {
            if (alert == AlertType.Succes)
            {
                return $"<span class='successAlert'>{alertMessage}</span>";
            }
            else if(alert == AlertType.Warning)
            {
                return $"<span class='warningAlert'>{alertMessage}</span>";
            }
            else if(alert == AlertType.Error)
            {
                return $"<span class='errorAlert'>{alertMessage}</span>";
            }
            else 
            {
                return $"<span class='infoAlert'>{alertMessage}</span>";
            }
        }

    }
}
