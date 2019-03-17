using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Commands;

namespace WpfTest
{
    class ViewModel
    {
        public delegate int testDelgate();
        public DelegateCommand<string> AddCommand { get; }
    }
}
