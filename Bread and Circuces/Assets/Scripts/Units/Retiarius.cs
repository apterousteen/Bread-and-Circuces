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

            offset = new UnityEngine.Vector3(0f, 0.4f);

            base.Start();

            gameManager = FindObjectOfType<GameManagerScript>();
        }

        public override void OnAttackEnd(UnitInfo target)
        {
            base.OnAttackEnd(target);
        }

        public override void OnAttackStart(UnitInfo target)
        {
            ChangeAnimation("Retiar", animation: Animation.Attack);
            //ChangeAnimationAttack(gameObject.name);
        }

        public override void OnDefenceStart()
        {
        }

        public override void OnDefenceEnd(float blockDamage)
        {
            if (blockDamage == 0)
            {
                ChangeAnimation("Retiar", animation: Animation.Block);
                //ChangeAnimationBlock(gameObject.name);
            }
            else
            {
                ChangeAnimation("Retiar", animation: Animation.Hit);
                //ChangeAnimationHit(gameObject.name);
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
