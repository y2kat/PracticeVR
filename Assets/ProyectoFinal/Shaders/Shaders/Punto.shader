Shader "Custom/Punto"
{
    Properties
    {
        MyColor(" COLOR ", Color) = (1,1,1,1)
        MyColor2(" COLOR2 ", Color) = (1,1,1,1)
        MyColor3(" COLOR3 ", Color) = (1,1,1,1)
        MyEmission("Emi", Color) = (1,1,1,1)
        MyNormal("Nor", Color) = (1,1,1,1)
        _RimColor("RimColor", Color) = (0, .5, .5, 0)
        
        

      
    }
    SubShader
    {
        

        CGPROGRAM
        #pragma surface surf Lambert
        

        struct Input
        {
            float3 viewDir;
            float3 worldPos;
        };
        float4 _RimColor;

        fixed4 MyColor;
        fixed4 MyColor2;
        fixed4 MyColor3;
        fixed4 MyEmission;
        fixed4 MyNormal;

        void surf (Input IN, inout SurfaceOutput o)
        {
            //half dotp = 1-dot(IN.viewDir, o.Normal); 
            half rim =  1-(dot(normalize(IN.viewDir), o.Normal));
            //o.Albedo = float3(dotp, dotp, dotp);
            //o.Emission = rim > 0.5 ? float3(0,1,0): rim > 0.3 ? float3(0, 0, 1):0;
            //o.Normal = MyNormal.rgb;
            //o.Emission = IN.worldPos.y > 1 ? float3(0, 1, 0) : float3(0, 0, 1);
            o.Emission = frac(IN.worldPos.y * 10 * 0.5) > 0.6 ? MyColor * rim : frac(IN.worldPos.y * 10 * 0.5) > 0.4 ? MyColor2 * rim : MyColor3;
            
            
        }
        ENDCG
    }
    FallBack "Diffuse"
}
