using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MVP.Dto.Base;

namespace MVP.Dto
{
    public class Player
    {
        public string Name { get; set; }
        public string NickName { get; set; }
        public Positions Positions { get; set; } = new Positions();

        ///// <summary>
        ///// It take all sports total rating point
        ///// </summary>
        public int MVPScore
        {
            get
            {
                //TODO : New Sport Edit Lane
                // if u add new sport position, you must add that 
                //It's not in the documentation, but I thought a player played dual sports as well.
                return Positions.Basketballs.Sum(s => s.TotalRatingPoint)
                     + Positions.Handballs.Sum(s => s.TotalRatingPoint);
            }
        }
    }
}
