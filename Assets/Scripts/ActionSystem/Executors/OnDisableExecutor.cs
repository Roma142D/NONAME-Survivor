using UnityEngine;

namespace RomaDoliba.ActionSystem
{
    public class OnDisableExecutor : ExecutorBase
    {
        public void OnDisable()
        {
            Execute();
        }
    }
}
