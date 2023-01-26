namespace Units
{
    public class Scissor : UnitInfo
    {
        protected override void Start()
        {
            damage = 0;

            health = 14;
            defence = 1;
            baseDefence = 1;
            attackReachDistance = 1;
            moveDistance = 2;
            withShield = false;

            offset = new UnityEngine.Vector3(0f, 0.7f);

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
            ChangeAnimation("Scissor", animation: Animation.Attack);
            if (currentStance == Stance.Raging)
                damage++;
        }

        public override void OnDefenceStart()
        {
        }

        public override void OnDefenceEnd(float blockDamage)
        {
            if (blockDamage == 0)
            {
                ChangeAnimation("Scissor", animation: Animation.Block);
            }
            else
            {
                ChangeAnimation("Scissor", animation: Animation.Hit);
            }

            base.OnDefenceEnd(blockDamage);
        }

        public override void OnMoveEnd()
        {
        }
    }
}