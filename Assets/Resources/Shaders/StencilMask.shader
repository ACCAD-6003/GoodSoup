Shader "Custom/StencilMask"
{
    SubShader
    {
        Tags { "Queue" = "Geometry" }
        Pass
        {
            Stencil
            {
                Ref 1
                Comp Always
                Pass Replace
            }
            ColorMask 0 // Don't render any color, only affect the stencil buffer
        }
    }
}