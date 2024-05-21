Shader "Hidden/Semi-transparent"
{//Modificable desde el inspector de Unity
    Properties
    {
        [HDR] _customColor("Glow color", Color) = (1,1,1,1)
        _alpha("alpha", Range(0,1)) = 1.0
    }
        SubShader
    {
        Tags {"Queue" = "Transparent" "RenderType" = "Transparent" }
        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard alpha

        struct Input
        {
            float2 uv_MainTex;
        };

        fixed4 _customColor;
        fixed _alpha;
        void surf(Input IN, inout SurfaceOutputStandard o)
        {
             o.Albedo = _customColor.rgb;
             o.Alpha = _alpha;
        }
        ENDCG
    }
        FallBack "Diffuse"
}
