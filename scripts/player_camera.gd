extends Camera3D

@onready var camera = $"."
@onready var player = $".."
@onready var music = $"Music"
@onready var android_control_ui = $"Android Control UI"
@onready var freelook_button : Button = $"Debug menu/Panel/Freelook/Button"
@onready var unlockedlook_button : Button = $"Debug menu/Panel/3DLook/Button"
@onready var pause_button : Button = $"Android Control UI/Pause Button/Button"
@export_category("camera")
@export var free_look_speed := 3.0
var camera_anglev=0
var last_camera_position := Vector3.ZERO
var last_camera_rotation := Vector3.ZERO
var temp_bool = false

func _input(event):
	if not Global.paused:
		if event is InputEventMouseMotion:
			camera.rotate_y(deg_to_rad(-event.relative.x*Global.mouse_sensitivity))
			if Global.unlockedlook:
				var changev=-event.relative.y*Global.mouse_sensitivity
				if camera_anglev+changev>-50 and camera_anglev+changev<50:
					camera_anglev+=changev
					camera.rotate_object_local(Vector3(1,0,0),deg_to_rad(changev))

func _process(delta: float) -> void:
	music.volume_db = -15
	if Global.paused:
		if Input.is_action_just_pressed("Esc") or pause_button.button_pressed:
			for child in camera.get_children():
				print(child.name)
				if child.name == "PM":
					camera.remove_child(child)
			Global.paused = false
	if Global.debug and not Global.paused:
		camera.rotation.z = 0
		if Input.is_action_just_pressed("3dlook") or unlockedlook_button.button_pressed:
			if Global.freelook:
				Global.unlockedlook = true
			else:
				Global.unlockedlook = !Global.unlockedlook
			if not Global.unlockedlook:
				camera.rotation.x = 0
		if Input.is_action_just_pressed("freelook") or freelook_button.button_pressed:
			if Global.freelook:
				camera.position = last_camera_position
				camera.rotation = last_camera_rotation
				last_camera_position = Vector3.ZERO
				last_camera_rotation = Vector3.ZERO
				camera.rotation.x = 0
				Global.unlockedlook = temp_bool
				temp_bool = null
			else:
				last_camera_position = camera.position
				last_camera_rotation = camera.rotation
				temp_bool = Global.unlockedlook
				Global.unlockedlook = true
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

func _on_music_finished() -> void:
	music.play()
