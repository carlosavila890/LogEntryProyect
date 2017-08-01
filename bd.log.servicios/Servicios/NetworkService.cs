using bd.log.servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace bd.log.servicios.Servicios
{
    class NetworkService : INetworkService
    {
        public string GetRemoteIpClientAddress()
        {
            var clientMachineName = GetClientMachineName();
            System.Threading.Tasks.Task<IPAddress[]> IpAddressList = Dns.GetHostAddressesAsync(clientMachineName);
            var remoteIpClientAddressList = IpAddressList.Result.ToList<IPAddress>();
            IPAddress remoteIpClientAddress = remoteIpClientAddressList.FirstOrDefault(ip => ip.AddressFamily.Equals(AddressFamily.InterNetwork));
            var remoteIpClientAddressString = (remoteIpClientAddress == null) ? "127.0.0.1" : remoteIpClientAddress.ToString();

            return remoteIpClientAddressString;
        }

        public string GetClientMachineName()
        {
            var clientMachineName = Dns.GetHostName();
            return clientMachineName;
        }
    }
}
