//////////////////////////////////////////
//
// NOTE: This is *not* a valid shader file
//
///////////////////////////////////////////
Shader "Hidden/ConvertTexture" {
    Properties {
    }
    SubShader {
        Pass {
            ZTest Always
            ZWrite Off
            Cull Off
            GpuProgramID 55655
            Program "vp" {
                SubProgram "d3d11 hw_tier03 " {
                    "// shader disassembly not supported on DXBC"
                }
            }
            Program "fp" {
                SubProgram "d3d11 hw_tier03 " {
                    "// shader disassembly not supported on DXBC"
                }
            }
        }
    }
}