using UnityEngine;

public class Mine_Behavour : MonoBehaviour
{
    private Material boltMaterial;
    private Color originalEmission;
    private Color targetGlowColor = Color.red;
    public float pulseSpeed = 2f;
    public float glowIntensity = 10000f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Find the emissive material
        Renderer renderer = GetComponent<Renderer>();
        Debug.Log("Renderer found: " + (renderer != null));
        
        if (renderer != null)
        {
            Material[] materials = renderer.materials;
            Debug.Log("Total materials found: " + materials.Length);
            
            for (int i = 0; i < materials.Length; i++)
            {
                Debug.Log("Material " + i + ": " + materials[i].name + " | Shader: " + materials[i].shader.name);
                
                // Check all properties available
                string[] propertyNames = new string[] { "_EmissionColor", "_EmissiveColor", "_GlowColor", "_BaseColor", "_Color" };
                foreach (string prop in propertyNames)
                {
                    if (materials[i].HasProperty(prop))
                    {
                        Debug.Log("Material has property: " + prop + " = " + materials[i].GetColor(prop));
                    }
                }
                
                if (materials[i].name.Contains("emissive") || materials[i].HasProperty("_EmissionColor"))
                {
                    boltMaterial = new Material(materials[i]);
                    materials[i] = boltMaterial;
                    renderer.materials = materials;
                    
                    Debug.Log("Selected material: " + boltMaterial.name);
                    Debug.Log("Shader: " + boltMaterial.shader.name);
                    
                    // Try multiple emission keywords
                    boltMaterial.EnableKeyword("_EMISSION");
                    boltMaterial.EnableKeyword("_EMISSIVE");
                    boltMaterial.EnableKeyword("EMISSION");
                    
                    boltMaterial.globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;
                    originalEmission = boltMaterial.GetColor("_EmissionColor");
                    
                    // Force a high emission value to test
                    boltMaterial.SetColor("_EmissionColor", Color.red * 5000f);
                    Debug.Log("Test emission set to: " + boltMaterial.GetColor("_EmissionColor"));
                    
                    break;
                }
            }
        }

        if (boltMaterial == null)
        {
            Debug.LogError("No suitable material found on " + gameObject.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (boltMaterial != null)
        {
            float pulse = (Mathf.Sin(Time.time * Mathf.PI) + 1f) * 0.5f;
            float intensity = pulse * glowIntensity;
            Color currentColor = targetGlowColor * intensity;
            
            boltMaterial.SetColor("_EmissionColor", currentColor);
            
            // Try alternative properties if available
            if (boltMaterial.HasProperty("_EmissiveColor"))
                boltMaterial.SetColor("_EmissiveColor", currentColor);
            if (boltMaterial.HasProperty("_GlowColor"))
                boltMaterial.SetColor("_GlowColor", currentColor);
            
            // Debug every 60 frames
            if (Time.frameCount % 60 == 0)
            {
                Debug.Log("Pulse: " + pulse + " | Intensity: " + intensity);
                Debug.Log("Shader: " + boltMaterial.shader.name);
                Debug.Log("Emission keywords enabled: " + boltMaterial.IsKeywordEnabled("_EMISSION"));
            }
        }
    }
}
