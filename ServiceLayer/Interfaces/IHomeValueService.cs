using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface IHomeValueService
    {
        bool SaveHomeValue(smHomeValue model);
        smHomeValue GetHomeValue(int postId);
    }
}
