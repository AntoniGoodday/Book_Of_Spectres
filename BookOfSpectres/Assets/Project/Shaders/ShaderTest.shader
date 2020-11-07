Shader "MyShader/ShaderTest"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_CopyTex ("CopyTexture", 2D) = "white" {}
		_Offset("TexOffset", float) = 1
		_ColorTint("Tint", Color) = (0,0,1,1)
    }
    SubShader
    {
        Tags 
		{ 
			//"RenderType"="Transparent" 
			"Queue" = "Transparent"
		}
        LOD 100

        Pass
        {
			Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
				float2 uv2 : TEXCOORD1;
				
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
				float uv2 : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
			
			float _Offset;
			
			sampler2D _CopyTex;
			float4 _CopyTex_ST;
			float4 _ColorTint;
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                //o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				
				

				o.uv = v.uv;
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                //fixed4 col = tex2D(_MainTex, i.uv);
				
				//fixed4 col = float4(i.uv.r, i.uv.g, 1, 1);
				float4 color = tex2D(_MainTex, i.uv);

				float2 movedUV;
				movedUV.x = i.uv.x + (_Time.y * _Offset) ;
				movedUV.y = i.uv.y;
				float4 copyThing = tex2D(_CopyTex, movedUV);
				fixed4 copyColour = float4(1, 1, 1, 1);
				

				//color = col * color;
				
				copyThing = _ColorTint * copyThing * color;
                // apply fog
                //UNITY_APPLY_FOG(i.fogCoord, col);
                return copyThing;
            }
            ENDCG
        }
    }
}
