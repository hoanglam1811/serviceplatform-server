using Repository.DTO;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service.Interface
{
	public interface IUserService
		: IGenericService<User, CreateUserDTO, UpdateUserDTO, UserDTO>
	{
		Task<UserDTO?> GetByUsernameAsync(string username);
	}
}
