using System;
using System.Collections.Generic;
using System.Text;
using DtoBase = MVP.Dto.Base;
using MVP.Common;

namespace MVP.Dto
{
    public class Handball : DtoBase.Sport
    {
        public PositionsHandball Position { get; set; }

        private int _InitialRatingPoint;
        public int InitialRatingPoint
        {
            get
            {
                switch (Position)
                {
                    case PositionsHandball.G:
                        _InitialRatingPoint = 50;
                        break;
                    case PositionsHandball.F:
                        _InitialRatingPoint = 20;
                        break;
                    default:
                        _InitialRatingPoint = 0;
                        break;
                }
                return _InitialRatingPoint;
            }
        }

        public int GoalMade { get; set; }
        private int _GoalMadeRatingPoint;
        public int GoalMadeRatingPoint
        {
            get
            {
                switch (Position)
                {
                    case PositionsHandball.G:
                        _GoalMadeRatingPoint = 5;
                        break;
                    case PositionsHandball.F:
                        _GoalMadeRatingPoint = 1;
                        break;
                    default:
                        _GoalMadeRatingPoint = 0;
                        break;
                }
                return GoalMade * _GoalMadeRatingPoint;
            }
        }


        public int GoalRecieved { get; set; }
        private int _GoalRecievedRatingPoint;
        public int GoalRecievedRatingPoint
        {
            get
            {
                switch (Position)
                {
                    case PositionsHandball.G:
                        _GoalRecievedRatingPoint = -2;
                        break;
                    case PositionsHandball.F:
                        _GoalRecievedRatingPoint = -1;
                        break;
                    default:
                        _GoalRecievedRatingPoint = 0;
                        break;
                }
                return GoalRecieved * _GoalRecievedRatingPoint;
            }
        }

        public int TotalRatingPoint
        {
            get
            {
                return InitialRatingPoint + GoalMadeRatingPoint + GoalRecievedRatingPoint + (Winner ? Constant.WinnerExtraPoint : Constant.LoserExtraPoint);
            }
        }
    }
}
