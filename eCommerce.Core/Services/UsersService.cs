using AutoMapper;
using eCommerce.Core.DTO;
using eCommerce.Core.Entities;
using eCommerce.Core.RepositoryContracts;
using eCommerce.Core.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.Services
{
    internal class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;

        public UsersService(IUsersRepository usersRepository, IMapper mapper)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
        }
        public async Task<AuthenticationResponse?> Login(LoginRequest loginRequest)
        {
            ApplicationUser? user = await _usersRepository.GetUserByEmailAndPassword(loginRequest.Email, loginRequest.Password);

            if (user == null) return null;

            //return new AuthenticationResponse(user.UserId, user.Email, user.PersonName, user.Gender, "token",true);

            // Use AutoMapper
            return _mapper.Map<AuthenticationResponse>(user) with { Success = true, Token = "Token"};
        }

        public async Task<AuthenticationResponse?> Register(RegisterRequest registerRequest)
        {
            /*
            ApplicationUser newUser = new()
            {
                Email = registerRequest.Email,
                PersonName = registerRequest.PersonName,
                Password = registerRequest.Password,
                Gender = registerRequest.Gender.ToString()
            };
            */

            // use AutoMapper
            ApplicationUser newUser = _mapper.Map<ApplicationUser>(registerRequest);

            ApplicationUser? registeredUser = await _usersRepository.AddUser(newUser);

            if (registeredUser == null) return null;

            //return new AuthenticationResponse(registeredUser.UserId, registeredUser.Email, registeredUser.PersonName, registeredUser.Gender, "token", true);

            // Use AutoMapper
            return _mapper.Map<AuthenticationResponse>(registeredUser) with { Success = true, Token = "Token" };
        }
    }
}
