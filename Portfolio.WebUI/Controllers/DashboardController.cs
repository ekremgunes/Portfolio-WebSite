using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Portfolio.Business.Interfaces;
using Portfolio.Common;
using Portfolio.Common.Enums;
using Portfolio.Dtos;
using Portfolio.Entities;

namespace Portfolio.WebUI.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private UserManager<AppUser> _userManager;

        private readonly ISettingsService _settingsService;
        private readonly ISkillService _skillService;
        private readonly IImageService _imageService;
        private readonly IServicesService _servicesService;
        private readonly IContentService _contentService;
        private readonly IContentDetailService _contentDetailService;

        public DashboardController(ISettingsService settingsService, ISkillService skillService, IContentService contentService, IServicesService servicesService, IContentDetailService contentDetailService, IImageService imageService, UserManager<AppUser> userManager)
        {
            _settingsService = settingsService;
            _skillService = skillService;
            _contentService = contentService;
            _servicesService = servicesService;
            _contentDetailService = contentDetailService;
            _imageService = imageService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _settingsService.GetSettingsAsync(1);
            ViewBag.visitorCount = response.Data.visitorCount;
            ViewBag.visitorInteraction = response.Data.visitorInteraction;
            ViewBag.contents = await _contentService.contentCount();
            ViewBag.details = await _contentDetailService.contentDetailCount();
            return View();

        }


        #region settings
        [Authorize]

        public async Task<IActionResult> SettingsUpdate()
        {
            var response = await _settingsService.GetSettingsUpdateAsync(1);
            if (response.ResponseType == ResponseType.NotFound)
            {
                return NotFound();
            }
            return View(response.Data);
        }
        [Authorize]

        public async Task<IActionResult> SettingsSeoUpdate()
        {
            var response = await _settingsService.GetSettingsUpdateAsync(1);
            ViewBag.seo = response.Data.seo;
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> SettingsSeoUpdate(string seo)
        {
            if (string.IsNullOrEmpty(seo) || seo.Length < 10)
                return Json("error");

            await _settingsService.updateSeo(seo);
            return Json("success");
        }
        [HttpPost]
        public async Task<IActionResult> SettingsUpdate(SettingsUpdateDto dto, IFormFile sliderImg, IFormFile aboutImg, IFormFile logoImg)
        {
            dto.id = 1; //we have one settings
            if (_settingsService.UpdateSettingsIsValid(dto))
            {
                if (sliderImg != null)
                {
                    var newPath = await FileHelper.ReplaceFile(dto.sliderImgPath, sliderImg);
                    dto.sliderImgPath = newPath;
                }
                if (aboutImg != null)
                {
                    var newPath = await FileHelper.ReplaceFile(dto.aboutImgPath, aboutImg);
                    dto.aboutImgPath = newPath;
                }
                if (logoImg != null)
                {
                    var newPath = await FileHelper.ReplaceFile(dto.logoImgPath, logoImg);
                    dto.logoImgPath = newPath;
                }
            }
            var response = await _settingsService.UpdateSettingsAsync(dto);
            if (response.ResponseType == ResponseType.Success)
            {
                TempData["alerts"] = this.ViewAlert(AlertType.Succes, "Update Succeed");
                return View(response.Data);
            }
            else
            {
                TempData["alerts"] = this.ViewAlert(AlertType.Warning, "Something went wrong");
                return this.ResponseValidation<SettingsUpdateDto>(response);
            }

        }
        #endregion

        #region messages

        [HttpGet]
        public async Task<IActionResult> Messages([FromServices] IMessageService _messageService)
        {
            var timedMessage = await _settingsService.TimeMessageIsActive(1);
            if (timedMessage == true)
            {
                var response = await _messageService.GetAllMessagesAsync(month: 3);
                return View(response.Data);
            }
            else
            {
                var response = await _messageService.GetAllMessagesAsync();
                return View(response.Data);
            }

        }
        [HttpPost]
        public async Task<JsonResult> ReadMessage(int id, bool read, [FromServices] IMessageService _messageService)
        {
            var response = await _messageService.isRead(id, read);
            if (response.ResponseType == ResponseType.Success)
            {
                return Json("success");
            }
            return Json("error");
        }
        [HttpPost]
        public async Task<JsonResult> DeleteMessage(int id, [FromServices] IMessageService _messageService)
        {
            var response = await _messageService.DeleteMessage(id);
            if (response.ResponseType == ResponseType.Success)
                return Json("success");

            return Json("error");
        }
        #endregion

        #region contents
        [Authorize]

        public async Task<IActionResult> Contents()
        {
            var response = await _contentService.GetAllAsync();
            return View(response.Data);
        }
        [HttpGet]
        public IActionResult CreateContent()
        {
            var dto = new ContentCreateDto();
            return View(dto);
        }
        [HttpPost]
        public async Task<IActionResult> CreateContent(ContentCreateDto dto, IFormFile Img)
        {
            if (_contentService.ContentIsValid(dto))
            {
                if (Img != null)
                {
                    var imgPath = await FileHelper.CreateFile(Img);
                    dto.imagePath = imgPath;
                }
                else
                {
                    TempData["alerts"] = this.ViewAlert(AlertType.Warning, "Please upload a image for Content");
                    return View(dto);
                }
            }
            var response = await _contentService.CreateAsync(dto);
            if (response.ResponseType == ResponseType.Success)
            {
                TempData["alerts"] = this.ViewAlert(AlertType.Succes, "Content Created");
                return RedirectToAction("Contents");
            }
            return this.ResponseValidation<ContentCreateDto>(response);
        }
        [HttpGet]
        public async Task<IActionResult> UpdateContent(int id)
        {
            var response = await _contentService.GetContentwithDetailsAsync(id);
            if (response.ResponseType == ResponseType.NotFound)
            {
                return NotFound();
            }
            ViewBag.Details = response.Data.Details;
            var dto = new ContentUpdateDto()
            {
                contentName = response.Data.contentName,
                InShowCase = response.Data.InShowCase,
                id = response.Data.id,
                imagePath = response.Data.imagePath,
                rank = response.Data.rank
            };
            return View(dto);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateContent(ContentUpdateDto dto, IFormFile Img)
        {
            if (_contentService.ContentUpdateIsValid(dto))
            {
                if (Img != null)
                {
                    var imgPath = await FileHelper.ReplaceFile(dto.imagePath, Img);
                    dto.imagePath = imgPath;
                }
            }
            var response = await _contentService.UpdateAsync(dto);
            if (response.ResponseType == ResponseType.Success)
            {
                TempData["alerts"] = this.ViewAlert(AlertType.Succes, "Content Updated");
                return RedirectToAction("Contents");
            }
            return this.ResponseValidation<ContentUpdateDto>(response);
        }
        [HttpPost]
        [Authorize]

        public async Task<JsonResult> DeleteContent(int id)
        {
            var response = await _contentService.RemoveContentAsync(id);
            if (response.ResponseType == ResponseType.Success)
            {
                return Json("success");
            }
            return Json("error");

        }
        #endregion

        #region contentDetails
        [Authorize]

        public IActionResult ContentDetails()
        {
            var response = _contentDetailService.GetAllDetailsWithContent();
            return View(response.Data);
        }
        [HttpGet]
        public async Task<IActionResult> CreateContentDetail()
        {
            var dto = new ContentDetailCreateDto();
            return View(dto);
        }
        [HttpPost]
        public async Task<IActionResult> CreateContentDetail(ContentDetailCreateDto dto, List<IFormFile> Images, IFormFile Img)
        {
            if (_contentDetailService.ContentDetailIsValid(dto))
            {
                if (Img != null)
                {
                    var imgPath = await FileHelper.CreateFile(Img);
                    dto.imagePath = imgPath;
                }
                else
                {
                    TempData["alerts"] = this.ViewAlert(AlertType.Warning, "Please upload a image for Content Detail");
                    return View(dto);
                }
            }
            var response = await _contentDetailService.CreateAsync(dto);
            if (response.ResponseType == ResponseType.Success)
            {
                int contentDetailId = _contentDetailService.LastContentDetailId();
                if (Images.Count > 0 && contentDetailId != 0)
                {
                    foreach (var img in Images)
                    {
                        var imgPath = await FileHelper.CreateFile(img);
                        await _imageService.CreateImage(imgPath, contentDetailId);

                    }
                }
                TempData["alerts"] = this.ViewAlert(AlertType.Succes, "Content Detail Created");
                return RedirectToAction("ContentDetails");
            }
            return this.ResponseValidation<ContentDetailCreateDto>(response);
        }
        [HttpGet]
        public async Task<IActionResult> UpdateContentDetail(int id)
        {
            var response = await _contentDetailService.GetDetailwithImagesAsync(id);
            if (response.ResponseType == ResponseType.NotFound)
            {
                return NotFound();
            }
            ViewBag.images = response.Data.images;
            var contentDetail = await _contentDetailService.getContentOfDetail(response.Data.id);
            ViewBag.contentName = contentDetail.content.contentName;
            var dto = new ContentDetailUpdateDto()
            {
                contentDetailName = response.Data.contentDetailName,
                contentId = response.Data.contentId,
                imagePath = response.Data.imagePath,
                rank = response.Data.rank,
                id = response.Data.id
            };
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateContentDetail(ContentDetailUpdateDto dto, List<IFormFile> Images, IFormFile Img)
        {
            if (_contentDetailService.ContentDetailUpdateIsValid(dto))
            {
                if (Img != null)
                {
                    var imgPath = await FileHelper.ReplaceFile(dto.imagePath, Img);
                    dto.imagePath = imgPath;
                }
            }
            var response = await _contentDetailService.UpdateAsync(dto);
            if (response.ResponseType == ResponseType.Success)
            {
                if (Images.Count > 0)
                {
                    foreach (var img in Images)
                    {
                        var imgPath = await FileHelper.CreateFile(img);
                        await _imageService.CreateImage(imgPath, dto.id);
                    }
                }
                TempData["alerts"] = this.ViewAlert(AlertType.Succes, "Content Detail Updated");
                return RedirectToAction("ContentDetails");
            }
            return this.ResponseValidation<ContentDetailUpdateDto>(response);
        }

        [HttpPost]
        [Authorize]

        public async Task<JsonResult> DeleteContentDetail(int id)
        {
            var response = await _contentDetailService.RemoveContentDetailAsync(id);
            if (response.ResponseType == ResponseType.NotFound)
            {
                return Json("error");
            }
            return Json("success");
        }
        #endregion

        #region skills
        [Authorize]

        public async Task<IActionResult> Skills()
        {
            var response = await _skillService.GetAllAsync();
            return View(response.Data);
        }
        [HttpGet]
        public async Task<IActionResult> UpdateSkill(int id)
        {
            var response = await _skillService.GetByIdAsync<SkillUpdateDto>(id);
            if (response.ResponseType == ResponseType.NotFound)
            {
                return NotFound();
            }
            return View(response.Data);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateSkill(SkillUpdateDto dto)
        {
            var response = await _skillService.UpdateAsync(dto);
            if (response.ResponseType == ResponseType.Success)
            {
                TempData["alerts"] = this.ViewAlert(AlertType.Succes, "Skill Updated");
                return RedirectToAction("Skills");
            }
            return this.ResponseValidation<SkillUpdateDto>(response);
        }
        [HttpGet]
        public IActionResult CreateSkill()
        {
            var dto = new SkillCreateDto();
            return View(dto);
        }
        [HttpPost]
        public async Task<IActionResult> CreateSkill(SkillCreateDto dto)
        {
            var response = await _skillService.CreateAsync(dto);
            if (response.ResponseType == ResponseType.Success)
            {
                TempData["alerts"] = this.ViewAlert(AlertType.Succes, "Skill Created");
                return RedirectToAction("Skills");
            }
            return this.ResponseValidation<SkillCreateDto>(response);
        }
        [HttpPost]
        [Authorize]

        public async Task<JsonResult> DeleteSkill(int id)
        {
            var response = await _skillService.RemoveAsync(id);
            if (response.ResponseType == ResponseType.Success)
                return Json("success");
            return Json("error");
        }
        #endregion

        #region Services
        [Authorize]

        public async Task<IActionResult> Services()
        {
            var response = await _servicesService.GetAllAsync();
            return View(response.Data);
        }
        [HttpGet]
        public IActionResult CreateService()
        {
            var dto = new ServiceCreateDto();
            return View(dto);
        }
        [HttpPost]
        public async Task<IActionResult> CreateService(ServiceCreateDto dto)
        {

            if (dto.iconPath == null)
            {
                TempData["alerts"] = this.ViewAlert(AlertType.Error, "Creâte Error");
                this.ModelState.AddModelError<ServiceUpdateDto>(x => x.iconPath, "Icon is required");
                return View(dto);
            }
            var response = await _servicesService.CreateAsync(dto);
            if (response.ResponseType == ResponseType.Success)
            {
                TempData["alerts"] = this.ViewAlert(AlertType.Succes, "Service Created");
                return RedirectToAction("Services");
            }
            return this.ResponseValidation<ServiceCreateDto>(response);
        }
        [HttpGet]
        public async Task<IActionResult> UpdateService(int id)
        {
            var response = await _servicesService.GetByIdAsync<ServiceUpdateDto>(id);
            if (response.ResponseType == ResponseType.NotFound)
            {
                return NotFound();
            }
            return View(response.Data);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateService(ServiceUpdateDto dto)
        {
            if (dto.iconPath == null)
            {
                TempData["alerts"] = this.ViewAlert(AlertType.Error, "Update Error");
                this.ModelState.AddModelError<ServiceUpdateDto>(x => x.iconPath, "Icon is required");
                return View(dto);
            }
            var response = await _servicesService.UpdateAsync(dto);
            if (response.ResponseType == ResponseType.Success)
            {
                TempData["alerts"] = this.ViewAlert(AlertType.Succes, "Update Succeed");
                return RedirectToAction("Services");
            }
            return this.ResponseValidation<ServiceUpdateDto>(response);
        }
        [HttpPost]
        [Authorize]

        public async Task<JsonResult> DeleteService(int id)
        {
            var response = await _servicesService.RemoveAsync(id);
            if (response.ResponseType == ResponseType.Success)
                return Json("success");
            return Json("error");
        }
        #endregion

        #region Image 
        [HttpPost]
        [Authorize]

        public async Task<JsonResult> DeleteImage(int id)
        {
            var response = await _imageService.DeleteImageAsync(id);
            if (response.ResponseType == ResponseType.NotFound)
            {
                return Json("error");
            }
            return Json("success");
        }
        #endregion

        #region userSettings
        [Authorize]
        public async Task<IActionResult> UserSettings()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public async Task<JsonResult> UpdateName(string oldName, string newName)
        {
            if (newName.Length > 3 && oldName.Length > 3)
            {
                var user = await _userManager.FindByNameAsync(oldName);

                if (user == null)
                {
                    return Json("error");
                }
                await _userManager.SetUserNameAsync(user, newName);
                TempData["alerts"] = this.ViewAlert(AlertType.Succes, "Name changed ," + newName);
                return Json("success");
            }
            return Json("error");
        }
        #endregion    
    }
}
