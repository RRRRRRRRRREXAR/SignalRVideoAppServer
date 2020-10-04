using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VideoAppBLL.DTO;

namespace VideoAppBLL.Interfaces
{
    public interface IMessageService
    {
        Task SaveMessage(MessageDTO message);
        Task DeleteMessage(MessageDTO message);
        Task EditMessage(MessageDTO message);
    }
}
