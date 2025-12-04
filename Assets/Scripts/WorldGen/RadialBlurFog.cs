using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class RadialBlurFog : MonoBehaviour
{
    [Header("Material con el shader de blur")]
    public Material blurMaterial;

    [Header("Jugador (centro del efecto)")]
    public Transform player;

    [Header("Radio de nitidez (unidades de mundo)")]
    [Tooltip("Radio alrededor del jugador donde la imagen se ve nítida (por ejemplo 6).")]
    public float worldRadius = 6f;

    [Header("Escalado de blur")]
    [Range(1.0f, 4.0f)]
    public float outerRadiusFactor = 1.5f;   // hasta dónde llega el blur máximo (relativo al inner)

    [Header("Intensidad máxima del blur (en píxeles)")]
    public float maxBlurPixels = 3f;

    Camera cam;

    void Awake()
    {
        cam = GetComponent<Camera>();
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (blurMaterial == null || player == null)
        {
            Graphics.Blit(src, dest);
            return;
        }

        // Centro del jugador en pantalla [0,1]
        Vector3 vp = cam.WorldToViewportPoint(player.position);
        blurMaterial.SetVector("_PlayerScreenPos", new Vector4(vp.x, vp.y, 0f, 0f));

        // ==== Convertimos worldRadius a radio en pantalla (0–1) ====
        // Solo funciona bien si la cámara es ortográfica.
        float innerRadius = 0.2f;  // valor por defecto

        if (cam.orthographic)
        {
            float worldHeight = 2f * cam.orthographicSize;   // alto visible en mundo
            innerRadius = worldRadius / worldHeight;         // fracción de la altura de pantalla
        }

        float outerRadius = innerRadius * outerRadiusFactor;

        blurMaterial.SetFloat("_InnerRadius", innerRadius);
        blurMaterial.SetFloat("_OuterRadius", outerRadius);
        blurMaterial.SetFloat("_MaxBlurPixels", maxBlurPixels);

        // Tamaño del texel (para saber cuánto es 1 píxel en UV)
        blurMaterial.SetVector("_TexelSize",
            new Vector4(1f / src.width, 1f / src.height, 0f, 0f));

        // Aplicamos el post-proceso
        Graphics.Blit(src, dest, blurMaterial);
    }
}




