using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace NetworkConfig
{
    /// <summary>
    ///     Reciever class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class Receiver<T> : IDisposable
    {
        private Socket listener;
        private Socket reciever;

        /// <summary>
        ///    Constructor
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="port"></param>
        public Receiver(IPAddress ipAddress, int port)
        {
            this.IpEndPoint = new IPEndPoint(ipAddress, port);
            this.listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.listener.Bind(this.IpEndPoint);
            this.listener.Listen(1);
        }

        /// <summary>
        ///     ip address
        /// </summary>
        public IPEndPoint IpEndPoint { get; }

        /// <summary>
        ///     Accept connection
        /// </summary>
        /// <returns></returns>
        public Task AcceptConnection()
        {
            return Task.Run(() =>
            {
                this.reciever = this.listener.Accept();
            });
        }

        /// <summary>
        ///     Recieve message
        /// </summary>
        /// <returns></returns>
        public Message<T> Receive()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            Message<T> message;

            using (var networkStream = new NetworkStream(this.reciever, false))
            {
                message = (Message<T>)formatter.Deserialize(networkStream);
            }
            return message;
        }

        /// <summary>
        ///     Dispose sockets
        /// </summary>
        public void Dispose()
        {
            reciever?.Shutdown(SocketShutdown.Both);
            reciever?.Close();
            listener?.Shutdown(SocketShutdown.Both);
            listener?.Close();
        }

    }
}
