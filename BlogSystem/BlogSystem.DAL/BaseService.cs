using BlogSystem.IDAL;
using BlogSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.DAL
{
    public class BaseService<T> : IBaseService<T> where T : BaseEntity,new ()//new ()代表可实例化
    {
        private readonly BlogContext _db;//一般在前面加“_”表示私有变量。
        public BaseService(Models.BlogContext db)
        {
            _db = db;
        }
        public async Task CreateAsync(T model, bool saved = true)//saved是否自动保存
        {
            _db.Set<T>().Add(model);
            if (saved)
            {
                await _db.SaveChangesAsync();
            }
        }

        public async Task EditAsync(T model, bool saved = true)
        {
            _db.Configuration.ValidateOnSaveEnabled = false;//修改前先关闭数据校验
            _db.Entry(model).State = EntityState.Modified;
            if (saved)
            {
                await _db.SaveChangesAsync();
                _db.Configuration.ValidateOnSaveEnabled = true;//修改后再开启数据校验
            }
        }

        public async Task RemoveAsync(Guid id, bool saved = true)
        {
            _db.Configuration.ValidateOnSaveEnabled = false;
            var t = new T(){ Id = id };
            _db.Entry(t).State = EntityState.Unchanged;//自对象加载到上下文中后，或自上次调用 System.Data.Objects.ObjectContext.SaveChanges() 方法后，此对象尚未经过修改
            t.IsRemoved = true;//改变是否删除标记
            if (saved)
            {
                await _db.SaveChangesAsync();
                _db.Configuration.ValidateOnSaveEnabled = true;
            }
        }

        public async Task RemoveAsync(T model, bool saved = true)
        {
            await RemoveAsync(model.Id,saved);
        }

        public async Task<T> GetOneByIdAsync(Guid id)
        {
            return await GetAllAsync().FirstAsync(u => u.Id == id);
        }

        /// <summary>
        /// 构造条件
        /// </summary>
        /// <returns></returns>
        public  IQueryable<T> GetAllAsync()
        {
            return _db.Set<T>().Where(u=>!u.IsRemoved).AsNoTracking();//AsNoTrackin：不会对查询返回的实体进行额外的处理或存储
        }

        public IQueryable<T> GetAllByPageAsync(int pageSize = 10, int pageIndex = 0)
        {
            return  GetAllAsync().Skip(pageSize * pageIndex).Take(pageSize);
        }

        public IQueryable<T>  GetByOrderAsync(bool asc = true)
        {
            var datas = GetAllAsync();
            return asc ? datas.OrderBy(u => u.CreateTime) : datas.OrderByDescending(u => u.CreateTime);
        }

        public IQueryable<T>  GetByPageOrderAsync(int pageSize = 10, int pageIndex =0, bool asc = true)
        {
            return GetByOrderAsync(asc).Skip(pageSize * pageIndex).Take(pageSize);
        }
   
         

        public async Task Save()
        {
            await _db.SaveChangesAsync();
            _db.Configuration.ValidateOnSaveEnabled = true;//修改前会关闭数据校验，保存后开启数据校验
        }
        //IDisposable接口的主要用途是释放非托管资源。当不再使用托管对象时，垃圾回收器会自动释放分配给该对象的内存。
        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
