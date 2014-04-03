using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Leap;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.IO.Ports;
namespace turretController
{
    public class Program
    {
        private int turretX = 2750;
        private int turretY = 1000;
        private int bulletsShot = 0;
        public readonly int TURRET_MAX_Y = 2000;
        public readonly int TURRET_MAX_X = 4000;
        public readonly int TURRET_MIN_Y = 0;
        public readonly int TURRET_MIN_X = 1500;

        private Object thisLock = new Object();
        private String numberLight;
        private static MissileLauncher myLauncher;
        private Controller leapController;
        private LeapListener leapListener;
        
        public static void Main()
        {
            
            Program p = new Program();
            myLauncher = new MissileLauncher();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainUI(p));
        }

        public void initLeap()
        {
            // Create a sample listener and controller
            leapListener= new LeapListener(this);
            leapController = new Controller();

            // Have the sample listener receive events from the controller
            leapController.AddListener(leapListener);
        }

        public void deInitLeap()
        {
            // Remove the sample listener when done
            leapController.RemoveListener(leapListener);
            leapController.Dispose();
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
            myLauncher.command_Fire();
            bulletsShot++;
        }

        public void reset()
        {
            turretY = 1500;
            turretX = 2750;
            myLauncher.command_reset();
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

        public void log(String s)
        {
            lock (thisLock)
            {
                Console.WriteLine(s);
            }
        }
        public void showLight(String numberLight)
        {
            SerialPort port = new SerialPort("COM7", 9600);
            port.Open();
            port.Write(numberLight + '\n');
            port.Close();
        }
    }

}
