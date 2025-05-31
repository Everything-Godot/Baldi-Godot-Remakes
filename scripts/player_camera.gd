extends Camera3D

@onready var player = $".."
@onready var android_control_ui = $"Android Control UI"
@export_category("camera")
@export var free_look_speed := 3.0
var camera_anglev := 0
var last_camera_position := Vector3.ZERO
var last_camera_rotation := Vector3.ZERO
var temp_bool := false

func _input(event):
	if not Global.paused:
		if not Global.is_on_android:
			if event is InputEventMouseMotion:
				rotate_y(deg_to_rad(-event.relative.x * Global.mouse_sensitivity))
				if Global.unlockedlook:
					var changev = - event.relative.y * Global.mouse_sensitivity
					if camera_anglev + changev > -90 and camera_anglev + changev < 90:
						camera_anglev += changev
						rotate_object_local(Vector3(1, 0, 0), deg_to_rad(changev))
		else:
			if event is InputEventScreenDrag:
				rotate_y(deg_to_rad(-event.relative.x * Global.mouse_sensitivity))
				if Global.unlockedlook:
					var changev = - event.relative.y * Global.mouse_sensitivity
					if camera_anglev + changev > -90 and camera_anglev + changev < 90:
						camera_anglev += changev
						rotate_object_local(Vector3(1, 0, 0), deg_to_rad(changev))

func _process(delta: float) -> void:
	if Global.paused and not Global.in_yctp:
		var pause_menu: Control
		for child in get_children():
			if child.name == "PM":
				pause_menu = child
				break
		var resume_button: Button
		resume_button = get_node("/root/SchoolHouse/Player/Camera3D/PM/Android/Resume/Button")
		if Input.is_action_just_pressed("Esc") or resume_button.button_pressed:
			remove_child(pause_menu)
			Global.paused = false
	if Global.debug and not Global.paused:
		rotation.z = 0
		if Input.is_action_just_pressed("look back"):
			Global.look_back = !Global.look_back
		if Input.is_action_just_pressed("run"):
			Global.running = !Global.running
		if Input.is_action_just_pressed("3dlook"):
			if Global.freelook:
				Global.unlockedlook = true
			else:
				Global.unlockedlook = !Global.unlockedlook
			if not Global.unlockedlook:
				rotation.x = 0
		if Input.is_action_just_pressed("freelook"):
			if Global.freelook:
				position = last_camera_position
				rotation = last_camera_rotation
				last_camera_position = Vector3.ZERO
				last_camera_rotation = Vector3.ZERO
				rotation.x = 0
				Global.unlockedlook = temp_bool
				temp_bool = false
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
