using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class FieldDrawer : MonoBehaviour
    {
        private FieldSettings settings;

        void Start()
        {
            settings = FieldSettings.Instance;
        }
    }
}

