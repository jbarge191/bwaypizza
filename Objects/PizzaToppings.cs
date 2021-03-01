using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace BrightPizza
{
    public class PizzaToppingsJson
    {
        public List<string> Toppings { get; set; }
    }

    public class PizzaToppings
    {
        public List<string> Toppings { get; set; }
        public int Count { get; set; }
    }
}
