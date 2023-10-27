using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener server = new TcpListener(IPAddress.Any, 9999);

            try
            {
                server.Start();
                Console.WriteLine("Server started on port 9999...");

                while (true)
                {
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Client connected...");

                    NetworkStream ns = client.GetStream();

                    byte[] hello = Encoding.Default.GetBytes("hello world");
                    ns.Write(hello, 0, hello.Length);

                    while (client.Connected)
                    {
                        byte[] msg = new byte[1024];
                        int bytesRead = ns.Read(msg, 0, msg.Length);

                        if (bytesRead == 0)
                        {
                            Console.WriteLine("Client disconnected...");
                            break;
                        }

                        string message = Encoding.Default.GetString(msg, 0, bytesRead);
                        Console.WriteLine("Received: " + message);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                server.Stop();
            }
        }
    } //Class
}
