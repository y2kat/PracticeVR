Shader "Custom/Metallic"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _MetalTex("Metal" , 2D) = "white"{}
        _Metallic("Metallic", Range(0.0,1.0))=0.0
        _MyBump("Bump Texture", 2D) = "bump"{}
    }
        SubShader
    {
        Tags { "Queue" = "Geometry"}


        CGPROGRAM

        #pragma surface surf StandardSpecular
         sampler2D _MyBump;
        

        struct Input
        {
            float2 uv_MetalTex;
            float2 uv_MyBump;
           
        };

        sampler2D _MetalTex;
        half _Metallic;
        fixed4 _Color;


        void surf(Input IN, inout SurfaceOutputStandardSpecular o)
        {
            o.Albedo = _Color.rgb;
            o.Smoothness = tex2D(_MetalTex, IN.uv_MetalTex).r;
            o.Specular = _Metallic;
            o.Normal = UnpackNormal(tex2D(_MyBump, IN.uv_MyBump));
        }
        ENDCG
    }
        FallBack "Diffuse"
}