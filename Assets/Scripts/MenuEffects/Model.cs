using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.MenuEffects
{
    using CubeList = List<GameObject>;

    class CubeData : MonoBehaviour
    {
        public Vector3 Destination;
        public float MoveSpeed;
        public float RotateSpeed;

        public void Initialize(Vector3 destination, float moveSpeed)
        {
            Destination = destination;
            MoveSpeed = moveSpeed;

            float cubeSize = gameObject.transform.localScale.x;
            RotateSpeed = MoveSpeed * cubeSize / 180;
        }

        public bool ObjectIsAtDestination()
        {
            return gameObject.transform.position == Destination;
        }

        public void MoveObject(float deltaTime)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, Destination, MoveSpeed * deltaTime);
        }

        public void RotateObject(float deltaTime)
        {
            Vector3 axis = Quaternion.Euler(0, 90, 0) * (Destination - gameObject.transform.position);
            gameObject.transform.Rotate(axis, RotateSpeed * deltaTime, Space.World);
        }
    }

    class EffectsModel : MonoBehaviour
    {
        private const string ObjectName = "Menu Effects Container";
        public CubeList Cubes = new CubeList();

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
        public void AddCubeObject(GameObject cubeObject)
        {
            Cubes.Add(cubeObject);
            cubeObject.transform.parent = cubesContainer.transform;
        }
    }
}