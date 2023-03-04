namespace AuthorizationInfrastructure.Tokens
{
    public class SecurityToken
    {
        public string? AccessToken { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int FarmId { get; set; }
        public DateTime ExpireAt { get; set; }
        public SecurityToken(string? accessToken, int userId, string userName, string email, int farmId, DateTime expireAt)
        {
            AccessToken = accessToken;
            UserId = userId;
            UserName = userName;
            Email = email;
            FarmId = farmId;
            ExpireAt = expireAt;
        }
    }
}
