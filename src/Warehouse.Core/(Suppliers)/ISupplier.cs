using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaPrint;

namespace Warehouse.Core
{
    public interface ISupplier : IPrintable
    {
        IEntities<IReception> Receptions { get; }
    }

    public class MockSupplier : ISupplier
    {
        private readonly string _supplierName;

        public MockSupplier()
            : this("MockSupplier")
        {
        }

        public MockSupplier(string supplierName) :
            this(
                supplierName,
                new MockReception(
                    new MockReceptionGood("1", 2, "123456789"),
                    new MockReceptionGood("2", 2, "123456780"),
                    new MockReceptionGood("3", 2, "123456781"),
                    new MockReceptionGood("4", 2, "123456782")
            )
        )
        {
        }

        public MockSupplier(params IReception[] receptions)
            : this("MockSupplier", receptions)
        {
        }

        public MockSupplier(string supplierName, params IReception [] receptions)
            : this(supplierName, new ListOfEntities<IReception>(receptions))
        {
        }

        public MockSupplier(string supplierName, IEntities<IReception> receptions)
        {
            _supplierName = supplierName;
            Receptions = receptions;
        }

        public IEntities<IReception> Receptions { get; }

        public override bool Equals(object? obj)
        {
            return ReferenceEquals(this, obj)
                || (obj is MockSupplier supplier && _supplierName == supplier._supplierName)
                || (obj is string suppllierName && suppllierName == _supplierName)
                || (obj is DateTime receptionDate && OneOfReceptionsDate(receptionDate));
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_supplierName, Receptions);
        }

        public void PrintTo(IMedia media)
        {
            media
                .Put("name", _supplierName)
                .Put("receptions", Receptions);
        }

        private bool OneOfReceptionsDate(DateTime dateTime)
        {
            var receptions = Receptions.ToListAsync().RunSync();
            foreach (var reception in receptions)
            {
                if (reception.Equals(dateTime))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
