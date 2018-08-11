// Date   : 11.08.2018 02:50
// Project: LD42
// Author : bradur

using UnityEngine;
using System.Collections;

public enum PixelRenderMode
{
    None,
    PixelPerfect,
    AspectStretch
}

public class ResolutionManager : MonoBehaviour
{
    [SerializeField]
    private Camera outputCamera;
    [SerializeField]
    private Camera nativeCamera;
    [SerializeField]
    private RenderTexture renderTexture;
    [SerializeField]
    private NativeRenderTextureController renderTextureManager;

    [SerializeField]
    private Vector2 outputResolution;
    [SerializeField]
    private Vector2 nativeResolution = new Vector2(1280, 720);

    [SerializeField]
    private float pixelScale;

    [Header("Output Configuration")]
    [SerializeField]
    private PixelRenderMode renderMode;
    [SerializeField]
    private FilterMode filterMode;

    void Awake()
    {
        renderTexture.width = (int)(nativeResolution.x);
        renderTexture.height = (int)(nativeResolution.y);
    }

    void Update()
    {
        outputResolution = new Vector2(outputCamera.pixelWidth, outputCamera.pixelHeight);
        renderTexture.filterMode = filterMode;

        UpdatePixelScale();
        UpdateCameraSize();
    }

    private void UpdateCameraSize()
    {
        outputCamera.orthographicSize = outputResolution.y / 2;
        //nativeCamera.orthographicSize = renderTexture.height / 2;
    }

    void UpdatePixelScale()
    {
        float width = outputResolution.x / nativeResolution.x;
        float height = outputResolution.y / nativeResolution.y;

        float scale;

        if (renderMode == PixelRenderMode.PixelPerfect)
        {
            scale = (int)Mathf.Max(Mathf.Min((int)width, (int)height), 1);
        }
        else
        {
            scale = Mathf.Min(width, height);
        }

        pixelScale = scale;

        renderTextureManager.UpdateScale(renderMode, pixelScale, nativeResolution, outputResolution);
    }
}
