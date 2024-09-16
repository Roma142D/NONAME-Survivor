using UnityEngine;

namespace RomaDoliba.ActionSystem
{
    public class OnCollideWith : ConditionBase
    {
        [SerializeField] private LayerMask _collideWith;

        public override bool Check(object data = null)
        {
            if (data == null)
            {
                return false;
            }

            if (data is Collider2D collider)
            {
                return _collideWith == (_collideWith | (1 << collider.gameObject.layer));
            }

            if (data is Collision2D collision)
            {
                return _collideWith == (_collideWith | (1 << collision.gameObject.layer));
            }

            return false;
        }
    }
}
