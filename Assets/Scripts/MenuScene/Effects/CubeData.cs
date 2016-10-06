using UnityEngine;

namespace Assets.Scripts.MenuScene.Effects
{
    [AddComponentMenu("")]
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
}
