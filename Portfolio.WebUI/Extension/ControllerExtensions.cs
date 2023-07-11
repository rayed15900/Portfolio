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
    }
}
