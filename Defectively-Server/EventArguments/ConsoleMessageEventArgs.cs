using System;

namespace DefectivelyServer.EventArguments
{
    public class ConsoleMessageEventArgs : EventArgs
    {
        public string Message { get; }

        public ConsoleMessageEventArgs(string message) {
            Message = message;
        }
    }
}
