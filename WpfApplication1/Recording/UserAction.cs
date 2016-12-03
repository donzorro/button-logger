using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace WpfApplication1.Recording
{
    public class UserAction
    {
        public DateTime Time { get; set; }

        public string Path { get; set; }
        
        public Point Point { get; set; }

        public string Char { get; set; }

        Type TargetUIType { get; set; }
    }
}
