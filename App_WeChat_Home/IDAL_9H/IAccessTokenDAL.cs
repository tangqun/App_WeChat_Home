using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL_9H
{
    public interface IAccessTokenDAL
    {
        string Get(string authorizerAppID);
    }
}
