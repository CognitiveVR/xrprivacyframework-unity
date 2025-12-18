Shader "Cognitive3D/UI/Gradient"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _ColorTop ("Top Color", Color) = (0.39, 0.4, 0.95, 1)
        _ColorBottom ("Bottom Color", Color) = (0.1, 0.1, 0.24, 1)
        _ColorLeft ("Left Color", Color) = (1, 1, 1, 1)
        _ColorRight ("Right Color", Color) = (1, 1, 1, 1)
        [Toggle] _UseHorizontal ("Use Horizontal Blend", Float) = 0
        [Toggle] _UseDiagonal ("Use Diagonal (4-corner)", Float) = 0
        _Angle ("Gradient Angle", Range(0, 360)) = 0
        
        // Required for UI
        _StencilComp ("Stencil Comparison", Float) = 8
        _Stencil ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask ("Stencil Read Mask", Float) = 255
        _ColorMask ("Color Mask", Float) = 15
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Stencil
        {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp]
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }

        Cull Off
        Lighting Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]
        Blend SrcAlpha OneMinusSrcAlpha
        ColorMask [_ColorMask]

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
                float4 color : COLOR;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            sampler2D _MainTex;
            float4 _ColorTop, _ColorBottom, _ColorLeft, _ColorRight;
            float _UseHorizontal, _UseDiagonal, _Angle;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.color = v.color;
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float4 tex = tex2D(_MainTex, i.uv);
                float4 gradientColor;

                if (_UseDiagonal > 0.5)
                {
                    // 4-corner gradient
                    float4 topColor = lerp(_ColorLeft, _ColorRight, i.uv.x);
                    float4 bottomColor = lerp(_ColorBottom, _ColorTop, i.uv.x);
                    gradientColor = lerp(bottomColor, topColor, i.uv.y);
                }
                else if (_UseHorizontal > 0.5)
                {
                    gradientColor = lerp(_ColorLeft, _ColorRight, i.uv.x);
                }
                else
                {
                    // Vertical or angled gradient
                    float angle = _Angle * 0.0174533; // degrees to radians
                    float2 dir = float2(cos(angle), sin(angle));
                    float t = dot(i.uv - 0.5, dir) + 0.5;
                    gradientColor = lerp(_ColorBottom, _ColorTop, saturate(t));
                }

                return gradientColor * tex * i.color;
            }
            ENDCG
        }
    }
}