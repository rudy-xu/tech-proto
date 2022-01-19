using System.ComponentModel.DataAnnotations;

/*
    Cause to add password Salt and Hash, Change password filed to pwdHash and pwdSalt filed.
    For user, They only input userName and password. However, the database table has no password field.
    Need a new model to achieve user inputting.
*/

namespace webApi.Models
{
    public class UserRequest
    {
        [Required]
        public string UserName { set; get; }

        [Required]
        public string Password { set; get; }
    }
}
