using UnityEngine;

namespace Tools
{
    public static class UnityVectorsTools
    {
        public static Vector2 RadianToVector2(float radian)
        {
            return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
        }

        public static Vector2 DegreeToVector2(float degree)
        {
            return RadianToVector2(degree * Mathf.Deg2Rad);
        }

        public static Vector2 Rotate(Vector2 v, float delta)
        {
            return new Vector2(
                v.x * Mathf.Cos(delta) - v.y * Mathf.Sin(delta),
                v.x * Mathf.Sin(delta) + v.y * Mathf.Cos(delta)
            );
        }

        public static Vector2 RandomVector2Normal()
        {
            return DegreeToVector2(Random.Range(0f, 360f));
        }

        public static Vector2 RandomVector2(Vector2 point1, Vector2 point2)
        {
            return new Vector2(Random.Range(point1.x, point2.x), Random.Range(point1.y, point2.y));
        }

        public static Vector3 RandomVector3(Vector3 point1, Vector3 point2)
        {
            return new Vector3(Random.Range(point1.x, point2.x), Random.Range(point1.y, point2.y), Random.Range(point1.z, point2.z));
        }

    }

}
