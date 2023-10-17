Shader "Unlit/WireFrameShader"
{
    Properties
    {
        _Color ("Color", color) = (1,1,1,1)
        _WireColor ("Wire Color", color) = (1,1,1,1)
        _WireThickness ("Wire Thickness", float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma geometry geom
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2g
            {
                float4 vertex : SV_POSITION;
                float4 worldPos : TEXCOORD0;
            };

            struct g2f {
                float4 pos : SV_POSITION;
                float3 barycentric : TEXCOORD0;
                float3 debugColor : TEXCOORD1;
            };

            v2g vert (appdata v)
            {
                v2g o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldPos = v.vertex;
                return o;
            }

            [maxvertexcount(3)]
            void geom(triangle v2g IN[3], inout TriangleStream<g2f> triStream) {
                g2f o;
                float3 a = IN[1].worldPos - IN[0].worldPos;
                float3 b = IN[2].worldPos - IN[1].worldPos;
                float3 c = IN[0].worldPos - IN[2].worldPos;
                float lengthA = length(a);
                float lengthB = length(b);
                float lengthC = length(c);
                float cosThetaAB = dot(a, b) / (lengthA * lengthB);
                float cosThetaBC = dot(b, c) / (lengthB * lengthC);
                float cosThetaCA = dot(c, a) / (lengthC * lengthA);
                o.pos = IN[0].vertex;
                o.barycentric = float3(1.0, 0.0, 0.0);
                o.debugColor = IN[0].vertex;
                if (abs(cosThetaAB) < 0.1f) {
                    o.barycentric.y = 1;
                }
                if (abs(cosThetaBC) < 0.1f) {
                    o.barycentric.z = 1;
                }
                triStream.Append(o);
                o.pos = IN[1].vertex;
                o.barycentric = float3(0.0, 1.0, 0.0);
                o.debugColor = IN[1].vertex;
                if (abs(cosThetaCA) < 0.1f) {
                    o.barycentric.x = 1;
                }
                if (abs(cosThetaBC) < 0.1f) {
                    o.barycentric.z = 1;
                }
                triStream.Append(o);
                o.pos = IN[2].vertex;
                o.barycentric = float3(0.0, 0.0, 1.0);
                o.debugColor = IN[2].vertex;
                if (abs(cosThetaCA) < 0.1f) {
                    o.barycentric.x = 1;
                }
                if (abs(cosThetaAB) < 0.1f) {
                    o.barycentric.y = 1;
                }
                triStream.Append(o);
            }

            fixed4 _Color;
            fixed4 _WireColor;
            float _WireThickness;

            fixed4 frag (g2f i) : SV_Target
            {
                float closest = min(i.barycentric.x, min(i.barycentric.y, i.barycentric.z));
                if (closest < _WireThickness) {
                    return _WireColor;
                } else {
                    return _Color;
                }
                // return fixed4(i.debugColor.x, i.debugColor.y, i.debugColor.z, 1);
            }
            ENDCG
        }
    }
}
