using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace MathAlgNet.Viewer.WriterProvider
{
    public class ControlWriter : TextWriter
    {
        private FrameworkElement textbox;
        public ControlWriter(FrameworkElement textbox)
        {
            this.textbox = textbox;
        }

        public override void Write(char value)
        {
            ((TextBlock)textbox).Text += value;
        }

        public override void Write(string value)
        {
            ((TextBlock)textbox).Text += value;
        }

        public override Encoding Encoding => Encoding.ASCII;
    }
}
