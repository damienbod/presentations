using System.Collections.Generic;
using AspNetCoreResourceServerTwo.Model;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreResourceServerTwo.Repositories
{
    public interface IDataEventRecordRepository
    {
        void Delete(long id);
        DataEventRecord Get(long id);
        List<DataEventRecord> GetAll();
        void Post(DataEventRecord dataEventRecord);
        void Put(long id, [FromBody] DataEventRecord dataEventRecord);
    }
}