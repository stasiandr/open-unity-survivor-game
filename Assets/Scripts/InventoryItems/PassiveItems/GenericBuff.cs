using System;
using Player;
using VContainer;

namespace InventoryItems.PassiveItems
{
    public class GenericBuff : IDisposable
    {
        private readonly Action<PlayerModel> _action;

        public GenericBuff(Action<PlayerModel> action)
        {
            _action = action;
        }

        [Inject]
        public void Construct(PlayerModel playerModel)
        {
            _action.Invoke(playerModel);
        }

        public void Dispose()
        {
        }
    }
}