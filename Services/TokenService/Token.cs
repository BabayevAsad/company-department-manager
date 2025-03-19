namespace Company_Expense_Tracker.Services.TokenService;

public class Token 
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public DateTime Expiration { get; set; }
}