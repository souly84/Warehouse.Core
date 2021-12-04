﻿using System.Threading.Tasks;

namespace Warehouse.Core.Tests.Extensions
{
    public static class GoodExtensions
    {
        public static async Task<IReceptionGood> FullyConfirmed(this IReceptionGood good)
        {
            while (!await good.ConfirmedAsync())
            {
                good.Confirmation.Increase(1);
            }
            return good;
        }

        public static async Task<IReceptionGood> PartiallyConfirmed(this IReceptionGood good, int confirmedQty)
        {
            if (!await good.ConfirmedAsync())
            {
                good.Confirmation.Increase(confirmedQty);
            }
               
            return good;
        }
    }
}
