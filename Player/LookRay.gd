extends Spatial


onready var camera = $Camera

var ray_origin = Vector3()
var ray_end = Vector3()



func _physics_process(_delta):
	var space_state = get_world().direct_space_state
	
	var mouse_position = get_viewport().get_mouse_position()
	
	ray_origin = camera.project_ray_origin(mouse_position)
	
	ray_end = ray_origin + camera.project_ray_normal(mouse_position) * 2000
	var intesaction = space_state.intersect_ray(ray_origin, ray_end)
	
	if !intesaction.empty():
		var pos = intesaction.position
		$Ray.look_at(Vector3(pos.x, translation.y, pos.z), Vector3(0,1,0 ))
	
