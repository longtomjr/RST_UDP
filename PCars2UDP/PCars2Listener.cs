using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PCars2UDP
{
    public class PCars2Listener : UdpClient
    {
        private IPEndPoint _groupEP;

        public IPEndPoint GroupEP { get => _groupEP; set => _groupEP = value; }     //Start recieving data from any IP listening on port 5606 (port for PCARS2)

        public PCars2Listener() : base(5606)
        {
            GroupEP = new IPEndPoint(IPAddress.Any, 5606);
        }

        public PCars2Listener(int port) : base(port)
        {
            GroupEP = new IPEndPoint(IPAddress.Any, 5606);
        }

        public byte[] Receive()
        {
            return base.Receive(ref _groupEP);
        }

        ~PCars2Listener()
        {
            Close();
        }
    }
}
