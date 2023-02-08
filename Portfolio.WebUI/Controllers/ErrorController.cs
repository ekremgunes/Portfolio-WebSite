using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace Portfolio.WebUI.Controllers
{
    public class ErrorController : Controller
    {
        [Route("/Error/Error")]
        public async Task<IActionResult> Error(int code)
        {
            ViewBag.code = code;
            if (code == 404)
            {
                return View();
            }
            var errorInfo = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            var strPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\ErrorLogs\\Error_Log.text");
            if (!System.IO.File.Exists(strPath))
            {
                System.IO.File.Create(strPath).Dispose();
            }
            using (StreamWriter sw = System.IO.File.AppendText(strPath))
            {
                sw.WriteLine("============= Error Logging ===========");
                sw.WriteLine("=========== Start ============= ");
                sw.WriteLine("Error Message: " + errorInfo?.Error.Message);
                sw.WriteLine("Error Source: " + errorInfo?.Error.Source);
                sw.WriteLine("Error Code: " + code.ToString());
                sw.WriteLine("Stack Trace: " + errorInfo?.Path);
                sw.WriteLine("Date : " + DateTime.Now.ToString());
                sw.WriteLine("=========== End =============");

            }
            return View();
        }

    }
}
