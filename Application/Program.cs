using System;
using MVP.Core.Business;
using MVP.Dto;

namespace Application
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MVPResultModel mvpPlayer = MVPBusiness.GetMVPPlayer();
        }
    }
}
