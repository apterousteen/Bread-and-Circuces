using UnityEditor;
using UnityEngine;

namespace Units
{
    public class Hoplomachus : UnitInfo
    {
        private DistanceFinder distanceFinder;
        private Animator _animator;
        private SpriteRenderer _spriteRenderer;

        protected override void Start()

        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
            damage = 0;

            health = 15;
            defence = 0;
            attackReachDistance = 2;
            moveDistance = 3;
            withShield = true;
            distanceFinder = FindObjectOfType<DistanceFinder>();
            base.Start();
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
            if (distance == 1 && currentStance == Stance.Attacking)
            {
                _animator.Play("HoplmachusInGameAttack");
                _spriteRenderer.sortingOrder += 1;
                target.damage += 1;
            }
        }

        public override void OnDefenceStart()
        {
            _animator.Play("HoplmachusInGameHit");
            if (_spriteRenderer.sortingOrder > 1)
            {
                _spriteRenderer.sortingOrder -= 1;
            }
        }

        public override void OnDefenceEnd()
        {
            base.OnDefenceEnd();
        }

        public override bool OnMoveStart()
        {
            if (motionType == MotionType.StraightType)
                return true;
            else return false;
        }

        public override void OnMoveEnd()
        {
        }
    }
}