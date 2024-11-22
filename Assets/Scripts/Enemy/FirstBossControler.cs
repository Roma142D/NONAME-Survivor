using System.Collections;
using RomaDoliba.Player;
using UnityEngine;

namespace RomaDoliba.Enemy
{
    public class FirstBossControler : EnemyMovement
    {
        private Coroutine _attackCoroutine;
        private float testCooldown = 5f;
        private void Update()
        {
            testCooldown -= Time.deltaTime;
            if (testCooldown <= 0)
            {
                StartFollowTarget();
                testCooldown = 5f;
            }
        }
        private void StartJumpAttack()
        {
            if (_attackCoroutine == null)
            {
                _attackCoroutine = StartCoroutine(JumpAttack());
            }
        }
        private IEnumerator JumpAttack()
        {
            _enemyStats.CurrentDamage = _enemyStats.CurrentDamage * 2f;
            yield return new WaitForSeconds(2f);
            _enemyStats.CurrentDamage = _enemyStats.CurrentDamage * 0.5f;
            _attackCoroutine = null;
        }
        protected override void StartFollowTarget()
        {
            if (Physics2D.CircleCast(this.transform.position, 44f, Vector2.zero, 0f, _targetLayer))
            {
                if (_followCoroutine == null && _attackCoroutine == null)
                {
                    Debug.Log("StartFollowPlayer");
                    
                    _followCoroutine = StartCoroutine(FollowTarget(_enemyStats.MoveSpeed));
                }
            }
        }
        protected override IEnumerator FollowTarget(float speed)
        {
            _target = PlayerControler.Instance.transform;
            _enemyStats.EnemyAnimator.SetTrigger("Walk");
            while (Mathf.Abs((transform.position - PlayerControler.Instance.transform.position).magnitude) > 2f)
            {
                CheckTargetPosition();
                this.transform.position = 
                Vector3.MoveTowards(this.transform.position, PlayerControler.Instance.transform.position, speed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
            _enemyStats.EnemyAnimator.SetTrigger("JumpAttack");
            yield return new WaitForSeconds(1f);
            _followCoroutine = null;
        }
        protected override void CheckTargetPosition()
        {
            _enemyRenderer.flipX = _target.position.x < transform.position.x;
        }
    }
}
