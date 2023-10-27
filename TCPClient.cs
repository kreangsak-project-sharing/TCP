using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ClientTest
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpClient client = new TcpClient();

            try
            {
                client.Connect("127.0.0.1", 9999); // Connect to the server (replace with the server's IP address if necessary)

                NetworkStream ns = client.GetStream();

                byte[] receivedData = new byte[1024];
                int bytesRead = ns.Read(receivedData, 0, receivedData.Length);

                string receivedMessage = Encoding.Default.GetString(receivedData, 0, bytesRead);
                Console.WriteLine("Received from server: " + receivedMessage);

                while (true)
                {
                    Console.Write("Enter a message to send to the server: ");
                    string messageToSend = Console.ReadLine();

                    if (messageToSend.ToLower() == "exit")
                    {
                        break;
                    }

                    byte[] messageBytes = Encoding.Default.GetBytes(messageToSend);
                    ns.Write(messageBytes, 0, messageBytes.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                client.Close();
            }
        }
    }
}
