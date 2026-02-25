/**
 *  UserRef : utiliser pour relation utile dans la communication
 *  EntityId : utiliser pour relation metier
 **/

namespace Identity.Service.Domaine.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public bool MustChangePassword { get; private set; }
        public string UserRef { get; set; } = string.Empty; // reference dans le context trace ecrite
        public Guid EntityId { get; set; }
        public int MainRoleId { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public bool IsEmailConfirmed()
        {
            return !MustChangePassword;
        }

 
        public void ForcePasswordChange()
        {
            MustChangePassword = true;
        }

        public void ChangePassword(string newPasswordHash)
        {
            PasswordHash = newPasswordHash;
            MustChangePassword = false;
            UpdatedAt = DateTime.UtcNow;
        }


    }
}
