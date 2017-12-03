Shader "Custom/PostEffectShader" {
Properties {
    _MainTex ("Main texture", 2D) = "white" {}
	_Pixels ("Pixels", Vector) = (10,10,0,0)
	_RGBOffsetDelta ("RGB offset", float) = 0
	_FadeTexture ("Fade texture", 2D) = "white" {}
	_FadeDelta ("Fade delta", range(-.1, 1.1)) = 0
	_FadeColor ("Fade color", color) = (1,1,1,1)
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
			};
			
			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				return o;
			}
			
			sampler2D _GrayScaleTexture;
			sampler2D _GrayScaleTextureWave;
			fixed4 _Pixels;
			fixed _RGBOffsetDelta;
			sampler2D _FadeTexture;
			fixed _FadeDelta;
			fixed4 _FadeColor;
						
			fixed4 frag (v2f i) : SV_Target
			{	
				fixed4 col = tex2D(_MainTex, i.texcoord);
				fixed fadeStep = step(tex2D(_FadeTexture, i.texcoord).r, _FadeDelta);
				col.rgb = col.rgb * fadeStep + _FadeColor.rgb * (1-fadeStep);
				return col;
			}
		ENDCG
    }
}

}
