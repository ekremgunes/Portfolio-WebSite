using Portfolio.Dtos.Interfaces;

namespace Portfolio.Dtos
{
    public class SettingsUpdateDto : IUpdateDto
    {
        public int id { get; set; }
        public string logoImgPath { get; set; }

        public string sliderImgPath { get; set; }
        public string sliderHeader { get; set; }
        public string sliderServices { get; set; }
        public string aboutContent { get; set; }
        public string aboutHeader { get; set; }
        public string aboutImgPath { get; set; }
        public string slogan { get; set; }
        public string email { get; set; }

        public bool timedMessage { get; set; }
        public string instagramUrl { get; set; }
        public bool instagramVisibility { get; set; }
        public string twitterUrl { get; set; }
        public bool twitterVisibility { get; set; }
        public string facebookUrl { get; set; }
        public bool facebookVisibility { get; set; }
        public string seo { get; set; }
        public int visitorCount { get; set; }
        public int visitorInteraction { get; set; }

    }
}
