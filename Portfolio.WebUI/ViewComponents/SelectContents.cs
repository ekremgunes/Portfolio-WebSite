using Microsoft.AspNetCore.Mvc;
using Portfolio.Business.Interfaces;

namespace Portfolio.WebUI.ViewComponents
{
    public class SelectContents : ViewComponent
    {
        private readonly IContentService _contentService;

        public SelectContents(IContentService contentService)
        {
            _contentService = contentService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _contentService.GetAllAsync();
            return View(result.Data);
        }
    }
}
