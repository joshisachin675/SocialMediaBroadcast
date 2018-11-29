using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain;

namespace AppInterfaces.Repository
{
    public interface IHomeValueRepository
    {

        bool SaveHomeValue(smHomeValue model);
        smHomeValue GetHomeValue(int postId);

        
    }
}
