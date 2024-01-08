shader_type spatial;

uniform sampler2D viewport_texture : source_color;

void fragment(){
    ALBEDO = vec3(texture(viewport_texture, SCREEN_UV).rgb);
    EMISSION = ALBEDO;
}	
render_mode unshaded;