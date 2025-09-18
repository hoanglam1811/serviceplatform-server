using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Repository.DTO;
using Repository.Entities;
using Repository.Repository.Interface;
using Service.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service.Implement
{
	public class TransactionService
		: GenericService<Transaction, CreateTransactionDTO, UpdateTransactionDTO, TransactionDTO>, ITransactionService
	{
		public TransactionService(IGenericRepository<Transaction> genericRepository, IMapper mapper)
			: base(genericRepository, mapper)
		{
		}

		public async Task<IEnumerable<TransactionDTO>> GetTransactionsByWalletIdAsync(Guid walletId)
		{
			var transactions = await _genericRepository.GetAllAsync();
			var result = transactions.Where(t => t.WalletId == walletId);
			return _mapper.Map<IEnumerable<TransactionDTO>>(result);
		}

		public async Task<IEnumerable<TransactionDTO>> GetTransactionsByUserIdAsync(Guid userId)
		{
			var transactions = await _genericRepository.GetAllAsync(
				q => q.Include(t => t.Wallet)
			);

			var result = transactions
				.Where(t => t.Wallet != null && t.Wallet.UserId == userId)
				.ToList();

			return _mapper.Map<IEnumerable<TransactionDTO>>(result);
		}

	}
}
