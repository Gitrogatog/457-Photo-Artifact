Shader "Filter/Blur" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _KernelSize ("Kernel Size", Range(3, 20)) = 3
        _Sigma ("Sigma", Range(0.1, 10)) = 2
        _Lerp ("Lerp", Range(0, 1)) = 0
    }

    SubShader {

        CGINCLUDE

        #include "UnityCG.cginc"

        struct VertexData {
            float4 vertex : POSITION;
            float2 uv : TEXCOORD0;
        };

        struct v2f {
            float2 uv : TEXCOORD0;
            float4 vertex : SV_POSITION;
        };

        v2f vp(VertexData v) {
            v2f o;
            o.vertex = UnityObjectToClipPos(v.vertex);
            o.uv = v.uv;
            return o;
        }

        #define PI 3.14159265358979323846f
        
        sampler2D _MainTex;
        float4 _MainTex_TexelSize;
        int _KernelSize;
        float _Sigma;
        float _Lerp;

        float gaussian(float sigma, float pos) {
            return (1.0f / sqrt(2.0f * PI * sigma * sigma)) * exp(-(pos * pos) / (2.0f * sigma * sigma));
        }

        ENDCG

        // Gaussian Blur First Pass
        Pass {
            CGPROGRAM
            #pragma vertex vp
            #pragma fragment fp

            float4 fp(v2f i) : SV_Target {
                float4 output = 0;
                float sum = 0;

                for (int x = -_KernelSize; x <= _KernelSize; ++x) {
                    float4 c = tex2D(_MainTex, i.uv + float2(x, 0) * _MainTex_TexelSize.xy);
                    float gauss = gaussian(_Sigma, x);
                    
                    output += c * gauss;
                    sum += gauss;
                }

                float4 col = tex2D(_MainTex, i.uv);

                return lerp(col, output / sum, _Lerp);
            }
            ENDCG
        }

        // Gaussian Blur Second Pass
        Pass {
            CGPROGRAM
            #pragma vertex vp
            #pragma fragment fp

            float4 fp(v2f i) : SV_Target {
                float4 output = 0;
                float sum = 0;

                for (int y = -_KernelSize; y <= _KernelSize; ++y) {
                    float4 c = tex2D(_MainTex, i.uv + float2(0, y) * _MainTex_TexelSize.xy);
                    float gauss = gaussian(_Sigma, y);
                    
                    output += c * gauss;
                    sum += gauss;
                }

                float4 col = tex2D(_MainTex, i.uv);

                return lerp(col, output / sum, _Lerp);
            }
            ENDCG
        }
    }
}