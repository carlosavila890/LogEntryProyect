using System;
using System.Collections.Generic;
using System.Text;

namespace bd.log.servicios.Interfaces
{
  public  interface INetworkService
    {
        string GetRemoteIpClientAddress();
        string GetClientMachineName();
    }
}
