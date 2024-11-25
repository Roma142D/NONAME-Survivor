using System;
using System.Collections;
using System.Collections.Generic;
using RomaDoliba.Player;
using UnityEngine;

namespace RomaDoliba.Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] protected EnemyStats _enemyStats;
        [SerializeField] protected LayerMask _targetLayer;
        [SerializeField] protected SpriteRenderer _enemyRenderer;
        [SerializeField] private SpriteDirection _enemySprites;
        protected Transform _target;
        protected Coroutine _followCoroutine;

        private void Update()
        {
            StartFollowTarget();
            //CheckTargetPosition();
        }
        protected virtual void StartFollowTarget()
        {
            if (Physics2D.CircleCast(this.transform.position, 44f, Vector2.zero, 0f, _targetLayer))
            {
                if (_followCoroutine == null)
                {
                    _followCoroutine = StartCoroutine(FollowTarget(_enemyStats.MoveSpeed));
                }
            }
        }
        protected virtual void CheckTargetPosition()
        {
            var targetPos = _target.position;
            var currentPos = transform.position;
            var heightDif = targetPos.y - currentPos.y;
            
            //targetPos.x > currentPos.x ? _enemyStats.EnemyAnimator.SetTrigger("LookRight") : _enemyStats.EnemyAnimator.SetTrigger("LookLeft");
            
            //_enemyStats.EnemyAnimator.SetBool("LookRight",lookRight);

            if (Mathf.Abs(heightDif) > 4f)
            {
                var lookUp = heightDif > 0;
                _enemyStats.EnemyAnimator.SetTrigger(lookUp ? "LookUp" : "LookDown");
            }
            else
            {
                _enemyStats.EnemyAnimator.SetTrigger(targetPos.x > currentPos.x ? "LookRight" : "LookLeft");
            }

            /*
            _enemyRenderer.sprite = targetPos.x > currentPos.x
                ? _enemySprites.LookRight
                : _enemySprites.LookLeft;
            
            if (Mathf.Abs(heightDif) > 2f)
            {
                _enemyRenderer.sprite = heightDif > 0 ? _enemySprites.LookUp : _enemySprites.LookDown;
            }


            if (_target.position.x > this.transform.position.x)
            {
                _enemyRenderer.sprite = _enemySprites.LookRight;
                if (_target.position.y > this.transform.position.y && (_target.position.y - this.transform.position.y) > 5f)
                {
                    _enemyRenderer.sprite = _enemySprites.LookUp;
                }
                else if (_target.position.y < this.transform.position.y && (this.transform.position.y - _target.position.y) > 5f)
                {
                    _enemyRenderer.sprite = _enemySprites.LookDown;
                }
            }
            else
            {
                _enemyRenderer.sprite = _enemySprites.LookLeft;
                if (_target.position.y > this.transform.position.y && (_target.position.y - this.transform.position.y) > 5f)
                {
                    _enemyRenderer.sprite = _enemySprites.LookUp;
                }
                else if (_target.position.y < this.transform.position.y && (this.transform.position.y - _target.position.y) > 5f)
                {
                    _enemyRenderer.sprite = _enemySprites.LookDown;
                }
            }
            */
        }
        protected virtual IEnumerator FollowTarget(float speed)
        {
            _target = PlayerControler.Instance.transform;
            while (_enemyStats.CurrentHP > 0 && Mathf.Abs((transform.position - PlayerControler.Instance.transform.position).magnitude) > 0.75f)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, PlayerControler.Instance.transform.position, speed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
                CheckTargetPosition();
            }
            _followCoroutine = null;
        }

        private void OnDisable()
        {
            _followCoroutine = null;
        }


        [System.Serializable]
        private struct SpriteDirection
        {
            public Sprite LookRight;
            public Sprite LookLeft;
            public Sprite LookDown;
            public Sprite LookUp;
        }
    }
}
