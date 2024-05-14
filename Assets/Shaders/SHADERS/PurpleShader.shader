Shader "Custom/PurpleShader"
{
    Properties
    {
        _MyColor("Color", Color) = (1, 1, 1, 1)
        _MyRange("Range", Range(0, 10)) = 1
        _MyTex("Textura", 2D) = "white" {}
        _MyCube("Cubo", CUBE) = " " {}
        _MyFloat("Float", Range(0, 1)) = 0.5
        _MyVector("Vector", Vector) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        CGPROGRAM
        #pragma surface surf Lambert fullforwardshadows

        fixed4 _MyColor;
        float _MyRange;
        sampler2D _MyTex;
        samplerCUBE _MyCube;
        float _MyFloat;
        float4 _MyVector;

        struct Input
        {
            float2 uv_MyTex;
            float3 worldRefl;
            INTERNAL_DATA
        };

        void surf(Input IN, inout SurfaceOutput o)
        {
            //Aplicar el color personalizado al albedo
            o.Albedo = tex2D(_MyTex, IN.uv_MyTex) * _MyColor;

            // Mantener el efecto de emisión utilizando el mapa de cubemaps
            o.Emission = texCUBE(_MyCube, IN.worldRefl).rgb;
        }

        ENDCG
    }
    FallBack "Diffuse"
}
