using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace weed_WPF_SQL
{
    internal class PlayerCharacter : GameCharacter
    {
        public PlayerCharacter()
        {
            Fill = Brushes.Yellow;
            Figure.Width = 20;
            Figure.Height = 20;
            Figure.Fill = Fill;
            Figure.Stroke = Brushes.Black;
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
            PlayerMovedEvent?.Invoke();
        }

        public override int[] PreviewUpdatedLocation()
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
            return newLoc;
        }

        public override string GetType()
        {
            return "player";
        }

        public delegate void GameCharacterHandler();
        public GameCharacterHandler myHandler;

        public event GameCharacterHandler PlayerMovedEvent;

    }
}
