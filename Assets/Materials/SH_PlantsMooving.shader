// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "SH_Tree"
{
	Properties
	{
		_Heightmap("Heightmap", 2D) = "white" {}
		_Height_Mask("Height_Mask", Int) = 20
		_Tile01("Tile01", Int) = 5000
		_Int01("Int01", Int) = 3
		_Speed01("Speed01", Float) = 0.1
		_Tile02("Tile02", Int) = 50
		_Int02("Int02", Int) = 5
		_Speed02("Speed02", Float) = 0.1
		_WindDirection("WindDirection", Range( 0 , 2)) = 0
		_amplitude("amplitude", Float) = 10
		_Color0("Color 0", Color) = (0,0,0,0)
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Off
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma multi_compile_instancing
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows vertex:vertexDataFunc 
		struct Input
		{
			fixed filler;
		};

		uniform float4 _Color0;
		uniform int _Height_Mask;
		uniform sampler2D _Heightmap;
		uniform float _Speed01;
		uniform int _Tile01;
		uniform int _Int01;
		uniform float _Speed02;
		uniform int _Tile02;
		uniform int _Int02;
		uniform float _WindDirection;
		uniform float _amplitude;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 ase_vertex3Pos = v.vertex.xyz;
			float mulTime106 = _Time.y * _Speed01;
			float3 ase_worldPos = mul( unity_ObjectToWorld, v.vertex );
			float4 appendResult19 = (float4(ase_worldPos.x , ase_worldPos.z , 0.0 , 0.0));
			float2 panner22 = ( ( appendResult19 / _Tile01 ).xy + mulTime106 * float2( 0.5,0.5 ));
			float mulTime107 = _Time.y * _Speed02;
			float2 panner43 = ( ( appendResult19 / _Tile02 ).xy + mulTime107 * float2( 0.5,0.5 ));
			float VcolR85 = v.color.r;
			float2 appendResult95 = (float2(sin( ( _WindDirection * UNITY_PI ) ) , cos( ( _WindDirection * UNITY_PI ) )));
			float4 appendResult28 = (float4(( ( ( ( ( ase_vertex3Pos.z / _Height_Mask ) * tex2Dlod( _Heightmap, float4( panner22, 0, 0.0) ) * _Int01 ) + ( tex2Dlod( _Heightmap, float4( panner43, 0, 0.0) ) * VcolR85 * _Int02 ) ) + float4( 0,0,0,0 ) ) * float4( appendResult95, 0.0 , 0.0 ) ).r , ( ( ( ( ( ase_vertex3Pos.z / _Height_Mask ) * tex2Dlod( _Heightmap, float4( panner22, 0, 0.0) ) * _Int01 ) + ( tex2Dlod( _Heightmap, float4( panner43, 0, 0.0) ) * VcolR85 * _Int02 ) ) + float4( 0,0,0,0 ) ) * float4( appendResult95, 0.0 , 0.0 ) ).g , (float)0 , 0.0));
			v.vertex.xyz += ( appendResult28 / _amplitude ).xyz;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			o.Albedo = _Color0.rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=13501
106;196;1585;823;-440.5558;-169.1193;1;True;True
Node;AmplifyShaderEditor.WorldPosInputsNode;18;-2814.353,980.3422;Float;False;0;4;FLOAT3;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.IntNode;41;-1941.71,975.1643;Float;False;Property;_Tile02;Tile02;7;0;50;0;1;INT
Node;AmplifyShaderEditor.RangedFloatNode;101;-1691.441,1033.427;Float;False;Property;_Speed02;Speed02;9;0;0.1;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.IntNode;21;-1969.372,624.0834;Float;False;Property;_Tile01;Tile01;4;0;5000;0;1;INT
Node;AmplifyShaderEditor.DynamicAppendNode;19;-2420.289,1048.403;Float;False;FLOAT4;4;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;3;FLOAT;0.0;False;1;FLOAT4
Node;AmplifyShaderEditor.RangedFloatNode;100;-1573.645,605.7377;Float;False;Property;_Speed01;Speed01;6;0;0.1;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.SimpleDivideOpNode;20;-1419.21,404.3435;Float;False;2;0;FLOAT4;0,0,0,0;False;1;INT;0,0,0,0;False;1;FLOAT4
Node;AmplifyShaderEditor.SimpleTimeNode;107;-1505.84,1024.185;Float;False;1;0;FLOAT;1.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleTimeNode;106;-1354.366,540.4969;Float;False;1;0;FLOAT;1.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleDivideOpNode;42;-1656.546,853.1743;Float;False;2;0;FLOAT4;0,0,0,0;False;1;INT;0,0,0,0;False;1;FLOAT4
Node;AmplifyShaderEditor.PosVertexDataNode;7;-990.4675,45.76503;Float;False;0;0;5;FLOAT3;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.VertexColorNode;1;-1633.701,-397.2343;Float;False;0;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.IntNode;80;-997.1567,235.7893;Float;False;Property;_Height_Mask;Height_Mask;3;0;20;0;1;INT
Node;AmplifyShaderEditor.PannerNode;43;-1377.041,854.475;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0.5,0.5;False;1;FLOAT;0.1;False;1;FLOAT2
Node;AmplifyShaderEditor.TexturePropertyNode;37;-1466.931,1167.312;Float;True;Property;_Heightmap;Heightmap;2;0;None;False;white;Auto;0;1;SAMPLER2D
Node;AmplifyShaderEditor.PannerNode;22;-1139.705,405.6442;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0.5,0.5;False;1;FLOAT;0.1;False;1;FLOAT2
Node;AmplifyShaderEditor.GetLocalVarNode;88;-671.8552,1008.641;Float;False;85;0;1;FLOAT
Node;AmplifyShaderEditor.IntNode;24;-466.5222,577.7643;Float;False;Property;_Int01;Int01;5;0;3;0;1;INT
Node;AmplifyShaderEditor.SimpleDivideOpNode;36;-580.8284,212.453;Float;False;2;0;FLOAT;0.0;False;1;INT;0;False;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;92;-174.6794,1114.105;Float;False;Property;_WindDirection;WindDirection;11;0;0;0;2;0;1;FLOAT
Node;AmplifyShaderEditor.SamplerNode;38;-1080.222,818.1946;Float;True;Property;_TextureSample0;Texture Sample 0;1;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SamplerNode;16;-826.4067,373.1431;Float;True;Property;_T_PerlinNoise;T_PerlinNoise;1;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.IntNode;45;-621.8245,1214.072;Float;False;Property;_Int02;Int02;8;0;5;0;1;INT
Node;AmplifyShaderEditor.RegisterLocalVarNode;85;-1328.486,-338.7112;Float;False;VcolR;-1;True;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;23;-212.8024,395.2439;Float;False;3;3;0;FLOAT;0.0;False;1;COLOR;0;False;2;INT;0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.PiNode;99;146.4738,1115.467;Float;False;1;0;FLOAT;1.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;47;-400.3361,844.9209;Float;False;3;3;0;COLOR;0,0,0,0;False;1;FLOAT;0.0,0,0,0;False;2;INT;0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.CosOpNode;94;373.73,1169.899;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SinOpNode;93;376.4516,1095.054;Float;False;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleAddOpNode;48;-79.88843,691.313;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.DynamicAppendNode;95;586.0173,1096.415;Float;False;FLOAT2;4;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;3;FLOAT;0.0;False;1;FLOAT2
Node;AmplifyShaderEditor.SimpleAddOpNode;91;146.9318,870.3651;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;96;765.6453,903.1789;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT2;0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.BreakToComponentsNode;97;901.727,905.9005;Float;False;COLOR;1;0;COLOR;0,0,0,0;False;16;FLOAT;FLOAT;FLOAT;FLOAT;FLOAT;FLOAT;FLOAT;FLOAT;FLOAT;FLOAT;FLOAT;FLOAT;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.IntNode;29;953.8832,1049.154;Float;False;Constant;_Int2;Int 2;1;0;0;0;1;INT
Node;AmplifyShaderEditor.DynamicAppendNode;28;1196.463,883.1268;Float;False;FLOAT4;4;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;3;FLOAT;0.0;False;1;FLOAT4
Node;AmplifyShaderEditor.RangedFloatNode;110;1210.556,1139.119;Float;False;Property;_amplitude;amplitude;15;0;10;0;0;0;1;FLOAT
Node;AmplifyShaderEditor.SamplerNode;70;-984.2653,2088.519;Float;True;Property;_TextureSample2;Texture Sample 2;1;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleDivideOpNode;64;-1577.069,2119.719;Float;False;2;0;INT;0;False;1;INT;0;False;1;INT
Node;AmplifyShaderEditor.GetLocalVarNode;90;-632.9669,2304.123;Float;False;86;0;1;FLOAT
Node;AmplifyShaderEditor.PannerNode;65;-1384.499,2480.138;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0.5,0.5;False;1;FLOAT;0.1;False;1;FLOAT2
Node;AmplifyShaderEditor.GetLocalVarNode;89;-638.5275,2653.454;Float;False;86;0;1;FLOAT
Node;AmplifyShaderEditor.SamplerNode;69;-1087.679,2443.858;Float;True;Property;_TextureSample1;Texture Sample 1;1;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleDivideOpNode;109;1439.556,898.1193;Float;False;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;0,0,0,0;False;1;FLOAT4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;76;-370.6613,2110.62;Float;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.SamplerNode;108;1108.792,367.4254;Float;True;Property;_TextureSample3;Texture Sample 3;0;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;75;-407.7939,2470.584;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0.0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.ColorNode;34;429.7426,-414.0617;Float;False;InstancedProperty;_PrincipalColor;PrincipalColor;0;0;1,0.3823529,0.497363,0;0;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleAddOpNode;77;-111.0936,2187.684;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.SimpleSubtractOpNode;81;-2027.126,2100.032;Float;False;2;0;INT;0;False;1;INT;0;False;1;INT
Node;AmplifyShaderEditor.RegisterLocalVarNode;86;247.3343,2207.175;Float;False;VcolG;-1;True;1;0;FLOAT;0.0;False;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;56;1136.651,-366.7344;Float;True;2;2;0;FLOAT;0.0;False;1;COLOR;0;False;1;COLOR
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;14;817.391,-134.0747;Float;True;2;2;0;FLOAT;0.0;False;1;COLOR;0;False;1;COLOR
Node;AmplifyShaderEditor.GetLocalVarNode;87;568.6927,-118.4114;Float;False;85;0;1;FLOAT
Node;AmplifyShaderEditor.IntNode;82;-2360.885,2282.722;Float;False;Property;_Delay;Delay;10;0;0;0;1;INT
Node;AmplifyShaderEditor.SimpleDivideOpNode;63;-1664.004,2478.837;Float;False;2;0;INT;0;False;1;INT;0;False;1;INT
Node;AmplifyShaderEditor.SimpleAddOpNode;57;1447.64,-175.7868;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0.0,0,0,0;False;1;COLOR
Node;AmplifyShaderEditor.PannerNode;66;-1297.564,2121.02;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0.5,0.5;False;1;FLOAT;0.1;False;1;FLOAT2
Node;AmplifyShaderEditor.GetLocalVarNode;84;800.4004,-554.9449;Float;False;86;0;1;FLOAT
Node;AmplifyShaderEditor.ColorNode;112;1362.556,674.1193;Float;False;Property;_Color0;Color 0;16;0;0,0,0,0;0;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.ColorNode;50;765.0101,-452.7378;Float;False;InstancedProperty;_SecondaryColor;SecondaryColor;1;0;1,0.6838235,0.8211967,0;0;5;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1634.149,574.7863;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;SH_Tree;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Off;0;0;False;0;0;Opaque;0.5;True;True;0;False;Opaque;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;0;0;0;0;False;2;15;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;OFF;OFF;0;False;0;0,0,0,0;VertexOffset;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;0;0;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;FLOAT;0.0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;19;0;18;1
WireConnection;19;1;18;3
WireConnection;20;0;19;0
WireConnection;20;1;21;0
WireConnection;107;0;101;0
WireConnection;106;0;100;0
WireConnection;42;0;19;0
WireConnection;42;1;41;0
WireConnection;43;0;42;0
WireConnection;43;1;107;0
WireConnection;22;0;20;0
WireConnection;22;1;106;0
WireConnection;36;0;7;3
WireConnection;36;1;80;0
WireConnection;38;0;37;0
WireConnection;38;1;43;0
WireConnection;16;0;37;0
WireConnection;16;1;22;0
WireConnection;85;0;1;1
WireConnection;23;0;36;0
WireConnection;23;1;16;0
WireConnection;23;2;24;0
WireConnection;99;0;92;0
WireConnection;47;0;38;0
WireConnection;47;1;88;0
WireConnection;47;2;45;0
WireConnection;94;0;99;0
WireConnection;93;0;99;0
WireConnection;48;0;23;0
WireConnection;48;1;47;0
WireConnection;95;0;93;0
WireConnection;95;1;94;0
WireConnection;91;0;48;0
WireConnection;96;0;91;0
WireConnection;96;1;95;0
WireConnection;97;0;96;0
WireConnection;28;0;97;0
WireConnection;28;1;97;1
WireConnection;28;2;29;0
WireConnection;70;1;66;0
WireConnection;65;0;63;0
WireConnection;69;1;65;0
WireConnection;109;0;28;0
WireConnection;109;1;110;0
WireConnection;76;1;70;0
WireConnection;76;2;90;0
WireConnection;75;0;69;0
WireConnection;75;1;89;0
WireConnection;77;0;76;0
WireConnection;77;1;75;0
WireConnection;81;1;82;0
WireConnection;56;0;84;0
WireConnection;56;1;50;0
WireConnection;14;0;87;0
WireConnection;14;1;34;0
WireConnection;57;0;56;0
WireConnection;57;1;14;0
WireConnection;66;0;64;0
WireConnection;0;0;112;0
WireConnection;0;11;109;0
ASEEND*/
//CHKSM=F1AB818DF8566543FA645662913EF81F8ED373EF