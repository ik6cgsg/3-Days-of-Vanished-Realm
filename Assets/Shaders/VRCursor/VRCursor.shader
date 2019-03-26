Shader "Custom/VR Cursor Shader"
{
    Properties
    {
        _MainTex("Font Texture", 2D) = "white" {}
        _Color("Text Color", Color) = (1,1,1,1)
        _DistanceInMeters("DistanceInMeters", Range(0.0, 100.0)) = 2.0
        _DrawingMode("DrawingMode", int) = 0
    }

    SubShader
    {
        Tags
        {
            "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"
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
            #pragma fragmentoption ARB_precision_hint_fastest
        
            #include "UnityCG.cginc"

            struct InV {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct OutV {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            sampler2D _MainTex;
            uniform float4 _MainTex_ST;
            uniform float4 _Color;
            uniform float _DistanceInMeters;
            uniform int _DrawingMode;

            OutV Vert(InV v)
            {
                OutV o;

                float scale = 1.0;
                if (_DrawingMode == 2)
                {
                    scale = lerp(3.0, 2.8, v.vertex.z);
                }

                float3 vertOut = float3(v.vertex.x * scale, v.vertex.y * scale, _DistanceInMeters);
                o.vertex = UnityObjectToClipPos(vertOut);
                o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
                return o;
            }

            fixed4 Frag(OutV i) : COLOR
            {
                float4 col = _Color;
                return tex2D(_MainTex, i.texcoord) * col;
            }

            ENDCG
        }
    }
}