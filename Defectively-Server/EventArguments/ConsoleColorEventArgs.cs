using System;
using System.Drawing;

namespace DefectivelyServer.EventArguments
{
    public class ConsoleColorEventArgs : EventArgs
    {
        public Color Foreground { get; }
        public Color Background { get; }

        public ConsoleColorEventArgs(Color foreground, Color background) {
            Foreground = foreground;
            Background = background;
        }
    }
}
