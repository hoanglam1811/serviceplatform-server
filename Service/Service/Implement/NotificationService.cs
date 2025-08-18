using AutoMapper;
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
	public class NotificationService
		: GenericService<Notification, CreateNotificationDTO, UpdateNotificationDTO, NotificationDTO>, INotificationService
	{
		private readonly IGenericRepository<Notification> _notificationRepository;
		private readonly IMapper _mapper;

		public NotificationService(IGenericRepository<Notification> genericRepository, IMapper mapper)
			: base(genericRepository, mapper)
		{
			_notificationRepository = genericRepository;
			_mapper = mapper;
		}

		public async Task<IEnumerable<NotificationDTO>> GetByUserIdAsync(Guid userId)
		{
			var list = await _notificationRepository.GetAllAsync();
			var userNoti = list.Where(x => x.UserId == userId)
							   .OrderByDescending(x => x.CreatedAt);
			return _mapper.Map<IEnumerable<NotificationDTO>>(userNoti);
		}

		public async Task<bool> MarkAsReadAsync(Guid id)
		{
			var notification = await _notificationRepository.GetByIdAsync(id);
			if (notification == null) return false;

			notification.Status = "Read";
			notification.UpdatedAt = DateTime.UtcNow;

			await _notificationRepository.UpdateAsync(notification);
			return true;
		}
	}
}
