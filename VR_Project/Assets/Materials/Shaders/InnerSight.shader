Shader "Custom/InnerSight"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        // Para que se renderice si se ve la cara trasera del tri�ngulo
        Cull Front

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            // Representa la posici�n y textura de los v�rtices del mesh que se pasan al shader
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
            // Representa la informaci�n que este shader pasar� al fragment shader (renderizar� la imagen final con esta informaci�n)
            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
            //Ejecuci�n del vertex shader
            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex); // Ubica su proyecci�n en pantalla
                o.uv = v.uv; // Asigna la textura
                return o;
            }

            sampler2D _MainTex;
            //Ejecuci�n del fragment shader
            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv); // Obtiene el color del v�rtice a partir de la textura
            return col;
    }
    ENDCG
}