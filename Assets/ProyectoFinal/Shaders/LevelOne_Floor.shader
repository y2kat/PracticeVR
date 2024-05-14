Shader "Custom/Mojadita"
{
    Properties
    {
        _MyTex("Albedo", 2D) = "white" {}
        _MyBump("Bump Texture", 2D) = "bump" {}
        MyRange("Range", Range(0, 10)) = 1
        _MyCube("Cubo", CUBE) = " " {}
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows

        sampler2D _MyTex;
        sampler2D _MyBump;
        half MyRange;
        samplerCUBE _MyCube;

        struct Input
        {
            float2 uv_MyTex;
            float2 uv_MyBump;
            float3 worldRefl;
        };

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            //Ajustar el Albedo para simular un efecto "mojado"
            o.Albedo = tex2D(_MyTex, IN.uv_MyTex).rgb * 0.8 + float3(0.2, 0.2, 0.2);

            // Aumentar la intensidad del brillo
            o.Metallic = 1;
            o.Smoothness = 1 - MyRange / 10;

            //Añadir un efecto de distorsión utilizando el mapa de bump
            o.Normal = UnpackNormal(tex2D(_MyBump, IN.uv_MyBump));
        }

            ENDCG
    }
    FallBack "Diffuse"
}
