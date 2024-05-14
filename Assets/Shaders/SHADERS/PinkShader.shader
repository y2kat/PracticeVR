Shader "Custom/PinkShader"
{
    Properties
    {
        _MyBump("Bump Texture", 2D) = "bump" {}
        _MyCube("Cubo", CUBE) = " " {}
        _EmissionColor("Emission Color", Color) = (1, 1, 1, 1)
        _EmissionStrength("Emission Strength", Range(0, 1)) = 1
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows

        sampler2D _MyBump;
        samplerCUBE _MyCube;
        fixed4 _EmissionColor;
        half _EmissionStrength;

        struct Input
        {
            float2 uv_MyBump;
            float3 worldRefl;
            INTERNAL_DATA
        };

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            // Modificación 1: Añadir un efecto de brillo especular utilizando el mapa de bump
            o.Normal = UnpackNormal(tex2D(_MyBump, IN.uv_MyBump));

            // Modificación 2: Incorporar una textura de reflejo especular utilizando el mapa de cubemaps
            o.Emission = _EmissionColor * _EmissionStrength; // Configuramos la emisión del objeto
            o.Albedo = texCUBE(_MyCube, WorldReflectionVector(IN, o.Normal)).rgb; // Agregamos el efecto de reflejo del cubemap
        }

            ENDCG
    }
    FallBack "Diffuse"
}
