using System;
using System.Collections.Generic;
using System.Text;
using DtoBase = MVP.Dto.Base;
using MVP.Common;

namespace MVP.Dto
{
    public class Basketball : DtoBase.Sport
    {
        public PositionsBasketball Position { get; set; }

        public int Scores { get; set; }
        private int _ScoreRatingPoint;
        public int ScoreRatingPoint
        {
            get
            {
                switch (Position)
                {
                    case PositionsBasketball.G:
                    case PositionsBasketball.F:
                    case PositionsBasketball.C:
                        _ScoreRatingPoint = 2;
                        break;
                    default:
                        _ScoreRatingPoint = 0;
                        break;
                }
                return Scores * _ScoreRatingPoint;
            }
        }


        public int Rebounds { get; set; }
        private int _ReboundRatingPoint;
        public int ReboundRatingPoint
        {
            get
            {
                switch (Position)
                {
                    case PositionsBasketball.G:
                        _ReboundRatingPoint = 3;
                        break;
                    case PositionsBasketball.F:
                        _ReboundRatingPoint = 2;
                        break;
                    case PositionsBasketball.C:
                        _ReboundRatingPoint = 1;
                        break;
                    default:
                        _ReboundRatingPoint = 0;
                        break;
                }
                return Rebounds * _ReboundRatingPoint;
            }
        }


        public int Assists { get; set; }
        private int _AssistsRatingPoint;
        public int AssistsRatingPoint
        {
            get
            {
                switch (Position)
                {
                    case PositionsBasketball.G:
                        _AssistsRatingPoint = 1;
                        break;
                    case PositionsBasketball.F:
                        _AssistsRatingPoint = 2;
                        break;
                    case PositionsBasketball.C:
                        _AssistsRatingPoint = 3;
                        break;
                    default:
                        _AssistsRatingPoint = 0;
                        break;
                }
                return Assists * _AssistsRatingPoint;
            }
        }

        public int TotalRatingPoint
        {
            get
            {
                return ScoreRatingPoint + ReboundRatingPoint + AssistsRatingPoint + (Winner ? Constant.WinnerExtraPoint : Constant.LoserExtraPoint);
            }
        }
    }
}
