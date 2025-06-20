using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Raftlab.Service.BaseResponse
{
    public class DataPage<TEntity>
    {
        public IQueryable<TEntity> Data { get; set; }
        public long TotalEntityCount { get; set; }

        public int PageNumber { get; set; }
        public int PageLength { get; set; }

        public int TotalPageCount
        {
            get
            {
                return Convert.ToInt32(Math.Ceiling((decimal)TotalEntityCount / PageLength));
            }
        }
    }
}
