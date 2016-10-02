using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class MenuController : MonoBehaviour {

        // Use this for initialization
        void Start()
        {
            Initialize();
        }

        protected void Initialize()
        {
            Button menuButton = GameObject.Find("/Canvas/Start Button").GetComponent<Button>();
            menuButton.onClick.AddListener(LoadField);
        }

        protected void LoadField()
        {
            GameObject.Find("/MenuEffects").GetComponent<MenuEffects.Controller>().HideEffects();
            SceneManager.LoadScene(1);
        }
    }
}
