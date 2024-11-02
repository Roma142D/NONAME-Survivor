using System.Collections;
using UnityEngine;

namespace RomaDoliba.Enemy
{
    public class FirstBossControler : EnemyMovement
    {
        private float testCooldown = 5f;
        private void Update()
        {
            testCooldown -= Time.deltaTime;
            if (testCooldown <= 0)
            {
                _enemyStats.EnemyAnimator.SetTrigger("JumpAttack");
                testCooldown = 5f;
            }
        }
        private IEnumerator JumpAttack()
        {
            Debug.Log("JumpAttack");
            return null;
        }
    }
}
