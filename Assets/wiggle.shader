Shader "GGJ2024/WiggleSprite"
{
    Properties
    {
        [HideInInspector]_MainTex ("Texture", 2D) = "white" {}
        _NormalMap ("Normal map", 2D) = "white" {}
        [IntRange]_AnimationSteps ("Animations Steps", Range(1,20)) = 4
        _AnimationSpeed("Animation Speed", Float) = 10
        _DisplacementIntensity("Displacement Intensity", Float) = 0.01
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
            int _AnimationSteps;
            float _AnimationSpeed;
            float _DisplacementIntensity;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                
                return o;
            }

            float CalculateNormalMapDisplacement()
            {
                const float animationDisplacement = 1.0/_AnimationSteps;
                const int animationStep = ceil(_Time.y * _AnimationSpeed % _AnimationSteps);
                return  animationStep * animationDisplacement;
            }

            float2 GetNormalMapUvs(float2 MainUvs)
            {
                return MainUvs * _NormalMap_ST.xy + _NormalMap_ST.zw;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 normalMap = tex2D(_NormalMap, GetNormalMapUvs(i.uv) + CalculateNormalMapDisplacement());
                i.uv = i.uv + normalMap.yz * _DisplacementIntensity;
                fixed4 col = tex2D(_MainTex, i.uv);

                return col;
            }
            ENDCG
        }
    }
}
