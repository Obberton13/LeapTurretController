using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using BuildDefender;

namespace turretController
{
    class Program
    {
        private int turretX = 2750;
        private int turretY = 1500;
        private readonly int TURRET_MAX_Y = 2000;
        private readonly int TURRET_MAX_X = 5500;
        private readonly int TURRET_MIN_Y = 0;
        private readonly int TURRET_MIN_X = 0;
        private static MissileLauncher myLauncher;
        
        public static void Main(string[] args)
        {
            myLauncher = new MissileLauncher();
        }

        public void goRight(int howFar)
        {
            turretX += howFar;
            if (turretX > TURRET_MAX_X)
            {
                turretX = TURRET_MAX_X;
            }
            myLauncher.command_Right(howFar);
        }

        public void goLeft(int howFar)
        {
            turretX -= howFar;
            if(turretX<TURRET_MIN_X)
            {
                turretX = TURRET_MIN_X;
            }
            myLauncher.command_Left(howFar);
        }

        public void goUp(int howFar)
        {
            turretY += howFar;
            if (turretY > TURRET_MAX_Y)
            {
                turretY = TURRET_MAX_Y;
            }
            myLauncher.command_Up(howFar);
        }
    }
}
