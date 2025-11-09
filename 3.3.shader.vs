#version 330 core
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec2 aTexCoord;
layout (location = 2) in vec3 aColor;
layout (location = 3) in vec3 aNormal;


//uniform sampler2D wood;
//uniform sampler2D face;

out vec2 vTexCoord;
out vec3 vColor;
out vec3 vNormal;
out vec3 FragPos;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;
uniform mat3 normalMatrix;


void main()
{   
    gl_Position = projection * view * model * vec4(aPos, 1.0);
    FragPos = vec3(view * model * vec4(aPos, 1.0));
    vTexCoord = aTexCoord;
    vColor = aColor;
    vNormal = normalMatrix * aNormal;
}
