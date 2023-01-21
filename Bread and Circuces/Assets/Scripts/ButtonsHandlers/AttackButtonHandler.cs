namespace ButtonsHandlers
{
	public class AttackButtonHandler : ButtonHandler
	{
		private ButtonsContainer buttonsContainer;

		void Start()
		{
			buttonsContainer = FindObjectOfType<ButtonsContainer>();
		}

		public new void HandleClick()
		{
			if(!buttonsContainer.moveButton.State)
				State = !State;
		}
	}
}
