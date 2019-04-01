using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleApplication20
{
    class DispatchersController
    {
        string[] names = { "Liam", "Jack", "Aiden", "Ethan", "Evelyn", "Zoey", "Nathan", "Chloe", "Elliot", "Charlie" };
        List<Dispatcher> activeDispatchers = new List<Dispatcher>(2);
        Random j= new Random();
        int swapRange;
        public int totalScore { get; private set; }

        private void ConnectDispatchers(Airplane A)
        {
            activeDispatchers.Add(new Dispatcher(names[j.Next(0, names.Length)]));
            Thread.Sleep(100);
            activeDispatchers.Add(new Dispatcher(names[j.Next(0, names.Length)]));
            if (activeDispatchers.Count != 0)
            {
                activeDispatchers[0].SetRecomendedHight(A.CurrentSpeed);
                activeDispatchers[1].SetRecomendedHight(A.CurrentSpeed);
            }
            for (int i = 0; i < activeDispatchers.Count; i++)
            {
                A.Handler += activeDispatchers[i].RecomendedHight;
            }
        }
        private void DisconnectDispatchers(Airplane A)
        {
            for (int i = 0; i < activeDispatchers.Count; i++)
            {
                totalScore += activeDispatchers[i].score;
                A.Handler -= activeDispatchers[i].RecomendedHight;
            }

            activeDispatchers.Clear();
        }
        public DispatchersController()
        {
            swapRange = Values.swapRange;
        }

        public void TakeOff(object sender, StatusEventArgs e)
        {
            if(e.status)
            {
                ConnectDispatchers((Airplane)sender);
            }
        }
        public void Landing(object sender, StatusEventArgs e)
        {
            if (e.status)
            {
                DisconnectDispatchers((Airplane)sender);
            }
        }

        public void Swap(object sender, RangeEventArgs e)
        {
            if(e.range > swapRange) 
            {
                DisconnectDispatchers((Airplane)sender);
                ConnectDispatchers((Airplane)sender);
                swapRange += swapRange;
            }
        }
        public List<Dispatcher> GetActive()
        {
            return activeDispatchers;
        }
    }
}
