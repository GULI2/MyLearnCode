using BlogSystem.Dto;
using IBLL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystemBLL
{
    public class UserManager : IUserManager
    {

        //注册
        public async Task Register(string email, string pwd)
        {
            using (IDAL.IUserService userSev = new BlogSystem.DAL.UserService())
            {
                await userSev.CreateAsync(new BlogSystem.Models.User
                {
                    Email = email,
                    Password = pwd,
                    SiteName = "默认小站",
                    ImgPath = "default.png"
                });
            }
        }

        //登录
        public bool Login(string email, string pwd, out Guid id)//异步方法不能使用out方法
        {
            using (IDAL.IUserService userSev = new BlogSystem.DAL.UserService())
            {
                var user = userSev.GetAllAsync().FirstOrDefaultAsync(u => u.Email == email && u.Password == pwd);
                user.Wait();
                var data = user.Result;
                if (user == null)
                {
                    id = new Guid();
                    return false;
                }
                else
                {
                    id = data.Id;
                    return true;
                }
            }
        }
        //修改密码
        public async Task ChangePassword(string email, string oldPassword, string newPassword)
        {
            using (IDAL.IUserService userSev = new BlogSystem.DAL.UserService())
            {
                if (await userSev.GetAllAsync().AnyAsync(u => u.Email == email && u.Password == oldPassword))
                {
                    var user = await userSev.GetAllAsync().FirstAsync(u => u.Email == email);
                    user.Password = newPassword;
                    await userSev.EditAsync(user);
                }
            }
        }

        public async Task ChangeUserInfo(string email, string imgPath, string siteName)
        {
            using (IDAL.IUserService userSev = new BlogSystem.DAL.UserService())
            {
                if (await userSev.GetAllAsync().AnyAsync(u => u.Email == email))
                {
                    var user = await userSev.GetAllAsync().FirstAsync(u => u.Email == email);
                    user.ImgPath = imgPath;
                    user.SiteName = siteName;
                    await userSev.EditAsync(user);
                }
            }
        }

        public async Task<UserInformationDto> GetUserByEmail(string email)
        {
            using (IDAL.IUserService userSev = new BlogSystem.DAL.UserService())
            {
                if (await userSev.GetAllAsync().AnyAsync(u => u.Email == email))
                {
                    return await userSev.GetAllAsync().Where(u => u.Email == email).Select(u =>
                    new UserInformationDto()
                    {
                        Id = u.Id,
                        Email = u.Email,
                        SiteName = u.SiteName,
                        ImgPath = u.ImgPath,
                        FansCount = u.FansCount,
                        FoucsCount = u.FoucsCount
                    }).FirstAsync();
                }
                else
                {
                    throw new ArgumentException("");
                }
            }
        }


    }
}
