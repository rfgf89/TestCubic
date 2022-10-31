using PathGame.Interface;
using UnityEngine;
using Zenject;

namespace PathGame.Player
{
   public class PlayerInputController : MonoBehaviour
   {
      private IPlayerInput _playerInput;

      [Inject]
      private void Construct(IPlayerInput playerInput)
      {
         _playerInput = playerInput;
      }

      public IPlayerInput GetInput() => _playerInput;
   }
}
