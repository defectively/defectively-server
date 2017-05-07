using System;

namespace DefectivelyServer.EventArguments
{
    public class StartEventArgs : EventArgs
    {
        public string IPAddress { get; }

        public StartEventArgs(string address) {
            IPAddress = address;
        }
    }
}
