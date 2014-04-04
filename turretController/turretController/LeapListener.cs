using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Leap;

namespace turretController
{
    interface LeapCommandInterface
    {
        void goRight(int howFar);
        void goLeft(int howFar);
        void goUp(int howFar);
        void goDown(int howFar);
    }
    class LeapListener : Listener
    {
        LeapCommandInterface program;

        public LeapListener(LeapCommandInterface program)
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
                    /* SafeWriteLine("Hand has " + fingers.Count
                                 + " fingers, average finger tip position: " + avgPos);*/
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
                    case Gesture.GestureType.TYPESWIPE:
                        SwipeGesture swipe = new SwipeGesture(gesture);
                        SafeWriteLine("Swipe id: " + swipe.Id
                                       + ", " + swipe.State
                                       + ", position: " + swipe.Position
                                       + ", direction: " + swipe.Direction
                                       + ", speed: " + swipe.Speed);
                        break;
                    case Gesture.GestureType.TYPESCREENTAP:
                        ScreenTapGesture screentap = new ScreenTapGesture(gesture);
                        SafeWriteLine("Tap id: " + screentap.Id
                                       + ", " + screentap.State
                                       + ", position: " + screentap.Position
                                       + ", direction: " + screentap.Direction);
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
