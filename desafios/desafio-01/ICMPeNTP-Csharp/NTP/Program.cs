using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NTP
{
    class Program
    {

        public static string[] NtpServerList = new string[]
                                    {
                                        "pool.ntp.org",
                                        "asia.pool.ntp.org",
                                        "europe.pool.ntp.org",
                                        "north-america.pool.ntp.org",
                                        "oceania.pool.ntp.org",
                                        "time-a.nist.gov",
                                        "time.windows.com",
                                        "south-america.pool.ntp.org"
                                    };

        static void Main(string[] args)
        {
            GetNetworkTime();
            Pause();
        }

        private static void Pause()
        {
            Console.Read();
        }


        public static void GetNetworkTime()
        {
            foreach (var itemNTPServer in NtpServerList)
            {
              try
              {
                  string ntpServer = itemNTPServer;
                  var ntpData = new byte[48];
                  ntpData[0] = 0x1B; //LeapIndicator = 0 (no warning), VersionNum = 3 (IPv4 only), Mode = 3 (Client Mode)

                  var addresses = Dns.GetHostEntry(ntpServer).AddressList;
                  var ipEndPoint = new IPEndPoint(addresses[0], 123);
                  var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                  socket.Connect(ipEndPoint);
                  socket.Send(ntpData);
                  socket.Receive(ntpData);
                  socket.Close();

                  ulong intPart = (ulong)ntpData[40] << 24 | (ulong)ntpData[41] << 16 | (ulong)ntpData[42] << 8 | (ulong)ntpData[43];
                  ulong fractPart = (ulong)ntpData[44] << 24 | (ulong)ntpData[45] << 16 | (ulong)ntpData[46] << 8 | (ulong)ntpData[47];

                  var milliseconds = (intPart * 1000) + ((fractPart * 1000) / 0x100000000L);
                  var networkDateTime = (new DateTime(1900, 1, 1)).AddMilliseconds((long)milliseconds);

                  Console.WriteLine(ntpServer + " - Time : - " + networkDateTime);
              }
              catch
              {
                  Console.Write(NtpServerList + " Failed.");
                  continue;
              }

            }

        }
    }
}


