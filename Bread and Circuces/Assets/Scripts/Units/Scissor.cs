namespace Units
{
    public class Scissor : UnitInfo
    {
        protected override void Start()
        {
            damage = 0;

            health = 14;
            defence = 1;
            attackReachDistance = 1;
            moveDistance = 2;
            withShield = false;

            base.Start();
        }

        public override void ChangeStance(Stance newStance)
        {
            if (currentStance == Stance.Raging && newStance == Stance.Attacking
                || currentStance == Stance.Raging && newStance == Stance.Raging)
                return;
            if (newStance == Stance.Defensive)
                newStance = Stance.Advance;
            base.ChangeStance(newStance);
        }

        public override void OnAttackEnd(UnitInfo target)
        {
            base.OnAttackEnd(target);
        }

        public override void OnAttackStart(UnitInfo target)
        {
            if (currentStance == Stance.Raging)
                damage++;
        }

        public override void OnDefenceStart()
        {

        }

        public override void OnDefenceEnd()
        {
            base.OnDefenceEnd();
        }

        public override void OnMoveEnd()
        {

        }
    }
}
