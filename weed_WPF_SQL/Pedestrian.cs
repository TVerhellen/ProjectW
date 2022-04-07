using System.Collections.Generic;
using System.Windows.Media;

namespace weed_WPF_SQL
{
    internal class Pedestrian : GameCharacter
    {
        public Pedestrian(int[] location, List<int[]> route, int speed, int behaviour)
        {
            Location = location;
            RouteCoordinates = route;
            Speed = speed;
            Behaviour = behaviour;
            direction = 1;
            CurrentTarget = RouteCoordinates[0];
            Fill = Brushes.Orange;
            Figure.Width = 20;
            Figure.Height = 20;
            Figure.Fill = Fill;
            Figure.Stroke = Brushes.Black;
        }

        public override string GetType()
        {
            return "ped";
        }

        public override int[] PreviewUpdatedLocation()
        {
            int[] newLoc = new int[2];
            switch (Behaviour)
            {
                case 1: //up/down
                    if (direction == 1 && _currentTarget[1] > Location[1]) //down (pos y)
                    {
                        newLoc[0] = Location[0];
                        newLoc[1] = Location[1]+Speed;
                    }
                    else if (direction == 0 && _currentTarget[1] < Location[1])//up (neg y)
                    {
                        newLoc[0] = Location[0];
                        newLoc[1] = Location[1]-Speed;
                    }
                    break;
                case 2: //left/right
                    if (direction == 1 && _currentTarget[0] > Location[0]) //right (pos x)
                    {
                        newLoc[0] = Location[0]+Speed;
                        newLoc[1] = Location[1];
                    }
                    else if(direction == 0 && _currentTarget[0] < Location[0]) //left (neg x)
                    {
                        newLoc[0] = Location[0]-Speed;
                        newLoc[1] = Location[1];
                    }
                    break;
                default: //includes 1: stationary
                    newLoc[0] = Location[0];
                    newLoc[1] = Location[1];
                    break;
            }
            return newLoc;
        }

        public override void UpdateLocation() // forward = following positive axis
        {
            int[] newLoc = new int[2];
            newLoc[0] = Location[0];
            newLoc[1] = Location[1];
            switch (Behaviour)
            {
                case 1: //up/down
                    if (direction == 1 && CurrentTarget[1] > Location[1]) //down (pos y)
                    {
                        newLoc[0] = Location[0];
                        newLoc[1] = Location[1]+Speed;
                        if(newLoc[1] >= CurrentTarget[1])
                        {
                            CurrentTarget = RouteCoordinates[1];
                            direction = 0;
                        }
                    }
                    else if (direction == 0 && CurrentTarget[1] < Location[1])//up (neg y)
                    {
                        newLoc[0] = Location[0];
                        newLoc[1] = Location[1]-Speed;
                        if (newLoc[1] <= CurrentTarget[1])
                        {
                            CurrentTarget = RouteCoordinates[0];
                            direction = 1;
                        }
                    }
                    break;
                case 2: //left/right
                    if (direction == 1 && CurrentTarget[0] > Location[0]) //right (pos x)
                    {
                        newLoc[0] = Location[0]+Speed;
                        newLoc[1] = Location[1];
                        if (newLoc[0] >= CurrentTarget[0])
                        {
                            CurrentTarget = RouteCoordinates[1];
                            direction = 0;
                        }
                    }
                    else if (direction == 0 && CurrentTarget[0] < Location[0]) //left (neg x)
                    {
                        newLoc[0] = Location[0]-Speed;
                        newLoc[1] = Location[1];
                        if (newLoc[0] <= CurrentTarget[0])
                        {
                            CurrentTarget = RouteCoordinates[0];
                            direction = 1;
                        }
                    }
                    break;
                default: //includes 1: stationary
                    newLoc[0] = Location[0];
                    newLoc[1] = Location[1];
                    break;
            }
            Location = newLoc;
            //update currentTarget

        }
    }
}
