using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Leap;

namespace turretController
{
    class LeapListener : Listener
    {
        Program program;
        private readonly int LEAP_MAX_Y = 350;
        private readonly int LEAP_MAX_X = 175;
        private readonly int TURRET_MIN_Y = 50;
        private readonly int TURRET_MIN_X = -175;

        public LeapListener(Program program)
        {
            this.program = program;
        }

        private Object thisLock = new Object();

        private void SafeWriteLine(String line)
        {
            lock (thisLock)
            {
                Console.WriteLine(line);
            }
        }

        public override void OnInit(Controller controller)
        {
            SafeWriteLine("Initialized");
        }

        public override void OnConnect(Controller controller)
        {
            SafeWriteLine("Connected");
            //controller.EnableGesture(Gesture.GestureType.TYPECIRCLE);
            //controller.EnableGesture(Gesture.GestureType.TYPEKEYTAP);
            controller.EnableGesture(Gesture.GestureType.TYPESCREENTAP);
            controller.EnableGesture(Gesture.GestureType.TYPESWIPE);
        }

        public override void OnDisconnect(Controller controller)
        {
            //Note: not dispatched when running in a debugger.
            SafeWriteLine("Disconnected");
        }

        public override void OnExit(Controller controller)
        {
            SafeWriteLine("Exited");
        }

        private int getCoordinateFromLeap(float coord)
        {
            return (int)(coord * 15.71428) + 2750;
        }

        public override void OnFrame(Controller controller)
        {
            // Get the most recent frame and report some basic information
            Frame frame = controller.Frame();

            /* SafeWriteLine("Frame id: " + frame.Id
                         + ", timestamp: " + frame.Timestamp
                         + ", hands: " + frame.Hands.Count
                         + ", fingers: " + frame.Fingers.Count
                         + ", tools: " + frame.Tools.Count
                         + ", gestures: " + frame.Gestures().Count);*/

            if (!frame.Hands.IsEmpty)
            {
                // Get the first hand
                Hand hand = frame.Hands[0];

                // Check if the hand has any fingers
                FingerList fingers = hand.Fingers;
                if (!fingers.IsEmpty)
                {
                    // Calculate the hand's average finger tip position
                    Vector avgPos = Vector.Zero;
                    foreach (Finger finger in fingers)
                    {
                        avgPos += finger.TipPosition;
                    }
                    avgPos /= fingers.Count;

                    int currentTurretX = program.getTurretX();
                    int shouldBeTurretX = getCoordinateFromLeap(avgPos.x);
                    
                    int currentTurretY = program.getTurretY();
                    int shouldBeTurretY = getCoordinateFromLeap(avgPos.y);

                    // if it is above or bel.ow bounds, reset it
                    if (shouldBeTurretY > program.TURRET_MAX_Y) { shouldBeTurretY = program.TURRET_MAX_Y; }
                    if (shouldBeTurretY < program.TURRET_MIN_Y) { shouldBeTurretY = program.TURRET_MIN_Y; }
                    if (shouldBeTurretX > program.TURRET_MAX_X) { shouldBeTurretX = program.TURRET_MAX_X; }
                    if (shouldBeTurretX < program.TURRET_MIN_X) { shouldBeTurretX = program.TURRET_MIN_X; }

                    SafeWriteLine("Turret is at " + currentTurretX + ", " + currentTurretY + ". Should be " + shouldBeTurretX + ", " + shouldBeTurretY + "." );

                    // TODO
                }

                // Get the hand's sphere radius and palm position
                /* SafeWriteLine("Hand sphere radius: " + hand.SphereRadius.ToString("n2")
                             + " mm, palm position: " + hand.PalmPosition);*/

                // Get the hand's normal vector and direction
                Vector normal = hand.PalmNormal;
                Vector direction = hand.Direction;

                // Calculate the hand's pitch, roll, and yaw angles
                /*  SafeWriteLine("Hand pitch: " + direction.Pitch * 180.0f / (float)Math.PI + " degrees, "
                              + "roll: " + normal.Roll * 180.0f / (float)Math.PI + " degrees, "
                              + "yaw: " + direction.Yaw * 180.0f / (float)Math.PI + " degrees");
                 */
            }

            // Get gestures
            GestureList gestures = frame.Gestures();
            for (int i = 0; i < gestures.Count; i++)
            {
                Gesture gesture = gestures[i];

                switch (gesture.Type)
                {
                   /* case Gesture.GestureType.TYPESWIPE:
                        SwipeGesture swipe = new SwipeGesture(gesture);
                        SafeWriteLine("Swipe id: " + swipe.Id
                                       + ", " + swipe.State
                                       + ", position: " + swipe.Position
                                       + ", direction: " + swipe.Direction
                                       + ", speed: " + swipe.Speed);

                        if (swipe.Direction.x > .3)
                        {
                            program.goRight(50);
                        }
                        else if (swipe.Direction.x < -.3)
                        {
                            program.goLeft(50);
                        }
                        else if (swipe.Direction.y < -.3)
                        {
                            program.goDown(50);
                        }
                        else if (swipe.Direction.y > .3)
                        {
                            program.goUp(50);
                        }

                        break;*/
                    case Gesture.GestureType.TYPESCREENTAP:
                        ScreenTapGesture screentap = new ScreenTapGesture(gesture);
                        SafeWriteLine("Tap id: " + screentap.Id
                                       + ", " + screentap.State
                                       + ", position: " + screentap.Position
                                       + ", direction: " + screentap.Direction);

                        program.fire();
                        break;
                    default:
                        SafeWriteLine("Unknown gesture type.");
                        break;
                }
            }

            if (!frame.Hands.IsEmpty || !frame.Gestures().IsEmpty)
            {
                //SafeWriteLine("");
            }
        }
    }
}
