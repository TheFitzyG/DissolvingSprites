using System;
using UnityEngine;

//Copyright: Sean FitzGerald (TheFitzyGames) CC BY-SA 4.0
//https://creativecommons.org/licenses/by-sa/4.0/
//modifications, and commercial use are allowed with appropriate credit given

// ReSharper disable InconsistentNaming
namespace TFG_SP
{
    /// <summary>
    /// A two dimensional bool structure, useful for deciding if a bool should be used or not. 
    /// </summary>
    [Serializable]
    public class LinkedBool
    {
        public LinkedBool(bool active, bool state)
        {
            this.active = active;
            this.state = state;
        }

        public bool active;
        public bool state;

        public bool And => active && state;
        public bool Or => active || state;
        public bool Xor => active ^ state;
    }

    /// <summary>
    /// A three dimensional boolean system
    /// </summary>
    [Serializable]
    public struct Bool3
    {
        public bool a;
        public bool b;
        public bool c;

        public bool And => a && b && c;
        public bool Or => a || b || c;
        public bool Xor => a ^ b ^ c;
    }

    /// <summary>
    /// A float struct that is clamped between 0 and 1
    /// </summary>
    [Serializable]
    public struct float01
    {
        private float value;

        public float Value
        {
            get => value;
            set => this.value = Mathf.Clamp01(value);
        }
    }

    /// <summary>
    /// A class structure for holding a maximum and minimum value (float)
    /// </summary>
    [Serializable]
    public class MinMaxFloat
    {
        [SerializeField] private Vector2 _minMax;

        public MinMaxFloat()
        {
            _minMax = Vector2.up;
        }

        public MinMaxFloat(float min, float max)
        {
            _minMax.x = min;
            _minMax.y = max;
        }

        public float Min => Mathf.Min(_minMax.x, _minMax.y);
        public float Max => Mathf.Max(_minMax.x, _minMax.y);

        /// <summary>
        /// A random value between the min and max
        /// </summary>
        public float Random => UnityEngine.Random.Range(Min, Max);

        /// <summary>
        /// Min + Max
        /// </summary>
        public float Total => Min + Max;

        /// <summary>
        /// Difference between Max and Min
        /// </summary>
        public float Range => Max - Min;

        /// <summary>
        /// Returns if the input is between Min and Max
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool IsInRange(float input)
        {
            return input >= Min && input <= Max;
        }
    }


    /// <summary>
    /// A class structure for holding a maximum and minimum value (int)
    /// </summary>
    [Serializable]
    public class MinMaxInt
    {
        [SerializeField] private Vector2Int _minMax;

        public MinMaxInt()
        {
            _minMax = Vector2Int.one;
        }

        public MinMaxInt(int min, int max)
        {
            _minMax.x = min;
            _minMax.y = max;
        }

        public int Min => Mathf.Min(_minMax.x, _minMax.y);
        public int Max => Mathf.Max(_minMax.x, _minMax.y);

        /// <summary>
        /// A random value between the min and max
        /// </summary>
        public int Random => (int) UnityEngine.Random.Range(Min, Max + 1);

        /// <summary>
        /// Min + Max
        /// </summary>
        public float Total => Min + Max;

        /// <summary>
        /// Difference between Max and Min
        /// </summary>
        public float RangeSize => Max - Min;

        /// <summary>
        /// Returns if the input is between Min and Max
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool IsInRange(int input)
        {
            return input >= Min && input <= Max;
        }
    }

    /// <summary>
    /// A class structure for holding a maximum and minimum value (Vector3)
    /// </summary>
    [Serializable]
    public class MinMaxVector3
    {
        [SerializeField] private Vector3 _min;
        [SerializeField] private Vector3 _max;

        public MinMaxVector3()
        {
            _min = Vector3.zero;
            _max = Vector3.one;
        }

        public MinMaxVector3(Vector3 min, Vector3 max)
        {
            _min = min.magnitude < max.magnitude ? min : max;
            _max = _min == min ? max : min;
        }

        public Vector3 Min => _min;
        public Vector3 Max => _max;

        /// <summary>
        /// A random value between the min and max
        /// </summary>
        public Vector3 Random => new Vector3(
            UnityEngine.Random.Range(_min.x, _max.x),
            UnityEngine.Random.Range(_min.y, _max.y),
            UnityEngine.Random.Range(_min.z, _max.z));

        /// <summary>
        /// Min + Max
        /// </summary>
        public Vector3 Total => Min + Max;

        /// <summary>
        /// Difference between Max and Min
        /// </summary>
        public Vector3 Range => Max - Min;

        /// <summary>
        /// Returns if the input is between Min and Max
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool IsInRange(Vector3 input)
        {
            return ((input.x > _min.x && input.x < _max.x)
                    && (input.y > _min.y && input.y < _max.y)
                    && (input.z > _min.z && input.z < _max.z));
        }
    }

    /// <summary>
    /// A class structure for holding a maximum and minimum value (Vector3)
    /// </summary>
    [Serializable]
    public class MinMaxVector2
    {
        [SerializeField] private Vector2 _min;
        [SerializeField] private Vector2 _max;

        public MinMaxVector2()
        {
            _min = Vector2.zero;
            _max = Vector2.one;
        }

        public MinMaxVector2(Vector2 min, Vector2 max)
        {
            _min = min.magnitude < max.magnitude ? min : max;
            _max = _min == min ? max : min;
        }

        public Vector2 Min => _min;
        public Vector2 Max => _max;

        /// <summary>
        /// A random value between the min and max
        /// </summary>
        public Vector2 Random => new Vector2(
            UnityEngine.Random.Range(_min.x, _max.x),
            UnityEngine.Random.Range(_min.y, _max.y));

        /// <summary>
        /// Min + Max
        /// </summary>
        public Vector2 Total => Min + Max;

        /// <summary>
        /// Difference between Max and Min
        /// </summary>
        public Vector2 Range => Max - Min;

        /// <summary>
        /// Returns if the input is between Min and Max
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool IsInRange(Vector2 input)
        {
            return ((input.x > _min.x && input.x < _max.x)
                    && (input.y > _min.y && input.y < _max.y));
        }
    }

}