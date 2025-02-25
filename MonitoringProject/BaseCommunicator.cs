using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteMonitoring
{
    public interface IBaseCommunicator
    {
        public bool IsConnected { get; set; }

        public string ConnectionName { get; }

        public  bool Connect();

        public bool DisConnect();

        public  string SendCommandAndGetAnswer(string p_Command);
    }
}
