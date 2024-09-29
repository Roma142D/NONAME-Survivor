using System.Collections;
using System.Collections.Generic;
using RomaDoliba.Player;
using UnityEngine;

namespace RomaDoliba.Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private EnemyStats _enemyStats;
        [SerializeField] private LayerMask _targetLayer;
        [SerializeField] private SpriteRenderer _enemyRenderer;
        [SerializeField] private SpriteDirection _enemySprites;
        private Transform _target;
        private Coroutine _followCoroutine;

        private void Update()
        {
            if (Physics2D.CircleCast(this.transform.position, 44f, Vector2.zero, 0f, _targetLayer))
            {
                if (_followCoroutine == null)
                {
                    _followCoroutine = StartCoroutine(FollowTarget(_enemyStats.MoveSpeed));
                }
            }
        }
        private void CheckTargetPosition()
        {
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
        }
        private IEnumerator FollowTarget(float speed)
        {
            _target = PlayerControler.Instance.transform;
            while (this.transform.position != PlayerControler.Instance.transform.position)
            {
                CheckTargetPosition();
                this.transform.position = Vector3.MoveTowards(this.transform.position, PlayerControler.Instance.transform.position, speed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
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
