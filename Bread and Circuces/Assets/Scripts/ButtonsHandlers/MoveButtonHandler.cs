namespace ButtonsHandlers
{
    public class MoveButtonHandler : ButtonHandler
    {
        private ButtonsContainer buttonsContainer;


        public new void HandleClick()
        {
            //if(!buttonsContainer.attackButton.State)
            //State = !State;
            var turnManager = FindObjectOfType<TurnManager>();
            FindObjectOfType<GameManagerScript>().ReduceMana(true, 1);
            turnManager.AddAction(new Action(ActionType.DiscardForMove, Team.Player, 1));
            turnManager.AddAction(new Action(ActionType.Move, Team.Player, turnManager.activeUnit.GetComponent<UnitInfo>().moveDistance));
            turnManager.AddAction(new Action(ActionType.Draw, Team.Player, 1));
            turnManager.EndAction();
        }
    }
}
