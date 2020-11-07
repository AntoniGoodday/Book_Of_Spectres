Shader "Hidden/MyEffects/CustomImageEffect"
{
	HLSLINCLUDE

#include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"

	TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
	
	
	float _Speed;
	sampler2D _Tex;
	
	

	float4 Frag(VaryingsDefault i) : SV_Target
	{
		float SpeedOverTime = _Speed * _Time;
		float2 coord = float2(i.texcoord.x + SpeedOverTime, i.texcoord.y);

		float4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord);

		float4 col2 = tex2D(_Tex, coord);
		col = col * col2;

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
