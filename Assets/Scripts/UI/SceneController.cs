using UnityEngine;
using UnityEngine.SceneManagement;

namespace RomaDoliba.UI
{
    public class SceneController : MonoBehaviour
    {
        public void ChangeScene(string name)
        {
            SceneManager.LoadScene(name);
        }
    }
}
