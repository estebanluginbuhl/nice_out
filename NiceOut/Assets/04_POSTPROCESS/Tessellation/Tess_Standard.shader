Shader "Tessellation/Tess_Cel_noShadow"
{
	Properties
	{
		_Tess("Tessellation", Range(1,32)) = 4
		_ShadowLOD("Shadow Gen LOD", Range(0, 1.0)) = 0
		_Displacement("Displacement", Range(0, 1.0)) = 0.3
		_DispOffset("Disp Offset", Range(0, 1)) = 0.5
		_Phong("Phong Smoothing Factor", Range(0, 0.5)) = 0
		[Enum(Distance Based,0,Edge Length,1)] _TessMode("Tessellation Mode", Float) = 1

		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo", 2D) = "white" {}

		_Cutoff("Alpha Cutoff", Range(0.0, 1.0)) = 0.5

		_Glossiness("Smoothness", Range(0.0, 1.0)) = 0.5
		_GlossMapScale("Smoothness Scale", Range(0.0, 1.0)) = 1.0
		[Enum(Metallic Alpha,0,Albedo Alpha,1)] _SmoothnessTextureChannel("Smoothness texture channel", Float) = 0

		[Gamma] _Metallic("Metallic", Range(0.0, 1.0)) = 0.0
		_MetallicGlossMap("Metallic", 2D) = "white" {}

		[ToggleOff] _SpecularHighlights("Specular Highlights", Float) = 1.0
		[ToggleOff] _GlossyReflections("Glossy Reflections", Float) = 1.0

		_BumpScale("Scale", Float) = 1.0
		_BumpMap("Normal Map", 2D) = "bump" {}

		_Parallax("Height Scale", Range(0.005, 0.08)) = 0.02
		_ParallaxMap("Height Map", 2D) = "black" {}

		_OcclusionStrength("Strength", Range(0.0, 1.0)) = 1.0
		_OcclusionMap("Occlusion", 2D) = "white" {}

		_EmissionColor("Color", Color) = (0,0,0)
		_EmissionMap("Emission", 2D) = "white" {}

		_DetailMask("Detail Mask", 2D) = "white" {}

		_DetailAlbedoMap("Detail Albedo x2", 2D) = "grey" {}
		_DetailNormalMapScale("Scale", Float) = 1.0
		_DetailNormalMap("Detail Normal Map", 2D) = "bump" {}

		[Enum(UV0,0,UV1,1)] _UVSec("UV Set for secondary textures", Float) = 0

		// Blending state
		[HideInInspector] _Mode("__mode", Float) = 0.0
		[HideInInspector] _SrcBlend("__src", Float) = 1.0
		[HideInInspector] _DstBlend("__dst", Float) = 0.0
		[HideInInspector] _ZWrite("__zw", Float) = 1.0

		_Color("Light Color", COLOR) = (1,1,1,1)
		_Brightness("Shadow Brightness", Range(0,1)) = 0.3
		_Strength("Light Strength", Range(0,1)) = 0.5
		_Detail("Shadow Layers", Range(0,1)) = 0.5
		_LineStrength("Line Thickness", Range(0,1)) = 0.005
	}

	SubShader
	{
		Tags { "RenderType" = "Opaque" "PerformanceChecks" = "False" }
		LOD 400

		Pass
		{
			Name "FORWARD"
			Tags { "LightMode" = "ForwardBase" }

			CGPROGRAM
			#pragma shader_feature _ FT_EDGE_TESS
			#define TESS_FORWARD

			#pragma vertex vertBase
			#pragma fragment fragBase
			#pragma hull hs_tess
			#pragma domain ds_tess

			#include "Tess_Standard_Core_Forward.cginc"
			ENDCG
		}

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
		}

		Pass
		{
			Cull front
			Tags { "LightMode" = "ForwardBase" }

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdbase

			#include "UnityCG.cginc"
			#include "AutoLight.cginc"
			#include "Lighting.cginc"

			float _LineStrength;

			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal: NORMAL;
			};

			struct v2f
			{
				float4 position : SV_POSITION;
			};

			v2f vert(appdata v)
			{
				v2f o;
				float4 position = UnityObjectToClipPos(v.vertex);
				//define line thickness
				o.position = position + _LineStrength * UnityObjectToClipPos(normalize(v.normal));
				return o;
			}

			fixed4 _Color2;

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 col = _Color2;
				col.a = 1.0;
				return col;
			}
			ENDCG
		}
	}
}
