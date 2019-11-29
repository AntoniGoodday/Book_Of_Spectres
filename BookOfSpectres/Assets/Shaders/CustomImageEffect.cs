using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess(typeof(CustomImageEffectRenderer), PostProcessEvent.AfterStack, "MyEffects/CustomImageEffect")]
public sealed class CustomImageEffect : PostProcessEffectSettings
{
    public TextureParameter texture = new TextureParameter { value = null };
    
    [Range(0f, 100f), Tooltip("CustomImageEffect effect intensity.")]
    public FloatParameter speed = new FloatParameter { value = 0.5f };
}

public sealed class CustomImageEffectRenderer : PostProcessEffectRenderer<CustomImageEffect>
{
    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(Shader.Find("Hidden/MyEffects/CustomImageEffect"));
        sheet.properties.SetFloat("_Speed", settings.speed);
        sheet.properties.SetTexture("_Tex", settings.texture);
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}
