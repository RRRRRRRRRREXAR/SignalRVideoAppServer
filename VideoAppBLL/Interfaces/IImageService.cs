using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VideoAppBLL.DTO;

namespace VideoAppBLL.Interfaces
{
    public interface IImageService
    {
        Task UploadImage(ImageDTO image);
        Task DeleteImage(ImageDTO image);
        Task ChangeImage(ImageDTO image);
    }
}
