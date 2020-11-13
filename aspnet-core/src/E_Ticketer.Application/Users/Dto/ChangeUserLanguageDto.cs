using System.ComponentModel.DataAnnotations;

namespace E_Ticketer.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}