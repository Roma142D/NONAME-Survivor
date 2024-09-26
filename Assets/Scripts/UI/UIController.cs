using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RomaDoliba.UI
{
    public class UIController : MonoBehaviour
    {
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
            
        }
        public void ChangeScene(string name)
        {
            SceneManager.LoadScene(name);
        }
    }
}

