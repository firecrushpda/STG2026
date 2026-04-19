using System.Collections.Generic;
using Battle;

namespace GameManager
{
    using UnityEngine;
    
    public class ChessManager : MonoBehaviour
    {
        public static ChessManager Instance { get; private set; }
        
        public List<Chess> ChessInBattle;
        public List<Chess> ChessInRepository;
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);

        }

        private void Start()
        {
            ChessInBattle = new List<Chess>();
            ChessInRepository = new List<Chess>();
        }
        
        
    }
}