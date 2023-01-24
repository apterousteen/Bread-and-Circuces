using UnityEditor;
using UnityEngine;

namespace Units
{
    public class Hoplomachus : UnitInfo
    {
        private DistanceFinder distanceFinder;

        protected override void Start()

        {
            damage = 0;

            health = 15;
            defence = 0;
            attackReachDistance = 2;
            moveDistance = 3;
            withShield = true;
            distanceFinder = FindObjectOfType<DistanceFinder>();

            offset = new UnityEngine.Vector3(0f, 0.4f);
            base.Start();
            Debug.Log(this.gameObject.name);
        }

        public override void OnAttackEnd(UnitInfo target)
        {
            base.OnAttackEnd(target);
        }

        public override void OnAttackStart(UnitInfo target)
        {
            var occupiedHex = transform.parent.GetComponent<HexTile>();
            var targetHex = target.transform.parent.GetComponent<HexTile>();
            var distance = distanceFinder.GetDistanceBetweenHexes(occupiedHex, targetHex);
            ChangeAnimation("Hoplomachus", animation: Animation.Attack);
            // ChangeAnimationAttack(gameObject.name);
            if (distance == 1 && currentStance == Stance.Attacking)
            {
                damage += 1;
            }
        }

        public override void OnDefenceStart()
        {
        }

        public override void OnDefenceEnd(float blockDamage)
        {
            if (blockDamage == 0)
            {
                ChangeAnimation("Hoplomachus", animation: Animation.Block);
                //ChangeAnimationBlock(gameObject.name);
            }
            else
            {
                ChangeAnimation("Hoplomachus", animation: Animation.Hit);
                //ChangeAnimationHit(gameObject.name);
            }

            base.OnDefenceEnd(blockDamage);
        }

        public override void OnMoveEnd()
        {
        }
    }
}