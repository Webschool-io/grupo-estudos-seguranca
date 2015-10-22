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

       
        static void Main()
        {
            
            //Este método chama o "Server" a partir deste ponto os listeners ficam na escuta de qualquer
            //novidade enviada no protocolo ICMP e faz o output dele no terminal.
            //Meu buffer envia as informações encodada em UFT8 e é decodificada assim na ponta do server.
            //ainda é preciso melhorar esse decript.
            CreateIcmpSocket();

            //O Cliente fica floodando com msg este protocolo e a cada msg enviada
            //eu rodo em todos os ips que o usuário possuí localmente e faço o envio.
            //o servidor deve responder para todos aqueles o qual o envio for sucessido.
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
