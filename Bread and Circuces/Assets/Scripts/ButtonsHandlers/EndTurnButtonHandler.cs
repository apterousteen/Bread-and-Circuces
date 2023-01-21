namespace ButtonsHandlers
{
    public class EndTurnButtonHandler : ButtonHandler
    {
        private ButtonsContainer buttonsContainer;
    
        void Start()
        {
            buttonsContainer = FindObjectOfType<ButtonsContainer>();
        }

        public new void HandleClick()
        {
            State = !State;
        }
    }
}
