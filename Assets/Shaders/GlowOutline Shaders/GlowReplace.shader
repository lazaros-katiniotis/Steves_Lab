﻿Shader "Custom/GlowReplace"
{
	SubShader
	{
		Tags { 
		"RenderType"="Opaque"
		"Glowable"="True"
		}

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
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			fixed4 _GlowColor;
			fixed _Cutoff;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				//col = col * _Cutoff;
				if (col.a == 0) {
					discard;
				}
				return max(1 - _Cutoff, 1 - _GlowColor);
			}
			ENDCG
		}
	}
}
