using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Warehouse.Core.Plugins;

namespace Warehouse.Core
{
    public class StatefulReception : IReception
    {
        private readonly IReception _reception;
        private readonly IKeyValueStorage _store;
        private IKeyValueStorage? _goodsSate;
        private IReceptionGoods? _goods;

        public StatefulReception(IReception reception, IKeyValueStorage store)
        {
            _reception = reception;
            _store = store;
        }

        /// <summary>
        /// Here we assume that receptionId is unique number in the system and
        /// can not be reused by different suppliers.
        /// </summary>
        private string ReceptionKey => $"Repcetion_{_reception.Id}";

        private IKeyValueStorage GoodsState => _goodsSate ??= new JObjectAsKeyStore(_store, ReceptionKey);

        public IReceptionGoods Goods => _goods ??= new StatefulReceptionGoods(
            _reception,
            GoodsState
        ).Cached();

        public string Id => _reception.Id;

        public async Task ValidateAsync(IList<IGoodConfirmation> goodsToValidate)
        {
            await _reception.ValidateAsync(goodsToValidate);
            _store.Remove(ReceptionKey);
        }

        public async Task<IList<IReceptionGood>> ByBarcodeAsync(string barcodeData, bool ignoreConfirmed = false)
        {
            await RestoreConfirmationProgressAsync();
            var goods = await _reception.ByBarcodeAsync(barcodeData, ignoreConfirmed);
            return goods
                .Select(good => new StatefulReceptionGood(good, GoodsState, barcodeData))
                .ToList<IReceptionGood>();
        }

        /// <summary>
        /// This method triggers goods initialization what causing StatefulConfirmationProgress
        /// to restore the progress.
        /// </summary>
        private async Task RestoreConfirmationProgressAsync()
        {
            if (_goods == null)
            {
                _ = await Goods.ToListAsync();
            }
        }
    }
}
