using AutoMapper;
using Portfolio.Business.Mappings.AutoMapper;

namespace Portfolio.Business.Business.Helpers
{
    public static class ProfileHelper
    {
        public static List<Profile> GetProfiles()
        {

            return new List<Profile>
            {
                new MessageProfile(),
                new ContentDetailProfile(),
                new ContentProfile(),
                new ImageProfile(),
                new ServiceProfile(),
                new SkillProfile(),
                new SettingsProfile()
            };
        }
    }
}
