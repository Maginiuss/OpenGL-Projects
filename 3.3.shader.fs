#version 330 core
out vec4 FragColor;

in vec3 vColor;
in vec3 vNormal;
in vec3 FragPos;

uniform float mixVal;


//uniform sampler2D face;

uniform vec3 cubeColor;
uniform vec3 lightColor;
uniform vec3 lightPos;
uniform vec3 viewPos;

struct Material {
    sampler2D diffuse;
    sampler2D specular;
    sampler2D emissive;
    sampler2D emissivemap;
    float shininess;
};

in vec2 vTexCoord;
uniform Material material;

struct Light {
    vec3 position;
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
};

uniform float increment;
uniform Light light;

uniform vec3 lightPosR;
uniform vec3 lightColorR;

void main()
{   
		//ambient
    vec3 ambient = light.ambient * vec3(texture(material.diffuse, vTexCoord));

    //diffuse
    vec3 norm = normalize(vNormal);
    vec3 lightDir = normalize(light.position - FragPos);
    float diff = max(dot(norm, lightDir), 0.0);
    vec3 diffuse = light.diffuse * diff * vec3(texture(material.diffuse, vTexCoord));
    
		//specular
    vec3 viewDir = normalize(-FragPos);
    vec3 reflectDir = reflect(-lightDir, norm);
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);
    vec3 specular = light.specular * (spec * vec3(texture(material.specular, vTexCoord)));

		//emission
    vec3 emission = vec3(texture(material.emissive, (vTexCoord + vec2(0.0, increment))));
    vec3 emask = vec3(texture(material.emissivemap, vTexCoord));
    vec3 emissionFull = emission * emask;

    vec3 result = ambient + diffuse + specular + emissionFull;
    FragColor =  vec4(result, 1.0);

}
