Shader "Unlit/ScreenCutoutShader"
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

		Pass
		{
			CGPROGRAM
			#pragma vertex Vert
			#pragma fragment Frag

			#include "UnityCG.cginc"

			struct Appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct V2F
			{
				float4 vertex : POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;

			V2F Vert(Appdata v)
			{
				V2F o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}

			fixed4 Frag(V2F i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, float2(i.vertex.x / _ScreenParams.x, i.vertex.y / _ScreenParams.y));
				return col;
			}
			ENDCG
		}
	}
}