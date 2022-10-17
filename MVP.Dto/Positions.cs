using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MVP.Dto.Base;


namespace MVP.Dto
{
    public class Positions
    {
        public List<Basketball> Basketballs { get; set; } = new List<Basketball>();
        public List<Handball> Handballs { get; set; } = new List<Handball>();

    }
}
