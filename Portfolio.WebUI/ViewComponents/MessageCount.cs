using Microsoft.AspNetCore.Mvc;
using Portfolio.Business.Interfaces;

namespace Portfolio.WebUI.ViewComponents
{
    public class MessageCount :  ViewComponent
    {
        private readonly IMessageService _messageService;

        public MessageCount(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _messageService.GetMessageCountAsync();
            return View(result);
        }

    }
}
