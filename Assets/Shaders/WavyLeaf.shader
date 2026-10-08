Shader "Custom/2D/SpriteWave"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)

        [Header(Wave)]
        _WaveAmplitude ("Amplitude", Range(0, 0.5)) = 0.05
        _WaveFrequency ("Frequency", Range(0, 20)) = 5
        _WaveSpeed ("Speed", Range(-10, 10)) = 2
        _WaveDirection ("Direction", Vector) = (1, 0, 0, 0)

        [Header(Wave Axis)]
        _WaveAxis ("Wave Axis", Vector) = (0, 1, 0, 0)

        [Header(Rendering)]
        [MaterialToggle] PixelSnap ("Pixel Snap", Float) = 0
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "RenderPipeline"=""
            "CanUseSpriteAtlas"="True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend One OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #pragma multi_compile _ PIXELSNAP_ON

            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex   : SV_POSITION;
                fixed4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            sampler2D _MainTex;
            fixed4 _Color;

            float _WaveAmplitude;
            float _WaveFrequency;
            float _WaveSpeed;

            float4 _WaveDirection;
            float4 _WaveAxis;

            v2f vert(appdata_t IN)
            {
                v2f OUT;

                float3 position = IN.vertex.xyz;

                // Position along the wave axis.
                float wavePosition =
                    dot(position.xy, _WaveAxis.xy);

                // Time-based wave.
                float wave =
                    sin(
                        wavePosition * _WaveFrequency
                        + _Time.y * _WaveSpeed
                    );

                // Offset in the wave direction.
                position.xy +=
                    _WaveDirection.xy
                    * wave
                    * _WaveAmplitude;

                OUT.vertex =
                    UnityObjectToClipPos(float4(position, 1));

                OUT.texcoord = IN.texcoord;
                OUT.color = IN.color * _Color;

                #ifdef PIXELSNAP_ON
                    OUT.vertex =
                        UnityPixelSnap(OUT.vertex);
                #endif

                return OUT;
            }

            fixed4 frag(v2f IN) : SV_Target
            {
                fixed4 color =
                    tex2D(_MainTex, IN.texcoord)
                    * IN.color;

                // Premultiplied-alpha compatible output.
                color.rgb *= color.a;

                return color;
            }

            ENDCG
        }
    }
}