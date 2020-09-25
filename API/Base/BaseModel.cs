using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Base
{
    public interface BaseModel
    {
        string Id { get; set; }
        DateTimeOffset CreateDate { get; set; }
        DateTimeOffset UpdateDate { get; set; }
        DateTimeOffset DeleteDate { get; set; }
        bool isDelete { get; set; }
    }
}
