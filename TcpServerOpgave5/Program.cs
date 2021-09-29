using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using FootBallLib;

namespace TcpServerOpgave5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Server");
            TcpListener listener = new TcpListener(IPAddress.Loopback, 2121);
            listener.Start();
            //maybe while loop here 
            while (true)
            {
                Console.WriteLine("Server is ready on port 2121");
                TcpClient socket = listener.AcceptTcpClient();
                Console.WriteLine("Incoming client");
                DoClient(socket);
            }
        }
        private static void DoClient(TcpClient socket)
        {
            NetworkStream ns = socket.GetStream();
            StreamReader reader = new StreamReader(ns);
            StreamWriter writer = new StreamWriter(ns);
            string message = reader.ReadLine();
            message = Console.ReadLine();
            FootballPlayer fromJson = JsonSerializer.Deserialize<FootballPlayer>(message);
            Console.WriteLine("Football player: " + message);
            writer.Write("Football player received");
            writer.Flush();
            socket.Close();
        }
    }
}
