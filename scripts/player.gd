extends CharacterBody3D

@onready var camera = $"Camera3D"
@onready var collision = $CollisionShape3D
@onready var area3d = $Camera3D/Area3D
@export_category("player")
@export var speed : float = 5.0
@export var gravity : float = 20.0
@export var jump_speed : float = 5.0
var yctp : PackedScene = load("res://scenes/yctp.tscn")
var yctp_node : Node
var jumping := false
var movement_vector := Vector2.ZERO
var last_floor := false
var parent : Node
var temp_bool
var temp_bool2
var last_camera_position
var last_camera_rotation
var check_yctp : bool = false

func _ready() -> void:
	Input.mouse_mode = Input.MOUSE_MODE_CAPTURED
	area3d.monitoring = false

func _process(_delta: float) -> void:
	if Global.debug:
		get_tree().debug_collisions_hint = true
		get_tree().debug_navigation_hint = true
		get_tree().debug_paths_hint = true

func _physics_process(delta: float) -> void:
	if not Global.paused:
		if Input.is_action_pressed("interact"):
			area3d.monitoring = true
			await get_tree().create_timer(0.5).timeout
			area3d.monitoring = false
		if not Global.freelook:
			velocity.y += -gravity * delta
			var vy = velocity.y
			velocity.y = 0
			var inputDir = Input.get_vector("left", "right", "forward", "backward")
			var relativeDir = Vector3(inputDir.x, 0.0, inputDir.y).rotated(Vector3.UP, camera.rotation.y)
			velocity = lerp(velocity, relativeDir * speed, speed * delta)
			if not Global.noclip:
				velocity.y = vy
				if is_on_floor() and not last_floor:
					jumping = false
				last_floor = is_on_floor()
				if is_on_floor() and Input.is_action_just_pressed("jump"):
					velocity.y = jump_speed
					jumping = true
			else:
				jumping = true
		else:
			velocity = Vector3(0, 0, 0)
		move_and_slide()
		if Global.debug:
			if Input.is_action_just_pressed("noclip"):
				if Global.noclip:
					collision.disabled = false
					gravity = 20.0
					Global.unlockedlook = temp_bool
					temp_bool = null
				else:
					collision.disabled = true
					gravity = 0.0
					temp_bool = Global.unlockedlook
					Global.unlockedlook = true
				if not Global.unlockedlook:
					camera.rotation.x = 0
				Global.noclip = !Global.noclip
			if Global.noclip:
				if Input.is_action_pressed("jump"):
					position.y += 0.1
				if Input.is_action_pressed("down"):
					position.y -= 0.1
	else:
		area3d.monitoring = false
	if check_yctp:
		if not Global.in_yctp:
			check_yctp = false
			Global.notebooks += 1
	if Global.paused and Global.in_yctp:
		Input.mouse_mode = Input.MOUSE_MODE_VISIBLE
	else:
		Input.mouse_mode = Input.MOUSE_MODE_CAPTURED
		if yctp_node != null:
			if camera.has_node(yctp_node.get_path()):
				camera.remove_child(yctp_node)
				yctp_node.free()

func _on_area_3d_area_entered(area:Area3D) -> void:
	print(area)
	parent = area.get_parent()
	print(parent)
	if not Global.paused:
		if not Global.freelook:
			if parent.has_meta("opened"):
				if not parent.get_meta("opened"):
					parent.set_meta("opened", true)
			elif parent.name == "Notebook":
				yctp_node = yctp.instantiate()
				camera.add_child(yctp_node)
				Global.in_yctp = true
				Global.paused = true
				check_yctp = true

func _on_area_3d_area_exited(area: Area3D) -> void:
	parent = area.get_parent()
