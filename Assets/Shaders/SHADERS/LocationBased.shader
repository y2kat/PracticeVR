Shader "Custom/LocationBased"
{
    Properties
    {
        _MainTex("Texture 1", 2D) = "white" {}
        _SecondTex("Texture 2", 2D) = "white" {}
        _Color("Color", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        CGPROGRAM
        #pragma surface surf Lambert fullforwardshadows

        sampler2D _MainTex;
        sampler2D _SecondTex;
        fixed4 _Color;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_SecondTex;
            float3 worldPos;
        };

        void surf(Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            fixed4 d = tex2D(_SecondTex, IN.uv_SecondTex) * _Color;
            o.Albedo = (IN.worldPos.y > 0.5) ? c.rgb : d.rgb;
            o.Alpha = c.a;
        }

        ENDCG
    }
    FallBack "Diffuse"
}
