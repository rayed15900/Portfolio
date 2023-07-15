using Microsoft.AspNetCore.Mvc;
using Portfolio.Utility;

namespace Portfolio.WebUI.Extension
{
    public static class ControllerExtensions
    {
        public static IActionResult ResponseValidation<T>(this Controller controller, IResponse<T> response)
        {
            if (response.ResponseType == ResponseType.NotFound)
                return controller.NotFound();
            if (response.ResponseType == ResponseType.ValidationError)
            {
                foreach (var error in response.ValidationErrors)
                {
                    controller.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

            }
            return controller.View(response.Data);
        }

        //public static string ViewAlert(this Controller controller, AlertType alert = AlertType.Info, string alertMessage = "Info")
        //{
        //    if (alert == AlertType.Success)
        //    {
        //        return $"<span class='text-danger'>{alertMessage}</span>";
        //    }
        //    else if (alert == AlertType.Warning)
        //    {
        //        return $"<span class='text-danger'>{alertMessage}</span>";
        //    }
        //    else if (alert == AlertType.Error)
        //    {
        //        return $"<span class='text-danger'>{alertMessage}</span>";
        //    }
        //    else
        //    {
        //        return $"<span class='text-danger'>{alertMessage}</span>";
        //    }
        //}
    }
}
