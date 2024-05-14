Shader "Custom/MyFirstTime"
{
    Properties
    {
        _MyColor("Color", Color) = (1, 1, 1, 1)
        _MyEmission("Emission", Color) = (1, 1, 1, 1)
        _MyNormal("Normal", Color) = (1, 1, 1, 1)
        _MyMultiplier("Multiplier", Range(1, 10)) = 5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        CGPROGRAM
        #pragma surface surf Lambert fullforwardshadows

        fixed4 _MyColor;
        fixed4 _MyEmission;
        fixed4 _MyNormal;
        float _MyMultiplier;

        struct Input 
        {
            float2 uv_MainTex;
        };

        void surf(Input IN, inout SurfaceOutput o)
        {
            // Mantener el color personalizado y multiplicarlo por un factor
            o.Albedo = _MyColor.rgb * _MyMultiplier;

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
