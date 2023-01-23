using UnityEngine;

namespace ButtonsHandlers
{
    public class ButtonsContainer : MonoBehaviour
    {
        public MoveButtonHandler moveButton;
        public EndTurnButtonHandler endButton;
        private TurnManager turnManager;

        void Start()
        { 
            moveButton = FindObjectOfType<MoveButtonHandler>();
            endButton = FindObjectOfType<EndTurnButtonHandler>();
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
            endButton.ActivateDeactivateButton(true);
        }
    
        public void DeactivateUnitButtons()
        {
            moveButton.resetButton();
            endButton.resetButton();
        }


    }
}
