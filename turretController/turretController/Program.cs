using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildDefender;

namespace turretController
{
    class Program
    {
        static void Main(string[] args)
        {
            MissileLauncher myLauncher = new MissileLauncher();
            myLauncher.command_Down(10);
        }
    }
}
