using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class MenuController : MonoBehaviour
    {

        [SerializeField] private Button startButton;

        // Use this for initialization
        void Start()
        {
            Initialize();
        }

        void OnDestroy()
        {
            startButton.onClick.RemoveAllListeners();
        }

        protected void Initialize()
        {
            startButton.onClick.AddListener(LoadField);
        }

        protected void LoadField()
        {
            GameObject.Find("/MenuEffects").GetComponent<MenuEffects.Controller>().HideEffects();
            SceneManager.LoadScene(1);
        }
    }
}
