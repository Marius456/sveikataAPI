using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using sveikata.DTOs;
using sveikata.DTOs.User;
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
using System.Text;
using System.Threading.Tasks;

namespace sveikata.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public UserService(IUserRepository userRepository,
                           ICommentRepository commentRepository, 
                           AppDbContext context,
                           IConfiguration config)
        {
            _userRepository = userRepository;
            _commentRepository = commentRepository;
            _context = context;
            _config = config;
        }

        public async Task<LoginResponse<AuthenticatedUserDTO>> LoginUser(PostUserDTO UserCredentials)
        {
            var user = await _userRepository.FindByEmail(UserCredentials.Email);
            if (user == null)
            {
                var errorMessage = $"Password or login is incorrect";
                Log.Error(errorMessage);
                return new LoginResponse<AuthenticatedUserDTO> { Message = errorMessage, Success = false };
            }

            if (!UserCredentials.Password.Equals(user.Password))
            {
                var errorMessage = $"Password or login is incorrect";
                Log.Error(errorMessage);
                return new LoginResponse<AuthenticatedUserDTO> { Message = errorMessage, Success = false };
            }

            var token = GenerateJwtToken(user);
            var authenticatedUserDTO = new AuthenticatedUserDTO()
                                            {
                                                Email = user.Email,
                                                Token = token
                                            };
            return new LoginResponse<AuthenticatedUserDTO> { Data = authenticatedUserDTO };
        }

        public async Task<UserResponse> Create(UserDTO newUser, params ERole[] userRoles)
        {
            var existingUser = await _userRepository.FindByEmail(newUser.Email);
            if (existingUser != null)
            {
                string errorMessage = $"User with Email: {existingUser.Email} already exists";
                return new UserResponse(errorMessage);
            }

            var user = UserMapper.Map(newUser);

            try
            {
                await _userRepository.Create(user, userRoles);
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

        public async Task<User> FindByEmail(string email)
        {
            return await _userRepository.FindByEmail(email);
        }

        private string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var permClaims = new List<Claim>();
            permClaims.Add(new Claim(ClaimTypes.Name, user.Email.ToString()));
            permClaims.AddRange(user.UserRoles.Select(ur => new Claim(ClaimsIdentity.DefaultRoleClaimType, ur.Role.Name)));

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                permClaims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
