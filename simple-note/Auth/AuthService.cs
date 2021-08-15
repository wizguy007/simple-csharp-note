using System;
using simple_note.User;

namespace simple_note.Auth
{
    public class AuthService
    {
        private readonly UserService _userService;
        private readonly JwtService _jwtService;

        public AuthService(UserService userService, JwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        public Object Login(LoginDto dto)
        {
            UserEntity user = _userService.FindByEmail(dto.Email);

            if (user == null) throw new Exception("Invalid credentials");

            if(!BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
            {
                throw new Exception("Invalid credentials");
            }

            var token = _jwtService.Generate(user.Id);

            return new { accessToken = token};
        }

        public UserEntity Register(RegisterDto dto)
        {
            UserEntity user = new UserEntity();
            user.Name = dto.Name;
            user.Email = dto.Email;
            user.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            return _userService.saveUser(user);
        }
    }
}
