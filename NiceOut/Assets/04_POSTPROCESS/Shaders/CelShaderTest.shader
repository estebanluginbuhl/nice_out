Shader "Unlit/CelShaderTest"
{
    Properties
    {
		_MainTex("Texture", 2D) = "white" {}
		_Brightness("Shadow Brightness", Range(0,1)) = 0.3
		_Strength("Light Strength", Range(0,1)) = 0.5
		_Color("Light Color", COLOR) = (1,1,1,1)
		_Detail("Shadow Layers", Range(0,1)) = 0.5
		_LineStrength("Line Thickness", Range (0,1)) = 0.005
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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.worldNormal = UnityObjectToWorldNormal(v.normal);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
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