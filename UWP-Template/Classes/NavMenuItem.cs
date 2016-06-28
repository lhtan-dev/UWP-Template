using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace UWP_Template.Classes
{
    public class NavMenuItem
    {
        public string Label { get; set; }
        public Symbol symbol { get; set; }

        public char SymbolAsChar
        {
            get
            {
                return (char)this.symbol;
            }
        }

        public Type DestinationPage { get; set; }
        public object Arguments { get; set; }
    }
}
