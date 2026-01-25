using Identity.Service.Domaine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Result;

namespace Identity.Service.Infrastructure.Validators
{
    internal class UserFieldsValidator
    {

        public CqsResult IsValid(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Email))
                return Error.Validation("Email is require.");

            if (!IsValidEmail(user.Email))
                return Error.Validation("Invalid email format");

            if (string.IsNullOrWhiteSpace(user.PasswordHash))
                return Error.Validation("Hash password is required.");

            if (user.EntityId == null)
                return Error.Validation("Entity id is required.");

            if (user.MainRoleId <= 0)
                return Error.Validation("Role id is require.");

            return CqsResult.Success();
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }


    }
}
