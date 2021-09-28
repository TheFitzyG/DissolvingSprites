using UnityEngine;

//Copyright: Sean FitzGerald (TheFitzyGames) CC BY-SA 4.0
//https://creativecommons.org/licenses/by-sa/4.0/
//modifications, and commercial use are allowed with appropriate credit given

namespace TFG_SP
{
    /// <summary>
    /// A static class containing methods for comparisons
    /// </summary>
    public static class TfgChecks
    {
        /// <summary>
        /// Checks if the gameObject's layer is within the layermask
        /// </summary>
        /// <param name="mask"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static bool CompareLayers(LayerMask mask, GameObject other)
        {
            return (mask & 1 << other.layer) == 1 << other.layer;
        }

        /// <summary>
        /// Checks line of sight between two transforms taking a layermask of blocking objects into account (3D)
        /// </summary>
        /// <param name="blockingMask"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static bool LineOfSight3D(LayerMask blockingMask, Transform from, Transform to)
        {
            return Physics.Linecast(from.position, to.position,blockingMask);
        }

        /// <summary>
        /// Checks line of sight between two transforms taking a layermask of blocking objects into account (2D)
        /// </summary>
        /// <param name="blockingMask"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static bool LineOfSight2D(LayerMask blockingMask, Transform from, Transform to)
        {
            var toPosition = to.position;
            var fromPosition = from.position;
            var direction = (toPosition - fromPosition).normalized;

            var distance = Vector2.Distance(toPosition, fromPosition);

            var sightTest = Physics2D.Raycast(fromPosition, direction, distance, blockingMask);

            return sightTest.collider == null;
        }


        /// <summary>
        /// Checks line of sight from a point to a transform taking a layermask of blocking objects into account (2D)
        /// </summary>
        /// <param name="blockingMask"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static bool LineOfSight2D(LayerMask blockingMask, Vector2 from, Transform to)
        {
            var toPosition = to.position;
            var direction = ((Vector2)toPosition - from).normalized;

            var distance = Vector2.Distance(toPosition, from);

            var sightTest = Physics2D.Raycast(from, direction, distance, blockingMask);

            return sightTest.collider == null;
        }

        /// <summary>
        /// Checks if an integer is an even number
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static bool IsEven(this int n)
        {
            return (n ^ 1) == n + 1;
        }


        /// <summary>
        /// Compares a list of LinkedBools. returns true if all active linkedBools are true and ignores any inactive LinkedBools. 
        /// </summary>
        /// <param name="linkedBools"></param>
        /// <returns></returns>
        public static bool LinkedBoolListCheckAnd(params LinkedBool[] linkedBools)
        {
            var successes  = 0;
            var checks = 0;
            foreach (var pair in linkedBools)
            {
                if (pair.active)
                {
                    checks++;
                    if (pair.state)
                    {
                        successes++;
                    }
                }
            }

            if (checks <= 0)
            {
                return false; 
            }

            return checks == successes;
        }

        /// <summary>
        /// Compares a list of LinkedBools. returns true if any active linkedBools are true and ignores any inactive LinkedBools. 
        /// </summary>
        /// <param name="linkedBools"></param>
        /// <returns></returns>
        public static bool LinkedBoolListCheckOr(params LinkedBool[] linkedBools)
        {

            foreach (var linkedBool in linkedBools)
            {

                if (!linkedBool.active)
                {
                    continue;
                }

                if (linkedBool.state)
                {
                    return true;
                }

            }

            return false;
        }


    }
}