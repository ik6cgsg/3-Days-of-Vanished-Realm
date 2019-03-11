Shader "Custom/VR Cursor Shader" {
  Properties{
      _MainTex("Font Texture", 2D) = "white" {}
      _Color("Text Color", Color) = (1,1,1,1)
      _DistanceInMeters("DistanceInMeters", Range(0.0, 100.0)) = 2.0
  }

  SubShader
  {
    Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
    Lighting Off
    Cull Off
    ZTest Always
    ZWrite Off
    Fog { Mode Off }
    Blend SrcAlpha OneMinusSrcAlpha

    Pass
    {
      CGPROGRAM
      #pragma vertex vert
      #pragma fragment frag
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

      OutV vert(InV v)
      {
        OutV o;
        float scale = lerp(2.0, 1.5, v.vertex.z);
        float3 vert_out = float3(v.vertex.x * scale, v.vertex.y * scale, _DistanceInMeters);
        o.vertex = UnityObjectToClipPos(vert_out);
        o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);
        return o;
      }

      half4 frag(OutV i) : COLOR
      {
        float4 col = _Color;
        return tex2D(_MainTex, i.texcoord) * col;
      }
      ENDCG
    }
  }

  SubShader
  {
    Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
    Lighting Off
    Cull Off
    ZTest Always
    ZWrite Off
    Fog { Mode Off }
    Blend SrcAlpha OneMinusSrcAlpha
    Pass {
        Color[_Color]
        SetTexture[_MainTex] {
          combine primary, texture * primary
        }
    }
  }
}