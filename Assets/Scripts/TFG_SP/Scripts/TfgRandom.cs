using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//Copyright: Sean FitzGerald (TheFitzyGames) CC BY-SA 4.0
//https://creativecommons.org/licenses/by-sa/4.0/
//modifications, and commercial use are allowed with appropriate credit given

namespace TFG_SP
{
    /// <summary>
    /// A static class for handling rng methods
    /// </summary>
    public static class TfgRandom
    {
        /// <summary>
        /// Returns a bool given a chance between 0 and 1
        /// </summary>
        /// <param name="chance"></param>
        /// <returns></returns>
        public static bool Random01(float chance)
        {
            var rand = Random.Range(0f, 1f + Mathf.Epsilon);

            return rand < chance;
        }


        /// <summary>
        /// Returns a random integer value between 0 and the Max value
        /// </summary>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int ZeroMax(int max)
        {
            return (Random.Range(0, max));
        }


        /// <summary>
        /// Returns a random position inside a unit arc. 
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="direction"></param>
        /// <param name="forward"></param>
        /// <returns></returns>
        public static Vector2 InsideUnitArc(float angle, Vector2 direction, Vector3 forward)
        {
            var halfAngle = angle / 2;
            Vector2 targetDir = Quaternion.AngleAxis(Random.Range(-halfAngle, halfAngle), forward) * direction;
            return targetDir;
        }

        /// <summary>
        /// Returns a random index with the chance being dependant on the weights passed in
        /// </summary>
        /// <param name="weights"></param>
        /// <returns></returns>
        public static int RandomByWeight(float[] weights)
        {
            if (weights == null || weights.Length == 0)
                return -1;

            float w;
            float t = 0;
            int i;
            for (i = 0; i < weights.Length; i++)
            {
                w = weights[i];
                if (float.IsPositiveInfinity(w))
                {
                    return i;
                }
                else if (w >= 0f && !float.IsNaN(w))
                {
                    t += weights[i];
                }
            }

            var r = Random.value;
            var s = 0f;

            for (i = 0; i < weights.Length; i++)
            {
                w = weights[i];
                if (float.IsNaN(w) || w <= 0f)
                    continue;

                s += w / t;
                if (s >= r)
                    return i;
            }

            return -1;
        }


        /// <summary>
        /// Returns a boolean value based on an input between 0 and 100
        /// </summary>
        /// <param name="chance"></param>
        /// <returns></returns>
        public static bool Percent(float chance)
        {
            var clampedChance = Mathf.Clamp(chance, 0f, 100f);

            return clampedChance > UnityEngine.Random.Range(0, 100f);
        }


        private static readonly System.Random Rng = new System.Random();
        /// <summary>
        /// Shuffles a list of elements
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void Shuffle<T>(this IList<T> list)
        {
            var n = list.Count;
            while (n > 1)
            {
                n--;
                var k = Rng.Next(n + 1);
                var value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        /// <summary>
        /// Returns a random position contained within a collider
        /// </summary>
        /// <param name="collider2D"></param>
        /// <param name="maxLoops"></param>
        /// <returns></returns>
        public static Vector2 GetRandomInternalPoint2D(this Collider2D collider2D, int maxLoops = 10)
        {
            for (var i = 0; i < maxLoops; i++)
            {
                var randomPoint = new Vector2(Random.Range(collider2D.bounds.min.x, collider2D.bounds.max.x),
                    Random.Range(collider2D.bounds.min.y, collider2D.bounds.max.y));

                if (collider2D.OverlapPoint(randomPoint))
                {
                    return randomPoint;
                }
            }

            return Vector2.positiveInfinity;
        }


        /// <summary>
        /// Returns a random position contained within a collider3D, Doesn't work the best with mesh colliders
        /// </summary>
        /// <param name="collider"></param>
        /// <param name="maxLoops"></param>
        /// <returns></returns>
        public static Vector3 GetRandomInternalPoint3D(this Collider collider, int maxLoops = 10)
        {
            
            if (collider is MeshCollider meshCollider)
            {
                if (!meshCollider.convex)
                {
                    return collider.bounds.center;
                }
            }

            for (var i = 0; i < maxLoops; i++)
            {
                var randomPoint = new Vector3(Random.Range(collider.bounds.min.x, collider.bounds.max.x),
                    Random.Range(collider.bounds.min.y, collider.bounds.max.y), Random.Range(collider.bounds.min.z, collider.bounds.max.z));

                var overlap = Physics.OverlapSphere(randomPoint, 0.000001f); //.Length > 0;

                if (overlap.Length <= 0) {continue;}

                if (overlap.Contains(collider))
                {
                    return randomPoint;
                }
            }
            
            return Vector3.positiveInfinity;
        }


        /// <summary>
        /// Returns a random element of a sequence
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="elements"></param>
        /// <returns></returns>
        public static T GetRandomElement<T>(IEnumerable<T> elements)
        {
            var enumerable = elements as T[] ?? elements.ToArray();
            if (enumerable.Length == 0)
            {
                return default;
            }

            var choice = Random.Range(0, enumerable.Length);

            return enumerable[choice];
        }


        /// <summary>
        /// Weighted selection for a sequence of elements
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="elements"></param>
        /// <param name="weights"></param>
        /// <returns></returns>
        public static T GetWeightedElement<T>(IEnumerable<T> elements, float[] weights)
        {
            var enumerable = elements as T[] ?? elements.ToArray();
            if (enumerable.Length == 0)
            {
                return default;
            }

            var choice = RandomByWeight(weights);

            var randomIndex = choice; //Random.Range(0, enumerable.Length);
            return enumerable.ElementAt(randomIndex);
        }


        /// <summary>
        /// Returns a list of random, non repeating values from a list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="valueCount"></param>
        /// <returns></returns>
        public static List<T> GetRandomValues<T>(this List<T> source, int valueCount)
        {
            var outList = new List<T>();

            outList.AddRange(source);

            outList.Shuffle();

            valueCount = Mathf.Clamp(valueCount, 0, outList.Count);

            if (valueCount == 0)
            {
                return new List<T>();
            }
            else if (valueCount == outList.Count)
            {
                return outList;
            }

            var diff = outList.Count - valueCount;

            for (var i = 0; i < diff; i++)
            {
                outList.RemoveAt(0);
            }

            return outList;
        }
    }
}