Shader "Custom/SafetyPanelStripe"
{
    Properties
    {
        [HDR] _Color ("Energy Color", Color) = (0.2, 0.6, 1.0, 1.0)
        _Density ("Stripe Density", Float) = 20.0
        _Speed ("Scroll Speed", Float) = 5.0
        _Intensity ("Intensity", Range(0, 5)) = 1.0
        _PulseSpeed ("Pulse Speed", Float) = 1.5
        _PulseAmount ("Pulse Amount", Range(0, 1)) = 0.3
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100

        // 両面描画、Z書き込みオフ
        Cull Off
        ZWrite Off
        
        // 加算合成
        Blend SrcAlpha One

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 localPos : TEXCOORD0;
            };

            fixed4 _Color;
            float _Density;
            float _Speed;
            float _Intensity;
            float _PulseSpeed;
            float _PulseAmount;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                // UVの代わりに、オブジェクトのローカル座標のXとYを取得
                o.localPos = v.vertex.xy;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // ローカル座標のXとYを足して斜めの波を作る
                // Z座標を使わないため、直方体の前面と背面の模様が空間上で完全に一致する
                float wave = sin((i.localPos.x + i.localPos.y) * _Density - _Time.y * _Speed);
                
                // smoothstepを使用してアンチエイリアスの効いた滑らかなストライプにする
                float stripe = smoothstep(-0.1, 0.1, wave);

                // 呼吸効果（ゆっくりとした明滅）
                float pulse = 1.0 - _PulseAmount * (0.5 * sin(_Time.y * _PulseSpeed) + 0.5);

                // 最終的な色（加算合成なのでアルファは常に1.0で出力し、黒が透過される）
                fixed3 finalColor = _Color.rgb * stripe * _Intensity * pulse;
                
                return fixed4(finalColor, 1.0);
            }
            ENDCG
        }
    }
}