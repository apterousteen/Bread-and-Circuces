namespace Units
{
    public class Retiarius : UnitInfo
    {
        private GameManagerScript gameManager;

        protected override void Start()
        {
            damage = 0;

            health = 16;
            defence = 0;
            attackReachDistance = 2;
            moveDistance = 3;
            withShield = false;

            base.Start();

            gameManager = FindObjectOfType<GameManagerScript>();
        }

        public override void OnAttackEnd(UnitInfo target)
        {
            base.OnAttackEnd(target);
        }

        public override void OnAttackStart(UnitInfo target)
        {
            ChangeAnimationAttack(gameObject.name);
        }

        public override void OnDefenceStart()
        {
        }

        public override void OnDefenceEnd(float blockDamage)
        {
            if (blockDamage == 0)
            {
                ChangeAnimationBlock(gameObject.name);
            }
            else
            {
                ChangeAnimationHit(gameObject.name);
            }
            base.OnDefenceEnd(blockDamage);
        }

        public override void OnMoveEnd()
        {
            if (motionType == MotionType.StraightType)
                gameManager.DrawCards(teamSide, 1); // Draw Card
        }
    }
}
