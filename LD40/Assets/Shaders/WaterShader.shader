Shader "Custom/Staff/Water Shader" {
Properties {
    _MainTex ("Main texture", 2D) = "white" {}
	_GrayScaleTexture ("Grayscale texture", 2D) = "white" {}
	_GrayScaleTextureWave ("Grayscale texture wave", 2D) = "white" {}
}

SubShader {
    Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
    LOD 100

    Blend SrcAlpha OneMinusSrcAlpha

    Pass {
		Cull Off
		
		CGPROGRAM
			#pragma target 3.0
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			
			struct appdata_t {
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
			};
			
			struct v2f {
				float4 vertex : SV_POSITION;
				float2 texcoord : TEXCOORD0;
				float2 actualTexcood : TEXCOORD1;
			};
			
			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				o.actualTexcood = v.texcoord;
				return o;
			}
			
			sampler2D _GrayScaleTexture;
			sampler2D _GrayScaleTextureWave;
						
			fixed4 frag (v2f i) : SV_Target
			{
				fixed value = tex2D(_GrayScaleTexture, i.actualTexcood)*3;
				float2 texcoord = i.texcoord + fixed2((value + _Time.x)%1, value);
				fixed4 col = tex2D(_MainTex, texcoord%1);
				col.rgb *= pow(tex2D(_GrayScaleTextureWave, (i.actualTexcood + fixed2(_Time.x/5, 0))%1).r, 5) + 1;
				return col;
			}
		ENDCG
    }
}

}
