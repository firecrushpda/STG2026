using Battle.Data;
using UnityEngine;

namespace Battle
{
    public class Chess
    {
        [Header("配置数据")]
        public ChessData Data;
        
        private int _id;
        private int _currentHp;

        public bool IsAlive => _currentHp > 0;

        public void Init(ChessData chessData)
        {
            Data = chessData;

            _id = Data.Id;
            _currentHp = Data.Hp;
        }
    }
}