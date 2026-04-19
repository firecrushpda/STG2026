namespace Battle.Data
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "ChessData", menuName = "Game/ChessData")]
    public class ChessData : ScriptableObject
    {
        public int Id;
        public int Hp;
        public int Cost;
        public int Level;
    }
}