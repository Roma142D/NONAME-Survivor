using UnityEngine;
using UnityEngine.SceneManagement;

namespace RomaDoliba.UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private bool _changeTimeScale;
        public void ToggleScreen(GameObject screen)
        {
            
            if (screen.activeSelf)
            {
                screen.SetActive(false);
            }
            else
            {
                screen.SetActive(true);
            }
            
            if (_changeTimeScale && Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }
            else if (_changeTimeScale && Time.timeScale == 1)
            {
                Time.timeScale = 0;
            }
            
        }
        public void ChangeScene(string name)
        {
            SceneManager.LoadScene(name);
        }
    }
}

