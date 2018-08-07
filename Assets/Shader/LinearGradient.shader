// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/LinearGradient"
 {
     Properties{
         _TopColor("Color1", Color) = (1,1,1,1)
         _MiddenColor("Color2", Color) = (1,1,1,1)
         _BottomColor("Color3", Color) = (1,1,1,1)
         [MaterialToggle] _Horizontation("Horizontation", Int) = 0

         _MainTex ("Main Texture", 2D) = "white" {}
     }
         SubShader
     {
         Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
         Pass
         {
             ZWrite Off
             Blend SrcAlpha OneMinusSrcAlpha
 
             CGPROGRAM
             #pragma vertex vert
             #pragma fragment frag
 
             #include "UnityCG.cginc"
 
             sampler2D _MainTex;
             float4 _MainTex_ST;
 
             fixed4 _TopColor;
             fixed4 _MiddenColor;
             fixed4 _BottomColor;
             int _Horizontation;
             half _Value;
 
             struct v2f {
                 float4 position : SV_POSITION;
                 fixed4 color : COLOR;
                 float2 uv : TEXCOORD0;
             };
 
             v2f vert (appdata_full v)
             {
                 v2f o;
                 o.position = UnityObjectToClipPos (v.vertex);
                 o.uv = TRANSFORM_TEX (v.texcoord, _MainTex);
                 if (_Horizontation == 1){
                 	o.color = lerp (_TopColor,_BottomColor, v.texcoord.x);
                 } else {
                 	o.color = lerp (_TopColor,_BottomColor, v.texcoord.y);
                 }
                 return o;
             }
 
             fixed4 frag(v2f i) : SV_Target
             {
                 float4 color;
                 color.rgb = i.color.rgb;
                 color.a = tex2D (_MainTex, i.uv).a * i.color.a;
                 return color;
             }
             ENDCG
         }
     }
 }