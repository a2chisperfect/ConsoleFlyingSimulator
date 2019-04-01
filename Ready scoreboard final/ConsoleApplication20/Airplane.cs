using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication20
{
    public class AirplaneEventArgs : EventArgs
    {
        public readonly int height;
        public readonly int speed;
        public AirplaneEventArgs(int Height, int Speed)
        {
            height = Height;
            speed = Speed;
        }
    }
    public class RangeEventArgs : EventArgs
    {
        public readonly int range;
        public RangeEventArgs(int Range)
        {
            range = Range;
        }
    }
    public class StatusEventArgs : EventArgs
    {
        public readonly bool status;
        public StatusEventArgs(bool Status)
        {
            status = Status;
        }
    }
    class Airplane
    {
        public event EventHandler<RangeEventArgs> RangeHandler;
        public event EventHandler<StatusEventArgs> LandingHandler;
        public event EventHandler<StatusEventArgs> StatusHandler;
        public event EventHandler<StatusEventArgs> TakeOffHandler;
        public event EventHandler<AirplaneEventArgs> Handler;
       
        public bool onStart { get; private set; }

        private bool takeOff;
        public bool landing { get; private set; }
        public bool task { get; private set; }
        public int currentHeight { get; private set; }

        private int currentSpeed;
        public int CurrentSpeed
        {
            get
            {
                return currentSpeed;
            }
            private set
            {
                currentSpeed = value;
                if (currentSpeed >= Values.taskSpeed)
                {
                    task = true;
                }
                if (Handler != null)
                {
                    Handler(this, new AirplaneEventArgs(currentHeight, currentSpeed));
                }
                if (StatusHandler != null && currentSpeed >= Values.taskSpeed)
                {
                    StatusHandler(this, new StatusEventArgs(true));
                }
            }
        }
       
        public bool TakeOff
        {
            get
            {
                return takeOff;
            }
            private set
            {
                takeOff = value;
                if (TakeOffHandler != null)
                {
                    TakeOffHandler(this, new StatusEventArgs(takeOff));
                }
            }
        }

        private int range;
        public int Range
        {
            get
            {
                return range;
            }
            private set
            {
                range = value;
                if (RangeHandler != null)
                {
                    RangeHandler(this, new RangeEventArgs(range));
                }
            }
        }

        public Airplane()
        {
            currentHeight = 0;
            currentSpeed = 0;
            takeOff = false;
            range = 0;
            onStart = true;
            landing = false;
            task = false;
        }

        private void CheckForTakeOff()
        {
            if (currentSpeed !=0 && currentHeight !=0 && !landing)
            {
                TakeOff = true;
                onStart = false;
            }
        }
        private void CheckForLanding()
        {
            if(currentHeight <=500 && currentSpeed <=50)
            {
                takeOff = false;
                landing = true;
                if (LandingHandler != null)
                {
                    LandingHandler(this, new StatusEventArgs(landing));
                }
            }
        }

        public bool Landed()
        {
            if (landing && currentSpeed == 0 && currentHeight == 0)
                return true;
            else
                return false;
        }

        public void changeSpeed(int delta)
        {
            if (CurrentSpeed + delta > 0 && CurrentSpeed + delta <= 50 && onStart || 
                takeOff && CurrentSpeed + delta >= 0 || 
                landing && CurrentSpeed + delta > 0 || 
                landing && CurrentSpeed + delta == 0 && currentHeight == 0)
            {
                CurrentSpeed += delta;
                Range += CurrentSpeed;
            }
        }
        public void changeHight(int delta)
        {
            if(currentHeight + delta >=0 & currentSpeed > 0)
            {
                currentHeight += delta;
                Range += currentSpeed;
            }
            if(!takeOff)
            {
                CheckForTakeOff();
            }
            if(task)
            {
                CheckForLanding();
            }
            if (Handler != null)
            {
                Handler(this, new AirplaneEventArgs(currentHeight, currentSpeed));
            }
        }
        
    }
}
