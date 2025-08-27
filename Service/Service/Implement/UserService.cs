using AutoMapper;
using Repository.DTO;
using Repository.Entities;
using Repository.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Service.Interface;

namespace Service.Service.Implement
{
	public class UserService
		: GenericService<User, CreateUserDTO, UpdateUserDTO, UserDTO>, IUserService
	{
		public UserService(IGenericRepository<User> genericRepository, IMapper mapper)
			: base(genericRepository, mapper)
		{
		}

		public async Task<UserDTO?> GetByUsernameAsync(string username)
		{
			var users = await _genericRepository.GetAllAsync();
			var user = users.FirstOrDefault(u => u.Username == username);
			return _mapper.Map<UserDTO?>(user);
		}
	}
}
