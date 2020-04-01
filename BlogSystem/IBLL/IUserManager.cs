using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public interface IUserManager
    {
        Task Register(string email, string pwd);

        bool Login(string email, string pwd, out Guid id);
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="email"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        Task ChangePassword(string email, string oldPassword, string newPassword);
        /// <summary>
        /// 修改用户头像和用户名
        /// </summary>
        /// <param name="email"></param>
        /// <param name="imgPath">头像</param>
        /// <param name="siteName">博客名</param>
        /// <returns></returns>
        Task ChangeUserInfo(string email, string imgPath, string siteName);
        Task<BlogSystem.Dto.UserInformationDto> GetUserByEmail(string email);
    }
}
