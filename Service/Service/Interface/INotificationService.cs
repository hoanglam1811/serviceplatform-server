using Repository.DTO;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service.Interface
{
	public interface INotificationService
		: IGenericService<Notification, CreateNotificationDTO, UpdateNotificationDTO, NotificationDTO>
	{
		Task<IEnumerable<NotificationDTO>> GetByUserIdAsync(Guid userId);
		Task<bool> MarkAsReadAsync(Guid id);
	}

}
