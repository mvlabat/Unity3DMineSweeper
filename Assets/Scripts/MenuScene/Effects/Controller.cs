using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.MenuScene.Effects
{
    using CubeList = List<GameObject>;

    public class Controller : MonoBehaviour
    {
        private static Material cubeMaterial; // TODO: Get rid of this after implementing MineSweeper cube view class.

        [SerializeField] private float CubeSpawnPeriod = 3;
        [SerializeField] private float CubeMinSize = 45;
        [SerializeField] private float CubeMaxSize = 75;
        [SerializeField] private float MinScreenPassTime = 5;
        [SerializeField] private float MaxScreenPassTime = 10;

        private EffectsModel model;
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
            if (cubeMaterial == null)
            {
                cubeMaterial = Resources.Load("Materials/MenuEffect") as Material;
            }

            model = EffectsModel.GetInstance();

            // If returning to the menu, show the previously spawned cubes.
            ShowEffects();
            // Add the first cube instantly.
            AddCube();
        }

        /**
         * Animate (move and rotate) cubes and remove the passed ones.
         */
        private void UpdateCubes()
        {
            CubeList newList = new CubeList();
            foreach (GameObject cubeObject in model.Cubes)
            {
                CubeData cubeData = cubeObject.GetComponent<CubeData>();
                cubeData.MoveObject(Time.deltaTime);
                if (cubeData.ObjectIsAtDestination())
                {
                    Destroy(cubeObject);
                }
                else
                {
                    cubeData.RotateObject(Time.deltaTime);
                    newList.Add(cubeObject);
                }
            }
            model.Cubes = newList;
        }

        /**
         * Activates all the cubes previosly added on the scene.
         */
        private void ShowEffects()
        {
            foreach (GameObject cubeObject in model.Cubes)
            {
                cubeObject.SetActive(true);
            }
        }

        /**
         * Deactivates all the cubes added on the scene.
         */
        public void HideEffects()
        {
            foreach (GameObject cubeObject in model.Cubes)
            {
                cubeObject.SetActive(false);
            }
        }

        private void SpawnCube()
        {
            spawnTimer = spawnTimer + Time.deltaTime;
            if (spawnTimer > CubeSpawnPeriod)
            {
                spawnTimer -= CubeSpawnPeriod;
                AddCube();
            }
        }

        private void AddCube()
        {
            float cubeSize = Random.Range(CubeMinSize, CubeMaxSize);

            // Get min-max viewport Z.
            float minZ = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane)).z + cubeSize;
            float maxZ = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.farClipPlane)).z + cubeSize;

            float cubeZ = Random.Range(minZ + cubeSize, maxZ - cubeSize);
            float viewCubeZ = Camera.main.WorldToViewportPoint(new Vector3(0, 0, cubeZ)).z;
            // We intentionally set cube less deeper, otherwise its corner will stick out.
            cubeZ -= cubeSize;

            Vector3 bottomLeftPoint = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, viewCubeZ));
            float destCubeX = bottomLeftPoint.x;
            float minY = bottomLeftPoint.y;

            Vector3 topRightPoint = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, viewCubeZ));
            float maxY = topRightPoint.y;
            float cubeX = topRightPoint.x;

            float cubeY = Random.Range(minY, maxY);

            Vector3 position = new Vector3(cubeX + cubeSize, cubeY, cubeZ);
            Vector3 destination = new Vector3(destCubeX - cubeSize, cubeY, cubeZ);
            CreateCube(position, destination, cubeSize);
        }

        private void CreateCube(Vector3 position, Vector3 destination, float cubeSize)
        {
            GameObject cubeObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            MeshRenderer renderer = cubeObject.GetComponent<MeshRenderer>();
            renderer.material = cubeMaterial;

            cubeObject.transform.position = position;
            cubeObject.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);

            cubeObject.transform.rotation = Random.rotation;

            float passDistance = Vector3.Distance(position, destination);
            float passTime = Random.Range(MinScreenPassTime, MaxScreenPassTime);

            CubeData cubeData = cubeObject.AddComponent<CubeData>();
            cubeData.Initialize(destination, passDistance / passTime);
            model.AddCubeObject(cubeObject);
        }
    }
}
