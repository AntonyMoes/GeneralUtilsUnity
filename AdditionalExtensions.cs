using UnityEngine;

namespace GeneralUtils {
    public static class AdditionalExtensions {
        #region Color

        public static Color WithAlpha(this Color color, float alpha) {
            var result = color;
            result.a = alpha;
            return result;
        }

        #endregion

        #region Vector3

        public static Vector3 With(this Vector3 vector, float? x = null, float? y = null, float? z = null) {
            var newVector = vector;

            if (x is { } newX) {
                newVector.x = newX;
            }

            if (y is { } newY) {
                newVector.y = newY;
            }

            if (z is { } newZ) {
                newVector.z = newZ;
            }

            return newVector;
        }

        #endregion

        #region Vector3Int

        public static Vector3Int With(this Vector3Int vector, int? x = null, int? y = null, int? z = null) {
            var newVector = vector;

            if (x is { } newX) {
                newVector.x = newX;
            }

            if (y is { } newY) {
                newVector.y = newY;
            }

            if (z is { } newZ) {
                newVector.z = newZ;
            }

            return newVector;
        }

        #endregion

        #region Vector2

        public static Vector2 With(this Vector2 vector, float? x = null, float? y = null) {
            var newVector = vector;

            if (x is { } newX) {
                newVector.x = newX;
            }

            if (y is { } newY) {
                newVector.y = newY;
            }

            return newVector;
        }

        #endregion
    }
}
