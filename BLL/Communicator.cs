using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using BLL.Models;
using NetworkConfig;


namespace BLL
{
    /// <summary>
    ///     Communicate master and slave
    /// </summary>
    [Serializable]
    public class Communicator : MarshalByRefObject, IDisposable
    {

        private readonly Receiver<BllUser> receiver;

        private readonly Sender<BllUser> sender;

        private Task recieverTask;

        private CancellationTokenSource tokenSource;

        public event EventHandler<BllUser> UserAdded;

        public event EventHandler<BllUser> UserDeleted;

        public Communicator(Sender<BllUser> sender, Receiver<BllUser> receiver)
        {
            this.sender = sender;
            this.receiver = receiver;
        }

        public Communicator(Sender<BllUser> sender) : this(sender, null)
        {
        }
        public Communicator(Receiver<BllUser> receiver) : this(null, receiver)
        {
        }

        public void Connect(IEnumerable<IPEndPoint> endPoints)
        {
            if (ReferenceEquals(sender, null))
            {
                return;
            }

            sender.Connect(endPoints);
        }

        public async void RunReceiver()
        {
            await receiver.AcceptConnection();

            tokenSource = new CancellationTokenSource();
            recieverTask = Task.Run((Action)ReceiveMessages, tokenSource.Token);
        }

        public void SendAdd(BllUser user)
        {
            if (ReferenceEquals(sender, null))
            {
                return;
            }

            if (ReferenceEquals(user, null))
            {
                throw new ArgumentNullException();
            }

            sender.Send(new Message<BllUser>(user, MessageType.Added));
        }
        public void SendDelete(BllUser user)
        {
            if (ReferenceEquals(sender, null))
            {
                return;
            }

            if (ReferenceEquals(user, null))
            {
                throw new ArgumentNullException();
            }

            sender.Send(new Message<BllUser>(user, MessageType.Deleted));
        }

        public void Dispose()
        {
            receiver?.Dispose();
            sender?.Dispose();
        }

        protected virtual void OnUserDeleted(object sender, BllUser user)
        {
            UserDeleted?.Invoke(sender, user);
        }

        protected virtual void OnUserAdded(object sender, BllUser user)
        {
            UserAdded?.Invoke(sender, user);
        }

        private void ReceiveMessages()
        {
            while (true)
            {
                if (tokenSource.IsCancellationRequested)
                {
                    return;
                }

                var message = receiver.Receive();
                if (message.MessageType == MessageType.Added)
                    OnUserAdded(this, message.Entity);
                else if (message.MessageType == MessageType.Deleted)
                    OnUserDeleted(this, message.Entity);
                else break;

            }
        }
    }

}

