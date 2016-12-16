using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

namespace NetworkConfig
{
    /// <summary>
    ///     Sender class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class Sender<T> : IDisposable
    {
        private IList<Socket> sockets;
        /// <summary>
        ///     Constructor
        /// </summary>
        public Sender()
        {
            this.sockets = new List<Socket>();
        }

        /// <summary>
        ///     Connects listeners
        /// </summary>
        /// <param name="ipEndPoints"></param>
        public void Connect(IEnumerable<IPEndPoint> ipEndPoints)
        {
            foreach (var ipEndPoint in ipEndPoints)
            {
                var socket = new Socket(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(ipEndPoint);
                this.sockets.Add(socket);
            }
        }

        /// <summary>
        ///     Sends message
        /// </summary>
        /// <param name="message"></param>
        public void Send(Message<T> message)
        {
            foreach (var socket in this.sockets)
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (NetworkStream networkStream = new NetworkStream(socket, false))
                {
                    formatter.Serialize(networkStream, message);
                }
            }
        }

        /// <summary>
        ///    Dispose
        /// </summary>
        public void Dispose()
        {
            foreach (var socket in this.sockets)
            {
                socket?.Shutdown(SocketShutdown.Both);
                socket?.Close();
            }
        }
    }
}
