Shader "Custom/Mojadita"
{
    Properties
    {
        _MyColor("Color", Color) = (1, 1, 1, 1)
        _MyRange("Range", Range(0, 10)) = 1
        _MyTex("Textura", 2D) = "white" {}
        _MyFloat("Float", Range(0, 1)) = 0.5
        _MyVector("Vector", Color) = (1, 1, 1, 1)
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows

        sampler2D _MyTex;
        fixed4 _MyColor;
        float _MyRange;
        float _MyFloat;
        float4 _MyVector;

        struct Input
        {
            float2 uv_MyTex;
            float3 worldRefl;
            INTERNAL_DATA
        };

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            //Aplicar el color personalizado al albedo
            fixed4 texColor = tex2D(_MyTex, IN.uv_MyTex) * _MyColor;

            //Ajustar la intensidad del color basado en MyRange
            texColor.rgb *= _MyRange / 10;

            // Ajustar el brillo del objeto utilizando MyFloat
            o.Smoothness = _MyFloat;

            // Ajustar la emisión del objeto utilizando MyVector
            o.Emission = _MyVector.rgb;

            // Asignar el color al albedo final
            o.Albedo = texColor.rgb;
        }

        ENDCG
    }
    FallBack "Diffuse"
}
