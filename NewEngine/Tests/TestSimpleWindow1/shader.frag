#version 330

out vec4 outputColor;

uniform vec4 vertexColor;

void main()
{
    outputColor = vertexColor;
}