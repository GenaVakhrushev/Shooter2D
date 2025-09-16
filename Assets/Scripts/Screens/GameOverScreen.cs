using DI.Attributes;
using Shooter.GameManagement;
using UnityEngine;
using UnityEngine.UI;

namespace Shooter.Screens
{
    public class GameOverScreen : MonoBehaviour
    {
        [SerializeField] private Button restartButton;
        
        [Inject] private GameManager gameManager;

        [Inject]
        private void Initialize()
        {
            gameManager.GameLost += GameManagerOnGameLost;
            
            restartButton.onClick.AddListener(gameManager.Restart);
            restartButton.onClick.AddListener(() => gameObject.SetActive(false));
        }

        private void GameManagerOnGameLost()
        {
            gameObject.SetActive(true);
        }
    }
}