// Date   : 11.08.2018 02:51
// Project: LD42
// Author : bradur

using UnityEngine;
using System.Collections;

public class NativeRenderTextureController : MonoBehaviour
{
    public void UpdateScale(PixelRenderMode renderMode, float pixelScale, Vector2 nativeResolution, Vector2 outputResolution)
    {
        if (renderMode == PixelRenderMode.PixelPerfect)
        {
            transform.localScale = new Vector3(
                (int)pixelScale * nativeResolution.x,
                (int)pixelScale * nativeResolution.y,
                1
            );
        }
        else if (renderMode == PixelRenderMode.AspectStretch)
        {
            transform.localScale = new Vector3(
                pixelScale * nativeResolution.x,
                pixelScale * nativeResolution.y,
                1
            );
        }
        else
        {
            float width =  outputResolution.x / nativeResolution.x;
            float height = outputResolution.y / nativeResolution.y;

            transform.localScale = new Vector3(
                outputResolution.x,
                outputResolution.y,
                1
            );
        }
    }
}
