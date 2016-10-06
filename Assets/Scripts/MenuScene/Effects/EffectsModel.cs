using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.MenuScene.Effects
{
    using CubeDataList = List<CubeData>;

    [AddComponentMenu("")]
    class EffectsModel : MonoBehaviour
    {
        private const string ObjectName = "Menu Effects Container";
        public CubeDataList Cubes = new CubeDataList();

        // Holds menu effects container, that is created only once.
        private GameObject cubesContainer;

        public static EffectsModel GetInstance()
        {
            GameObject cubesContainer = GameObject.Find(ObjectName);
            if (cubesContainer == null)
            {
                cubesContainer = new GameObject();
                cubesContainer.name = ObjectName;
                DontDestroyOnLoad(cubesContainer);
                EffectsModel model = cubesContainer.AddComponent<EffectsModel>();
                model.cubesContainer = cubesContainer;
            }
            return cubesContainer.GetComponent<EffectsModel>();
        }

        /**
         * Populates Cubes list and assignes cubeObject's parent as cubesContainer.
         */
        public void AddCubeObject(CubeData cubeObject)
        {
            Cubes.Add(cubeObject);
            cubeObject.transform.parent = cubesContainer.transform;
        }
    }
}
