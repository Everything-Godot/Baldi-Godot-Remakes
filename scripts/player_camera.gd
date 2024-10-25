extends Camera3D

@onready var camera = $"."
@onready var player = $".."
@export_category("camera")
@export var free_look_speed := 3.0
var camera_anglev=0
var last_camera_position := Vector3.ZERO
var last_camera_rotation := Vector3.ZERO

func _input(event):
	if not Global.paused:
		if event is InputEventMouseMotion:
			camera.rotate_y(deg_to_rad(-event.relative.x*Global.mouse_sensitivity))
			if Global.freelook:
				var changev=-event.relative.y*Global.mouse_sensitivity
				if camera_anglev+changev>-50 and camera_anglev+changev<50:
					camera_anglev+=changev
					camera.rotate_object_local(Vector3(1,0,0),deg_to_rad(changev))

func _process(delta: float) -> void:
	if Global.paused:
		if Input.is_action_just_pressed("Esc"):
			for child in camera.get_children():
				print(child.name)
				if child.name == "PM":
					camera.remove_child(child)
			Global.paused = false
	if Global.debug and not Global.paused:
		camera.rotation.z = 0
		if Input.is_action_just_pressed("freelook"):
			if Global.freelook:
				camera.position = last_camera_position
				camera.rotation = last_camera_rotation
				last_camera_position = Vector3.ZERO
				last_camera_rotation = Vector3.ZERO
				camera.rotation.x = 0
			else:
				last_camera_position = camera.position
				last_camera_rotation = camera.rotation
			Global.freelook = !Global.freelook
		if Global.freelook:
			free_look(camera, delta)

func free_look(camera_node: Camera3D, delta: float) -> void:
	var inputDir = Input.get_vector("left", "right", "forward", "backward")
	var relativeDir = Vector3(inputDir.x, 0.0, inputDir.y).rotated(Vector3.UP, camera_node.rotation.y)
	player.velocity = lerp(player.velocity, relativeDir * free_look_speed, free_look_speed * delta)
	camera_node.position.x += player.velocity.x
	camera_node.position.z += player.velocity.z
	if Input.is_action_pressed("jump"):
		camera_node.position.y += 0.1
	if Input.is_action_pressed("down"):
		camera_node.position.y -= 0.1
