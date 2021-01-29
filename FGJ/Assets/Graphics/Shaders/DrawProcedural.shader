Shader "Custom/DrawProcedural"
{
    SubShader
    {
        Tags { "RenderType" = "Opaque" "RenderPipelines" = "UniversalPipeline" }

        Cull off
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct Vertex
            {
                float3 position;
                float4 color;
            };

            StructuredBuffer<Vertex> _VertexBuffer;

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 color : COLOR;
            };

            v2f vert(uint id : SV_VertexID)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(float4(_VertexBuffer[id].position.xyz, 1.0f));
                o.color = _VertexBuffer[id].color;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                return float4(i.color, 1.0f);
            }

            ENDCG
        }
    }
}
