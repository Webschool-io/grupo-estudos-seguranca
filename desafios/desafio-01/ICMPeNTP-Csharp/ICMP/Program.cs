using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ICMP
{
    static class Program
    {
        private static Socket icmpSocket;
        private static byte[] receiveBuffer = new byte[256];
        private static EndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            
            //Server
            CreateIcmpSocket();

            //Client
            while (true) { 
                
                Thread.Sleep(10);
                Console.WriteLine("Digite uma msg pro servidor: ");
                var msg = Console.ReadLine();
                SendServerMsg(msg);
            }
        }

        private static void SendServerMsg(string msg)
        {
            foreach (var item in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                var udpClient = new UdpClient(item.ToString(), 0);   
                Byte[] messagebyte = Encoding.UTF8.GetBytes(msg);
                udpClient.Send(messagebyte, messagebyte.Length);
            }
        }

        private static void CreateIcmpSocket()
        {
            icmpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.Icmp);
            icmpSocket.Bind(new IPEndPoint(IPAddress.Any, 0));
            // Uncomment to receive all ICMP message (including destination unreachable).
            // Requires that the socket is bound to a particular interface. With mono,
            // fails on any OS but Windows.
            //if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            //{
            //    icmpSocket.IOControl(0x4004667F, null, null);
            //}
            BeginReceiveFrom();
        }

        private static void BeginReceiveFrom()
        {
            icmpSocket.BeginReceiveFrom(receiveBuffer, 0, receiveBuffer.Length, SocketFlags.None,
                ref remoteEndPoint, ReceiveCallback, null);
        }

        private static void ReceiveCallback(IAsyncResult ar)
        {
            int len = icmpSocket.EndReceiveFrom(ar, ref remoteEndPoint);
            Console.WriteLine(string.Format("{0} Received {1} bytes from {2}",
                DateTime.Now, len, remoteEndPoint));
            LogIcmp(receiveBuffer, len);
            BeginReceiveFrom();
        }

        private static void LogIcmp(byte[] buffer, int length)
        {
            //for (int i = 0; i < length; i++)
            //{
            //    Console.Write(String.Format("{0:X2} ", buffer[i]));
            //}

            Console.WriteLine(Encoding.UTF8.GetString(buffer, 0, length));
            Console.WriteLine("");
        }
    }
}
