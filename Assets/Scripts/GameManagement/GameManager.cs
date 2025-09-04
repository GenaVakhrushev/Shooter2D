using DI.Attributes;
using Shooter.Services;

namespace Shooter.GameManagement
{
    public class GameManager
    {
        [Inject] private PlayerService playerService;

        public void StartGame()
        {
            playerService.SpawnPlayerView();
        }
    }
}