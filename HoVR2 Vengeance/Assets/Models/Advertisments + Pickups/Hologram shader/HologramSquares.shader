Shader "RIGPR/HologramSquares"
{
	Properties
	{
		// Scan Lines
		_MainTex("Texture", 2D) = "white" {}
		_angle("Texture Rotation", Range(-5.0,  5.0)) = 0.0
		_colour("Scanline Colour", Color) = (0, 1, 0, 1)
		_scale("Scanline Scale", Range(1.0, 5.0)) = 1
		_speed("Scanline Speed", Range(-1, 1)) = 0.1
		_bias("Scanline Bias", Range(-1,-0.6)) = -0.6
		_dotBias("Dot Bias", Range(-1,1)) = -1

		_flicker("Flicker Texture", 2D) = "white" {}
	_flickerSpeed("Flicker Speed",Range(0.0, 10)) = 1.0
	}
		SubShader
	{
		Tags{ "Queue" = "Transparent" "RenderType" = "Transparant" }
		LOD 100
		ZWrite Off
		Blend SrcAlpha One
		Cull off

		Pass
	{
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
		// make fog work
#pragma multi_compile_fog

#include "UnityCG.cginc"

		struct appdata
	{
		float4 vertex : POSITION;
		float2 uv : TEXCOORD0;
	};

	struct v2f
	{
		float2 uv : TEXCOORD0;
		UNITY_FOG_COORDS(2)
			float4 vertex : SV_POSITION;
		float4 objVertex : TEXCOORD1;
	};

	sampler2D _flicker;
	sampler2D _MainTex;
	float4 _MainTex_ST;
	fixed4 _colour;
	float _scale;
	float _speed;
	float _bias;
	float _dotBias;
	float _flickerSpeed;
	float _angle;

	v2f vert(appdata v)
	{
		v2f o;
		o.objVertex = mul(unity_ObjectToWorld, v.vertex);

		o.vertex = UnityObjectToClipPos(v.vertex);

		UNITY_TRANSFER_FOG(o, o.vertex);

		float cosAngle = cos(_angle);
		float sinAngle = sin(_angle);
		float2x2 rot = float2x2(cosAngle, -sinAngle, sinAngle, cosAngle);

		// Pivot
		float2 pivot = float2(0.5, 0.5);

		float2 uv = v.uv.xy - pivot;
		o.uv = TRANSFORM_TEX((mul(rot, uv)), _MainTex);
		o.uv += pivot;
		return o;
	}

	fixed4 frag(v2f i) : SV_Target
	{
		fixed4 flicker = tex2D(_flicker, _Time * _flickerSpeed);
	_speed = _speed * 100;
	_scale = _scale * 100;

	// apply fog
	UNITY_APPLY_FOG(i.fogCoord, col);

	//Make the scanlines
	fixed4 col = _colour * tex2D(_MainTex, i.uv) * max(0, cos(i.objVertex.y * _scale + _Time.x * _speed) + _bias);
	col += _colour * tex2D(_MainTex, i.uv) * max(0, cos(i.objVertex.x * _scale + _Time.x * _speed) + _bias);
	col += _colour * tex2D(_MainTex, i.uv) * max(0, cos(i.objVertex.z * _scale + _Time.x * _speed) + _bias);
	//add bias for making dots
	col *= 1 - max(0, cos(i.objVertex.y * _scale + _Time.x * _speed) + _dotBias);
	col *= 1 - max(0, cos(i.objVertex.x * _scale + _Time.x * _speed) + _dotBias);
	col *= 1 - max(0, cos(i.objVertex.z * _scale + _Time.x * _speed) + _dotBias);


	//Make the alpha affected by the texture and speed
	col.a *= _colour.a * flicker;
	return col;
	}
		ENDCG
	}
	}
}
