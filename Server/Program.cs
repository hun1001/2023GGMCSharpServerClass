﻿using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server;

internal class Program
{
    private static void Main(string[] args)
    {
        using (Socket serverSocket = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
        {
            IPEndPoint endPoint = new(IPAddress.Parse("127.0.0.1"), 5000);

            serverSocket.Bind(endPoint);

            serverSocket.Listen(20);

            while (true)
            {
                using (Socket clientSocket = serverSocket.Accept())
                {
                    Console.WriteLine("Client connected " + clientSocket.RemoteEndPoint.ToString());

                    byte[] buffer = new byte[256];
                    int totalByte = clientSocket.Receive(buffer);

                    if (totalByte < 1)
                    {
                        Console.WriteLine("Connection closed");
                        return;
                    }

                    string str = Encoding.UTF8.GetString(buffer, 0, totalByte);
                    Console.WriteLine("Client: " + str);

                    clientSocket.Send(buffer);
                }
            }
        }

    }
}