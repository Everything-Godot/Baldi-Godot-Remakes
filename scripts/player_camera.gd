extends Camera3D

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
		if Global.os_name != "Android":
			if event is InputEventMouseMotion:
				rotate_y(deg_to_rad(-event.relative.x*Global.mouse_sensitivity))
				if Global.unlockedlook:
					var changev=-event.relative.y*Global.mouse_sensitivity
					if camera_anglev+changev>-50 and camera_anglev+changev<50:
						camera_anglev+=changev
						rotate_object_local(Vector3(1,0,0),deg_to_rad(changev))
		else:
			if event is InputEventScreenDrag:
				print(event.as_text())
				if not Input.is_action_pressed("left") or not Input.is_action_pressed("right") or not Input.is_action_pressed("forward") or not Input.is_action_pressed("backward") or not Input.is_action_pressed("up") or not Input.is_action_pressed("down"):
					rotate_y(deg_to_rad(-event.relative.x*Global.mouse_sensitivity))
					if Global.unlockedlook:
						var changev=-event.relative.y*Global.mouse_sensitivity
						if camera_anglev+changev>-50 and camera_anglev+changev<50:
							camera_anglev+=changev
							rotate_object_local(Vector3(1,0,0),deg_to_rad(changev))

func _process(delta: float) -> void:
	music.volume_db = -15
	if Global.paused:
		var pause_menu : Control
		for child in get_children():
			if child.name == "PM":
				pause_menu = child
				break
		var resume_button : Button
		resume_button = get_node("/root/SchoolHouse/Player/Camera3D/PM/Android/Resume/Button")
		if Input.is_action_just_pressed("Esc") or resume_button.button_pressed:
			remove_child(pause_menu)
			Global.paused = false
	if Global.debug and not Global.paused:
		rotation.z = 0
		if Input.is_action_just_pressed("3dlook") or unlockedlook_button.button_pressed:
			if Global.freelook:
				Global.unlockedlook = true
			else:
				Global.unlockedlook = !Global.unlockedlook
			if not Global.unlockedlook:
				rotation.x = 0
		if Input.is_action_just_pressed("freelook") or freelook_button.button_pressed:
			if Global.freelook:
				position = last_camera_position
				rotation = last_camera_rotation
				last_camera_position = Vector3.ZERO
				last_camera_rotation = Vector3.ZERO
				rotation.x = 0
				Global.unlockedlook = temp_bool
				temp_bool = null
			else:
				last_camera_position = position
				last_camera_rotation = rotation
				temp_bool = Global.unlockedlook
				Global.unlockedlook = true
			Global.freelook = !Global.freelook
		if Global.freelook:
			free_look(delta)

func free_look(delta: float) -> void:
	var inputDir = Input.get_vector("left", "right", "forward", "backward")
	var relativeDir = Vector3(inputDir.x, 0.0, inputDir.y).rotated(Vector3.UP, rotation.y)
	player.velocity = lerp(player.velocity, relativeDir * free_look_speed, free_look_speed * delta)
	position.x += player.velocity.x
	position.z += player.velocity.z
	if Input.is_action_pressed("jump"):
		position.y += 0.1
	if Input.is_action_pressed("down"):
		position.y -= 0.1

func _on_music_finished() -> void:
	music.play()