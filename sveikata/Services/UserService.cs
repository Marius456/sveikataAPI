using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using sveikata.DTOs;
using sveikata.Mappers;
using sveikata.Models;
using sveikata.Repositories.Interfaces;
using sveikata.Services.Interfaces;
using sveikata.Services.Responses;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace sveikata.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly AppDbContext _context;

        public UserService(IUserRepository userRepository, ICommentRepository commentRepository, AppDbContext context)
        {
            _userRepository = userRepository;
            _commentRepository = commentRepository;
            _context = context;
        }

        public async Task<UserResponse> Create(UserDTO newUser)
        {
            var user = UserMapper.Map(newUser);

           
            try
            {
                user.RefreshToken = GenerateRefreshToken();
                await _userRepository.Create(user);
                await _context.SaveChangesAsync();
                return new UserResponse(UserMapper.Map(user));
            }
            catch (Exception exception)
            {
                string errorMessage = $"An error occured when creating the item: {exception.Message}";
                return new UserResponse(errorMessage);
            }
        }

        public async Task<UserResponse> Delete(int id)
        {
            var item = await _userRepository.GetById(id);
            if (item == null)
            {
                throw new KeyNotFoundException();
            }

            try
            {
                _userRepository.Delete(item);
                await _context.SaveChangesAsync();
                return new UserResponse();
            }
            catch (Exception exception)
            {
                string errorMessage = $"An error occured when deleting the item: {exception.Message}";
                return new UserResponse(errorMessage);
            }
        }

        public async Task<IEnumerable<UserDTO>> GetAll()
        {
            return (await _userRepository.GetAll()).Select(UserMapper.Map).ToList();
        }

        public async Task<UserResponse> GetById(int id)
        {
            var user = await _userRepository.GetById(id);

            if (user == null)
            {
                string errorMessage = "User not found.";

                Log.Error(errorMessage);

                return new UserResponse(errorMessage);
            }

            return new UserResponse(UserMapper.Map(user));
        }


        public async Task<IEnumerable<CommentDTO>> GetByComment(int id, int commentId)
        {
            return (await _commentRepository.GetByUser(id)).Select(CommentMapper.Map).Where(item => item.Id == commentId).ToList();
        }

        public async Task<IEnumerable<CommentDTO>> GetUserCommentsList(int id)
        {
            return (await _commentRepository.GetByUser(id)).Select(CommentMapper.Map).ToList();
        }

        public async Task<UserResponse> Update(int id, UserDTO updatedUser)
        {
            var user = await _userRepository.GetById(id);
            if (user == null)
            {
                throw new KeyNotFoundException();
            }

            user.Id = updatedUser.Id;
            user.Name = updatedUser.Name;
            user.Email = updatedUser.Email;
            user.Password = updatedUser.Password;

            try
            {
                _userRepository.Update(user);
                await _context.SaveChangesAsync();
                return new UserResponse();
            }
            catch (Exception exception)
            {
                string errorMessage = $"An error occured when updating the item: {exception.Message}";
                return new UserResponse(errorMessage);
            }
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        private string GenerateJSONWebToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("Admin", user.isAdmin.ToString()),
                new Claim("Id", user.Id.ToString())
            };

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
