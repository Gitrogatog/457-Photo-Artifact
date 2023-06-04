// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Filter/PaletteSwapMatrix"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
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
			
			sampler2D _MainTex;
            float _Lerp;
			float4x4 _ColorMatrix;

            float luminance(float3 color) {
                return dot(color, float3(0.299f, 0.587f, 0.114f));
            }

			fixed4 frag (v2f i) : SV_Target
			{
                float4 col = tex2D(_MainTex, i.uv);
				float x = luminance(col.rgb);
				return lerp(col, _ColorMatrix[x * 3], _Lerp);
			}

			ENDCG
		}
	}
}