using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using BuildDefender;
using Leap;

namespace turretController
{
    class Program
    {
        private int turretX = 2750;
        private int turretY = 1500;
        private int numProjectiles = 3;
        private readonly int TURRET_MAX_Y = 2000;
        private readonly int TURRET_MAX_X = 5500;
        private readonly int TURRET_MIN_Y = 0;
        private readonly int TURRET_MIN_X = 0;
        private static MissileLauncher myLauncher;
        
        public static void Main(string[] args)
        {
            myLauncher = new MissileLauncher();
            Program p = new Program();
            p.reset();
            p.initLeap();
            
        }

        private void initLeap()
        {
            // Create a sample listener and controller
            LeapListener listener = new LeapListener(this);
            Controller controller = new Controller();

            // Have the sample listener receive events from the controller
            controller.AddListener(listener);

            // Keep this process running until Enter is pressed
            Console.WriteLine("Press Enter to quit...");
            Console.ReadLine();

            // Remove the sample listener when done
            controller.RemoveListener(listener);
            controller.Dispose();
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

        public void goDown(int howFar)
        {
            turretY -= howFar;
            if(turretY < TURRET_MIN_Y)
            {
                turretY = TURRET_MIN_Y;
            }
            myLauncher.command_Down(howFar);
        }

        public void fire()
        {
            if(numProjectiles>0)
            {
                numProjectiles--;
                myLauncher.command_Fire();
            }
        }

        public void reset()
        {
            turretY = 1500;
            turretX = 2750;
            myLauncher.command_reset();
            numProjectiles = 3;
        }

        public int getTurretX()
        {
            return turretX;
        }

        public int getTurretY()
        {
            return turretY;
        }

        public void setTurretX(int newX)
        {
            turretX = newX;
        }

        public void setTurretY(int newY)
        {
            turretY = newY;
        }
    }
}
