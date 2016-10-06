using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.MenuScene.Effects
{
    using CubeDataList = List<CubeData>;

    public class Controller : MonoBehaviour
    {
        [SerializeField] private float CubeSpawnPeriod = 3;

        private EffectsModel model;
        private CubeFactory cubeFactory;
        private float spawnTimer = 0;

        void Start()
        {
            Initialize();
        }

        void Update()
        {
            UpdateCubes();
            SpawnCube();
        }

        private void Initialize()
        {
            model = EffectsModel.GetInstance();
            cubeFactory = gameObject.GetComponent<CubeFactory>();
            cubeFactory.Initialize();

            // If returning to the menu, show the previously spawned cubes.
            ShowEffects();
            // Add the first cube instantly.
            cubeFactory.CreateCube();
        }

        /**
         * Animate (move and rotate) cubes and remove the passed ones.
         */
        private void UpdateCubes()
        {
            CubeDataList newList = new CubeDataList();
            foreach (CubeData cube in model.Cubes)
            {
                CubeData cubeData = cube.GetComponent<CubeData>();
                cubeData.MoveObject(Time.deltaTime);
                if (cubeData.ObjectIsAtDestination())
                {
                    Destroy(cube.gameObject);
                }
                else
                {
                    cubeData.RotateObject(Time.deltaTime);
                    newList.Add(cube);
                }
            }
            model.Cubes = newList;
        }

        /**
         * Activates all the cubes previosly added on the scene.
         */
        private void ShowEffects()
        {
            foreach (CubeData cube in model.Cubes)
            {
                cube.gameObject.SetActive(true);
            }
        }

        /**
         * Deactivates all the cubes added on the scene.
         */
        public void HideEffects()
        {
            foreach (CubeData cube in model.Cubes)
            {
                cube.gameObject.SetActive(false);
            }
        }

        private void SpawnCube()
        {
            spawnTimer = spawnTimer + Time.deltaTime;
            if (spawnTimer > CubeSpawnPeriod)
            {
                spawnTimer -= CubeSpawnPeriod;
                cubeFactory.CreateCube();
            }
        }
    }
}
