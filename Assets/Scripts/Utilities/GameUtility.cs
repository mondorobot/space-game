using UnityEngine;

namespace Assets.Scripts.lib.Utilities
{
    public class GameUtility
    {
        private static GameObject _player;
        public static GameObject Player
        {
            get { return _player ?? (_player = GameObject.FindWithTag("Player")); }
        }
    }
}
