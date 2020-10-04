using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VideoAppBLL.DTO;

namespace VideoAppBLL.Interfaces
{
    public interface IDialogService
    {
        Task CreateDialog(DialogDTO dialog);
        Task DeleteDialog(int DialogId,UserDTO owner);
    }
}
