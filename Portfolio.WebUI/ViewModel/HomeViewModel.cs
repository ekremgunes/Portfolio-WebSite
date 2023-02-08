using Portfolio.Dtos;

namespace Portfolio.WebUI.ViewModel
{
    public class HomeViewModel
    {
        public SettingsListDto SettingsDto { get; set; }
        public MessageCreateDto? MessageDto { get; set; }
        public List<SkillListDto>? Skills { get; set; } 
        public List<ServiceListDto>? Services { get; set; } 
        public List<ContentListDto>? Contents { get; set; } 
    }
}