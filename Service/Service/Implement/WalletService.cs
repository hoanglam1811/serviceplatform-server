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
	public class WalletService
		: GenericService<Wallet, WalletDTO, CreateWalletDTO, UpdateWalletDTO>, IWalletService
	{
		public WalletService(IGenericRepository<Wallet> genericRepository, IMapper mapper)
			: base(genericRepository, mapper)
		{
		}

		public async Task<WalletDTO?> GetByUserIdAsync(Guid userId)
		{
			var wallets = await _genericRepository.GetAllAsync();
			var wallet = wallets.FirstOrDefault(w => w.UserId == userId);
			return _mapper.Map<WalletDTO?>(wallet);
		}
	}
}
