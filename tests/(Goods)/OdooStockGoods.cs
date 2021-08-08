using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PortaCapena.OdooJsonRpcClient;

namespace Warehouse.Core.Tests.Goods
{
    public class OdooStockGoods : IGoods
    {
        private readonly OdooClient _client;
        private readonly long _receptionId;
        private readonly IOdooRepository<OdooStockGoodDto> _repository;

        public OdooStockGoods(OdooClient client, long receptionId)
            : this(
                  client,
                  receptionId,
                  new OdooRepository<OdooStockGoodDto>(client.Config)
              )
        {
        }

        public OdooStockGoods(
            OdooClient client,
            long receptionId,
            IOdooRepository<OdooStockGoodDto> repository)
        {
            _client = client;
            _receptionId = receptionId;
            _repository = repository;
        }

        public async Task<IList<IGood>> ToListAsync()
        {
            var goods = await _repository.Query().ToListAsync();
            return goods.Value.Select(
                good => new OddoGood(_client, good)
            ).ToList<IGood>();
        }
    }
}
