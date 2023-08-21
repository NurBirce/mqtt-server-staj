using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StajUygulama.Models
{
    internal class Device<T>
    {
        public Device()
        {
        }

        internal Device(string name, string topic)
        {
            this.Name = name;
            Topic = topic;
        }

        public string Name { get; set; }

        public string Topic { get; set; }

        public T Value { get; set; }

    }
}
