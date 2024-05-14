Shader "Custom/MyFirstTime"
{
    Properties
    {
        _MyColor("Color", Color) = (1, 1, 1, 1)
        _MyEmission("Emission", Color) = (1, 1, 1, 1)
        _MyNormal("Normal", Color) = (1, 1, 1, 1)
        _MyMultiplication("Multiplication", int) = 5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        CGPROGRAM
        #pragma surface surf Lambert fullforwardshadows

        struct Input 
        {
            float2 uvMainTex;
        };

        fixed4 _MyColor;
        fixed4 _MyEmission;
        fixed4 _MyNormal;
        int _MyMultiplication;

        void surf(Input IN, inout SurfaceOutput o)
        {
            // Modificación mínima: Aplicar el color personalizado al albedo
            o.Albedo = _MyColor.rgb * _MyMultiplication;

            // Mantener el efecto de emisión utilizando MyEmission
            o.Emission = _MyEmission.rgb;

            // Mantener el efecto de normal utilizando MyNormal
            o.Normal = _MyNormal.rgb;
        }

        //ODIO MI VIDA!!!!!!!!!!!!111
        
        ENDCG
    }
    FallBack "Diffuse"
}
