using UnityEngine;

namespace RomaDoliba.ActionSystem
{
    public class ToggleActive : ActionBase
    {
        [SerializeField] private GameObject[] _objectsToToggle;

        public override void Execute()
        {
            foreach (var objectToToggle in _objectsToToggle)
            {
                if (!objectToToggle.activeSelf)
                {
                    objectToToggle.SetActive(true);
                }
                else
                {
                    objectToToggle.SetActive(false);
                }
            }
            
        }
    }
}