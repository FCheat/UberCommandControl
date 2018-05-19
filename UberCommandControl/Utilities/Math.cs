using UnityEngine;

namespace UberCommandControl.Utilities
{
    public class Math
    {
        //**SQRT Function Removed For Unsafe Permisions**//
        public float FSQRT(float number, float precision) => 0f;

        //**VECDST Function Removed For Unsafe Permissions**//
        public float VectorDistance(Vector3 PositionA, Vector3 PositionB)
        {
            return UnityEngine.Vector3.Distance(PositionA, PositionB);
        }
    }
}
