using BlogSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.IDAL
{
    //IDisposable接口的主要用途是释放非托管资源。当不再使用托管对象时，垃圾回收器会自动释放分配给该对象的内存。
    public interface IBaseService<T>:IDisposable where T: BaseEntity//约束泛型，将T约束在BaseEntity中
    {
        Task CreateAsync(T model,bool saved = true);//saved自动保存
        Task EditAsync(T model, bool saved = true);
        Task RemoveAsync(Guid id, bool saved = true);
        Task RemoveAsync(T model, bool saved = true);

        Task Save();

        Task<T> GetOneByIdAsync(Guid id);
        IQueryable<T> GetAllAsync();
        IQueryable<T> GetAllByPageAsync(int pageSize=10,int pageIndex=1);

        IQueryable<T> GetByOrderAsync(bool asc=true);
        IQueryable<T> GetByPageOrderAsync(int pageSize = 10, int pageIndex = 1, bool asc = true);
    }
}
