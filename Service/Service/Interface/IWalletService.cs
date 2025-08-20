using Repository.DTO;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service.Interface
{
	public interface IWalletService
		: IGenericService<Wallet, CreateWalletDTO, UpdateWalletDTO, WalletDTO>
	{
		Task<WalletDTO?> GetByUserIdAsync(Guid userId);
	}
}
