Shader "Unlit/CelShaderTest"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Brightness("Shadow Brightness", Range(0,1)) = 0.3
		_Strength("Light Strength", Range(0,1)) = 0.5
		_Color("Light Color", COLOR) = (1,1,1,1)
		_Detail("Shadow Layers", Range(0,1)) = 0.5
	}
	SubShader
	{
		Tags { "RenderType" = "Opaque" }
		LOD 100

		Pass
		{
			Tags { "LightMode" = "ForwardBase" }

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag 

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				half3 worldNormal : NORMAL;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _Color;
			float _Brightness;
			float _Strength;
			float _Detail;

			float Cel(float3 normal, float3 lightDir)
			{
				float NdotL = max(0.0, dot(normalize(normal), normalize(lightDir)));

				return floor(NdotL / _Detail);
			}

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.worldNormal = UnityObjectToWorldNormal(v.normal);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				col *= Cel(i.worldNormal, _WorldSpaceLightPos0.xyz) * _Strength * _Color + _Brightness;
				return col;
			}
			ENDCG

			CGPROGRAM
				#pragma target 3.0

				#pragma multi_compile_fwdbase

				#pragma vertex vertBase
				#pragma fragment fragBase
				#include "UnityStandardCoreForward.cginc"
			ENDCG
		}
	}
	FallBack "VertexLit"
}