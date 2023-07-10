using AutoMapper;
using Portfolio.BusinessLogic.Mappings.AutoMapper;

namespace Portfolio.BusinessLogic.Helpers
{
    public static class ProfileHelper
    {
        public static List<Profile> GetProfiles()
        {

            return new List<Profile>
            {
                new SkillProfile()
            };
        }
    }
}
