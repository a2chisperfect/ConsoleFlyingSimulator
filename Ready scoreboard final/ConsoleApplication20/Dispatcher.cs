using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication20
{
    class Dispatcher
    {
        public string name { get; private set; }
        public int recomendedHight { get;  set; }
        public int score { get; private set; }
        public string message { get; private set; }
        private int N;

        public Dispatcher(string Name)
        {
            name = Name;
            Random rnd = new Random();
            N = rnd.Next(-100,200); //?
            message = "-";
        }
        public void SetRecomendedHight(int speed)
        {
            recomendedHight = 7 * speed - N;
        }
        public void RecomendedHight(object sender, AirplaneEventArgs e)
        {
            try
            {
                if (e.speed > Values.maxSpeed)
                {
                    score += Values.penaltyPointsOverMaxSpeed;
                    message = "Decrease your speed!";
                }
                else
                {
                    message = "-";
                }
                if (Math.Abs(e.height - recomendedHight) > 300 && Math.Abs(e.height - recomendedHight) < 600)
                {
                    score += Values.penaltyPointsBetween300And600;
                }
                else if (Math.Abs(e.height - recomendedHight) > 600 && Math.Abs(e.height - recomendedHight) < 1000)
                {
                    score += Values.penaltyPointsBetween600And1000;
                }
                else if (Math.Abs(e.height - recomendedHight) > 1000)
                {
                    throw new PlaneCrashedException("Your height is more or less on 1,000m than the recommended");
                }
                if (score > 1000)
                {
                    throw new NotSuitableException("Your penalty points more or equal to 1000, from " + name);
                }
                if (e.speed == 0)
                {
                    throw new PlaneCrashedException("During the flight, your speed can not be equal to zero");
                }
                if (e.height == 0)
                {
                    throw new PlaneCrashedException("During the flight, your height can not be equal to zero");
                }
                recomendedHight = 7 * e.speed - N;
            }

            catch (PlaneCrashedException ex)
            {
                throw new PlaneCrashedException("Crashed", ex);
            }
            catch (NotSuitableException ex)
            {
                throw new NotSuitableException("Failed", ex);
            }
        }
    }
}
