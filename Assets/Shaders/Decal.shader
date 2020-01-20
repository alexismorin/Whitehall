Shader "Decal" {
  Properties {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _Glossiness ("Smoothness", Range(0,1)) = 0.5
  }
  SubShader {
    Tags { "RenderType"="Opaque" "Queue"="Geometry+1" "ForceNoShadowCasting"="True" }
    LOD 200
    Offset -1, -1
    
    CGPROGRAM
    #pragma surface surf Standard decal:blend
    
    sampler2D _MainTex;
    half _Glossiness;
    
    struct Input {
      float2 uv_MainTex;
    };
    


    void surf (Input IN, inout SurfaceOutputStandard o) {
        half4 c = tex2D (_MainTex, IN.uv_MainTex);
        o.Albedo = c.rgb;
        o.Smoothness = c.r + _Glossiness;
        o.Alpha = c.a;
      }
    ENDCG
    }
}