using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReachTheEndGame
{
    class TestingAttribute() : Attribute
    {
        public bool ShowConnections { get; set; } = false;
        public bool ShowPatterns { get; set; } = false;
    }
}
