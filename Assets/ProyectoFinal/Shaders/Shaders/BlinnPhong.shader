Shader "Custom/BlinnPhong"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _SpecColor("Spec Color",Color) = (1,1,1,1)
        _Spec("Specular",Range(0,1)) = 0.5
        _Gloss("Gloss",Range(0,1)) = 0.5
        _MyBump("Bump Texture", 2D) = "bump"{}
    }
    SubShader
    {
        Tags { "Queue"="Geometry"}


        CGPROGRAM

        #pragma surface surf BlinnPhong
        sampler2D _MyBump;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_MyBump;
        };

        float4 _Color;
        half _Spec;
        fixed _Gloss;


        void surf(Input IN, inout SurfaceOutput o)
        {
            o.Albedo = _Color.rgb;
            o.Normal = UnpackNormal(tex2D(_MyBump, IN.uv_MyBump));
            o.Specular = _Spec;
            o.Gloss = _Gloss;
        }
        ENDCG
    }
        FallBack "Diffuse"
}
