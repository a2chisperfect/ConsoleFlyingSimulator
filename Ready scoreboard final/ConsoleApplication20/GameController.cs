using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleApplication20
{
    public class GameController
    {
        private bool speedStatus;
        string[] Status = { "Take off and increase speed to 1,000 km", "Land the plane" };

        public void SetStatus(object sender, StatusEventArgs e)
        {
            speedStatus = e.status;
        }
        public string GameStatus()
        {
            if (!speedStatus)
            {
                return Status[0];
            }
            else
                return Status[1]; 
        }
        
    }
}
