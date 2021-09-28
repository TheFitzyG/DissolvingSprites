using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

//Copyright: Sean FitzGerald (TheFitzyGames) CC BY-SA 4.0
//https://creativecommons.org/licenses/by-sa/4.0/
//modifications, and commercial use are allowed with appropriate credit given

namespace TFG_SP
{
    /// <summary>
    /// A static class for miscellaneous mathematical methods 
    /// </summary>
    public static class TfgMaths
    {
       

        /// <summary>
        /// Swaps the source elements ant indexes a and b and returns true if successful 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool SwapArrayElements(int a, int b, ref int[] source)
        {
            // check for out of range
            if (source.Length <= b || source.Length <= a)
                return false;

            // swap index a and b
            var buffer = source[a];
            source[a] = source[b];
            source[b] = buffer;

            return true;
        }

        /// <summary>
        /// Swaps the source elements ant indexes a and b and returns true if successful 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool SwapArrayElements2D<T>(Vector2Int a, Vector2Int b, ref T[,] source)
        {
            try
            {
                var buffer = source[a.x, a.y];
                source[a.x, a.y] = source[b.x, b.y];
                source[b.x, b.y] = buffer;

                return true;
            }
            catch
            {
                Debug.LogError("Out of bounds");
                return false;
            }
        }

        /// <summary>
        /// Splits a list into smaller lists with a fixed size
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputList"></param>
        /// <param name="nSize"></param>
        /// <returns></returns>
        public static IEnumerable<List<T>> SplitList<T>(List<T> inputList, int nSize = 30)
        {
            for (var i = 0; i < inputList.Count; i += nSize)
            {
                yield return inputList.GetRange(i, Math.Min(nSize, inputList.Count - i));
            }
        }

        /// <summary>
        /// Adds a unique numerical Id suffix to a string. 
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="joiningString"></param>
        /// <returns></returns>
        public static string AddUniqueIdSuffix(this string prefix, string joiningString = "")
        {
            var id = new UniqueId();
            return $"{prefix}{joiningString}({id})";
        }

        /// <summary>
        /// Returns a position along a quadratic curve defined by three vectors
        /// </summary>
        /// <param name="t"></param>
        /// <param name="p0"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static Vector3 GetQuadraticCurvePoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
        {
            var u = 1 - t;
            var tt = t * t;
            var uu = u * u;
            return (uu * p0) + (2 * u * t * p1) + (tt * p2);
        }



        public static Vector3 GetRelativePosition(Transform origin, Vector3 position)
        {
            var distance = position - origin.position;
            var relativePosition = Vector3.zero;
            relativePosition.x = Vector3.Dot(distance, origin.right.normalized);
            relativePosition.y = Vector3.Dot(distance, origin.up.normalized);
            relativePosition.z = Vector3.Dot(distance, origin.forward.normalized);

            return relativePosition;
        }


        /// <summary>
        /// Remaps a value relative to the range difference between A and B
        /// </summary>
        /// <param name="value"></param>
        /// <param name="minA"></param>
        /// <param name="maxA"></param>
        /// <param name="minB"></param>
        /// <param name="maxB"></param>
        /// <returns></returns>
        public static float Remap(this float value, float minA, float maxA, float minB, float maxB)
        {
            return (value - minA) / (maxA - minA) * (maxB - minB) + minB;
        }

        /// <summary>
        /// Returns the next value in an enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="src"></param>
        /// <returns></returns>
        public static T Next<T>(this T src) where T : struct
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException($"Argument {typeof(T).FullName} is not an Enum");

            var Arr = (T[])Enum.GetValues(src.GetType());
            var j = Array.IndexOf<T>(Arr, src) + 1;
            return (Arr.Length == j) ? Arr[0] : Arr[j];
        }



        /// <summary>
        /// A calculation for velocity with acceleration and deceleration
        /// </summary>
        /// <param name="desiredVelocity"></param>
        /// <param name="previousVelocity"></param>
        /// <param name="acceleration"></param>
        /// <param name="deceleration"></param>
        /// <returns></returns>
        public static Vector2 CalculateVelocity2D(Vector2 desiredVelocity, Vector2 previousVelocity, float acceleration, float deceleration)
        {
            //if within 180 degrees and magnitude is greater then can use accelerate
            //if not within 180 degrees or magnitude is smaller then decelerate
            //do it only for magnitude?

            var delta = desiredVelocity.magnitude > previousVelocity.magnitude ? acceleration : deceleration;
            var calculatedVelocity = Vector2.Lerp(previousVelocity, desiredVelocity, delta * Time.fixedDeltaTime);

            return calculatedVelocity;
        }


        /// <summary>
        /// A calculation for velocity with acceleration and deceleration
        /// </summary>
        /// <param name="desiredVelocity"></param>
        /// <param name="previousVelocity"></param>
        /// <param name="acceleration"></param>
        /// <param name="deceleration"></param>
        /// <returns></returns>
        public static Vector3 CalculateVelocity3D(Vector3 desiredVelocity, Vector3 previousVelocity, float acceleration, float deceleration)
        {
            //if within 180 degrees and magnitude is greater then can use accelerate
            //if not within 180 degrees or magnitude is smaller then decelerate
            //do it only for magnitude?

            var delta = desiredVelocity.magnitude > previousVelocity.magnitude ? acceleration : deceleration;
            var calculatedVelocity = Vector3.Lerp(previousVelocity, desiredVelocity, delta * Time.fixedDeltaTime);

            return calculatedVelocity;
        }
    }
}
