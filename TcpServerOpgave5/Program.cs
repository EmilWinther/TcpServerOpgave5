using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using FootBallLib;

namespace TcpServerOpgave5
{
    class Program
    {
        private static readonly List<FootballPlayer> footballPlayers = new List<FootballPlayer>()
        {
            new FootballPlayer(1, "Lionel Messi", 12.2, 1),
            new FootballPlayer(2, "Cristiano Ronaldo", 13.45, 2),
            new FootballPlayer(3, "Mohamed Salah", 17.3, 3),
            new FootballPlayer(4, "Robert Lewandowski", 19.2, 4)

        };
        static void Main(string[] args)
        {
            Console.WriteLine("Server");
            TcpListener listener = new TcpListener(IPAddress.Loopback, 2121);
            listener.Start();
            //maybe while loop here 
            while (true)
            {
                TcpClient socket = listener.AcceptTcpClient();
                Task.Run(
                    () =>
                    {
                        TcpClient tmpSocket = socket;
                        DoClient(tmpSocket);
                    }
                );
            }
        }

        private static void DoClient(TcpClient socket)
        {
            using (StreamReader reader = new StreamReader(socket.GetStream()))
            using (StreamWriter writer = new StreamWriter(socket.GetStream()))
            {
                writer.AutoFlush = true;
                String kommando = reader.ReadLine();
                String fp = reader.ReadLine();

                switch (kommando)
                {
                    case "HentAlle":
                        String json = JsonSerializer.Serialize(footballPlayers);
                        writer.WriteLine(json);
                        break;
                    default:
                        writer.WriteLine("not allowed");
                        break;
                }
            }

            socket?.Close();
        }
    }
}
