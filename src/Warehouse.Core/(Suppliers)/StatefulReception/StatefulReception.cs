using System.Collections.Generic;
using System.Threading.Tasks;
using Warehouse.Core.Plugins;

namespace Warehouse.Core
{
    public class StatefulReception : IReception
    {
        private readonly ReceptionWithExtraConfirmedGoods _reception;
        private readonly IKeyValueStore _store;

        public StatefulReception(ReceptionWithExtraConfirmedGoods reception, IKeyValueStore store)
        {
            _reception = reception;
            _store = store;
        }

        public IReceptionGoods Goods => new StatefulReceptionGoods(
            _reception,
            new JObjectAsKeyStore(_store, $"Repcetion_{_reception.Id}")
        );

        public string Id => _reception.Id;

        public Task ValidateAsync(IList<IGoodConfirmation> goodsToValidate)
        {
            return _reception.ValidateAsync(goodsToValidate);
        }
    }
}
