Shader "Custom/Tiles/Tile Shader" {
Properties {
    _MainTex ("Main texture", 2D) = "white" {}
	_CrackTex ("Crack texture", 2D) = "white" {}
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
			
			sampler2D _CrackTex;
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.texcoord);
				fixed4 crackColor = tex2D(_CrackTex, i.texcoord);
				col.rgb = col.rgb * (1-crackColor.a) + crackColor.rgb * crackColor.a;
				return col;
			}
		ENDCG
    }
}

}
