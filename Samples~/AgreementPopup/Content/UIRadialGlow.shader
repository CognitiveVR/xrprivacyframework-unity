Shader "Cognitive3D/UI/RadialGlow"
{
    Properties
    {
        _ColorCenter ("Center Color", Color) = (0.39, 0.4, 0.95, 0.5)
        _ColorEdge ("Edge Color", Color) = (0.55, 0.36, 0.96, 0)
        _Radius ("Radius", Range(0, 1)) = 0.5
        _Softness ("Softness", Range(0.01, 1)) = 0.5
        _OffsetX ("Offset X", Range(-1, 1)) = 0
        _OffsetY ("Offset Y", Range(-1, 1)) = 0
        
        _StencilComp ("Stencil Comparison", Float) = 8
        _Stencil ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask ("Stencil Read Mask", Float) = 255
        _ColorMask ("Color Mask", Float) = 15
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata { float4 vertex : POSITION; float2 uv : TEXCOORD0; };
            struct v2f { float4 vertex : SV_POSITION; float2 uv : TEXCOORD0; };

            float4 _ColorCenter, _ColorEdge;
            float _Radius, _Softness, _OffsetX, _OffsetY;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float2 center = float2(0.5 + _OffsetX, 0.5 + _OffsetY);
                float dist = distance(i.uv, center);
                float t = smoothstep(_Radius - _Softness, _Radius + _Softness, dist);
                return lerp(_ColorCenter, _ColorEdge, t);
            }
            ENDCG
        }
    }
}