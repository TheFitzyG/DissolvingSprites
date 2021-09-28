using System.Collections.Generic;
using UnityEngine;


//Copyright: Sean FitzGerald (TheFitzyGames) CC BY-SA 4.0
//https://creativecommons.org/licenses/by-sa/4.0/
//modifications, and commercial use are allowed with appropriate credit given


namespace TFG_SP
{
    /// <summary>
    /// A static class for transformation extension methods
    /// </summary>
    public static class TfgTransform
    {
        /// <summary>
        /// Resets the Position, rotation and scale of a transform
        /// </summary>
        /// <param name="T"></param>
        public static void ResetTransform(this Transform T)
        {
            T.localEulerAngles = Vector3.zero;
            T.localPosition = Vector3.zero;
            T.localScale = Vector3.one;
        }

        /// <summary>
        /// Destroys all child GameObjects of a transform
        /// </summary>
        /// <param name="T"></param>
        public static void DestroyAllChildren(this Transform T)
        {
            foreach (Transform child in T)
            {
                Object.Destroy(child.gameObject);
            }
        }

        /// <summary>
        /// Sets the active state every child GameObject of a transform
        /// </summary>
        /// <param name="T"></param>
        /// <param name="activeState"></param>
        public static void SetActiveAllChildren(this Transform T, bool activeState = false)
        {
            foreach (Transform child in T)
            {
                child.gameObject.SetActive(activeState);
            }
        }


        /// <summary>
        /// Returns the transform in the list with a word position closest to the world position of the currentTransform
        /// </summary>
        /// <param name="list"></param>
        /// <param name="currentTransform"></param>
        /// <returns></returns>
        public static Transform GetNearestTransform(IList<Transform> list, Transform currentTransform)
        {
            Transform tMin = null;
            var minDist = Mathf.Infinity;
            var currentPos = currentTransform.position;
            foreach (var t in list)
            {
                var dist = Vector3.Distance(t.position, currentPos);
                if (!(dist < minDist))
                {
                    continue;
                }

                tMin = t;
                minDist = dist;
            }

            return tMin;
        }


        /// <summary>
        /// Returns the transform in the list with a word position closest to the world position of the currentTransform
        /// </summary>
        /// <param name="list"></param>
        /// <param name="currentTransform"></param>
        /// <returns></returns>
        public static Transform GetNearestTransform(IList<MonoBehaviour> list, Transform currentTransform)
        {
            Transform tMin = null;
            var minDist = Mathf.Infinity;
            var currentPos = currentTransform.position;
            foreach (var t in list)
            {
                var dist = Vector3.Distance(t.transform.position, currentPos);
                if (!(dist < minDist))
                {
                    continue;
                }

                tMin = t.transform;
                minDist = dist;
            }

            return tMin;
        }

        /// <summary>
        /// Returns the transform in a list closest to the screens center
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static Transform GetTransformClosestToScreenCenter(IList<Transform> list)
        {
            Transform tMin = null;
            var minDist = Mathf.Infinity;

            foreach (var transform in list)
            {
               var dist= Camera.main.WorldToViewportPoint(transform.position);
               dist.z = 0;

               if (dist.magnitude < minDist)
               {
                   minDist = dist.magnitude;
                   tMin = transform;
               }
            }

            return tMin;
        }


        /// <summary>
        /// Converts a RectTransforms local Rect into a rect in world coordinates
        /// </summary>
        /// <param name="rt"></param>
        /// <returns></returns>
        public static Rect ToWorldSpaceRect(this RectTransform rt)
        {
            var r = rt.rect;
            r.center = rt.TransformPoint(r.center);
            r.size = rt.TransformVector(r.size);
            return r;
        }




        public static Vector3Int ToVector3Int(this Vector3 vector3)
        {
            var x = Mathf.FloorToInt(vector3.x);
            var y = Mathf.FloorToInt(vector3.y);
            var z = Mathf.FloorToInt(vector3.z);

            return new Vector3Int(x, y, z);
        }

        public static Vector2Int ToVector2Int(this Vector2 vector2)
        {
            var x = Mathf.FloorToInt(vector2.x);
            var y = Mathf.FloorToInt(vector2.y);
            
            return new Vector2Int(x, y);
        }


        public static Vector2Int ToVector2Int(this Vector3 vector3)
        {
            var x = Mathf.FloorToInt(vector3.x);
            var y = Mathf.FloorToInt(vector3.y);

            return new Vector2Int(x, y);
        }

        public static Vector2Int ToCardinal(this Vector2Int vec)
        {

            if (Mathf.Abs(vec.x) > Mathf.Abs(vec.y))
            {
                if (vec.x > 0)
                {
                    return Vector2Int.right;
                }
                else
                {
                    return Vector2Int.left;
                }
            }
            else
            {
                if (vec.y > 0)
                {
                    return Vector2Int.up;
                }
                else
                {
                    return Vector2Int.down;
                }
            }
        }

    }
}