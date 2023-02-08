using Microsoft.AspNetCore.Mvc;
using Portfolio.Business.Interfaces;

namespace Portfolio.WebUI.ViewComponents
{
    public class LastMessages : ViewComponent
    {
        private readonly IMessageService _messageService;

        public LastMessages(IMessageService messageService)
        {
            _messageService = messageService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _messageService.GetLastMessagesAsync();//default 4 message you can change it
            return View(result.Data);
        }
    }
}
