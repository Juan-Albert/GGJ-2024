Shader "Unlit/wiggle"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _NormalMap ("Normal map", 2D) = "white" {}
        _Speed("Speed", float)=0.1
        _Factor("Factor", float)=0.1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Blend SrcAlpha OneMinusSrcAlpha
        LOD 100

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
            sampler2D _NormalMap;
            float4 _NormalMap_ST;
            float _Speed;
            float _Factor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 normalUV = i.uv * _NormalMap_ST.xy + _NormalMap_ST.zw;
                float myTime=_Time.y%5;
                fixed4 normalMap = tex2D(_NormalMap, normalUV+myTime);
                return normalMap;

                //return normalMap;
                i.uv = i.uv+normalMap.yz*_Factor;
                fixed4 col = tex2D(_MainTex, i.uv);

                return col;
            }
            ENDCG
        }
    }
}
