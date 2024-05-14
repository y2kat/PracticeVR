Shader "Custom/OPShader"
{
    Properties
    {
       
       _MyBump("Bump Texture", 2D) = "bump"{}
      
       _MyCube("Cubo", CUBE) = " " {}
    }
        SubShader
    {
            CGPROGRAM
            #pragma surface surf Lambert
            
            sampler2D _MyBump;
            
            samplerCUBE _MyCube;

            struct Input
            {
                
                    float2 uv_MyBump;
                    float3 worldRefl; INTERNAL_DATA
            };

            void surf(Input IN, inout SurfaceOutput o) 
            {
                o.Normal = UnpackNormal(tex2D(_MyBump, IN.uv_MyBump));
                o.Albedo = texCUBE(_MyCube, WorldReflectionVector(IN, o.Normal)).rgb;
                
                
            }

            ENDCG
    }
        FallBack "Diffuse"
}