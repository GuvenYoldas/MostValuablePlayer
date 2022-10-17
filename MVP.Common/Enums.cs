using System;
using System.ComponentModel;

namespace MVP.Common
{
    public enum Sports
    {
        [Description("Basketball")]
        Basketball = 1,

        [Description("Handball")]
        Handball = 2
    }

    public enum PositionsBasketball
    {
        [Description("Guard")]
        G,

        [Description("Forward")]
        F,

        [Description("Center")]
        C
    }

    public enum PositionsHandball
    {
        [Description("Guard")]
        G,

        [Description("Forward")]
        F
    }

}
