using Company_Expense_Tracker.Dtos.UserDtos;
using Company_Expense_Tracker.Entities;
using Company_Expense_Tracker.Repositories;

namespace Company_Expense_Tracker.Services.AuthService;

public class UserService : IUserService
{
   private readonly IUserRepository _repo;
   private readonly TokenService.TokenService _service;

   public UserService(IUserRepository repo, TokenService.TokenService service)
   {
      _repo = repo;
      _service = service;
   }

   public async Task<string> RegisterUser(RegisterUserDto dto)
   {
      var exsitUser = await _repo.GetUserByMail(dto.Email);
      
      if (exsitUser != null)
         throw new Exception("This User already registerd");
      
      var newUser = new User()
      {
         Email = dto.Email,
         PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
         Role = (Role)dto.RoleId
      };

      await _repo.CreateAsync(newUser);
      var token = _service.CreateToken(newUser);
      
      return token.AccessToken;
   }

   public async Task<string> LoginUser(LoginUserDto loginUserDto)
   {
      var user = await _repo.GetUserByMail(loginUserDto.Email);
      
      var result = user != null && BCrypt.Net.BCrypt.Verify(loginUserDto.Password, user.PasswordHash);

      if (result != true)
         return "Token created failed";
      
      var token = _service.CreateToken(user);
      return token.AccessToken;
   }
}