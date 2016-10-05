using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class FieldController : MonoBehaviour
    {
        protected FieldSettings settings;
        protected FieldDrawer fieldDrawer;

        [SerializeField] private Button menuButton;

        // Use this for initialization
        void Start()
        {
            Initialize();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnDestroy()
        {
            menuButton.onClick.RemoveAllListeners();
        }

        protected void Initialize()
        {
            settings = FieldSettings.Instance;
            fieldDrawer = gameObject.AddComponent<FieldDrawer>();
            InitializeMenu();
        }

        protected void InitializeMenu()
        {
            menuButton.onClick.AddListener(LoadMenu);
        }

        protected void LoadMenu()
        {
            SceneManager.LoadScene(0);
        }
    }
}
