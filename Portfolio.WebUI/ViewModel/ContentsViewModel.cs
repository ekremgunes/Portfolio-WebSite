using Portfolio.Dtos;

namespace Portfolio.WebUI.ViewModel
{
    public class ContentsViewModel
    {
        public FooterViewModel footerViewModel { get; set; }
        public List<ContentListDto> dtos { get; set; }
    }
}
