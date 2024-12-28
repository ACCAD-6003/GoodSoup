Shader "Custom/Outline Fill" {
  Properties {
    [Enum(UnityEngine.Rendering.CompareFunction)] _ZTest("ZTest", Float) = 0

    _OutlineColor("Outline Color", Color) = (1, 1, 1, 1)
    _OutlineWidth("Outline Width", Range(0, 10)) = 2
    _DotSpacing("Dot Spacing", Range(1, 10)) = 3
    _DotSize("Dot Size", Range(0.1, 1)) = 0.5
  }

  SubShader {
    Tags {
      "Queue" = "Transparent+110"
      "RenderType" = "Transparent"
      "DisableBatching" = "True"
    }

    Pass {
      Name "Fill"
      Cull Off
      ZTest [_ZTest]
      ZWrite Off
      Blend SrcAlpha OneMinusSrcAlpha
      ColorMask RGB

      Stencil {
        Ref 1
        Comp NotEqual
      }

      CGPROGRAM
      #include "UnityCG.cginc"

      #pragma vertex vert
      #pragma fragment frag

      struct appdata {
        float4 vertex : POSITION;
        float3 normal : NORMAL;
        float3 smoothNormal : TEXCOORD3;
        UNITY_VERTEX_INPUT_INSTANCE_ID
      };

      struct v2f {
        float4 position : SV_POSITION;
        float2 screenUV : TEXCOORD0;
        fixed4 color : COLOR;
        UNITY_VERTEX_OUTPUT_STEREO
      };

      uniform fixed4 _OutlineColor;
      uniform float _OutlineWidth;
      uniform float _DotSpacing;
      uniform float _DotSize;

      v2f vert(appdata input) {
        v2f output;

        UNITY_SETUP_INSTANCE_ID(input);
        UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);

        float3 normal = any(input.smoothNormal) ? input.smoothNormal : input.normal;
        float3 viewPosition = UnityObjectToViewPos(input.vertex);
        float3 viewNormal = normalize(mul((float3x3)UNITY_MATRIX_IT_MV, normal));

        output.position = UnityViewToClipPos(viewPosition + viewNormal * _OutlineWidth / 50);
        output.color = _OutlineColor;

        // Calculate screen-space UV coordinates
        output.screenUV = output.position.xy / output.position.w * 0.5 + 0.5;

        return output;
      }

      fixed4 frag(v2f input) : SV_Target {
        // Create a dotted pattern using screen-space coordinates
        float2 pattern = frac(input.screenUV * _DotSpacing);
        float dot = smoothstep(_DotSize * 0.5, _DotSize, abs(pattern.x - 0.5)) * 
                    smoothstep(_DotSize * 0.5, _DotSize, abs(pattern.y - 0.5));

        // Apply the dotted pattern to the outline
        return input.color * dot;
      }
      ENDCG
    }
  }
}
