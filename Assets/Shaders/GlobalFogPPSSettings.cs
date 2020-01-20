// Amplify Shader Editor - Visual Shader Editing Tool
// Copyright (c) Amplify Creations, Lda <info@amplify.pt>
#if UNITY_POST_PROCESSING_STACK_V2
using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess( typeof( GlobalFogPPSRenderer ), PostProcessEvent.BeforeStack, "GlobalFog", true )]
public sealed class GlobalFogPPSSettings : PostProcessEffectSettings
{
	[Tooltip( "Scale" )]
	public FloatParameter _Scale = new FloatParameter { value = 0.2f };
	[Tooltip( "Offset" )]
	public FloatParameter _Offset = new FloatParameter { value = -0.9f };
	[Tooltip( "Contrast" )]
	public FloatParameter _Contrast = new FloatParameter { value = 0.1f };
	[Tooltip( "Tint" )]
	public ColorParameter _Tint = new ColorParameter { value = new Color(1f,1f,1f,0f) };
}

public sealed class GlobalFogPPSRenderer : PostProcessEffectRenderer<GlobalFogPPSSettings>
{
	public override void Render( PostProcessRenderContext context )
	{
		var sheet = context.propertySheets.Get( Shader.Find( "Global Fog" ) );
		sheet.properties.SetFloat( "_Scale", settings._Scale );
		sheet.properties.SetFloat( "_Offset", settings._Offset );
		sheet.properties.SetFloat( "_Contrast", settings._Contrast );
		sheet.properties.SetColor( "_Tint", settings._Tint );
		context.command.BlitFullscreenTriangle( context.source, context.destination, sheet, 0 );
	}
}
#endif
