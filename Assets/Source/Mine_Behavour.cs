using UnityEngine;

public class Mine_Behavour : MonoBehaviour
{
    [Tooltip("The material to make pulse. If empty, it will use the material from this object's Renderer.")]
    public Material targetMaterial;

    [Tooltip("The maximum intensity of the emission.")]
    private float maxIntensity = 100000.0f;

    [Tooltip("The time in seconds for one full pulse cycle.")]
    private float pulseDuration = 2.0f;

    [Tooltip("The base color of the emission. The script will control the intensity.")]
    [ColorUsage(true, true)]
    public Color baseEmissiveColor = Color.white;

    private Material _material;
    private int _emissiveColorID;
    private bool _isMaterialInstance = false;
    private Color _originalEmissiveColor;
    private float _randomOffset;

    void Start()
    {
        if (targetMaterial != null)
        {
            _material = targetMaterial;
            _isMaterialInstance = false;
        }
        else
        {
            var renderer = GetComponent<Renderer>();
            if (renderer != null)
            {
                // Use .material to create a new instance of the material for this object.
                _material = renderer.material;
                _isMaterialInstance = true;
            }
        }

        if (_material != null)
        {
            _emissiveColorID = Shader.PropertyToID("_EmissiveColor");
            if (_material.HasProperty(_emissiveColorID))
            {
                _originalEmissiveColor = _material.GetColor(_emissiveColorID);
            }
        }
        else
        {
            Debug.LogError("Mine_Behavour: No material found. Assign a Target Material or ensure a Renderer is on the GameObject.", this);
        }

        _randomOffset = Random.value * pulseDuration;
    }

    void Update()
    {
        if (_material != null)
        {
            // Calculate a value that ping-pongs between 0 and 1 over the pulse duration.
            float pingPongValue = Mathf.PingPong((Time.time + _randomOffset) * (2.0f / pulseDuration), 1.0f);

            // Calculate the current intensity and the final color.
            float currentIntensity = pingPongValue * maxIntensity;
            Color finalColor = baseEmissiveColor * currentIntensity;

            // Set the emissive color on the material.
            _material.SetColor(_emissiveColorID, finalColor);
        }
    }

    void OnDestroy()
    {
        if (_material != null)
        {
            if (_isMaterialInstance)
            {
                // Clean up the created material instance to avoid memory leaks.
                Destroy(_material);
            }
            else
            {
                // Restore the original emissive color on the shared material.
                if (_material.HasProperty(_emissiveColorID))
                {
                    _material.SetColor(_emissiveColorID, _originalEmissiveColor);
                }
            }
        }
    }
}
