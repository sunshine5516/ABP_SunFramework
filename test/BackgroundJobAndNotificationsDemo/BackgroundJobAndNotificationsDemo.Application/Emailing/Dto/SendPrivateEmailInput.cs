using BackgroundJobAndNotificationsDemo.Core.Users;
using System.ComponentModel.DataAnnotations;
namespace BackgroundJobAndNotificationsDemo.Application.Emailing.Dto
{
    public class SendPrivateEmailInput
    {
        [Required]
        [MaxLength(User.MaxUserNameLength)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(128)]
        public string Subject { get; set; }
        [Required]
        [MaxLength(4000)]
        public string Body { get; set; }
        public bool SendNotification { get; set; }
    }
}
