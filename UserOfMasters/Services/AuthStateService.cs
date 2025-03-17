namespace UserOfMasters.Services
{
    public class AuthStateService
    {
        public bool IsLoggedIn { get; set; } = false;
        public string? UserName { get; set; }
        public Guid? UserId { get; set; }

        public event Action? OnAuthStateChanged;

        public void Login(string userName, Guid userId)
        {
            IsLoggedIn = true;
            UserName = userName;
            UserId = userId;

            OnAuthStateChanged?.Invoke();
        }

        public void Logout()
        {
            IsLoggedIn = false;
            UserName = null;
            UserId = null;

            OnAuthStateChanged?.Invoke();
        }
    }
}
