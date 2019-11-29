using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess(typeof(TransitionImageEffectRenderer), PostProcessEvent.AfterStack, "MyEffects/ScreenTransition")]
public sealed class TransitionImageEffect : PostProcessEffectSettings
{
    public TextureParameter transitionTexture = new TextureParameter { value = null };

    public ColorParameter color = new ColorParameter { value = Color.white };

    [Range(0f, 1f), Tooltip("Image Cutoff Amount.")]
    public FloatParameter cutoff = new FloatParameter { value = 0f };
}

public sealed class TransitionImageEffectRenderer : PostProcessEffectRenderer<TransitionImageEffect>
{
    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(Shader.Find("Hidden/MyEffects/ScreenTransition"));
        sheet.properties.SetFloat("_Cutoff", settings.cutoff);
        sheet.properties.SetTexture("_TransitionTex", settings.transitionTexture);
        sheet.properties.SetColor("_Color", settings.color);
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}