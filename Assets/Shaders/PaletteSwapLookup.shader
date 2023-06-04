// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Filter/PaletteSwapLookup"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_PaletteTex("Texture", 2D) = "white" {}
        _Lerp ("Lerp", Range(0, 1)) = 0
	}
	SubShader
	{
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

            float luminance(float3 color) {
                return dot(color, float3(0.299f, 0.587f, 0.114f));
            }
			
			sampler2D _MainTex;
			sampler2D _PaletteTex;
            float _Lerp;

			fixed4 frag (v2f i) : SV_Target
			{
				float4 col = tex2D(_MainTex, i.uv);
                float x = saturate(luminance(col));
                float4 tex = tex2D(_PaletteTex, float2(x, 0));
				return lerp(col, tex, _Lerp);
			}

			ENDCG
		}
	}
}