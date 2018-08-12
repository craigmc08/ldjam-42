Shader "DistortedTransparent" {
	Properties
	{
		[NoScaleOffset] _MainTex("Texture", 2D) = "white" {}
		_Tint("Tint", Color) = (1, 1, 1)
		_Background("Background", Color) = (1, 1, 1, 0)
		_MainTiling("Texture Tiling", Vector) = (1, 1, 0, 0)
		_MainOffset("Texture X Offset", Float) = 0
		[NoScaleOffset] _Distortion("Distortion Texture", 2D) = "white" {}
		_DistortionTiling("Distortion Tiling", Vector) = (1, 1, 0, 0)
		_Amount("Distortion Amount", Range(0, 1)) = 0.2
		_Offset("Distortion X Offset", Float) = 0
		_Brightness("Brightness", Float) = 1
	}
	SubShader
	{	
		Tags { "Queue"="Transparent" "RenderType"="Transparent" }

		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

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

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			sampler2D _MainTex;
			float4 _Tint;
			float4 _Background;
			float2 _MainTiling;
			float _MainOffset;
			sampler2D _Distortion;
			float2 _DistortionTiling;
			float _Amount;
			float _Offset;
			float _Brightness;

			float4 frag(v2f i) : SV_Target
			{
				float3 dist = tex2D(_Distortion, float2(i.uv.x * _DistortionTiling.x + _Offset, i.uv.y * _DistortionTiling.y));
				float2 uvOffset = float2(dist.r * _Amount, dist.g * _Amount) - _Amount / 2;
				float2 uv = i.uv + uvOffset;
				float4 col = _Tint * tex2D(_MainTex, float2(uv.x * _MainTiling.x + _MainOffset, uv.y * _MainTiling.y)) + _Background * _Background.a;
				return col * _Brightness;
			}

			ENDCG
		}
	}
}
