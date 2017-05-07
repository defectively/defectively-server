using System;
using System.Windows.Forms;

namespace DefectivelyServer.EventArguments
{
    public class FormCreatedEventArgs : EventArgs
    {
        public Form Form { get; }

        public FormCreatedEventArgs(Form form) {
            Form = form;
        }
    }
}
