Shader "Custom/RadialGaussianBlur"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _InnerRadius ("Inner Radius", Float) = 0.15
        _OuterRadius ("Outer Radius", Float) = 0.6
        _MaxBlurPixels ("Max Blur Pixels", Float) = 3.0
    }

    SubShader
    {
        // post-proceso full screen
        Tags { "RenderType"="Opaque" "Queue"="Overlay" }
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _TexelSize;          // x = 1/width, y = 1/height
            float4 _PlayerScreenPos;    // xy = pos en viewport [0,1]
            float _InnerRadius;
            float _OuterRadius;
            float _MaxBlurPixels;

            fixed4 frag (v2f_img i) : SV_Target
            {
                float2 uv = i.uv;

                // Distancia radial al jugador en coords [0,1]
                float2 center = _PlayerScreenPos.xy;
                float2 rel = uv - center;

                // aspect = width / height = (_TexelSize.y / _TexelSize.x)
                float aspect = _TexelSize.y / _TexelSize.x;
                rel.x *= aspect;          // corregimos X para que el círculo no sea óvalo

                float d = length(rel);    // distancia "circular" en pantalla

                // Factor de blur: 0 dentro de inner, 1 fuera de outer
                float t = saturate((d - _InnerRadius) / max(1e-5, (_OuterRadius - _InnerRadius)));

                // Si casi no hay blur, devolvemos el color original
                if (t <= 0.001)
                    return tex2D(_MainTex, uv);

                // Escalamos el radio de blur según lo lejos que está
                float blurPixels = _MaxBlurPixels * t;
                float2 offset = _TexelSize.xy * blurPixels;

                // Kernel simple tipo gaussiano aproximado (9 muestras)
                fixed4 c = tex2D(_MainTex, uv) * 0.28;

                c += tex2D(_MainTex, uv + float2( offset.x, 0)) * 0.16;
                c += tex2D(_MainTex, uv - float2( offset.x, 0)) * 0.16;
                c += tex2D(_MainTex, uv + float2( 0, offset.y)) * 0.16;
                c += tex2D(_MainTex, uv - float2( 0, offset.y)) * 0.16;

                c += tex2D(_MainTex, uv + float2( offset.x,  offset.y)) * 0.06;
                c += tex2D(_MainTex, uv + float2(-offset.x,  offset.y)) * 0.06;
                c += tex2D(_MainTex, uv + float2( offset.x, -offset.y)) * 0.06;
                c += tex2D(_MainTex, uv + float2(-offset.x, -offset.y)) * 0.06;

                return c;
            }
            ENDCG
        }
    }
    FallBack Off
}

