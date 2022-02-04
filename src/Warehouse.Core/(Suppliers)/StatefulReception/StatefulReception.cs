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

        private string ReceptionKey => $"Repcetion_{_reception.Id}";

        public IReceptionGoods Goods => new StatefulReceptionGoods(
            _reception,
            new JObjectAsKeyStore(_store, ReceptionKey)
        );

        public string Id => _reception.Id;

        public async Task ValidateAsync(IList<IGoodConfirmation> goodsToValidate)
        {
            await _reception.ValidateAsync(goodsToValidate);
            _store.Remove(ReceptionKey);
        }
    }
}
