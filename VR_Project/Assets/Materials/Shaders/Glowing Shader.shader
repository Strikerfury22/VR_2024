Shader "Custom/Glowing Shader"
{
    //Modificable desde el inspector de Unity
    Properties
    {
        [HDR] _customColor("Glow color", Color) = (1,1,1,1)
        _percentage("porcentaje", Range(0,1)) = 0.0
        //_customColor("Main color", Color) = (1,1,1,1)
        _enabled("Emits light", Integer) = 0
    }
        SubShader
    {
        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard

        struct Input
        {
            float2 uv_MainTex;
        };

        fixed4 _customColor;
        float _percentage;
        uint _enabled;
        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            /*timer += unity_DeltaTime;
            if (timer >= waitTime) {
                timer = 0;
                if (_enabled == 1) {
                    _enabled = 0;
                    //_customColor.rgb = (0, 0, 0, 1);
                    o.Albedo = (0,0,0);
                }
                else {
                    _enabled = 1;
                    o.Albedo = _customColor.rgb;
                    //_customColor.rgb = (0, 1, 0, 1);
                }
            }*/
            if (_enabled == 1) {
                o.Emission = _customColor.rgb * _percentage;// _customColor.rgb;// true; // Vi que existia en el por defecto, probé y existía. No he encontrado documentación.
            }
            else {
               // o.Albedo = _customColor.rgb;
            }
            
        }
        ENDCG
    }
        FallBack "Diffuse"
}
