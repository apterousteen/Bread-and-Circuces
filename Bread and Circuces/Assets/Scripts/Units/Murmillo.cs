using UnityEngine;

namespace Units
{
    public class Murmillo : UnitInfo
    {
        private TurnManager turnManager;

        protected override void Start()
        {
            damage = 0;

            health = 15;
            defence = 0;
            attackReachDistance = 1;
            moveDistance = 3;
            withShield = true;

            offset = new UnityEngine.Vector3(0f, 0.4f);

            turnManager = FindObjectOfType<TurnManager>();
            base.Start();
        }

        public override void OnAttackEnd(UnitInfo target)
        {
            base.OnAttackEnd(target);
        }

        public override void OnAttackStart(UnitInfo target)
        {
            ChangeAnimation("Murmillo", animation: Animation.Attack);
        }

        public override void OnDefenceStart()
        {
            if (turnManager.defCardPlayed)
                defence++;
        }

        public override void OnDefenceEnd(float blockDamage)
        {
            if (blockDamage == 0)
            {
                ChangeAnimation("Murmillo", animation: Animation.Block);
            }
            else
            {
                ChangeAnimation("Murmillo", animation: Animation.Hit);
            }

            base.OnDefenceEnd(blockDamage);
        }

        public override void OnMoveEnd()
        {
        }
    }
}