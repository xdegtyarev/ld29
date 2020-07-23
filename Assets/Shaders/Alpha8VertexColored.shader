// unlit, vertex color, 2 textures, alpha blended
// cull off

Shader "xdegtyarev/Alpha8VertexColored" 
{
	Properties 
	{
		_MainTex ("Alpha8", 2D) = "white" {}
	}

	SubShader
	{
		Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
		ZWrite Off Lighting Off Cull Off Fog { Mode Off } Blend SrcAlpha OneMinusSrcAlpha
		LOD 110
		
		Pass 
		{
			CGPROGRAM
			#pragma vertex vert_vctt
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest
			#include "UnityCG.cginc"
			
			sampler2D _MainTex;
			float4 _MainTex_ST;

			struct vin_vctt
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f_vctt
			{
				float4 vertex : POSITION;
				fixed4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			v2f_vctt vert_vctt(vin_vctt v)
			{
				v2f_vctt o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.color = v.color;
				o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);
				return o;
			}

			fixed4 frag(v2f_vctt i) : COLOR
			{
				fixed4 col = half4(1,1,1,1) * tex2D(_MainTex, i.texcoord).a * i.color;
				return col;
			}
			
			ENDCG
		} 
	}
}
