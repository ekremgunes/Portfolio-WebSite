using Microsoft.AspNetCore.Mvc;
using Portfolio.Business.Interfaces;
using Portfolio.Dtos;
using Portfolio.Common;
using Portfolio.WebUI.ViewModel;

namespace Portfolio.WebUI.Controllers
{
    public class HomeController : Controller
	{
		private readonly IMessageService _messageService;
		private readonly ISettingsService _settingsService;
		private readonly ISkillService _skillService;
		private readonly IImageService _imageService;
		private readonly IServicesService _servicesService;
		private readonly IContentService _contentService;
		private readonly IContentDetailService _contentDetailService;
		public HomeController(IMessageService messageService, IContentDetailService contentDetailService, IContentService contentService, IServicesService servicesService, IImageService imageService, ISkillService skillService, ISettingsService settingsService)
		{
			_messageService = messageService;
			_contentDetailService = contentDetailService;
			_contentService = contentService;
			_servicesService = servicesService;
			_imageService = imageService;
			_skillService = skillService;
			_settingsService = settingsService;
		}

		public async Task<IActionResult> Index()
		{
			var model = new HomeViewModel();
			model.MessageDto = new MessageCreateDto();
			var response = await _servicesService.GetAllbyRankAsync();
			model.Services = response.Data;
			var responseSkill = await _skillService.GetAllSkillbyRankAsync();
			model.Skills = responseSkill.Data;
			var responseSettings = await _settingsService.GetSettingsAsync(1);
			model.SettingsDto = responseSettings.Data;
			var responseContent = await _contentService.GetShowcaseContents();
			model.Contents = responseContent.Data;
			return View(model);
		}
        public async Task<IActionResult> ContentDetail(int id)
        {
            var response = await _contentService.GetContentwithDetailsAsync(id);
            if (response.ResponseType == ResponseType.NotFound)
            {
                return NotFound();
            }
            var settingsResponse = await _settingsService.GetSettingsAsync(1);

            var messageDto = new MessageCreateDto();

            var footerModel = new FooterViewModel()
            {
                MessageDto = messageDto,
                SettingsDto = settingsResponse.Data
            };
            var model = new ContentDetailViewModel()
            {
                dto = response.Data,
                footerViewModel = footerModel
            };
            return View(model);
        }
        public async Task<IActionResult> Contents()
        {
			var data = await _contentService.GetContentsbyRankAsync();
            var settingsResponse = await _settingsService.GetSettingsAsync(1);

            var messageDto = new MessageCreateDto();

            var footerModel = new FooterViewModel()
            {
                MessageDto = messageDto,
                SettingsDto = settingsResponse.Data
            };
            var model = new ContentsViewModel()
            {
                dtos = data,
                footerViewModel = footerModel
            };
            return View(model);
        }

        [HttpPost]
		public async Task<JsonResult> SendMessage(string senderName,string senderMail,string Content)
		{

			if (senderName != "" && senderMail != "" && Content != "")
			{
				var dto = new MessageCreateDto()
				{
					messageContent = Content,
					senderMail = senderMail,
					senderName = senderName,
					isRead = false
				};
                var response = await _messageService.SendMessage(dto);
				if (response.ResponseType == ResponseType.Success)
				{
					return Json("success");
				}
            }
            return Json("error");
		}
		[HttpPost]
		public async Task<JsonResult> addVisitor()
		{
			await _settingsService.addVisitor();
			return Json("");
		}

		[HttpPost]
		public async Task<JsonResult> addVisitorInteraction()
		{
			await _settingsService.addVisitorInteraction();
			return Json("");
		}
	}
}