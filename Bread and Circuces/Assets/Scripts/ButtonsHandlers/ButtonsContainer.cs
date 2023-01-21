using UnityEngine;

namespace ButtonsHandlers
{
    public class ButtonsContainer : MonoBehaviour
    {
        public MoveButtonHandler moveButton;
        private TurnManager turnManager;

        void Start()
        { 
            moveButton = FindObjectOfType<MoveButtonHandler>();
            turnManager = FindObjectOfType<TurnManager>();
            DeactivateUnitButtons();
        }

        public int GetAction()
        {
            if(moveButton.State)
            {
                return 1;
            }

            return -1;
        }

        public void EndAction()
        {
            if(moveButton.State)
            {
                moveButton.State = !moveButton.State;
            }
        }    

        public void ActivateUnitButtons()
        {
            if (turnManager.currTeam == Team.Enemy)
                return;
            moveButton.ActivateDeactivateButton(true);
        }
    
        public void DeactivateUnitButtons()
        {
            moveButton.resetButton();
        }
    }
}
