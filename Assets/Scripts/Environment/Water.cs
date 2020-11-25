using UnityEngine;

/* Based on the great tutorial on mass spring damper physics for 2D water by Alex Rose.
 * https://gamedevelopment.tutsplus.com/tutorials/creating-dynamic-2d-water-effects-in-unity--gamedev-14143 */

namespace Environment
{
    public class Water : MonoBehaviour
    {
        [Header("Water Surface")]
        [SerializeField] private Material material;
        [SerializeField] private Color color = Color.white;
        [SerializeField] private int nodeFrequency;
        [SerializeField] private float z = -1.0f;
        [SerializeField] private float width = 1.0f;
        [SerializeField] private float surfaceThickness = 0.05f;
        [SerializeField] private float colliderDepth = 1.0f;

        [Header("Spring/Damper Constants")]
        [SerializeField] private float springConstant = 0.02f;
        [SerializeField] private float damping = 0.04f;
        [SerializeField] private float spread = 0.05f;
        [SerializeField] private float mass = 1.0f;

        [Header("Waves")] 
        [SerializeField] private float maxBaseWaveHeight = 0.1f;
        [SerializeField] private float baseWaveFrequency = 0.4f;

        [Header("Splash")] 
        [SerializeField] private GameObject splashPrefab;
        private ParticleSystem _splash;

        private LineRenderer _surface;
        private BoxCollider2D _collider;
        private float[] _xPositions;
        private float[] _yPositions;
        private float[] _leftDeltas;
        private float[] _rightDeltas;
        private float[] _velocities;
        private float[] _accelerations;
        private float _baseHeight;
        private float _top;
        private float _left;

        public void Awake()
        {
            int edgeCount = Mathf.RoundToInt(width) * nodeFrequency;
            int nodeCount = edgeCount + 1;
            
            _top = transform.position.y;
            _left = transform.position.x;
            _baseHeight = _top;

            GameObject splash = Instantiate(splashPrefab, transform);
            _splash = splash.GetComponent<ParticleSystem>();

            _surface = GetComponent<LineRenderer>() == null ? gameObject.AddComponent<LineRenderer>() : GetComponent<LineRenderer>();
            _surface.material = material;
            _surface.material.renderQueue = -1;
            _surface.positionCount = nodeCount;
            _surface.startColor = color;
            _surface.endColor = color;
            _surface.startWidth = surfaceThickness;
            _surface.endWidth = surfaceThickness;

            _collider = GetComponent<BoxCollider2D>() == null ? gameObject.AddComponent<BoxCollider2D>() : GetComponent<BoxCollider2D>();
            _collider.isTrigger = true;
            _collider.offset = new Vector2( width / 2.0f, -colliderDepth / 2.0f);
            _collider.size = new Vector2(width, colliderDepth);
            
            _xPositions = new float[nodeCount];
            _yPositions = new float[nodeCount];
            _velocities = new float[nodeCount];
            _accelerations = new float[nodeCount];
            _leftDeltas = new float[_xPositions.Length];
            _rightDeltas = new float[_xPositions.Length];

            for (int i = 0; i < nodeCount; i++)
            {
                _yPositions[i] = _top;
                _xPositions[i] = _left + width * i / edgeCount;
                _accelerations[i] = 0;
                _velocities[i] = 0;
                _surface.SetPosition(i, new Vector3(_xPositions[i], _yPositions[i], z));
            }
        }

        private void Start()
        {
            InvokeRepeating(nameof(GenerateWave), 1.0f, baseWaveFrequency);
        }

        private void FixedUpdate()
        {
            for (int i = 0; i < _xPositions.Length ; i++)
            {
                float force = springConstant * (_yPositions[i] - _baseHeight) + _velocities[i]*damping ;
                _accelerations[i] = -force/mass;
                _yPositions[i] += _velocities[i];
                _velocities[i] += _accelerations[i];
                _surface.SetPosition(i, new Vector3(_xPositions[i], _yPositions[i], z));
            }
            
            for (int j = 0; j < 8; j++)
            {
                for (int i = 0; i < _xPositions.Length; i++)
                {
                    if (i > 0)
                    {
                        _leftDeltas[i] = spread * (_yPositions[i] - _yPositions[i-1]);
                        _velocities[i - 1] += _leftDeltas[i];
                    }
                    if (i < _xPositions.Length - 1)
                    {
                        _rightDeltas[i] = spread * (_yPositions[i] - _yPositions[i + 1]);
                        _velocities[i + 1] += _rightDeltas[i];
                    }
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Splash(other.transform.position.x, other.GetComponent<Rigidbody2D>().velocity.y/20);
        }

        private void GenerateWave()
        {
            _velocities[_xPositions.Length-1] += Random.Range(0.0f, maxBaseWaveHeight)/mass;
        }

        private void Splash(float xPos, float force)
        {
            if (xPos >= _xPositions[0] && xPos <= _xPositions[_xPositions.Length - 1])
            {
                xPos -= _xPositions[0];
                int index = Mathf.RoundToInt((_xPositions.Length-1)*(xPos / (_xPositions[_xPositions.Length-1] - _xPositions[0])));
                _velocities[index] += force/mass;

                _splash.transform.localPosition = new Vector3(xPos, -0.15f, z);
                _splash.Play();
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.right*width);
        }
    } 
}