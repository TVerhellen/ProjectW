using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace weed_WPF_SQL
{
    internal class Cop : GameCharacter
    {
        private int _copType;
        

        public Cop()
        {
            Fill = Brushes.Blue;
            Behaviour = 3;
        }

        public int CopType { get { return _copType; } set { _copType = value; } }
        public int BackupDirection { get; set; }

        public override int[] PreviewUpdatedLocation()
        {
            //CalculateDirection();
            int[] newLoc = new int[2];
            switch (direction)
            {
                case 1: //north (neg Y)
                    newLoc[0] = Location[0];
                    newLoc[1] = Location[1]-Speed;
                    break;
                case 2: //east (pos X)
                    newLoc[0] = Location[0]+Speed;
                    newLoc[1] = Location[1];
                    break;
                case 3: //south (pos Y)
                    newLoc[0] = Location[0];
                    newLoc[1] = Location[1]+Speed;
                    break;
                case 4: //west (neg X)
                    newLoc[0] = Location[0]-Speed;
                    newLoc[1] = Location[1];
                    break;
                default:
                    newLoc[0] = Location[0];
                    newLoc[1] = Location[1];
                    break;
            }
            return newLoc;
        }

        public override void UpdateLocation()
        {
            int[] newLoc = new int[2];
            switch (direction)
            {
                case 1: //north (neg Y)
                    newLoc[0] = Location[0];
                    newLoc[1] = Location[1]-Speed;
                    break;
                case 2: //east (pos X)
                    newLoc[0] = Location[0]+Speed;
                    newLoc[1] = Location[1];
                    break;
                case 3: //south (pos Y)
                    newLoc[0] = Location[0];
                    newLoc[1] = Location[1]+Speed;
                    break;
                case 4: //west (neg X)
                    newLoc[0] = Location[0]-Speed;
                    newLoc[1] = Location[1];
                    break;
                default:
                    newLoc[0] = Location[0];
                    newLoc[1] = Location[1];
                    break;
            }
            Location = newLoc;
        }

        private void CalculateDirection()
        {
            int xDiff = CurrentTarget[0] - Location[0];
            int yDiff = CurrentTarget[1] - Location[1];
            if(Math.Abs(xDiff) > Math.Abs(yDiff))
            {
                if(xDiff > 0)
                {
                    direction = 2;
                }
                else if (xDiff < 0)
                {
                    direction = 4;
                }

                if (yDiff > 0)
                {
                    BackupDirection = 3;
                }
                else if (yDiff < 0)
                {
                    BackupDirection = 1;
                }

            }
            else
            {
                if (yDiff > 0)
                {
                    direction = 3;
                }
                else if(yDiff < 0)
                {
                    direction = 1;
                }

                if (xDiff > 0)
                {
                    BackupDirection = 2;
                }
                else if (xDiff < 0)
                {
                    BackupDirection = 4;
                }


            }
        }
    }
}
