Shader "Custom/VR Cursor Shader"
{
    Properties
    {
        _MainTex("Font Texture", 2D) = "white" {}
        _Color("Text Color", Color) = (1,1,1,1)
        _DistanceInMeters("DistanceInMeters", Range(0.0, 100.0)) = 2.0
        _DrawingMode("DrawingMode", int) = 0
        _FixedScale("FixedScale", float) = 1.0
        _FixedDistanceToCursor("FixedDistanceToCursor", float) = 1.0
    }

    SubShader
    {
        Tags
        {
            "Queue" = "Transparent"
            "IgnoreProjector" = "True"
            "RenderType" = "Transparent"
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
            uniform float4 _Color;
            uniform float _DistanceInMeters;
            uniform int _DrawingMode;
            uniform float _FixedScale;
            uniform float _FixedDistanceToCursor;

            static const int FIXED_MODE = 1;
            static const int VARIABLE_MODE = 2;

            OutV Vert(InV v)
            {
                OutV o;

                float scale = _FixedScale;
                if (_DrawingMode == VARIABLE_MODE)
                {
                    scale = lerp(3.0, 2.8, v.vertex.z);
                }

                float3 vertOut;
                if (_DrawingMode == VARIABLE_MODE)
                {
                    vertOut = float3(v.vertex.x * scale, v.vertex.y * scale, _DistanceInMeters);
                }
                else
                {
                    vertOut = float3(v.vertex.x * scale * _DistanceInMeters / _FixedDistanceToCursor,
                      v.vertex.y * scale * _DistanceInMeters / _FixedDistanceToCursor, _DistanceInMeters);
                }

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