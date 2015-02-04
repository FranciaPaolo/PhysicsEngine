float4x4 xWorldViewProjection;
float4 coloreDiffuso;

struct VertexIn
{
	float4 position:POSITION0;	
};

struct VertexOut
{
	float4 Position:POSITION0;	
};


VertexOut VertexShaderFunction(VertexIn input)
{
    VertexOut output;
    output.Position=mul(input.position,xWorldViewProjection);    

    return output;
}

float4 PixelShaderFunction(VertexOut input) : COLOR0
{    
    return coloreDiffuso;
}

technique Textured
{
    pass Pass0
    {        
        VertexShader = compile vs_1_1 VertexShaderFunction();
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
