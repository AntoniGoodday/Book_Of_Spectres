Shader "Hidden/MyEffects/ScreenTransition"
{
	HLSLINCLUDE
	
	#include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"
	TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
	sampler2D _Tex;
	TEXTURE2D_SAMPLER2D(_TransitionTex, sampler_TransitionTex);
	half3 _Color;
	half _Cutoff;

	half4 Frag(VaryingsDefault i) : SV_Target
	{
				float4 dissolve_value = SAMPLE_TEXTURE2D(_TransitionTex, sampler_TransitionTex, i.texcoord);
				half oneValue = dissolve_value.a;
				clip(_Cutoff - oneValue);

				// sample the texture

				float4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord);
				col = col * dissolve_value;
				return col;
	}
		ENDHLSL

		SubShader
	{
		Cull Off ZWrite Off ZTest Always

			Pass
		{
			HLSLPROGRAM

				#pragma vertex VertDefault
				#pragma fragment Frag

			ENDHLSL
		}
	}
}

/*#pragma target 3.0
#include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"
#include "Packages/com.unity.postprocessing/PostProcessing/Shaders/Colors.hlsl"
#include "Packages/com.unity.postprocessing/PostProcessing/Shaders/Builtins/Distortion.hlsl"
#include "Packages/com.unity.postprocessing/PostProcessing/Shaders/Builtins/Dithering.hlsl"
#define MAX_CHROMATIC_SAMPLES 16

//TEXTURE2D_SAMPLER2D(_AutoExposureTex, sampler_AutoExposureTex);

TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
float4 _MainTex_TexelSize;

sampler2D _Tex;
TEXTURE2D_SAMPLER2D(_TransitionTex, sampler_TransitionTex);
half3 _Color;
half _Cutoff;

half4 Frag(VaryingsDefault i) : SV_Target
{
	float2 uv = i.texcoord;

	float2 uvDistorted = Distort(i.texcoord);

	//half autoExposure = SAMPLE_TEXTURE2D(_AutoExposureTex, sampler_AutoExposureTex, uv).r;
	half4 color = (0.0).xxxx;


	half vfactor = SAMPLE_TEXTURE2D(_TransitionTex, sampler_TransitionTex, uvDistorted).a;




	vfactor = SRGBToLinear(vfactor);


	half3 new_color = color.rgb * lerp(_Color, (1.0).xxx, vfactor);
	color.rgb = lerp(color.rgb, new_color, _Cutoff);
	color.a = lerp(1.0, color.a, vfactor);
	half4 output = color;



	return output;
}*/

