extends Sprite3D

func _process(_delta: float) -> void:
	var camera_global_transform = get_viewport().get_camera_3d().global_transform
	var camera_rotation = camera_global_transform.basis.get_euler()
	rotation = camera_rotation
