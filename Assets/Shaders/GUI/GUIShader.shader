Shader "Custom/GUIShader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
    }

    SubShader
    {
        Tags
        {
            "Queue" = "Geometry+1"
            "RenderType" = "Opaque"
        }

        Lighting Off
        Cull Off
        ZTest Always
        ZWrite Off
        Fog
        {
            Mode Off
        }
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex Vert
            #pragma fragment Frag

            #include "UnityCG.cginc"

            struct InV
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct OutV
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            sampler2D _MainTex;
            uniform float4 _MainTex_ST;

            OutV Vert(InV v)
            {
                OutV o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
                return o;
            }

            fixed4 Frag(OutV i) : COLOR
            {
                return tex2D(_MainTex, i.texcoord);
            }

            ENDCG
        }
    }
}
