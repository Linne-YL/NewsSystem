using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsSystem.Models

{
     /// <summary>
     /// 数据库操作类的接口
     /// </summary>
    public interface IProvider<T> where T : class //泛型接口
    {
        /// <summary>
        /// 查询所有记录
        /// </summary>
        /// <returns></returns>
        List<T> Select();
        /// <summary>
        /// 插入一条记录
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        int Insert(T t);
        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        int Update(T t);
        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        int Delete(T t);

    }
}