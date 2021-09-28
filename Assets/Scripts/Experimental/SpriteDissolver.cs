using System;
using System.Collections;
using TFG_SP;
using UnityEngine;
using Random = UnityEngine.Random;


namespace BloodPath.Experimental
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteDissolver : MonoBehaviour
    {

        [Header("Basic")]
        [SerializeField] private bool _dissolveOnStart;
        [SerializeField] private float _dissolveOnStartDelay;


        [Header("Performance")]
        [SerializeField] [Min(0)] private int _maxPixels = 0;
        [SerializeField] private MinMaxFloat _pixelLifetime = new MinMaxFloat(0, 0);
        [SerializeField] [Min(0)] private float _pixelsPerUnitOverride = 0;

        
        [Header("Collision")]
        [SerializeField] private bool _applyCollider;
        [SerializeField] private ColliderType _colliderType;

        [Header("Physics")]
        [SerializeField] private bool _applyPhysics;
        [SerializeField] private MinMaxFloat _dissolveForce;
        [SerializeField] private float _randomDirectionStrength;
        [SerializeField] private Vector2 _forceBias = Vector3.zero;
        [SerializeField] private float _drag = 0.5f;
        [SerializeField] private RigidbodyConstraints2D _constraints = RigidbodyConstraints2D.FreezeRotation;
        [SerializeField] private float _gravityScale = 0f;

        private IEnumerator Start()
        {
            if (!_dissolveOnStart)
            {
                yield break;
            }

            if (_dissolveOnStartDelay > 0)
            {
                yield return new WaitForSeconds(_dissolveOnStartDelay);
            }

            Dissolve();

        }


        private enum ColliderType
        {
            Circle,
            Box,
           // Polygon
        }

        public void Dissolve()
        {
            var renderer = GetComponent<SpriteRenderer>();
            var sprite = renderer.sprite;
            var ppu = _pixelsPerUnitOverride > 0 ? _pixelsPerUnitOverride : sprite.pixelsPerUnit;

            if (!sprite) { return;}

            var sourceTexture = sprite.texture;
            if (!sourceTexture.isReadable)
            {
                Debug.LogError($"Sprite {sprite} must be read and write enabled");
                return;
            }

            var tex = new Texture2D(1, 1);
            tex.name = "Pixel";
            tex.SetPixel(0, 0, Color.white);
            tex.filterMode = FilterMode.Point;
            tex.Apply(false);


            var spriteBounds = sprite.bounds;
            var spriteRect = sprite.textureRect;
            var spriteOrigin = new Vector2Int(Mathf.FloorToInt(spriteRect.position.x), Mathf.FloorToInt(spriteRect.position.y));


            int count = 0;
            for (int x = 0; x < spriteRect.width; x++)
            {
                for (int y = 0; y < spriteRect.height; y++)
                {

                    if (_maxPixels > 0 && count >= _maxPixels)
                    {
                        continue;
                    }

                    var color = sourceTexture.GetPixel(x + spriteOrigin.x, y + spriteOrigin.y);

                    if (color.a == 0f)
                    {
                        continue;
                    }

                    var pixelGameObject = new GameObject();
                    pixelGameObject.name = $"SplitPixel ({x},{y})";

                    var rend = pixelGameObject.AddComponent<SpriteRenderer>();
                    rend.color = color;

                    rend.sprite = Sprite.Create(tex, new Rect(Vector2.zero, new Vector2(1, 1)), Vector2.zero, ppu);

                    pixelGameObject.transform.parent = transform;
                    pixelGameObject.transform.position = CalculateWorldPosOfPixelCoordinate(new Vector2Int(x, y), spriteBounds.size, transform.position, ppu);


                    if (_applyCollider)
                    {

                        switch (_colliderType)
                        {
                            case ColliderType.Circle:
                                pixelGameObject.AddComponent<CircleCollider2D>();
                                break;
                            case ColliderType.Box:
                                var boxCollider = pixelGameObject.AddComponent<BoxCollider2D>();
                                break;
                            //case ColliderType.Polygon:
                            //   var polyCollider = pixelGameObject.AddComponent<PolygonCollider2D>();
                            //   //polyCollider.CreateMesh(true, true);
                            //    break;
                        }
                    }

                    if (_applyPhysics)
                    {
                        var pixelRigidBody = pixelGameObject.AddComponent<Rigidbody2D>();
                        pixelRigidBody.gravityScale = _gravityScale;
                        pixelRigidBody.constraints = _constraints;
                        pixelRigidBody.collisionDetectionMode = CollisionDetectionMode2D.Discrete;
                        //pixelRigidBody.angularDrag = 0.8f;
                        pixelRigidBody.drag = _drag;

                        pixelRigidBody.AddForce(
                            ((pixelRigidBody.position - (Vector2)transform.position) + (Random.insideUnitCircle * _randomDirectionStrength) + _forceBias).normalized * _dissolveForce.Random ,
                            ForceMode2D.Impulse);
                    }

                    if (_pixelLifetime.Max > 0)
                    {
                        Destroy(pixelGameObject, _pixelLifetime.Random);
                    }

                    count++;
                }
            }

            renderer.enabled = false;
        }


        Vector2 CalculateWorldPosOfPixelCoordinate(Vector2Int coord, Vector2 boundsSize, Vector2 position, float pixelsPerUnit)
        {
            var PixelInWorldSpace = 1.0f / pixelsPerUnit;
            var startPos = position - (boundsSize * 0.5f);

            return startPos + (PixelInWorldSpace * (Vector2)coord);
        }


    }
}
