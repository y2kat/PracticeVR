Shader "Custom/Normals_shader"
{
    Properties
    {
       _MyTex("Albedo", 2D) = "white"{}
       _MyBump("Bump Texture", 2D) = "bump"{}
       MyRange("Range",Range(0,10)) = 1
       _MyCube("Cubo", CUBE) = " " {}
    }
        SubShader
    {
            CGPROGRAM
            #pragma surface surf Lambert
            sampler2D _MyTex;
            sampler2D _MyBump;
            half MyRange;
            samplerCUBE _MyCube;

            struct Input {
                float2 uv_MyTex;
                    float2 uv_MyBump;
                    float3 worldRefl; INTERNAL_DATA
            };

            void surf(Input IN, inout SurfaceOutput o) {
                o.Albedo = tex2D(_MyTex, IN.uv_MyTex).rgb;
                o.Normal = UnpackNormal(tex2D(_MyBump, IN.uv_MyBump));
                //o.Normal.xy *= MyRange;
                o.Normal *= float3 (MyRange, MyRange, 1);
                o.Emission = texCUBE(_MyCube, WorldReflectionVector(IN,o.Normal)).rgb;
            }

            ENDCG
    }
        FallBack "Diffuse"
}
